using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCreator : MonoBehaviour
{
    [SerializeField] private int xSize = 6;
    [SerializeField] private int ySize = 4;
    [SerializeField] private int thickness = 3;

    [SerializeField] private GameObject _brickPrefab;

    private List<GameObject> _bricks = new();

    private void OnEnable()
    {
        var transf = this.transform;
        for (var z = 0; z < thickness; z++)
        {
            for (var x = 0; x < xSize; x++)
            {
                for (var y = 0; y < ySize; y++)
                {
                    var instance = GameObject.Instantiate(_brickPrefab, transf);
                    instance.transform.localPosition = new Vector3(x, y, z) + Vector3.one * 0.5f;
                    _bricks.Add(instance);
                }
            }
        }
    }

    private void OnDisable()
    {
        foreach (var b in _bricks)
        {
            Destroy(b);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 size = new Vector3(xSize, ySize, thickness);
        Gizmos.color = new Color(1, 1, 1, 0.4f);
        Gizmos.DrawCube(this.transform.position + size * 0.5f,size);
        
    }
}
