using System;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform target;
    public Vector3 cameraOffset;
    public float DampingIntesity;
    Vector3 velocity =  Vector3.zero;
    public Vector2 xMinMax;
    public Vector2 yMinMax;

    private void Update()
    {
        float xClamp = Mathf.Clamp(target.position.x, xMinMax.x, xMinMax.y);
        float yClamp = Mathf.Clamp(target.position.y, yMinMax.x, yMinMax.y);
        
        Vector3 targetPosition = target.position +  cameraOffset;
        Vector3 clampedPosition = new Vector3(Mathf.Clamp(targetPosition.x, xMinMax.x, xMinMax.y),Mathf.Clamp(targetPosition.y, yMinMax.x, yMinMax.y),targetPosition.z);
        
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, clampedPosition, ref velocity, DampingIntesity * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
