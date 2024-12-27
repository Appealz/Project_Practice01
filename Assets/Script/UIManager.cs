using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Image[] lifeImg;
    public TextMeshProUGUI coinCount;
    GameDataManager gameDataManager;

    private void Awake()
    {
        gameDataManager = FindAnyObjectByType<GameDataManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCoin(gameDataManager.coinCount);
        UpdateLifeCount(gameDataManager.lifeCount);
    }

    public void UpdateCoin(int coin)
    {
        coinCount.text = ":  " + coin.ToString();
    }

    private void UpdateLifeCount(int life)
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < life) 
            {
                lifeImg[i].enabled = true; // i값이 life보다 작다면 lifeImg를 활성화 
            }
            else
            {
                lifeImg[i].enabled = false; 
            }
        }
    }
}
