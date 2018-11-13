using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Utils.State Alive;
    public Utils.State AliveNextGen;

    public MeshRenderer mesh;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    public void Apply()
    {
        mesh.enabled = AliveNextGen==Utils.State.Alive;
        Alive = AliveNextGen;
    }
}