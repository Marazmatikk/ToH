using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour {
    private UIController UICtrl;
    private HPScript hpScript;
    private PlayerMovement playerMovement;
    [Header("проверяемый слой для магии земли")] [SerializeField] private LayerMask layer_Ground;
    [SerializeField] private Transform earthSpawnPoint;
    [SerializeField] private Transform fireStartPoint;
    [SerializeField] private GameObject earth;
    [SerializeField] private GameObject fire;
    [SerializeField] private float timeEarthSkillKD;
    private float timeEarthSkill;
    [SerializeField] private float timeHealSkillKD;
    private float timeHealSkill;
    [SerializeField] private float healPoints;
    [SerializeField] private float multiplyRegenFire;
    [SerializeField] private float firePoints;
    private float curFirePoints;
    [SerializeField] private float FP_perAttack;
    private bool canDoEarthSkill = false;

    void Start ()
    {
        curFirePoints = firePoints;
        timeEarthSkill = timeEarthSkillKD;
        timeHealSkill = timeHealSkillKD;
        hpScript = GetComponent<HPScript>();
        UICtrl = FindObjectOfType<UIController>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        TimerSkills();
        Check_EarthSkill();
    }

    private void Check_EarthSkill()
    {
        if(timeEarthSkill<=0 && playerMovement.isGrounded)
        {
            Collider2D[] grounds = Physics2D.OverlapBoxAll(earthSpawnPoint.position, new Vector2(0.1f, 0.2f), 0, layer_Ground);
            if (grounds != null) canDoEarthSkill = true;
        }
        else canDoEarthSkill = false;
    }

    public void Earth_Skill()
    {
        if (!GameData.isGameOver)
        {
            if (canDoEarthSkill)
            {
                Instantiate(earth, earthSpawnPoint.position, Quaternion.identity);
                timeEarthSkill = timeEarthSkillKD;
            }
        }
    }


    public void FireSkill()
    {
        if (!GameData.isGameOver)
        {
            if (curFirePoints > FP_perAttack)
            {
                SoundController.instance.PlayerFireAttack();
                curFirePoints -= FP_perAttack;
                Instantiate(fire, fireStartPoint.position, Quaternion.identity);
                UICtrl.UpdateFireBar(curFirePoints / firePoints, curFirePoints / FP_perAttack);
            }
        }
    }

    public void HealSkill()
    {
        if (!GameData.isGameOver)
        {
            if (timeHealSkill<=0)
            {
                hpScript.TakeHeal(healPoints);
                timeHealSkill = timeHealSkillKD;
                SoundController.instance.HealSound();
            }
        }
    }

    private void TimerSkills()
    {
        if(!GameData.isGameOver)
        {
            if (timeEarthSkill > 0)
            {
                timeEarthSkill -= Time.deltaTime;
                UICtrl.UpdateEarthBtn(timeEarthSkill / timeEarthSkillKD, canDoEarthSkill);
            }
            else UICtrl.UpdateEarthBtn(timeEarthSkill / timeEarthSkillKD, canDoEarthSkill);
            if (curFirePoints<1)
            {
                curFirePoints += Time.deltaTime * multiplyRegenFire;
                UICtrl.UpdateFireBar(curFirePoints/firePoints, curFirePoints/FP_perAttack);
            }
            if(timeHealSkill>0)
            {
                timeHealSkill -= Time.deltaTime;
                UICtrl.UpdateHealBtn(timeHealSkill/ timeHealSkillKD);
            }
        }
    }
}
