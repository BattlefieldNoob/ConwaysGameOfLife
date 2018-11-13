using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Random = UnityEngine.Random;

[BurstCompile]
public class TableJobs : MonoBehaviour
{
    public Cell CellPrefab;

    public int size = 10;
    public float CellSize = 0.5f;

    public Cell[] CellTable;

    public float ScrollMultiplier = 4;
    public Camera camera;
  
    private NativeArray<CellState> OriginArray;
    private NativeArray<CellState> ResultArray;

    private JobHandle EvaluateHandle;

    private EvaluateJob _evaluateJob;


    private Vector3 MouseOld;

    private bool OldIsValid = false;
    

    public struct CellState
    {
        public Utils.State Alive;
    }


    void Start()
    {
        Utils.Initialize(size, CellSize);

        CellTable = Enumerable.Range(0, size * size)
            .Select(i => Instantiate(CellPrefab, Utils.GeneratePositionByIndex(i), Quaternion.identity)).ToArray();

        
        OriginArray=new NativeArray<CellState>(size*size,Allocator.Persistent);

        for (var index = 0; index < CellTable.Length; index++)
        {
            var cell = CellTable[index];
            cell.transform.SetParent(transform);
            cell.AliveNextGen = Random.value > 0.7f ? Utils.State.Alive : Utils.State.Dead;
            cell.Apply();

            OriginArray[index]=new CellState()
            {
                Alive = cell.Alive
            };
        }
    }


    private void OnDestroy()
    {
        if(ResultArray.IsCreated)
            ResultArray.Dispose();
        
        if(OriginArray.IsCreated)
            OriginArray.Dispose();
    }


    private void Update()
    {
        ResultArray=new NativeArray<CellState>(size*size,Allocator.Persistent);

        _evaluateJob = new EvaluateJob()
        {
            OriginTable = OriginArray,
            ResultTable = ResultArray
        };

        EvaluateHandle = _evaluateJob.Schedule(OriginArray.Length, 128);


        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize-(Input.mouseScrollDelta.y * Time.deltaTime * ScrollMultiplier),20f,100f);

        if (Input.GetMouseButton(0))
        {
            if (!OldIsValid)
            {
                MouseOld = Input.mousePosition;
                OldIsValid = true;
            }
            else
            {
                var delta = (Input.mousePosition - MouseOld)*Time.deltaTime*((100-camera.orthographicSize)/500);
                
                camera.transform.Translate(delta);
            }
        }else if (Input.GetMouseButtonUp(0))
        {
            MouseOld=Vector3.negativeInfinity;
            OldIsValid = false;
        }
        
        
        
        EvaluateHandle.Complete();
        
        ApplyCycle(ResultArray);
        OriginArray.Dispose();
        OriginArray = ResultArray;

    }


    private void ApplyCycle(NativeArray<CellState> result)
    {
        for (var i = 0; i < CellTable.Length; i++)
        {
            var cell = CellTable[i];
            cell.mesh.enabled = result[i].Alive==Utils.State.Alive;
        }
    }
    
}