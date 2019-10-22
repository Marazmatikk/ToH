using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button musicBtn;
    [SerializeField] private Button soundsBtn;
    [SerializeField] private GameObject startPreloadFrame;
    [SerializeField] private GameObject preloadFrame;
    [SerializeField] private GameObject PlayBtn;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject player;
    [SerializeField] private float jumpPower = 160.0f;
    [SerializeField] private Sprite[] turnMusic;
    [SerializeField] private Sprite[] turnSounds;
    public float delayLoad;
    private Rigidbody2D myRigidbody;
    private Animator animatorPlayer;
    private bool move = false;
    private Vector3 speedMove = new Vector3(5f, 0f, 0f);
    private int countJump = 0;
    public static UnityEvent changeMusicVolume = new UnityEvent();
    public static UnityEvent changeMasterVolume = new UnityEvent();
    private bool musicEnable;
    private bool soundsEnable;

    void Start()
    {
        GameData.isGameOver = false;
        startPreloadFrame.SetActive(true);
        animatorPlayer = player.GetComponent<Animator>();
        myRigidbody = player.GetComponent<Rigidbody2D>();
        SetBtnSprites();
    }

    void Update()
    {
        MoveCharacter();
    }

    public void StartGame()
    {
        SoundController.instance.PlayBtnSound();
        move = true;
        animatorPlayer.SetInteger("State", 1);
        Destroy(PlayBtn);
    }

    private void MoveCharacter()
    {
        if (move)
        {
            player.transform.Translate(speedMove * Time.deltaTime);
        }
        if (player.transform.position.x > 6f)
        {
            if(countJump==0)
            {
                animatorPlayer.SetInteger("State", 2);
                myRigidbody.AddForce(Vector3.up * (jumpPower * myRigidbody.mass * myRigidbody.gravityScale));
                countJump++;
                Instantiate(preloadFrame, Vector3.zero, Quaternion.identity, canvas.transform);
            }
        }
    }

    public void ExitApp()
    {
        SoundController.instance.ClickBtnSound();
        Application.Quit();
    }

    public void MusicBtn()
    {
        SoundController.instance.ClickBtnSound();
        SoundData.instance.ChangeMusicTurn();
        if (musicEnable)
        {
            musicBtn.image.sprite = turnMusic[0];
            musicEnable = false;
        }
        else
        {
            musicBtn.image.sprite = turnMusic[1];
            musicEnable = true;
        }
    }

    public void SoundBtn()
    {
        SoundController.instance.ClickBtnSound();
        SoundData.instance.ChangeMasterTurn();
        if (soundsEnable)
        {
            soundsBtn.image.sprite = turnSounds[0];
            soundsEnable = false;
        }
        else
        {
            soundsBtn.image.sprite = turnSounds[1];
            soundsEnable = true;
        }
    }

    private void SetBtnSprites()
    {
        if(PlayerPrefs.GetFloat("MasterVolume") == -80)
        {
            soundsBtn.image.sprite = turnSounds[0];
            soundsEnable = false;
        }
        else
        {
            soundsBtn.image.sprite = turnSounds[1];
            soundsEnable = true;
        }
        if (PlayerPrefs.GetFloat("MusicVolume") == -80)
        {
            musicBtn.image.sprite = turnMusic[0];
            musicEnable = false;
        }
        else
        {
            musicBtn.image.sprite = turnMusic[1];
            musicEnable = true;
        }
    }
}
