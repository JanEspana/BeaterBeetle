using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SugarRush", menuName = "Upgrades/SugarRush")]
public class SugarRush : UpgradeSO
{
    public int overallBoost = 1;
    public override void ApplyUpgrade()
    {
        GameManager.instance.player.GetComponent<Movement>().speed += overallBoost;
        GameManager.instance.player.GetComponentInChildren<Punch>().dmg += overallBoost;
        //implementar CD
        //implementar armadura
    }
}
