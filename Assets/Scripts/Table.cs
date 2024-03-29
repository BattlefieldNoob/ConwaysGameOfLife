﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Table : MonoBehaviour
{
    public Cell CellPrefab;

    public int size = 10;
    public float CellSize = 0.5f;

    public Cell[] CellTable;
    
    void Start()
    {
        Utils.Initialize(size,CellSize);
        
        CellTable = Enumerable.Range(0, size * size)
            .Select(i => Instantiate(CellPrefab, Utils.GeneratePositionByIndex(i), Quaternion.identity)).ToArray();
        
        foreach (var cell in CellTable)
        {
            cell.transform.SetParent(transform);
            cell.AliveNext=Random.value>0.7f;
            cell.AliveNow = cell.AliveNext;
            cell.mesh.enabled = cell.AliveNow;
        }

        StartCoroutine(MainLoop());
    }

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

                count += CellTable[Utils.CoordinateToIndex(x, y)].AliveNow ? 1 : 0;
            }
        }

        return count;
    }

   /* private void Update()
    {
        foreach (var index in Enumerable.Range(0,size*size))
        {
            var current = CellTable[index];
            current.Evaluate(NeighbourCount(index));
        }
            
        foreach (var cell in CellTable)
        {
            cell.Commit();
        }
    }*/
    
    /* private void FixedUpdate()
    {
        foreach (var index in Enumerable.Range(0,size*size))
        {
            var current = CellTable[index];
            current.Evaluate(NeighbourCount(index));
        }
            
        foreach (var cell in CellTable)
        {
            cell.Commit();
        }
    }*/


    IEnumerator MainLoop()
    {
        yield return null;
        yield return null;
        yield return null;
        
        while (true)
        {
            foreach (var index in Enumerable.Range(0,size*size))
            {
                var current = CellTable[index];
                current.AliveNext=Utils.Evaluate(current.AliveNow,NeighbourCount(index));
            }

            yield return null;
            
            foreach (var cell in CellTable)
            {
                var alive = cell.AliveNext;
                cell.AliveNow = alive;
                cell.mesh.enabled = alive;
            }

           // yield return null;
        }
    }
}
