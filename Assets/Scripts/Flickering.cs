using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flickering : MonoBehaviour {
    private Image image;
    private Color color;
    [Range(0f, 1f)] public float speed;
    private bool visible = true;

	void Start ()
    {
        image = GetComponent<Image>();
        color = image.color;
	}
	
	void Update ()
    {
        if (visible)
        {
            color.a -= Time.deltaTime * speed;
            image.color = color;
            if(color.a <= 0)
            {
                visible = false;
            }
        }
        else
            color.a += Time.deltaTime * speed;
        image.color = color;
        if (color.a >= 1)
        {
            visible = true;
        }
    }
}
