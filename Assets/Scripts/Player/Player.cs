using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    public Camera cam;
    public float calories;
    public float gainedCalories=100;
    public bool isBlocking;
    public int armor = 0;
    public DeathMenuManager DeathMenuManager;
    public override void CheckIfAlive(bool hasKnockback)
    {
        if (HP <= 0)
        {
            HP = 0;
            Die();
        }
        else
        {
            isInvincible = true;
            StartCoroutine(InvincibilityFrames());
        }
    }

    public override void Die()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<Movement>().enabled = false;
        gameObject.GetComponent<AttackManager>().enabled = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        cam.gameObject.GetComponent<CameraFollow>().enabled = false;

        Cursor.lockState = CursorLockMode.None;
        DeathMenuManager.activateCanvas();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isBlocking = true;
            gameObject.GetComponent<Movement>().speed=0;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isBlocking = false;
            gameObject.GetComponent<Movement>().speed = 10;
        }
    }
    IEnumerator InvincibilityFrames()
    {
        yield return new WaitForSeconds(0.5f);
        isInvincible = false;
    }
}
