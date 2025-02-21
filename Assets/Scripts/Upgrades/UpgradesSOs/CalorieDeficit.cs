using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CalorieDeficit", menuName = "Upgrades/CalorieDeficit")]
public class CalorieDeficit : UpgradeSO
{
    public override void ApplyUpgrade()
    {
        MenuManager menuManager = GameManager.instance.menuManager;
        menuManager.price = 60;
        menuManager.hpPrice = 40;
    }
}
