using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPScript : MonoBehaviour
{
    public enum TypeCharacter
    {
        Enemy,
        Player
    }
    public TypeCharacter typeChar;
    [SerializeField] private float hp;
    private UIController UICtrl;

    private void Start()
    {
        if (typeChar == TypeCharacter.Player) UICtrl = FindObjectOfType<UIController>();
    }

    public void TakeDamage(float damage)
    {
        if (hp - damage < 0) hp = 0;
        else hp -= damage;
        if(typeChar == TypeCharacter.Player)
        {
            UICtrl.UpdateHPBar(hp, false);
            SoundController.instance.Hurtlayer();
        }
        else
        {
            GetComponent<EnemyHpUI>().UpdateHPBar((int)hp);
        }
        if (hp==0)
        {
            if (typeChar == TypeCharacter.Player) gameObject.GetComponent<PlayerMovement>().Die();
            else Destroy(gameObject);
        }
    }

    public void TakeHeal(float heal)
    {
        hp += heal;
        if (hp > 100)
        {
            hp = 100;
        }
        UICtrl.UpdateHPBar(hp, true);
    }
}
