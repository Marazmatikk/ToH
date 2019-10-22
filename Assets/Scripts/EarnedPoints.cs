using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EarnedPoints : MonoBehaviour {
    [SerializeField] private float speed;
    private Text text;

    private void Update()
    {
        MoveUp();
    }

    private void MoveUp()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        Color color = text.color;
        if (color.a > 0)
        {
            color.a -= Time.deltaTime * speed;
            text.color = color;
        }
        else Destroy(transform.parent);
    }
}
