using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;
using Random = UnityEngine.Random;

[BurstCompile]
public class TableECS : MonoBehaviour
{
    public CellECS CellPrefab;

    public int size = 10;
    public float CellSize = 0.5f;

    public CellECS[] CellTable;

    public float ScrollMultiplier = 4;
    public Camera camera;

    private EvaluateTransformJob _evaluateJob;
    private ApplyTransformJob _applyJob;


    private Vector3 MouseOld;

    private bool OldIsValid = false;
   


    void Start()
    {
        Utils.Initialize(size, CellSize);

        CellTable = Enumerable.Range(0, size * size)
            .Select(i => Instantiate(CellPrefab, Utils.GeneratePositionByIndex(i), Quaternion.identity)).ToArray();
 

        foreach (var cell in CellTable)
        {
            cell.transform.SetParent(transform);
            cell.AliveNext = Random.value > 0.7f;
            cell.AliveNow = cell.AliveNext;
            cell.mesh.enabled = cell.AliveNow;
        }
        
       
    }



    private void Update()
    {

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
    }
    
}