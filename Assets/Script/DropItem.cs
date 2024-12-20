using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    // ������ IsTrigger�� true�� �Ǵ� ���� ������ ����
    // ���� �Ѱ� ����
    // �̶� ������ ������Ʈ Ǯ �̿�
    // ĳ���Ϳ� �ٷ� �浹�ϴ� ���� �ƴ� ������ 1�ʵ� �浹

    

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
