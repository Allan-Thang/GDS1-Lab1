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
        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");
        Debug.Log(soldiers.Count);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + _movement * (moveSpeed * Time.fixedDeltaTime));
    }
}