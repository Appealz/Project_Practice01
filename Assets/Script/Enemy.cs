using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    BoxCollider2D col;
    


    private void Awake()
    {
        col = GetComponentInParent<BoxCollider2D>();
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            col.isTrigger = true;
            BoxCollider2D enemyCol = GetComponent<BoxCollider2D>();
            enemyCol.isTrigger = true;
        }
    }
}
