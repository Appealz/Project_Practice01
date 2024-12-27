using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public int coinCount;
    public int lifeCount;


    private void Awake()
    {
        InitData();
    }

    private void InitData()
    {
        coinCount = 0;
        lifeCount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
