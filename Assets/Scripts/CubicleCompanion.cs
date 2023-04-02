using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubicleCompanion : MonoBehaviour
{
    [SerializeField]
    private CubicleController _cubicleController;

    [SerializeField]
    private Vector3 _offset = new Vector3(0, 0.6f, 0);
    [SerializeField]
    private float _distanceThreshold = 0.25f;

    private float _waitSeconds;

    [Header("Boost")]
    
    [SerializeField]
    private float _boostDuration = 2;

    [SerializeField]
    private AnimationCurve _boostCurve;
    
    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        var movementDirection = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));

        movementDirection = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * movementDirection;
        transform.position = _cubicleController.transform.position + (_offset + movementDirection)*0.5f;

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");
        if (x == 0 && y == 0)
        {
            return;
        }
        if(new Vector2(x,y).magnitude > 0.8f)
            _cubicleController.MoveTo(transform.position);
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(BoostCoroutine());
        }

        
    }

    private void Jump()
    {
        _cubicleController.Jump();
    }

    IEnumerator BoostCoroutine()
    {
        float t = _boostDuration;
        while (t > 0)
        {
            t -= Time.deltaTime;
            var value = _boostCurve.Evaluate(t / _boostDuration);
            _cubicleController.SetAdditionalForcePower(value);
            yield return new WaitForEndOfFrame();
        }
        
        _cubicleController.SetAdditionalForcePower(0);
    }
}
