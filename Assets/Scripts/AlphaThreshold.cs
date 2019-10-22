using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaThreshold : MonoBehaviour {
    [SerializeField] private float threshold;
    private Image image;

    void Start ()
    {
        image = GetComponent<Image>();
        image.alphaHitTestMinimumThreshold = threshold;
    }
}
