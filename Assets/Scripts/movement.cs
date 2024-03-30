using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{  
    // initialize moving, jumping, horizontal movement, vertical movement, direction and grabbing cc as charactercontroller component

    private CharacterController cc;
    public float move_speed;
    public float jump_speed;

    private float h_move, v_move;

    private Vector3 dir;

    //setting up gravity instead of using rigid body
    public float gravity;
    private Vector3 velocity;


    //using checksphere to check if the player is on the ground
    public Transform ground_check;
    public float check_radius;
    public LayerMask ground_layer;
    public bool on_ground;
    



    // Initialize function (character controller component)
    private void Start(){
        cc = GetComponent<CharacterController> ();
        // move_speed = 4;
    }

    private void Update(){
        on_ground = Physics.CheckSphere(ground_check.position, check_radius, ground_layer);
        //check if player is touching the ground
        if(on_ground && velocity.y < 0){
            velocity.y += 3f;
            on_ground = false;
        }
        //check if space is pressed, if so, jump
        if(Input.GetButtonDown("Jump")){
            velocity.y = jump_speed;
            on_ground = false;
        }

        //  move_speed = 4;
        // keep updating player's horizontal and vertical movements
        h_move = Input.GetAxis("Horizontal")*move_speed;
        v_move = Input.GetAxis("Vertical")*move_speed;

        // stored up direction
        dir = transform.forward*v_move + transform.right*h_move;
        cc.Move(dir*Time.deltaTime);

        //adding "force" to gravity
        velocity.y -= gravity*Time.deltaTime;
        cc.Move(velocity*Time.deltaTime);
    }
}
