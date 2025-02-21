using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HighlyEvolved", menuName = "Upgrades/HighlyEvolved")]
public class HighlyEvolved : UpgradeSO
{
    public override void ApplyUpgrade()
    {
        MenuManager menuManager = GameManager.instance.menuManager;
        menuManager.statsGainedFromBuying += 1;
    }
}
