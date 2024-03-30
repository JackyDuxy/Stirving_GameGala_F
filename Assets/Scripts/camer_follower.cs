using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camer_follower : MonoBehaviour
{   
    // public Transform camera;
    // public Transform player;
    // // Update is called once per frame
    // void Update()
    // {
    //     camera.position = player.position;
    //     transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z);
    // }

    public Transform player;

    private float mouseX, mouseY;

    public float mouseSensitivity;

    public float x_rotation;

    private void Update(){
        mouseX = Input.GetAxis("Mouse X")*mouseSensitivity*Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y")*mouseSensitivity*Time.deltaTime;

        x_rotation -= mouseY;
        x_rotation = Mathf.Clamp(x_rotation, -70f, 70f);

        player.Rotate(Vector3.up*mouseX);
        transform.localRotation = Quaternion.Euler(x_rotation,0,0);
    }
}
