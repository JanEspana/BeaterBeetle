using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public MenuManager menuManager;
    public GameObject playerHPBar, enemyHPBar;
    public List<GameObject> enemyPrefabs;
    public GameObject enemy, player;
    public Canvas statsMenu, hpBars, upgradeCanvas;
    public TextMeshProUGUI roundText;
    void Awake()
    {
        menuManager = GameObject.Find("GameManager").GetComponent<MenuManager>();
        //busca el objeto con tag upgradeCanvas y asignalo a upgradeCanvas
        upgradeCanvas = GameObject.FindGameObjectWithTag("UpgradeCanvas").GetComponent<Canvas>();
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHPBar.GetComponent<Slider>().value = playerHPBar.GetComponent<Slider>().maxValue;
        StartRound();
    }
    public void StartRound()
    {
        roundText.text = "Round " + menuManager.Round;
        statsMenu.GetComponent<Canvas>().enabled = false;
        upgradeCanvas.GetComponent<Canvas>().enabled = false;
        //elimina todo con tag enemy
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        if (menuManager.Round % 5 != 0)
        {
            //menuManager.sliderCanvas.GetComponent<Canvas>().enabled = true;
            enemyHPBar.SetActive(true);
            menuManager.hpBar.value = instance.player.GetComponent<Player>().HP / menuManager.maxHP;
            menuManager.UIhpBar.value = instance.player.GetComponent<Player>().HP / menuManager.maxHP;
            enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)]);
            enemy.GetComponent<Character>().slider = enemyHPBar;
            enemyHPBar.GetComponent<Slider>().value = enemyHPBar.GetComponent<Slider>().maxValue;
        }
        else
        {
            menuManager.foodRoundManager.StartFoodRound();
            //busca statscanvas
            GameObject.Find("StatsCanvas").GetComponent<Canvas>().enabled = false;
            enemyHPBar.SetActive(false);
            //menuManager.sliderCanvas.GetComponent<Canvas>().enabled = false;
        }
    }
    private void Update()
    {
        if (statsMenu.enabled || upgradeCanvas.enabled)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
