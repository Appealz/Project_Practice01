using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveGround : MonoBehaviour
{
    [SerializeField]
    int moveDirX;
    [SerializeField]
    int moveDirY;
    [SerializeField]
    float moveSpeed;

    [SerializeField]
    float moveLength;
    Vector3 startPos;

    private void Awake()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (moveDirX != 0)
        {
            moveX();
        }

        if (moveDirY != 0)
        {
            moveY();
        }        
    }

    void moveX()
    {
        transform.position += new Vector3(Time.deltaTime * moveSpeed*  moveDirX, 0f, 0f);
        if (Vector3.Distance(startPos, transform.position) > 4f)
        {
            moveDirX = -1;
        }
        if (Vector3.Distance(startPos, transform.position) < 0.2f)
        {
            moveDirX = 1;
        }
    }

    void moveY()
    {
        transform.position += new Vector3(0f, Time.deltaTime * moveSpeed  * moveDirY, 0f);
        if (Vector3.Distance(startPos, transform.position) > moveLength)
        {
            moveDirY = -1;
        }
        if (Vector3.Distance(startPos, transform.position) < 0.2f)
        {
            moveDirY = 1;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
        collision.transform.SetParent(null);
        }        
    }
}
