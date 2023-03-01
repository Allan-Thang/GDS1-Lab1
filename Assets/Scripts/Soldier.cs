using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool _pickedUp = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && _pickedUp == false)
        {
            this.transform.position = new Vector2(0.0f, 0.0f);
            spriteRenderer.enabled = false;
        }
    }
}