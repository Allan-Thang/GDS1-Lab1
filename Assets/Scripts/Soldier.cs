using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool _pickedUp;
    private HelicopterController _helicopter;

    private void OnTriggerEnter2D(Collider2D trigCollider2D)
    {
        // Check if collided obj is the player
        if (!trigCollider2D.gameObject.CompareTag("Player")) return;
        _helicopter = trigCollider2D.GetComponent<HelicopterController>();

        // Check if Helicopter is full and if the soldier was already picked up
        if (_helicopter.soldiers.Count >= _helicopter.maxSoldiers || _pickedUp) return;
        _pickedUp = true; // Make sure it can't be counted twice
        spriteRenderer.enabled = false; // Make it disappear
        trigCollider2D.GetComponent<HelicopterController>().soldiers.Add(this); // Add the Soldier to the Helicopter
        // Update the tracker
    }
}