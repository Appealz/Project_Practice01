using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TreeEditor;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed;
    [SerializeField]
    public Vector3 moveDir;
    int jumpCount;

    [SerializeField]
    public float jump;

    Animator animator;

    Rigidbody2D rb;

    private void Awake()
    {        
        TryGetComponent<Animator>(out animator);
        TryGetComponent<Rigidbody2D>(out rb);
    }

    // Update is called once per frame
    void Update()
    {
        CharMove();
        CharDir();
        CharMotion();
    }

    private void CharMove()
    {
        moveDir.x = Input.GetAxis("Horizontal");
        moveDir.y = 0f;
        moveDir.z = 0f;

        transform.position += moveDir * moveSpeed * Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space) && jumpCount<2)
        {
            rb.AddForce(new Vector3(0f, jump, 0f), ForceMode2D.Impulse);
            jumpCount++;
        }
    }

    private void CharDir()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (moveDir.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveDir.x > 0)
        {
            spriteRenderer.flipX = true;
        }        
    }

    private void CharMotion()
    {
        animator.SetFloat("MoveSpeed", Mathf.Abs(moveDir.x));

        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Collision"))
        {
            jumpCount = 0;
            Debug.Log("Ground Ãæµ¹");
        }
    }
}
