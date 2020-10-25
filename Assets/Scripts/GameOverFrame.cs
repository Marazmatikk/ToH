using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverFrame : MonoBehaviour
{
    [SerializeField] private GameObject loadFrame;
    
    [HideInInspector] public Canvas canvas;

    public void Restart()
    {
        SoundController.instance.ClickBtnSound();
        SceneManager.LoadScene("Game");
    }

    public void Menu()
    {
        SoundController.instance.ClickBtnSound();
        Instantiate(loadFrame, transform.position, Quaternion.identity, canvas.transform);
        Destroy(gameObject);
    }
}
