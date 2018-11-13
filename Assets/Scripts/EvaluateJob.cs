
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public struct EvaluateJob : IJobParallelFor
{
    public NativeArray<TableJobs.CellState> ResultTable;
    
    [ReadOnly]
    public NativeArray<TableJobs.CellState> OriginTable;

    private int NeighbourCount(int cellIndex)
    {
        var count = 0;
        var (xcell, ycell) = Utils.IndexToCoordinate(cellIndex);

        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                var x = xcell + i;
                var y = ycell + j;
                if (!Utils.IsPositionValid(x, y) || (x==xcell && y==ycell))
                    continue;

                count += OriginTable[Utils.CoordinateToIndex(x, y)].Alive==Utils.State.Alive ? 1 : 0;
            }
        }

        return count;
    }


    public void Execute(int index)
    {
        //var current = CellTable[index];
        ResultTable[index] = new TableJobs.CellState()
        {
            Alive = Utils.Evaluate(OriginTable[index].Alive, NeighbourCount(index))
        };
        //current.AliveNextGen = actualResult;
    }
}
