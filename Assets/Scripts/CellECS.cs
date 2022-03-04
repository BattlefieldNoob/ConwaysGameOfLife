using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class CellECS : MonoBehaviour
{
    public bool AliveNow;
    public bool AliveNext;

    public MeshRenderer mesh;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }
}

public struct CellP : IComponentData
{
    public byte AliveNow;
    public byte AliveNext;
    public int index;
}
