
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public struct EvaluateJob : IJobParallelFor
{
    public NativeArray<TableJobs.CellState> ResultTable;
    
    [ReadOnly]
    public NativeArray<TableJobs.CellState> OriginTable;

    public void Execute(int index)
    {
   /*     ResultTable[index] = new TableJobs.CellState()
        {
            Alive = Utils.Evaluate(OriginTable[index].Alive, Utils.NeighbourCount(OriginTable,index))
        };*/
    }
}
