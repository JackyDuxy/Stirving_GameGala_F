using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_shooting : MonoBehaviour
{
    public static Action shootInput;
    public static Action reloadInput;

    [SerializeField] private KeyCode reloadKey;
    private void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update(){
        if (Input.GetMouseButton(0)){
            shootInput?.Invoke();
        }
        if (Input.GetKeyDown(reloadKey)){
            reloadInput?.Invoke();
        }

    }       
}
