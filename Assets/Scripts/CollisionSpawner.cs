using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollisionSpawner : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleSystem;

    [SerializeField]
    private ParticleSystem.EmitParams _emitParams;

    [SerializeField]
    private float _magnitudeThreshold = 0.5f;

    private void OnEnable()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        for (int j = 0; j < collision.contacts.Length; j++)
        {
            var contact = collision.contacts[j];
            if(contact.impulse.magnitude < _magnitudeThreshold)
                continue;
            for (int i = 0; i < 5; i++)
            {
                _emitParams.position = contact.point + Random.insideUnitSphere * 0.01f;
                _emitParams.startSize = math.remap(_magnitudeThreshold, _magnitudeThreshold * 2, 1f, 1.5f, contact.impulse.magnitude);
                
                
                _emitParams.velocity = contact.impulse * Random.Range(-0.1f, 0.1f);
                _particleSystem.Emit(_emitParams, 1);

            }
        }

    }
}
