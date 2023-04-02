using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstMover : MonoBehaviour
{

    [SerializeField] private Vector3 _direction;

    [SerializeField] private float _speed;
    
    void Update()
    {
        transform.Translate(_direction * (_speed * Time.deltaTime));        
    }
}
