using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Camera : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField]
    float camMoveSpeed;
    GameObject obj;
    Vector3 camPos;
    Vector3 playerPos;
    float offset;
    

    SpriteRenderer sr;

    private void Awake()
    {
        obj = GameObject.Find("Player");
        obj.TryGetComponent<PlayerController>(out playerController);
        camPos = transform.position;
        
        offset = Mathf.Abs(camPos.x - playerPos.x);        

        obj.TryGetComponent<SpriteRenderer>(out sr);
        //camPos.x = playerController.transform.position.x;
        camPos.y = playerController.transform.position.y;
        camPos.z = transform.position.z;
    }

    private void LateUpdate()
    {
        playerPos = playerController.transform.position;
        if (sr.flipX == true)
        {
            camPos.x = camPos.x + camMoveSpeed * Time.deltaTime ;
            if(camPos.x > playerPos.x + offset)
            {
                camPos.x = playerPos.x + offset;
            }
            camPos.y = playerController.transform.position.y;
            transform.position = camPos;
        }
        else
        {
            camPos.x = camPos.x + camMoveSpeed * (-Time.deltaTime);
            if(camPos.x < playerPos.x - offset)
            {
                camPos.x = playerPos.x - offset;
            }
            camPos.y = playerController.transform.position.y;
            transform.position = camPos;
        }

        
    }
}
