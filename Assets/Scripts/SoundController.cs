using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource clickBtnSound;
    [SerializeField] private AudioSource playBtnSound;
    [SerializeField] private AudioSource playerFireAttack;
    [SerializeField] private AudioSource enemyFireAttack;
    [SerializeField] private AudioSource enemyPunch;
    [SerializeField] private AudioSource jupmPlayer;
    [SerializeField] private AudioSource hurtPlayer;
    [SerializeField] private AudioSource explosionFire;
    [SerializeField] private AudioSource healSound;
    [SerializeField] private AudioSource explosionEarth;

    public static SoundController instance = new SoundController();

    private void Start()
    {
        instance = this;
    }

    public void PlayBtnSound()
    {
        playBtnSound.Play();
    }

    public void PlayerFireAttack()
    {
        playerFireAttack.pitch = Random.Range(0.8f, 1.2f);
        playerFireAttack.Play();
    }

    public void EnemieFireAttack()
    {
        enemyFireAttack.pitch = Random.Range(0.8f, 1.2f);
        enemyFireAttack.Play();
    }

    public void EnemyPunch()
    {
        enemyPunch.pitch = Random.Range(0.8f, 1.2f);
        enemyPunch.Play();
    }

    public void JupmPlayer()
    {
        jupmPlayer.pitch = Random.Range(0.9f, 1.1f);
        jupmPlayer.Play();
    }

    public void Hurtlayer()
    {
        hurtPlayer.pitch = Random.Range(0.8f, 1.1f);
        hurtPlayer.Play();
    }

    public void HealSound()
    {
        healSound.pitch = Random.Range(0.8f, 1.1f);
        healSound.Play();
    }

    public void ExplosionEarth()
    {
        explosionEarth.pitch = Random.Range(0.8f, 1.1f);
        explosionEarth.PlayOneShot(explosionEarth.clip);
    }

    public void ExplosionFire()
    {
        explosionFire.pitch = Random.Range(0.9f, 1.2f);
        explosionFire.PlayOneShot(explosionFire.clip);
    }

    public void ClickBtnSound()
    {
        clickBtnSound.Play();
    }
}
