
using Unity.Collections;
using Unity.Entities;

public class CellSystem : ComponentSystem
{
    
    public struct CellData {
        [ReadOnly] public ComponentArray<CellECS> States;
    }

    [Inject] private CellData cellsArray;
    
    protected override void OnUpdate()
    {
        var cells = cellsArray.States;
        for(var i=0;i<cells.Length;i++)
        {
            var alive = cells[i].AliveNow;
            var nextCycleAlive = Utils.Evaluate(alive, Utils.NeighbourCount(cells, i));
            cells[i].AliveNext = nextCycleAlive;
        }
    }
}


public struct CellJob : IJobProcessComponentData<CellP>
{
    public void Execute(ref CellP data)
    {
        
    }
}