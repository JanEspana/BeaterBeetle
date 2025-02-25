using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DieSO", menuName = "States/DieSO")]
public class DieSO : StatesSO
{
    public override void OnStateEnter(EnemyController ec)
    {
        /*if (ec.GetComponent<Player>() != null)
        {
            ec.target.GetComponent<Player>().calories += ec.target.GetComponent<Player>().gainedCalories;

        }*/

        ec.StartCoroutine(FuckingDies(ec));
    }
    public override void OnStateExit(EnemyController ec)
    {
    }

    public override void OnStateUpdate(EnemyController ec)
    {
    }
    IEnumerator FuckingDies(EnemyController ec)
    {
        yield return new WaitForSeconds(2);
        if (!ec.isAnt)
        {
            GameManager.instance.player.GetComponent<Player>().calories += GameManager.instance.player.GetComponent<Player>().gainedCalories;

            GameManager.instance.menuManager.ActiveCanvas();
        }
        Destroy(ec.gameObject);
    }
}
