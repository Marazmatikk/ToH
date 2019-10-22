using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisualHurt : MonoBehaviour
{
    private SpriteRenderer m_renderer;
    private Color baseColor = Color.white;
    private Color hurtColor = Color.red;
    private float speed = 2;

    private void Start()
    {
        m_renderer = GetComponent<SpriteRenderer>();
        m_renderer.color = hurtColor;
    }

    private void Update()
    {
        ToBaseColor();
    }

    private void ToBaseColor()
    {
        m_renderer.color = Color.Lerp(m_renderer.color, baseColor, speed * Time.deltaTime);
        if (m_renderer.color == baseColor) Destroy(this);
    }
}
