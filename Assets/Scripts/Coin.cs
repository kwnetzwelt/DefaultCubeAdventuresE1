using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class Coin : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private float _upDownSpeed;
    [SerializeField]
    private float _upDownHeight = 0.3f;
    [SerializeField]
    private GameObject _coinMesh;

    public UnityEvent OnCoinCollected;

    void Update()
    {
        _coinMesh.transform.localRotation = Quaternion.Euler(0,_rotationSpeed * Time.time, 0);
        _coinMesh.transform.localPosition = Vector3.up * Mathf.Sin(Mathf.Deg2Rad * _upDownSpeed * Time.time) * _upDownHeight;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCoinCollected.Invoke();
        }
    }
}
