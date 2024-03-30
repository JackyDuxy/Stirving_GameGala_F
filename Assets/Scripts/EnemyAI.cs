using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    public float moveSpeed;

    private Rigidbody rb;

    private Vector3 moveDir;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        moveDir = player.position - transform.position;

        if (Vector3.Distance(transform.position, player.position) <= 15 && Vector3.Distance(transform.position, player.position) > 2) {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
        } else if (Vector3.Distance(transform.position, player.position) > 2) {
            // do some attacking stuff
        } else {
            // randomly move the enemy but for now it stops moving
            rb.AddForce(-(moveDir.normalized * moveSpeed * 10f), ForceMode.Force);
        }
    }
}
