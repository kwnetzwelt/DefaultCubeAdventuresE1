using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class CubicleController : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField]
    private float _moveForcePower = 10f;

    [SerializeField]
    private float _moveForceDuration = 1;

    
    private float _moveTimeLeft;
    private float _maxAngularVelocity = 30f;
    private Vector3 _moveAnchorPosition;
    [SerializeField]
    private Transform _moveAnchor;

    private float _additionalForcePower;
    [SerializeField]
    private float _jumpForce = 5;

    private TogglePlayerInput _togglePlayerInput;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _togglePlayerInput = GetComponent<TogglePlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        //_rigidbody.AddForceAtPosition(new Vector3());
    }

    private void OnDrawGizmos()
    {
        foreach (Transform child in this.transform)
        {
            Gizmos.DrawLine(this.transform.position, child.position);
        }
    }

    public float MoveTo(Vector3 position)
    {
        if (!_togglePlayerInput.PlayerInputEnabled) return 0;
        _moveTimeLeft = _moveForceDuration;
        _moveAnchorPosition =_rigidbody.ClosestPointOnBounds(position);
        
        _moveAnchor.position = _moveAnchorPosition;
        return _moveForceDuration;
    }

    

    void FixedUpdate()
    {
        
        if(_moveTimeLeft > 0)
        {
            _moveTimeLeft -= Time.deltaTime;
            var direction = Vector3.Normalize(_moveAnchorPosition - transform.position);
            direction.y = -0.5f;
        
            if(_additionalForcePower != 0 || _rigidbody.angularVelocity.sqrMagnitude < _maxAngularVelocity)
                _rigidbody.AddForceAtPosition(direction * (_moveForcePower + _additionalForcePower), _moveAnchorPosition, ForceMode.Force);
            

            Ray r = new Ray(_moveAnchor.position, Vector3.down);
            var hits = Physics.RaycastAll(r);
            for (int i = 0; i < hits.Length; i++)
            {
                if(hits[i].transform == transform)
                    continue;
                
                if (hits[i].distance >= .5f) continue;
                
                _moveTimeLeft = 0;
                Debug.Log("stop");

            }
            Debug.Log("move");
        }
    }

    public void SetAdditionalForcePower(float value)
    {
        if (!_togglePlayerInput.PlayerInputEnabled) return;
        _additionalForcePower = value;
    }

    public void Jump() // TODO Improve. Use Box Collider or Mesh Vertices to more acurately determine if we are grounded. 
    {
        
        if (!_togglePlayerInput.PlayerInputEnabled) return;
        
        Ray r = new Ray(transform.position, Vector3.down);
        var hits = Physics.RaycastAll(r);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform == transform)
                continue;
            if (hits[i].distance >= 1.5f) continue;

            _rigidbody.AddForceAtPosition(Vector3.up * _jumpForce, transform.position + Random.insideUnitSphere * 0.01f, ForceMode.VelocityChange);
            return;
        }
    }
}
