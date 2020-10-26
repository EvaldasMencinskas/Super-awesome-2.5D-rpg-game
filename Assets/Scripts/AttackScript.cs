using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{

    public PlayerStats player;
    
    private void Start()
    {
       // player = GetComponentInParent<PlayerStats>(); // PRINTS Strenght VALUE FROM PLAYER STATS SCRIPT
        //print("Player attack value" + player.Strenght);
    }

    //register attacks
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy") // colider has colided with an enemy object
        {
            other.gameObject.GetComponent<EnemyStats>().ApplyDamage(player.Strenght);
            //print("Atack has been registered");
        }
    }
}
