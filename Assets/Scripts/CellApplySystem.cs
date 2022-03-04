using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateAfter(typeof(CellSystem))]
public class CellApplySystem : ComponentSystem
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
            var alive = cells[i].AliveNext;
            cells[i].AliveNow = alive;
            cells[i].mesh.enabled = alive;
        }
    }
}
