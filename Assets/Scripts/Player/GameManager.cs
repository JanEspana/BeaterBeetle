using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public MenuManager menuManager;
    public GameObject playerHPBar, enemyHPBar;
    public List<GameObject> enemyPrefabs;
    public GameObject enemy, player;
    public Canvas statsMenu, hpBars;
    void Awake()
    {
        menuManager = GameObject.Find("GameManager").GetComponent<MenuManager>();
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHPBar.GetComponent<Slider>().value = playerHPBar.GetComponent<Slider>().maxValue;
        StartRound();
    }
    public void StartRound()
    {
        statsMenu.GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        if (menuManager.Round % 5 != 0)
        {
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
            menuManager.sliderCanvas.GetComponent<Canvas>().enabled = false;
        }
    }
    private void Update()
    {
        if (statsMenu.enabled)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
