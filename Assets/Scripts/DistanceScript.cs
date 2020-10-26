using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DistanceScript : MonoBehaviour
{

    public GameObject player;
    public Canvas canvas;
    public TMPro.TextMeshProUGUI damageValueText;
    public int number;
    


    // Update is called once per frame
    public void Update()
    {  
        number = Mathf.RoundToInt(player.transform.position.x);
        damageValueText.text = number.ToString() + " m ";

        
        PlayerPrefs.SetFloat("DistanceX", number);
        PlayerPrefs.SetFloat("DistanceY", player.transform.position.y);

    }
}
