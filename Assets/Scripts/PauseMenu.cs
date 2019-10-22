using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject loadFrame;
    [HideInInspector] public Canvas canvas;
    [HideInInspector] public GameObject deathAreas;

    private void OnEnable()
    {
        Time.timeScale = 0;
        SoundData.instance.NormalToSilence();
    }

    public void Continue()
    {
        SoundController.instance.ClickBtnSound();
        Destroy(gameObject);
    }

    public void Restart()
    {
        SoundController.instance.ClickBtnSound();
        SceneManager.LoadScene("Game");
    }

    public void Menu()
    {
        SoundController.instance.ClickBtnSound();
        Instantiate(loadFrame, transform.position, Quaternion.identity, canvas.transform);
        Destroy(deathAreas);
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        SoundData.instance.SilenceToNormal();
    }
}
