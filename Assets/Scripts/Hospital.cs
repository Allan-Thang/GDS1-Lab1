using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hospital : MonoBehaviour
{
    private HelicopterController _helicopter;

    private void OnTriggerEnter2D(Collider2D trigCollider2D)
    {
        // Check if collided obj is the player
        if (!trigCollider2D.gameObject.CompareTag("Player")) return;
        _helicopter = trigCollider2D.GetComponent<HelicopterController>();

        // Check if Helicopter isn't empty
        if (_helicopter.soldiers.Count == 0) return;
        // Update other stuff
        _helicopter.soldiers.Clear(); // Empty Helicopter
    }
}