using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    // 몬스터의 IsTrigger가 true가 되는 순간 아이템 생성
    // 코인 한개 생성
    // 이때 코인은 오브젝트 풀 이용
    // 캐릭터와 바로 충돌하는 것이 아닌 생성후 1초뒤 충돌

    

    bool isCoin;

    GameObject obj;
    [SerializeField]
    GameObject coinPrefab;
    BoxCollider2D col;



    private void Awake()
    {
        obj = GameObject.Find("Enemy");
        col = obj.GetComponent<BoxCollider2D>();
        isCoin = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(col.isTrigger)
        {            
            CreateCoin();            
        }
    }

    void CreateCoin()
    {
        if (isCoin)
        {
            Instantiate(coinPrefab, col.transform);
            isCoin = false;
            
        }
    }

    void EnemyDie()
    {
        isCoin = true;
    }
}
