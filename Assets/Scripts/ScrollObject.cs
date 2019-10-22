using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObject : MonoBehaviour
{
    [SerializeField] private float speed = 5;

    void Start ()
    {
        speed *= GameData.speedMultiply;
	}

	void Update ()
    {
        Scroll();
    }

    private void Scroll()
    {
        if (!GameData.isGameOver) transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
