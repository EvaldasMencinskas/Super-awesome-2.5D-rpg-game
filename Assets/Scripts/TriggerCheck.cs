using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    public GameObject enemy;
    private const float DistanceFromEnemy = 1f;
    public GameObject player;
    private EnemyController Enemycontroller;
    void Start()
    {
        // Gets enemy controler script
        Enemycontroller = transform.root.GetComponent<EnemyController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //IF ENEMY COLIDES WITH PLAYER IT WILL FOLOWHIM FOREVER
    // USES SEPERATE GAME OBJECT WITH COLLIDER 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Vector3.Distance(player.transform.position, enemy.transform.position) < DistanceFromEnemy)
        {
            //spawn another level part
            Enemycontroller.attack = true;
        }


    }

}

