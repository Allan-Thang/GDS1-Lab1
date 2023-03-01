using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    public Rigidbody2D rb;
    private Vector2 _movement;

    public List<Soldier> soldiers;
    public int maxSoldiers = 3;

    // Update is called once per frame
    void Update()
    {
        // Get a vector for movement based on input
        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");
        Debug.Log(soldiers.Count); // DebugHere
    }

    private void FixedUpdate()
    {
        // Move the player independent of frame-rate
        rb.MovePosition(rb.position + _movement * (moveSpeed * Time.fixedDeltaTime));
    }
}