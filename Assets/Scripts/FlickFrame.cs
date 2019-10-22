using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlickFrame : MonoBehaviour {
    private Image image;
    public bool setCanvasInd;
    public int index;
    public bool alphaUp;
    public float speed;

    void Start()
    {
        if(setCanvasInd)
        {
            transform.SetSiblingIndex(index);
        }
        image = GetComponent<Image>();
    }

    void Update()
    {
        Flick();
    }

    private void Flick()
    {
        Color color = image.color;
        if (!alphaUp)
        {
            if (color.a > 0)
            {
                color.a -= Time.deltaTime * speed;
                image.color = color;
            }
            else Destroy(gameObject);
        }
        if (alphaUp)
        {
            if (color.a < 1)
            {
                color.a += Time.deltaTime * speed;
                image.color = color;
            }
            else
                if (SceneManager.GetActiveScene().name == "Menu") SceneManager.LoadScene("Game");
                else SceneManager.LoadScene("Menu");
        }
    }
}
