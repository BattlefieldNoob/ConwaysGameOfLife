
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public struct ApplyTransformJob : IJobParallelForTransform
{
    public void Execute(int index, TransformAccess transform)
    {
        var nextCycleAlive = transform.rotation.z > 0;
        transform.rotation = Utils.LifeToQuaternion(nextCycleAlive);
    }
}
