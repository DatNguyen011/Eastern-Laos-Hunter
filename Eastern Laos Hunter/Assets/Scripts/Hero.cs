using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    
    [SerializeField] float speed=5f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float movex = Input.GetAxisRaw("Horizontal");
        float movey = Input.GetAxisRaw("Vertical");
        Vector3 vector3 = new Vector3(movex, movey, 0);
        transform.position += vector3*speed*Time.deltaTime;
        
    }
}
