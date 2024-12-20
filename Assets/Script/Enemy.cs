using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // �̵� ����
    int moveDir;
    SpriteRenderer sr;

    [SerializeField]
    int moveSpeed;
    


    private void Awake()
    {
        // �ʱ� �̵����� ������(+1)
        moveDir = 1;
        TryGetComponent<SpriteRenderer>(out sr);
    }

    private void Update()
    {        
        // �̵��������� �̵�
        transform.position += new Vector3(Time.deltaTime * moveDir * moveSpeed, 0f, 0f);
    }

    // Ʈ���� ó��(������)
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

        // Ʈ���ŵ� ������Ʈ�� �±װ� Right���
        if (collision.gameObject.CompareTag("Right"))
        {
            // �̵����� �ݴ�� -1
            moveDir = -1;
            // ĳ������ ���� �ݴ��
            sr.flipX = true;
        }

        if (collision.gameObject.CompareTag("Left"))
        {
            // �̵����� �ݴ�� +1
            moveDir = 1;
            // ĳ������ ���� �����·�
            sr.flipX = false;
        }
    }
}
