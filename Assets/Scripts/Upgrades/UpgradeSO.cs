using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrades/Upgrade")]
public abstract class UpgradeSO : ScriptableObject
{
    public Sprite icon;
    public string upgradeName;
    public string description;

    public abstract void ApplyUpgrade();
}
