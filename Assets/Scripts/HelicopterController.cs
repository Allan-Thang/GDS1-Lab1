using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    private Vector2 _movement;
    private bool _isAlive = true;

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
        if (!_isAlive) return;
        rb.MovePosition(rb.position + _movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter2D(Collider2D trigCollider2D)
    {
        // If Tree
        if (!trigCollider2D.gameObject.CompareTag("Tree")) return;
        HandleTreeCollision();

        // If Hospital
        if (!trigCollider2D.gameObject.CompareTag("Hospital")) return;
        HandleHospitalCollision();

        // If Soldier
        if (!trigCollider2D.gameObject.CompareTag("Soldier")) return;
        HandleSoldierCollision(trigCollider2D);
    }

    private void HandleTreeCollision()
    {
        _isAlive = false;
        spriteRenderer.enabled = false;
        Debug.Log("Game Over");
    }

    private void HandleHospitalCollision()
    {
        // Check if Helicopter isn't empty
        if (soldiers.Count == 0) return;
        // Reset each soldier in the Helicopter
        foreach (Soldier soldier in soldiers)
        {
            soldier.pickedUp = false;
            soldier.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        soldiers.Clear(); // Empty Helicopter
    }

    private void HandleSoldierCollision(Collider2D soldierCollider2D)
    {
        Soldier soldier = soldierCollider2D.GetComponent<Soldier>();

        // Check if Helicopter is full and if the soldier was already picked up
        if (soldiers.Count >= maxSoldiers || soldier.pickedUp) return;
        soldier.pickedUp = true; // Make sure it can't be counted twice
        soldier.gameObject.GetComponent<SpriteRenderer>().enabled = false; // Make it disappear
        soldiers.Add(soldierCollider2D.gameObject.GetComponent<Soldier>()); // Add the Soldier to the Helicopter
        // Update the tracker
    }
}