using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool levelCompleted;

    public GameObject gameOverPanel;
    public GameObject levelCompletedPanel;
    

    public int currentLevelIndex;
    public Text currentLevelText;
    public Text nextLevelText;

    public TextMeshProUGUI onPlayScoreText;
    public TextMeshProUGUI onPlayScoreText2;
    public TextMeshProUGUI totalScoreText;
    public int onPlayScore = 0;
    public int totalScore = 0;

    public double moneyConverter = 0;

    public TextMeshProUGUI currentLevelTextOnStart;
    public TextMeshProUGUI totalScoreTextOnStart;   //agat lama bet

    private PlayerController playerController1;

    public GameObject pauseBtn;
    public GameObject scoreGroup;
    public GameObject levelProgress;

    public static Vector3 lastCheckPointPos = new Vector3(0.062f, 1.493f);

    [SerializeField] GameObject handInstructionImage;
    [SerializeField] GameObject handInstructionSwipeUpImage;

    bool isHandSwipeUpShowed = false;


    private void Awake()
    {
        currentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
    }

    void Start()
    {
        gameOver = false;
        levelCompleted = false;
        Time.timeScale = 1;
        LoadTotalScore();

        currentLevelTextOnStart.text = "Level " + currentLevelIndex.ToString();
        //totalScoreTextOnStart.text = totalScore.ToString();
        SetScoreUI();
        
        playerController1 = GameObject.FindObjectOfType<PlayerController>();

        AdManager.instance.RequestInterstitial();

    }

    // Update is called once per frame
    void Update()
    {
        SetScoreUI();
        currentLevelText.text = currentLevelIndex.ToString();
        nextLevelText.text = (currentLevelIndex + 1).ToString();

        if (currentLevelIndex == 1)
        {
            //handInstructionImage.SetActive(true);
            StartCoroutine(StopHandInstruction());

            //StartCoroutine(ShowHandSwipeUpInstruction());

            //if (isHandSwipeUpShowed == true)
            //{
            //    StartCoroutine(HideHandSwipeUpInstruction());
            //}
            if (FindObjectOfType<PlayerController>().gameObject.transform.position.z >= 30)
            {
                handInstructionSwipeUpImage.SetActive(true);
            }
            if (FindObjectOfType<PlayerController>().gameObject.transform.position.z >= 40)
            {
                handInstructionSwipeUpImage.SetActive(false);
            }


        }

        if (gameOver)
        {
            //Time.timeScale = 0; // instead of this stop the player.
            FindObjectOfType<PlayerController>().playerRB.constraints = RigidbodyConstraints.FreezeAll;
            gameOverPanel.SetActive(true);

            pauseBtn.SetActive(false);
            levelProgress.SetActive(false);
            scoreGroup.SetActive(false);

            //AdManager.instance.RequestInterstitial();
            //AdManager.instance.ShowInterstitial();
            

        }

        if (levelCompleted)
        {
            //levelCompletedPanel.SetActive(true);
            //playerController1.StopPlayerMovement();
            StartCoroutine(StopGameAfterWin());
            //Time.timeScale = 0;

            //if (Input.GetButtonDown("Fire1"))
            //{
            //    PlayerPrefs.SetInt("CurrentLevelIndex", currentLevelIndex + 1);
            //    SceneManager.LoadScene(0);
            //}
        }

        
    }
    
    public void AddScore()
     {
       onPlayScore++;
     }

    public void AddToTotalScore()
    {
        totalScore = totalScore + onPlayScore;
        SaveTotalScore();
        SceneManager.LoadScene(0);
    }

    public void AddToTotalScoreTimesTwoReward()
    {
        totalScore = totalScore + (onPlayScore*2);
        SaveTotalScore();
        SceneManager.LoadScene(0);
    }

    public void AddScoreReward()
    {
        totalScore = totalScore + 100;
        SaveTotalScore();
        //SceneManager.LoadScene(0);
    }

    public void SaveTotalScore()
    {
        PlayerPrefs.SetInt("totalScore", totalScore);
    }
    public void LoadTotalScore()
    {
        totalScore = PlayerPrefs.GetInt("totalScore");
    }

    public void GoToNextLevel()
    {
        PlayerPrefs.SetInt("CurrentLevelIndex", currentLevelIndex + 1);
        AddToTotalScore();
    }


    IEnumerator StopGameAfterWin()
    {
        yield return new WaitForSeconds(1.5f);
        levelCompletedPanel.SetActive(true);

        pauseBtn.SetActive(false);
        levelProgress.SetActive(false);
        scoreGroup.SetActive(false);
        //Time.timeScale = 0;

    }


    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }



    public void DeleteSaved()
    {
        PlayerPrefs.DeleteAll();
    }

    public void CashConverter()
    {
        if (totalScore >= 1000)
        {
            moneyConverter = (float) totalScore / 1000;

            
        }
    
    }

    public void SetScoreUI()
    {
        CashConverter();
        if (totalScore >= 100)
        {
            totalScoreText.text = moneyConverter.ToString("F2") + "K";
            totalScoreTextOnStart.text = moneyConverter.ToString("F2") + "K";
            //onPlayScoreText.text = moneyConverter.ToString("F2") + "K";
            //onPlayScoreText2.text = moneyConverter.ToString("F2") + "K";

        }
        if (totalScore < 1000)
        {
            
            totalScoreText.text = totalScore.ToString();
            totalScoreTextOnStart.text = totalScore.ToString();
        }

        onPlayScoreText.text = onPlayScore.ToString();
        onPlayScoreText2.text = onPlayScore.ToString();
    }


    public void SaveMe()
    {
        gameOver = false;
       GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckPointPos;

        FindObjectOfType<PlayerController>().playerRB.constraints = RigidbodyConstraints.None;
        gameOverPanel.SetActive(false);

        pauseBtn.SetActive(true);
        levelProgress.SetActive(true);
        scoreGroup.SetActive(true);

    }


    IEnumerator StopHandInstruction()
    {
        yield return new WaitForSeconds(5.0f);
        handInstructionImage.SetActive(false);
        
    }



    public void ShowHandInstruction()
    {

        if (currentLevelIndex == 1)
        {
            handInstructionImage.SetActive(true);
        }
    
    }

    }
