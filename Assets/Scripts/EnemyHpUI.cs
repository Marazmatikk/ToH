using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpUI : MonoBehaviour
{
    [SerializeField] private GameObject[] hpUI;

    public void UpdateHPBar(int current) 
    {
        Destroy(hpUI[current]);
        gameObject.AddComponent<EnemyVisualHurt>();
    }
}
