using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{  
    [SerializeField] private Image PlayerHPBar;
    [SerializeField] private Image FireBar;
    [SerializeField] private Image earth_BtnFill;
    [SerializeField] private Image fire_BtnFill;
    [SerializeField] private Image Heal_BtnFill;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject whiteFrame;
    [SerializeField] private GameObject damageFrame;
    [SerializeField] private GameObject healFrame;
    [SerializeField] private GameObject deathAreas;
    [SerializeField] private Text textHP;
    [SerializeField] private Text textMP;
    private Button earth_Btn;
    private GameObject curWindow;
    [Header("Префаб окна поражения")][SerializeField] private GameObject gameOverPanel;
    public Text scoreText;
    //3 надписи с панели поражения
    [HideInInspector]
    public Text bestScoreText;
    [HideInInspector]
    public Text currentScoreText;
    [HideInInspector]
    public GameObject newAlertText;

    private void Start()
    {
        earth_Btn = earth_BtnFill.GetComponent<Button>();
        whiteFrame.SetActive(true);
    }

    void Update ()
    {
        UpdateScore();
    }

    public void LoseFrame()
    {
        SoundData.instance.NormalToSilence();
        curWindow = Instantiate(gameOverPanel, Vector3.zero, Quaternion.identity, canvas.transform);
        curWindow.GetComponent<GameOverFrame>().canvas = canvas;
        currentScoreText = curWindow.transform.Find("Frame/ScorePoints").GetComponent<Text>();
        bestScoreText = curWindow.transform.Find("Frame/Text_BestScore").GetComponent<Text>();
        currentScoreText.text = "Score: " + Mathf.FloorToInt(GameData.score);
        if(GameData.score > PlayerPrefs.GetInt("Best", 0))
        {
            PlayerPrefs.SetInt("Best", Mathf.FloorToInt(GameData.score));
            newAlertText = curWindow.transform.Find("Frame/Text_New").gameObject;
            newAlertText.SetActive(true);
        }
        bestScoreText.text = "Best score: " + PlayerPrefs.GetInt("Best", 0);
    }

    public void UpdateHealBtn(float amount)
    {
        Heal_BtnFill.fillAmount = amount;
    }

    public void UpdateHPBar(float HP, bool heal)
    {
        if(heal) Instantiate(healFrame, Vector3.zero, Quaternion.identity, canvas.transform);
        else Instantiate(damageFrame, Vector3.zero, Quaternion.identity, canvas.transform);
        PlayerHPBar.fillAmount = HP / 100;
        textHP.text = HP + "%";
    }

    public void UpdateEarthBtn(float amount, bool canDoEarthSkill)
    {
        if (amount>0)
        {
            earth_BtnFill.fillAmount = amount;
        }
        else 
            if(canDoEarthSkill) earth_BtnFill.fillAmount = 0;
            else earth_BtnFill.fillAmount = 1;
    }

    public void UpdateFireBar(float amount, float oneFire)
    {
        FireBar.fillAmount = amount;
        textMP.text = Mathf.Floor(amount*100) + "%";
        if (oneFire < 1) fire_BtnFill.fillAmount = 0;
        else fire_BtnFill.fillAmount = 1;
    }

    private void UpdateScore()
    {
        if(!GameData.isGameOver && Time.timeScale == 1)
        {
            GameData.score += Time.deltaTime * GameData.scoreMultiply;
            scoreText.text = "" + Mathf.FloorToInt(GameData.score);
        }
    }

    public void Pause()
    {
        SoundController.instance.ClickBtnSound();
        curWindow = Instantiate(pauseMenu, Vector3.zero, Quaternion.identity, canvas.transform);
        PauseMenu script = curWindow.GetComponent<PauseMenu>();
        script.canvas = canvas;
        script.deathAreas = deathAreas;
    }
}
