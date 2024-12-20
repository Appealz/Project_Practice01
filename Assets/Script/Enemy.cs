using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 이동 방향
    int moveDir;
    SpriteRenderer sr;

    [SerializeField]
    int moveSpeed;
    


    private void Awake()
    {
        // 초기 이동방향 오른쪽(+1)
        moveDir = 1;
        TryGetComponent<SpriteRenderer>(out sr);
    }

    private void Update()
    {        
        // 이동방향으로 이동
        transform.position += new Vector3(Time.deltaTime * moveDir * moveSpeed, 0f, 0f);
    }

    // 트리거 처리(겹쳐짐)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.gameObject.CompareTag("Player"))
        //{
        //    BoxCollider2D col = obj.GetComponent<BoxCollider2D>();
        //    col.isTrigger = true;
        //    col = GetComponent<BoxCollider2D>();
        //    col.isTrigger = true;
        //    OnTrigger = true;
        //    Debug.Log(OnTrigger);
        //}

        // 트리거된 오브젝트의 태그가 Right라면
        if (collision.gameObject.CompareTag("Right"))
        {
            // 이동방향 반대로 -1
            moveDir = -1;
            // 캐릭터의 방향 반대로
            sr.flipX = true;
        }

        if (collision.gameObject.CompareTag("Left"))
        {
            // 이동방향 반대로 +1
            moveDir = 1;
            // 캐릭터의 방향 원상태로
            sr.flipX = false;
        }
    }
}
