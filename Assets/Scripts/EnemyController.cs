using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private Animator animator; // CALLS ANIMATOR
    private Rigidbody2D enemyRigidBody; //CALLS ENEMY RIGIDBODY
    private EnemyStats stats;
    public Transform Enemy;
    public Transform Player;
    public GameObject player;
    [SerializeField] private float range = 10.0f;
    public float m_Speed; // MOVEMENT SPEED
    public bool attack = false; // ATTACKS IF TRUE

    public string attackAnimationString; // PLACE ENEMY ATTACK ANIMATION
    public string movementAnimationString; //PLACE ENEMY RUN ANIMATION
    public bool goingLeft, cameIntoContactWithPlayer; //fOLOW PLAYERS DIRECTION


    private void Awake()
    {
        Enemy = this.transform;
        Player = Player.transform;
    }

    private float Distance()
    {
        return Vector3.Distance(Enemy.position, Player.position);
    }



    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        stats = GetComponent<EnemyStats>();
        animator = GetComponent<Animator>();
        enemyRigidBody = GetComponent<Rigidbody2D>();
        if (!goingLeft) //FLIPS ENEMY SPRITE AND MOVEMENT DIRETION IF NOT GOING LEFT
            Flip(); // FLIP FUNCTION CALLED

        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (Player)
            print(Player.name + " is " + Distance().ToString() + " units from " + Enemy.name);

        else
            print("Player not found!");
     
        // IF ENEMY CAME INTO CONTACT WITH A PLAYERS RIGID BODY IT WILL FLIP AND FOLOW IN ANY DIRECTION
        if (cameIntoContactWithPlayer)
        {
            if (player.transform.position.x > this.transform.position.x && goingLeft)
            {
                Flip();
                goingLeft = false;
            }
            if (player.transform.position.x < this.transform.position.x && !goingLeft)
            {
                Flip();
                goingLeft = true;
            }
        }
    }

    // move to player
    void FixedUpdate()
    {
        if (!attack) 
        {
            // IF NOT ATTACKING TURN ON RUN ANIMATION ,WITH SET SPEED 
            //print("Finding player");
            animator.SetTrigger(movementAnimationString);
            SetMovementSpeed(m_Speed);
            animator.SetBool("IsAttacking", attack);
            
        }
        // enemy is next to player and should attack BY ACTIVATING ATTACK ANIMATION
        else
        {
            SetMovementSpeed(0.0f);
            animator.SetTrigger(attackAnimationString);
        }

    }

    void OnTriggerEnter2D(Collider2D other) // enemy comes into contact
    {
        
            if (other.gameObject.tag == "Player")
        {
            cameIntoContactWithPlayer = true;
            //Debug.Log("hit the player");
            attack = true;
            animator.SetBool("IsAttacking", attack);
        }
    }

    void OnTriggerExit2D(Collider2D other) // enemy is not in contact
    {   // colision lost
        if (other.gameObject.tag == "Player")
        {
            attack = false;
            //Debug.Log("found the player");
            animator.SetBool("IsAttacking", attack);
        }
    }


    // MOVEMENT SPEED FUNCTION
    void SetMovementSpeed(float speed)
    {
        Vector2 enemyVol = enemyRigidBody.velocity;
        enemyVol.x = goingLeft ? -speed : speed;
        enemyRigidBody.velocity = enemyVol;
        /*enemyVol.x = speed;
        if (goingLeft)
        {
            enemyRigidBody.velocity = -enemyVol;
        }
        else
        {
            enemyRigidBody.velocity = enemyVol;
        }*/
    }


    // FLIP ENEMY FUNCTION
    private void Flip()
    {
        // multiply players x local scale by -1
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    // controled though animation events
    public void IsWeaponActive(int value)
    {
        if (value == 1) //weapon active
        {
            player.GetComponent<PlayerStats>().ApplyDamage(stats.Strenght);
        }
        
    }

}
