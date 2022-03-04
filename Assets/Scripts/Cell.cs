﻿using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool AliveNow;
    public bool AliveNext;

    public MeshRenderer mesh;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }
}

