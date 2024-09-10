using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvaController : MonoBehaviour
{ 
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Animator animator;
    public bool isSoundOff;
    public bool isFxOff;
    private int timerImageMax;
    private float timerImageRemain;
    [SerializeField] private Image timerImage;
    [Header("-=-TEXT-=-")]
    [SerializeField] private TextMeshProUGUI countCapsuleText;
    [SerializeField] private TextMeshProUGUI countCapsuleTotalText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI lvlText;

    [Header("-=-BUTTON-=-")]
    [SerializeField] private Button pauseBtn;
    [SerializeField] private Button soundBtn;
    [SerializeField] private Button fxBtn;
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button retryBtn;
    [SerializeField] private Button homeBtn;
    [Header("-=-SLIDER-=-")]
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider fxSlider;
    [SerializeField] private Slider countCapsuleSlider;
    [Header("-=-SPRITE-=-")]
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;   
    [SerializeField] private Sprite fxOnSprite;
    [SerializeField] private Sprite fxOffSprite;
    // Start is called before the first frame update
    private void Awake()
    {
        pauseMenu.SetActive(false);
        animator ??= GetComponent<Animator>();
        soundBtn.onClick.AddListener(() =>
            ChangeSpriteSoundBtn() );
        fxBtn.onClick.AddListener(() =>
            ChangeSpriteFxBtn());
        continueBtn.onClick.AddListener(() =>
            ContinueButton());
        pauseBtn.onClick.AddListener(() =>
            PauseButton());
        retryBtn.onClick.AddListener(() =>
            RetryButton());
        homeBtn.onClick.AddListener(() =>
            HomeButton());
    }
    void Start()
    {
        isSoundOff = false;
        isFxOff = false;
        soundSlider.value = 1;
        fxSlider.value = 1;
   
        timerImageMax = GameManager.time;
        lvlText.text = GameManager.levelOfMap.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isPaused)
        {
            countCapsuleText.text = GameManager.count.ToString();
            countCapsuleSlider.maxValue = GameManager.countTotal;
            countCapsuleTotalText.text = GameManager.countTotal.ToString();
            countCapsuleSlider.value = GameManager.count;
            timerImageRemain = GameManager.time;
            timerImage.fillAmount = timerImageRemain / timerImageMax;
            if (GameManager.time < 10)
            {
                timerText.color = Color.red;
            }
            if (GameManager.time <= 0)
            {
                GameManager.isLost = true;
            }
            timerText.text = (GameManager.time / 60).ToString() + ":" + ((GameManager.time % 60) < 10 ? "0" + (GameManager.time % 60).ToString() : (GameManager.time % 60).ToString());
            if (GameManager.isWon)
            {
                animator.SetBool("isTransitted", true);
                GameManager.isWon = false;
            }
            if (GameManager.isLost)
            {
                continueBtn.gameObject.SetActive(false);
                Invoke("PauseButton", 0);
                GameManager.isLost = false;
            }       
        }
        else
        {       
            AudioManager.Instance.bgm.volume = soundSlider.value;
            AudioManager.Instance.fx.volume = fxSlider.value;
            if (soundSlider.value > 0)
            {
                soundBtn.image.sprite = soundOnSprite;
                isSoundOff = false;
            }
            else
            {
                soundBtn.image.sprite = soundOffSprite;
                isSoundOff = true;
            }
            if (fxSlider.value > 0)
            {
                fxBtn.image.sprite = fxOnSprite;
                isFxOff = false;
            }
            else
            {
                fxBtn.image.sprite = fxOffSprite;
                isFxOff = true;
            }
        }
    }
    void ChangeSpriteSoundBtn()
    {
        if (GameManager.isPaused)
        {
            if (isSoundOff)
            {
                soundSlider.value = 1;
            }
            else
            {
                soundSlider.value = 0;
            }
        }
    }
    void ChangeSpriteFxBtn()
    {
        if (GameManager.isPaused)
        {
            if (isFxOff)
            {
                fxSlider.value = 1;
            }
            else
            {
                fxSlider.value = 0;
            }
        }
    }
    void PauseButton()
    {
        GameManager.isPaused = true;
        animator.SetBool("isMenuOn", true);
    }
    void ContinueButton()
    {
        animator.SetBool("isMenuOn", false);
    }
    void RetryButton()
    {
        GameManager.isLost = false;
        GameManager.isWon = false;
        GameManager.isPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void HomeButton()
    {
        SceneManager.LoadScene("Home");
    }
    public void PauseMenuOn() //use for event in animation
    {
        pauseMenu.SetActive(true);
    }
    public void PauseMenuOff() //use for event in animation
    {
        GameManager.isPaused = false;
        pauseMenu.SetActive(false);
    }
    public void ImageTransittionOn() //use for event in animation
    {
        AudioManager.Instance.fxPlay(AudioManager.Instance.winMap);             
    }
    public void ImageTransittionOff() //use for event in animation
    {
        AudioManager.Instance.fxStop();
        animator.SetBool("isTransitted", false);
        GameManager.isWon = false;
        GameManager.isLoadingMap = true;
    }
}
