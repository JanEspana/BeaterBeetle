using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggScript : Character
{
    public GameObject ant;
    public int AntSpawnAmount;

    public override void CheckIfAlive(bool hasKnockback)
    {
        if (HP <= 0)
        {
            Debug.Log("Egg Destroyed");
            Destroy(gameObject);
        }
    }

    public override void Die()
    {
    }

    private void Awake()
    {
        StartCoroutine(StartEgg());
    }

    private IEnumerator StartEgg()
    {
        yield return new WaitForSeconds(3);
        //haz un bucle 
        for (int i = 0; i < AntSpawnAmount; i++)
        {
            //instancia ant en la posicion del huevo
            Instantiate(ant, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
