using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetation : MonoBehaviour
{
    [SerializeField] private float delayDestroy;

    private void Start()
    {
        StartCoroutine(DestroyVegetation(delayDestroy));
    }

    IEnumerator DestroyVegetation(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if(!GameData.isGameOver) Destroy(gameObject);
    }
}
