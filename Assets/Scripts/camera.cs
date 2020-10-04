using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    Quaternion rotation;

    // Update is called once per frame
    void Update()
    {
        /*
        Vector3 playerPos = player.transform.position;
        Vector3 playerRot = player.transform.eulerAngles;
        transform.position = new Vector3(playerPos.x,playerPos.y,transform.position.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,playerRot.z);
        */ 
        transform.position = new Vector3(transform.position.x,transform.position.y,-10); 
    }
    void Awake()
    {
        rotation = transform.rotation;
    }
    void LateUpdate()
    {
        //transform.rotation = Quaternion.Euler(transform.rotation.x,transform.rotation.y,rotation.z);
        //transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, 0);
        transform.position = new Vector3(transform.position.x,transform.position.y,-10); 
    } 
}
