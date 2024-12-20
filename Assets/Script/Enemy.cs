using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject obj;
    bool OnTrigger;
    


    private void Awake()
    {
        OnTrigger = false;
        obj = GameObject.Find("Enemy");
        Debug.Log(OnTrigger);
    }

    private void Update()
    {
        transform.position = obj.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            BoxCollider2D col = obj.GetComponent<BoxCollider2D>();
            col.isTrigger = true;
            col = GetComponent<BoxCollider2D>();
            col.isTrigger = true;
            OnTrigger = true;
            Debug.Log(OnTrigger);
        }
    }
}
