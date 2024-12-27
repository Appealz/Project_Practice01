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
    // 이동속도
    [SerializeField]
    public float moveSpeed;
    // 이동방향
    [SerializeField]
    public Vector3 moveDir;
    // 점프 횟수
    int jumpCount;
    // 점프세기
    [SerializeField]
    public float jump;
    // 이동 여부
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

    // 캐릭터 이동
    private void CharMove()
    {
        moveDir.x = Input.GetAxis("Horizontal");
        moveDir.y = 0f;
        moveDir.z = 0f;

        // isMove가 true일때
        if(isMove)
        {
            // moveDir 방향으로 moveSpeed만큼 이동
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }

        // Space를 누르고 jumpCount가 1보다 작을때
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount<2)
        {
            // jumpCount 1 증가
            jumpCount++;
            // jump세기만큼 y축으로 impulse
            rb.AddForce(new Vector3(0f, jump, 0f), ForceMode2D.Impulse);            
            Debug.Log(jumpCount);
            isMove = true;
        }
        
        // Player가 떨어질때만 체크(속도가 - 일때)
        if(rb.velocity.y < 0f)
        {
            // Raycast를 현재 위치에서 아래방향으로 1f만큼거리로 쏘며 부딪힌대상의 Layer가 Ground라면 참조.
            RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector3.down, jumpRay, LayerMask.GetMask("Ground"));
            // ray가 있다면
            if (ray)
            {
                // jumpCount 초기화
                jumpCount = 0;
            }
        }
        
    }

    // 캐릭터 방향
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

    // 캐릭터 모션
    private void CharMotion()
    {
        // moveDir의 절대값을 받으며 MoveSpeed 의 파라메터를 받아옴.
        animator.SetFloat("MoveSpeed", Mathf.Abs(moveDir.x));

        // Space바를 누르면
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // Jump 파라메터를 받아옴.
            animator.SetTrigger("Jump");
        }
    }

    // 적과 충돌
    //private void EnemyCol()
    //{
    //    RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Enemy"));
    //    obj.TryGetComponent<BoxCollider2D>(out BoxCollider2D col);
    //    Rigidbody2D enemyRb = obj.GetComponent<Rigidbody2D>();
        
    //    // ray가 있다면
    //    if (ray)
    //    {            
    //        // col(Enemy의 BoxCollider2D 컴포넌트)의 istrigger를 true로
    //        col.isTrigger = true;
    //        obj.TryGetComponent<Enemy>(out Enemy enemy);
            
    //        // 1.1f 만큼 y축으로 impulse            
    //        if (rb.velocity.y < 0f)
    //        {                
    //            rb.AddForce(new Vector3(0f, jump, 0f), ForceMode2D.Impulse);
    //            enemyRb.AddForce(new Vector3(0f, 1.1f, 0f), ForceMode2D.Impulse);
    //            enemy.Die(); // enemy 처리 메소드 
    //            Debug.Log("Enemy 충돌");
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
                    Debug.Log("Enemy 충돌");
                }
            }
        }
    }



    // 충돌처리
    // 추가 구현 : 플레이어의 무적시간 1초 부여
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌 오브젝트의 태그가 Enemy라면 
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 이동 금지
            isMove = false;
            // 충돌 오브젝트의 위치가 캐릭터의 왼쪽이라면 (캐릭터위치 : 적군의 오른쪽)
            if(collision.transform.position.x < transform.position.x)
            {
                // 오른쪽으로 2만큼 위로 5만큼 impusle
                rb.AddForce(new Vector3(2f, 5f, 0f), ForceMode2D.Impulse);
            }
            else // 그게 아니라면
            {
                // 왼쪽으로 2만큼(-2) 위로 5만큼 impulse
                rb.AddForce(new Vector3(-2f, 5f, 0f), ForceMode2D.Impulse);
            }
            gameDataManager.lifeCount--;
        }

        // 충돌 오브젝트의 태그가 Ground라면
        if(collision.gameObject.CompareTag("Ground"))
        {
            // 이동 가능하도록 isMove를 true
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
