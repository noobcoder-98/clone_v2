using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectContainer : MonoBehaviour
{
    public bool isFull;
    public GameManager gameManager;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        gameManager = GameManager.instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter() {
        if (gameManager.draggingObject != null && !isFull) {
            gameManager.currentContainter = gameObject;
            spriteRenderer.enabled = true;
        }
    }

    private void OnMouseExit() {
        gameManager.currentContainter = null;
        spriteRenderer.enabled = false;
    }
}
