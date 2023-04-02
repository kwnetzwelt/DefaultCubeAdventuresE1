using Unity.Mathematics;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    public Transform target; // The target object for the camera to follow
    public float distance = 5.0f; // The distance between the camera and target
    public float xSpeed = 120.0f; // The camera rotation speed around the x-axis
    public float ySpeed = 120.0f; // The camera rotation speed around the y-axis

    private float x = 0.0f;
    private float y = 0.0f;
    [SerializeField]
    private float _targetYAngle = 45;

    
    private Vector3 _lastPosition;

    private const float snap = 30;

    void LateUpdate()
    {
        if (target)
        {
            // Clamp the vertical angle
            y = Mathf.Clamp(y, 0, 70);

            if (!Input.GetMouseButton(1))
            {
                y = LerpToNearestSnap(y);
                x = LerpToNearestSnap(x);
            }

            // Calculate the rotation
            Quaternion rotation = Quaternion.Euler(y, x, 0);

            // Calculate the position
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            // Apply the rotation and position to the camera
            transform.rotation = rotation;
            transform.position = position;
        }

        if (Input.GetMouseButtonDown(1))
        {
            _lastPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            var position = Input.mousePosition;
            x += position.x - _lastPosition.x;
            y -= position.y - _lastPosition.y;
            _lastPosition = position;
        }


    }

    private float LerpToNearestSnap(float f)
    {
        float nearest = Mathf.Round(f / snap) * snap;
        return Mathf.MoveTowards(f, nearest, Time.deltaTime * snap * 2);
    }
}