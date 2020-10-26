using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{

    public PlayerStats player;
    public Image expFillImage;
    public TMPro.TextMeshProUGUI levelValueText;

    private float expAmount;


    // Update is called once per frame
    void Update()
    {
        expAmount = player.exp;
        expFillImage.fillAmount = expAmount / player.maxExp;
        //print(player.Level);
        levelValueText.text = "LVL " + player.Level.ToString();
    }
}
