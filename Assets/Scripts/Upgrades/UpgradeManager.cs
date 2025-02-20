using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;
    public List<UpgradeSO> upgrades = new List<UpgradeSO>();
    public GameObject player;
    public MenuManager menuManager;

    private void Start()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        menuManager = GameObject.Find("GameManager").GetComponent<MenuManager>();

    }
}
