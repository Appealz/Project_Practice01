using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TreeEditor;
using UnityEditor.EventSystems;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Scripting;

public class PlayerController : MonoBehaviour
{
    // �̵��ӵ�
    [SerializeField]
    public float moveSpeed;
    // �̵�����
    [SerializeField]
    public Vector3 moveDir;
    // ���� Ƚ��
    int jumpCount;
    // ��������
    [SerializeField]
    public float jump;
    // �̵� ����
    bool isMove;

    [SerializeField]
    float jumpRay;

    Animator animator;
    Rigidbody2D rb;
    GameObject obj;
    GameDataManager gameDataManager;
    

    private void Awake()
    {        
        TryGetComponent<Animator>(out animator);
        TryGetComponent<Rigidbody2D>(out rb);
        obj = GameObject.FindGameObjectWithTag("Enemy");
        isMove = true;
        gameDataManager = FindAnyObjectByType<GameDataManager>();    
    }

    // Update is called once per frame
    void Update()
    {
        CharMove();
        CharDir();
        CharMotion();
        EnemyCol();
    }

    // ĳ���� �̵�
    private void CharMove()
    {
        moveDir.x = Input.GetAxis("Horizontal");
        moveDir.y = 0f;
        moveDir.z = 0f;

        // isMove�� true�϶�
        if(isMove)
        {
            // moveDir �������� moveSpeed��ŭ �̵�
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }

        // Space�� ������ jumpCount�� 1���� ������
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount<2)
        {
            // jumpCount 1 ����
            jumpCount++;
            // jump���⸸ŭ y������ impulse
            rb.AddForce(new Vector3(0f, jump, 0f), ForceMode2D.Impulse);            
            Debug.Log(jumpCount);
            isMove = true;
        }
        
        // Player�� ���������� üũ(�ӵ��� - �϶�)
        if(rb.velocity.y < 0f)
        {
            // Raycast�� ���� ��ġ���� �Ʒ��������� 1f��ŭ�Ÿ��� ��� �ε�������� Layer�� Ground��� ����.
            RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector3.down, jumpRay, LayerMask.GetMask("Ground"));
            // ray�� �ִٸ�
            if (ray)
            {
                // jumpCount �ʱ�ȭ
                jumpCount = 0;
            }
        }
        
    }

    // ĳ���� ����
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

    // ĳ���� ���
    private void CharMotion()
    {
        // moveDir�� ���밪�� ������ MoveSpeed �� �Ķ���͸� �޾ƿ�.
        animator.SetFloat("MoveSpeed", Mathf.Abs(moveDir.x));

        // Space�ٸ� ������
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // Jump �Ķ���͸� �޾ƿ�.
            animator.SetTrigger("Jump");
        }
    }

    // ���� �浹
    //private void EnemyCol()
    //{
    //    RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Enemy"));
    //    obj.TryGetComponent<BoxCollider2D>(out BoxCollider2D col);
    //    Rigidbody2D enemyRb = obj.GetComponent<Rigidbody2D>();
        
    //    // ray�� �ִٸ�
    //    if (ray)
    //    {            
    //        // col(Enemy�� BoxCollider2D ������Ʈ)�� istrigger�� true��
    //        col.isTrigger = true;
    //        obj.TryGetComponent<Enemy>(out Enemy enemy);
            
    //        // 1.1f ��ŭ y������ impulse            
    //        if (rb.velocity.y < 0f)
    //        {                
    //            rb.AddForce(new Vector3(0f, jump, 0f), ForceMode2D.Impulse);
    //            enemyRb.AddForce(new Vector3(0f, 1.1f, 0f), ForceMode2D.Impulse);
    //            enemy.Die(); // enemy ó�� �޼ҵ� 
    //            Debug.Log("Enemy �浹");
    //        }
    //    }
    //}
    private void EnemyCol()
    {
        // Perform Raycast
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Enemy"));

        // Check if the ray hit an object
        if (ray.collider != null)
        {
            GameObject obj = ray.collider.gameObject;

            // Try to get necessary components
            if (obj.TryGetComponent<BoxCollider2D>(out BoxCollider2D col) &&
                obj.TryGetComponent<Rigidbody2D>(out Rigidbody2D enemyRb) &&
                obj.TryGetComponent<Enemy>(out Enemy enemy))
            {
                // Make the enemy collider a trigger
                col.isTrigger = true;

                // Only apply force if the player is falling
                if (rb.velocity.y < 0f)
                {
                    // Player jumps
                    rb.AddForce(new Vector3(0f, jump, 0f), ForceMode2D.Impulse);

                    // Apply impulse to the enemy
                    if (enemyRb != null)
                    {
                        enemyRb.AddForce(new Vector3(0f, 2f, 0f), ForceMode2D.Impulse);
                    }
                    // Kill the enemy
                    enemy.Die();

                    // Debug log
                    Debug.Log("Enemy �浹");
                }
            }
        }
    }



    // �浹ó��
    // �߰� ���� : �÷��̾��� �����ð� 1�� �ο�
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹 ������Ʈ�� �±װ� Enemy��� 
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // �̵� ����
            isMove = false;
            // �浹 ������Ʈ�� ��ġ�� ĳ������ �����̶�� (ĳ������ġ : ������ ������)
            if(collision.transform.position.x < transform.position.x)
            {
                // ���������� 2��ŭ ���� 5��ŭ impusle
                rb.AddForce(new Vector3(2f, 5f, 0f), ForceMode2D.Impulse);
            }
            else // �װ� �ƴ϶��
            {
                // �������� 2��ŭ(-2) ���� 5��ŭ impulse
                rb.AddForce(new Vector3(-2f, 5f, 0f), ForceMode2D.Impulse);
            }
            gameDataManager.lifeCount--;
        }

        // �浹 ������Ʈ�� �±װ� Ground���
        if(collision.gameObject.CompareTag("Ground"))
        {
            // �̵� �����ϵ��� isMove�� true
            isMove = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            gameDataManager.coinCount++;
        }
    }

}
