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
    public float price = 100;
    public float statsGainedFromBuying = 1;
    public FoodRoundManager foodRoundManager;
    public GameObject sliderCanvas;
    public Player player;
    public void ActiveCanvas()
    {
        player = GameManager.instance.player.GetComponent<Player>();
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
        if (player.HP != maxHP && player.calories >= price)
        {
            if (player.HP + statsGainedFromBuying > maxHP)
            {
                player.HP = maxHP;
                UIhpBar.value = statsGainedFromBuying;
                hpBar.value = statsGainedFromBuying;
            }
            else
            {
                player.HP += statsGainedFromBuying;
                UIhpBar.value = player.HP / maxHP;
                hpBar.value = player.HP / maxHP;
            }
            player.calories -= price;
            calories.text = player.calories.ToString();
        }
    }
    public void UpgradeDamage()
    {
        if (player.calories >= price)
        {
            player.GetComponent<AttackManager>().leftPunch.GetComponent<Punch>().dmg += statsGainedFromBuying;
            player.GetComponent<AttackManager>().rightPunch.GetComponent<Punch>().dmg += statsGainedFromBuying;
            player.calories -= price;
            calories.text = player.calories.ToString();
        }
    }
    public void UpgradeSpeed()
    {
        if (player.calories >= price)
        {
            player.gameObject.GetComponent<Movement>().speed += statsGainedFromBuying;
            player.calories -= price;
            calories.text = player.calories.ToString();
        }
    }
    public void UpgradeCD()
    {
        if (player.calories >= price && player.GetComponent<AttackManager>().specialCooldown >= statsGainedFromBuying / 2.5f)
        {
            player.GetComponent<AttackManager>().specialCooldown -= statsGainedFromBuying/2.5f;
            player.calories -= price;
            calories.text = player.calories.ToString();
        }
    }
    public void UpgradeArmor()
    {
        if (player.calories >= price && player.armor <= 15)
        {
            player.armor += (int)statsGainedFromBuying;
            player.calories -= price;
            calories.text = player.calories.ToString();
        }
    }
}
