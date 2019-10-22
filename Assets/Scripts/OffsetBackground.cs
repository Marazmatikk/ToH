using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetBackground : MonoBehaviour
{
    public float scrollSpeed;
    private Vector2 savedOffset;
    private Renderer renderer;

    void Start()
    {
        renderer = transform.GetComponent<Renderer>();
        savedOffset = renderer.sharedMaterial.GetTextureOffset("_MainTex");
    }

    void Update()
    {
        if(GameData.isGameOver)
        {
            return;
        }
        float x = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offset = new Vector2(x, savedOffset.y);
        renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

    void OnDisable()
    {
        renderer.sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
    }
}
