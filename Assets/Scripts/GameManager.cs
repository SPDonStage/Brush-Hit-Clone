using System.Collections;
using System.Collections.Generic;
using UnityEditor;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour // Singleton<GameManager>
{
    private enum BGMType
    {
        BGM0, //main menu
        BGM1,
        BGM2,
    }
    [SerializeField] private BGMType bgm;
    public static bool isPaused;
    public static bool isWon;
    public static bool isLost;
    public static bool isLoadingMap;
    [HideInInspector] public static int count;
    [HideInInspector] public static int countTotal;
    //timer
    private bool isTriggerTimer;
    [SerializeField] private int timeValue;
    [HideInInspector] public static int time;
    IEnumerator timerCoroutine;
    [SerializeField] private int onLevel;
    public static int levelOfMap;
    [SerializeField] public string nextLvlMapScene; //scene to load
    private void Awake()
    {
        levelOfMap = onLevel;
        count = 0;
        countTotal = 0;
        //timer
        isTriggerTimer = true;
        time = timeValue;
        timerCoroutine = timer();
        isPaused = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        switch (bgm)
        {
            case BGMType.BGM0:
                {
                    AudioManager.Instance.bgmPlay(AudioManager.Instance.mainMenu);
                    break;
                }
            case BGMType.BGM1:
                {
                    AudioManager.Instance.bgmPlay(AudioManager.Instance.BGM1);
                    break;
                }
            case BGMType.BGM2:
                {
                    AudioManager.Instance.bgmPlay(AudioManager.Instance.BGM2);
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (onLevel != 0) //not on menu
        {
            if (!isPaused)
            {
                if (isTriggerTimer)
                {
                    StartCoroutine(timerCoroutine);
                    isTriggerTimer = false;
                }
                WinGame();
                LoadMap();
            }
            else
            {
                if (!isTriggerTimer)
                {
                    StopCoroutine(timerCoroutine);
                    isTriggerTimer = true;
                }
            }
        }
    }
    IEnumerator timer()
    {
        while (time > 0 && !isWon)
        {
            yield return new WaitForSeconds(1);
            time--;
        }
    }
    void WinGame()
    {
        if (count == countTotal && !isWon)
        {
            isWon = true;          
        }
    }
    void LoadMap()
    {
        if (isLoadingMap)
        {
            isWon = false;
            SceneManager.LoadScene(nextLvlMapScene);
            isLoadingMap = false;
        }
    }
}
