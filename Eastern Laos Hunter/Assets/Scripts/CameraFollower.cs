using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target; // Nhân vật mà camera sẽ theo dõi
    public Vector3 offset = new Vector3(0, 0, -10); // Khoảng cách giữa camera và nhân vật
    public float smoothSpeed = 0.125f; // Tốc độ di chuyển mượt

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset; // Vị trí mong muốn của camera
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Di chuyển mượt
            transform.position = smoothedPosition;
        }
    }
}
