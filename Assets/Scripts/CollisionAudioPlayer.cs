using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollisionAudioPlayer : MonoBehaviour
{
    public AudioClip[] _clip;

    public int _audioSourceCount = 6;
    
    private List<AudioSource> _audioSources = new List<AudioSource>();

    private int _lastPlayed = 0;
    [SerializeField]
    private float _magnitudeThreshold = 0.8f;

    private void OnEnable()
    {
        for (int i = 0; i < _audioSourceCount; i++)
        {
            GameObject go = new GameObject("Audio");
            go.transform.parent = this.transform;
            var asource = go.AddComponent<AudioSource>();
            _audioSources.Add(asource);
            asource.playOnAwake = false;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        
        _lastPlayed = (_lastPlayed + 1) % _audioSourceCount;
        _audioSources[_lastPlayed].PlayOneShot(_clip[Random.Range(0, _clip.Length)]);
        _audioSources[_lastPlayed].volume = math.remap(_magnitudeThreshold, _magnitudeThreshold * 2, 0.5f, 1.2f, collision.contacts[0].impulse.magnitude);;
        _audioSources[_lastPlayed].transform.position = collision.contacts[0].point;
    }
}
