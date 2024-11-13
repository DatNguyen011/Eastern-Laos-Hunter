using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public float cameraSpeed = 2f;
    //public float delayCamera = 1f;
    public Transform targetCharactor;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(targetCharactor.position.x,targetCharactor.position.y,-15f);
        transform.position = Vector3.Slerp(transform.position, newPos, cameraSpeed*Time.deltaTime);  
    }
}
