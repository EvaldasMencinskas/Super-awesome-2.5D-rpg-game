using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    float playerHealthvalue;
    public PlayerStats player;
    public Image healthFillImage;
    public GameObject heartIcon;
    public TMPro.TextMeshProUGUI healthValueText;
    public Gradient gradient; //(requires slider component to be used) ability to adjust bar collour depending on hp amount

    void Start()
    {  //disable heart animation on start
        heartIcon.GetComponent<Animator>().enabled = false;
        healthFillImage.color = gradient.Evaluate(1f);// ability to adjust bar collour depending on hp amount
    }

    void Update()
    {
        //curent health of the player
        playerHealthvalue = player.Health;
        healthValueText.text = "HP " + playerHealthvalue.ToString();
        healthFillImage.fillAmount = playerHealthvalue / player.MaxHealth;
        // how fill amount works,maxhealth == 100,current health == 50,50/ 100 = 0.5
        //print("Health fill image Value: " + playerHealthvalue / player.maxHealth + "Players current health:" + player.Health +
        //    " Players maxhealth" + player.maxHealth);
        healthFillImage.color = gradient.Evaluate(healthFillImage.fillAmount);// ability to adjust bar collour depending on hp amount
        float Percentage = 25f / 100f * player.MaxHealth;
        if (player.Health <= Percentage)
        {
            heartIcon.GetComponent<Animator>().enabled = true;
        }
        else
        {
            heartIcon.GetComponent<Animator>().enabled = false;

        }
        
    }

}
