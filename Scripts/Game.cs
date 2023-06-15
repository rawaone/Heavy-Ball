using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    #region SIngleton:Game

    public static Game Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    GameManager gameManager;

    //public int coins;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        //coins = gameManager.totalScore;
        gameManager = FindObjectOfType<GameManager>();
    }
    public void UseCoins(int amount)
    {
        gameManager.totalScore -= amount;
    }

    public bool HasEnoughCoins(int amount)
    {
        return (gameManager.totalScore >= amount);
    }


}
