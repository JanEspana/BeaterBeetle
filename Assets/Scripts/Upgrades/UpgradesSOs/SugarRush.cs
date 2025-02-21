using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[CreateAssetMenu(fileName = "SugarRush", menuName = "Upgrades/SugarRush")]
public class SugarRush : UpgradeSO
{
    public int overallBoost = 1;
    public override void ApplyUpgrade()
    {
        GameManager.instance.player.GetComponent<Movement>().speed += overallBoost;
        GameManager.instance.player.GetComponentInChildren<Punch>().dmg += overallBoost;
        if (GameManager.instance.player.GetComponentInChildren<Player>().armor <= 15)
        {
            GameManager.instance.player.GetComponentInChildren<Player>().armor += overallBoost;
        }
        if  (GameManager.instance.player.GetComponent<AttackManager>().specialCooldown >= overallBoost / 2.5f)
        {
            GameManager.instance.player.GetComponent<AttackManager>().specialCooldown -= overallBoost / 2.5f;

        }
    }
}
