using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool _pickedUp;
    private HelicopterController _helicopter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        _helicopter = other.GetComponent<HelicopterController>();

        if (_helicopter.soldiers.Count >= _helicopter.maxSoldiers || _pickedUp) return;
        _pickedUp = true;
        spriteRenderer.enabled = false;
        other.GetComponent<HelicopterController>().soldiers.Add(this);
    }
}