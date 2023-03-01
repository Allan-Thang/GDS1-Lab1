using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelicopterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TextMeshProUGUI soldiersInHelicopterNum;
    [SerializeField] private TextMeshProUGUI soldiersRescuedNum;
    [SerializeField] private GameObject GameOverScreen;
    public Rigidbody2D rb;
    private Vector2 _movement;
    private bool IsAlive { get; set; } = true;
    public List<Soldier> soldiers;
    public int maxSoldiers = 3;
    private int _soldiersRescued;

    // Update is called once per frame
    void Update()
    {
        // Get a vector for movement based on input
        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");
        Debug.Log(soldiers.Count); // DebugPlaceholder
    }

    private void FixedUpdate()
    {
        // Move the player independent of frame-rate
        if (!IsAlive) return;
        rb.MovePosition(rb.position + _movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter2D(Collider2D trigCollider2D)
    {
        // If Tree
        if (trigCollider2D.gameObject.CompareTag("Tree"))
        {
            HandleTreeCollision();
        }
        // If Hospital
        else if (trigCollider2D.gameObject.CompareTag("Hospital"))
        {
            HandleHospitalCollision();
        }
        // If Soldier
        else if (trigCollider2D.gameObject.CompareTag("Soldier"))
        {
            HandleSoldierCollision(trigCollider2D);
        }
    }

    private void HandleTreeCollision()
    {
        IsAlive = false;
        spriteRenderer.enabled = false;
        GameOverScreen.SetActive(true);
        Debug.Log("Game Over"); // DebugPlaceholder
    }

    private void HandleHospitalCollision()
    {
        // Check if Helicopter is empty
        if (soldiers.Count == 0) return;
        // Reset each soldier in the Helicopter
        foreach (Soldier soldier in soldiers)
        {
            _soldiersRescued++;
            soldier.pickedUp = false;
            soldier.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        soldiersRescuedNum.text = _soldiersRescued.ToString();
        soldiers.Clear(); // Empty Helicopter
        soldiersInHelicopterNum.text = "0";
    }

    private void HandleSoldierCollision(Collider2D soldierCollider2D)
    {
        Soldier soldier = soldierCollider2D.GetComponent<Soldier>();
        // Check if Helicopter is full and if the soldier was already picked up
        if (soldiers.Count >= maxSoldiers || soldier.pickedUp) return;
        soldier.pickedUp = true; // Make sure it can't be counted twice
        soldier.gameObject.GetComponent<SpriteRenderer>().enabled = false; // Make it disappear
        soldiers.Add(soldierCollider2D.gameObject.GetComponent<Soldier>()); // Add the Soldier to the Helicopter
        soldiersInHelicopterNum.text = soldiers.Count.ToString();
    }
}