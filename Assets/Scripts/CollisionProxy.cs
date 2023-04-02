using UnityEngine;
using UnityEngine.Events;

public class CollisionProxy : MonoBehaviour
{
    public UnityEvent<Collision> CollisionEnter;
    private void OnCollisionEnter(Collision collision)
    {
            CollisionEnter.Invoke(collision);
    }
}
