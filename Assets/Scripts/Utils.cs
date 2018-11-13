﻿using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public static class Utils
{
    private static int Size;
    private static float CellSize;
    
    public enum State:byte
    {
        Dead,
        Alive
    }

    public static void Initialize(int size, float cellSize)
    {
        Size = size;
        CellSize = cellSize;
    }
    
    
    // false = count < 2 || count > 3
    // true = count = 3
    // same = count = 2 
    public static State Evaluate(State alive,int neighbors)
    {
        if (neighbors < 2 || neighbors > 3)
        {
            return State.Dead;
        }
        else if (neighbors == 3)
        {
            return State.Alive;
        }

        return alive;
    }
    
    
    public static (int,int) IndexToCoordinate(int index) => ((index % Size), Mathf.FloorToInt(index / Size));
    
    public static bool IsPositionValid(int x, int y) => x > -1 && y > -1 && x < Size && y < Size;
    
    public static Vector3 GeneratePositionByIndex(int index)
    {
        var(x,y) = IndexToCoordinate(index);

        var extreme = -((Size - 1) * CellSize / 2);
        
        var finalX = extreme + CellSize * x;
        var finalY = -extreme - CellSize * y;
        
        return new Vector3(finalX,finalY, 0);
    }
    
    public static int CoordinateToIndex(int x, int y) => (y * Size) + x;
    
}
