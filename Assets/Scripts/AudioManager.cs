using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource musicSource;

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
}
