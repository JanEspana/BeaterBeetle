using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource musicSource;

    [Header ("Audio Clips")]
    public AudioClip waspDeath;
    public AudioClip waspFly;
    public AudioClip waspHit;
    public AudioClip waspMiss;
    public AudioClip bomberAttack;
    public AudioClip bomberCharge;
    public AudioClip bomberDeath;
    public AudioClip bomberWalk;
    public AudioClip mantisAttack;
    public AudioClip mantisDeath;
    public AudioClip mantisWalk;
    public AudioClip mantisHarakiri;
    public AudioClip grasshopperAtkHit;
    public AudioClip grasshopperAtkMiss;
    public AudioClip grasshopperDeath;
    public AudioClip grasshopperJump;
    public AudioClip grasshopperKick;
    public AudioClip grasshopperKickHit;
    public AudioClip beetleDash;
    public AudioClip beetleDeath;
    public AudioClip beetleHornHit;
    public AudioClip beetleHorn;
    public AudioClip beetlePunch1Hit;
    public AudioClip beetlePunch2Hit;
    public AudioClip beetlePunch1;
    public AudioClip beetlePunch2;
    public AudioClip beetleWalk;

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
