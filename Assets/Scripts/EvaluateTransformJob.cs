
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public struct EvaluateTransformJob : IJobParallelForTransform
{
    
    [ReadOnly]
    [NativeDisableUnsafePtrRestriction]
    public Transform[] OriginTable;

    public void Execute(int index, TransformAccess transform)
    {
  /*      var alive = transform.rotation.y > 0.5f;
        var nextCycleAlive =
            Utils.Evaluate(alive ? Utils.State.Alive : Utils.State.Dead, Utils.NeighbourCount(OriginTable, index)) ==
            Utils.State.Alive;
        transform.rotation = Utils.NextCycleLifeToQuaternion(transform.rotation, nextCycleAlive);*/
    }
}
