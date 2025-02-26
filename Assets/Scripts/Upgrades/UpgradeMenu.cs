using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    public UpgradeSO upgrade;
    public MenuManager menuManager;
    public UpgradeSO leftoverUpgradeSO;

    public void SelectRandomUpgrade()
    {
        if (UpgradeManager.instance.upgrades.Count != 0)
        {
            upgrade = UpgradeManager.instance.upgrades[Random.Range(0, UpgradeManager.instance.upgrades.Count)];
            UpgradeManager.instance.upgrades.Remove(upgrade);
            gameObject.GetComponentsInChildren<Image>()[1].sprite = upgrade.icon;
            gameObject.GetComponentsInChildren<TextMeshProUGUI>()[1].text = upgrade.description;
            //gameObject.GetComponent<Image>().sprite = upgrade.icon;
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = upgrade.upgradeName;
        }
        else
        {
            //pone una upgrade generica por si no hay para elegir
            upgrade = leftoverUpgradeSO;
            gameObject.GetComponentsInChildren<Image>()[1].sprite = upgrade.icon;
            gameObject.GetComponentsInChildren<TextMeshProUGUI>()[1].text = upgrade.description;
            //gameObject.GetComponent<Image>().sprite = upgrade.icon;
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = upgrade.upgradeName;
        }

    }
    public void ApplyUpgrade()
    {
        upgrade.ApplyUpgrade();
        menuManager.ActiveCanvas();

    }
}
