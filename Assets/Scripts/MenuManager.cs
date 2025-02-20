using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Slider hpBar, UIhpBar;
    public TextMeshProUGUI calories;
    public float maxHP = 10;
    public float healedHP = 3;
    public int Round = 1;
    public FoodRoundManager foodRoundManager;
    public GameObject sliderCanvas;
    public void ActiveCanvas()
    {
        Player player = GameManager.instance.player.GetComponent<Player>();
        GameManager.instance.statsMenu.enabled = true;
        Time.timeScale = 0;
        if (player.HP + healedHP > maxHP)
        {
            player.HP = maxHP;
        }
        else
        {
            player.HP += healedHP;
        }
        hpBar.value = GameManager.instance.player.GetComponent<Player>().HP/ maxHP;
        UIhpBar.value = GameManager.instance.player.GetComponent<Player>().HP / maxHP;
        calories.text = GameManager.instance.player.GetComponent<Player>().calories.ToString();
    }
    public void NextBattle()
    {
        Round++;
        GameManager.instance.StartRound();
        GameManager.instance.player.gameObject.transform.position = new Vector3(0, 2, 0);
        GameManager.instance.player.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

    }
    public void UpgradeHP()
    {
        Player player = GameManager.instance.player.GetComponent<Player>();
        if (player.HP != maxHP && player.calories >= 100)
        {
            if (player.HP + 1 > maxHP)
            {
                player.HP = maxHP;
                UIhpBar.value = 1;
                hpBar.value = 1;
            }
            else
            {
                player.HP += 1;
                UIhpBar.value = player.HP / maxHP;
                hpBar.value = player.HP / maxHP;
            }
            player.calories -= 100;
            calories.text = player.calories.ToString();
        }
        
    }
}
