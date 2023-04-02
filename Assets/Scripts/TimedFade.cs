using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class TimedFade : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private float _alphaTarget;
    [SerializeField] private float _fadeSpeed = 2;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_delay);
        var group = GetComponent<CanvasGroup>();

        while (group.alpha != _alphaTarget)
        {
            group.alpha = Mathf.MoveTowards(group.alpha, _alphaTarget, Time.deltaTime * _fadeSpeed);
            yield return new WaitForEndOfFrame();
        }
    }
}
