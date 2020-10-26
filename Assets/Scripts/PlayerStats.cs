using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

     Animator animator;
    public BoxCollider2D weapon;
    public GameObject playerLevelupParticles;
    public GameObject player;
    // current and max Health
    [SerializeField]
    public float currentHealth;
    public float Health
    {
        get { return currentHealth; }
         set{currentHealth = value;}
    }
    //Max Health
    [SerializeField]
    public float maxHealth = 100;
    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }
        //Strenght = atatack value
        [SerializeField]
    int  strenght= 10;
    public int Strenght
    {
        get { return strenght; }
        private set { strenght = value; }
    }

    //defence - strenght = attack value
    [SerializeField]
    int defence = 1;
    public int Defence
    {
        get { return defence; }
        private set { defence = value; }
    }

    //exp gained from enemies and max exp required to lvl up
    public float exp, maxExp = 100f;
    

    //Player Level
    int level = 1;
    public int Level
    {
        get { return level; }
        private set { level = value; }
    }
        
    void Awake()

    {

        currentHealth = maxHealth;

        
    }

    // Start is called before the first frame update
    void Start()
    {
        exp = PlayerPrefs.GetFloat("exp", exp);
        maxExp = PlayerPrefs.GetFloat("maxExp", maxExp);
        currentHealth = PlayerPrefs.GetFloat("currentHealth", currentHealth);
        maxHealth = PlayerPrefs.GetFloat("maxHealth", maxHealth);
        Level = PlayerPrefs.GetInt("Level", Level);
        strenght = PlayerPrefs.GetInt("strenght", strenght);
        defence = PlayerPrefs.GetInt("defence", defence);
        // weapon is inactive on start
        IsWeaponActive(0);
        animator = GetComponent<Animator>();
        playerLevelupParticles.SetActive(false);

    }
    
    

    // Update is called once per frame
    void Update()
    {
        // testing if player can recieve dmg
        if (Input.GetKeyDown("h"))
        {
            ApplyDamage(12);
        }
        //testing if player can gain exp
        if (Input.GetKeyDown("e"))
        {
            ApplyExp(3000);
        }

        //Saves stats
        
        PlayerPrefs.SetInt("Level", Level);
        PlayerPrefs.SetInt("strenght", strenght);
        PlayerPrefs.SetInt("defence", defence);
        PlayerPrefs.SetFloat("maxExp", maxExp);
        PlayerPrefs.SetFloat("exp", exp);
        PlayerPrefs.SetFloat("currentHealth", currentHealth);
        PlayerPrefs.SetFloat("maxHealth", maxHealth);

    }

    void Die()
    {
        //Debug.Log("Player has died");
        animator.SetBool("Death Bool", true);
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<CharacterController2D>().enabled = false;
        
        

    }

    public void ApplyDamage(int dmg)
    {
        animator.SetTrigger("Hurt Trigger"); // if player recieves damage play hurt animation
        dmg -= this.Defence; //defence 1 point == -1 point to recieved damage
        dmg = Mathf.Clamp(dmg, 1, int.MaxValue);
        // apply damage to health
        AffectHealth(-dmg); // damage value recieved takes away same amount of health
        //Debug.Log("player health is" + this.Health);
        if (currentHealth <= 0) // if health is 0 or less run diefunction 
        {
            Die();
        }
            
    }

    //lvlup function adds atributes each time it  is activated and returns max health
    /*void LevelUp()
    {
        this.Level += 1; // raises lvl by 1 each time 100 exp iscolected by killing enemies or by pressing e key
        this.Defence += 1;
        this.Strenght += 1;
        this.maxHealth += 10;
        this.currentHealth = maxHealth; // Returns full hp after lvlup
        //Debug.Log("Level up");
    }*/


    // shows players strenght - enemy defense value value
    int Attack()
    {
        //Debug.Log("Player has attacked for " + this.Strenght + " Damage");
        return this.Strenght;
    }

    // aplly possitive or negative effect to hp
    void AffectHealth(int value)
    {
        currentHealth += value;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }
    

    // gain expierience
    public void ApplyExp(float value)
    {
        exp += value;
        while(exp >= maxExp) //if exp is more or equal to max exp initiate levelup function
        {
            maxExp += Level * 10 ;
            StartCoroutine("PlayParticleLevelUpSystem");
            //LevelUp();
            exp = exp - maxExp; // exp after lvl upwill be exp - max exp 
            Level++;
            Defence += 1;
            Strenght += 1;
            maxHealth += 10;
            currentHealth = maxHealth;
            
        }
        
        //print("current level" + Level + "current exp" + Exp + "current exp" + maxExp);
       
    }

    // controled though animation events
    public void IsWeaponActive(int value)
    {
        if(value == 1) //weapon active
        {
            weapon.enabled = true; 
        }
        else
        {
            weapon.enabled = false; //weapon inactive
        }
    }

    IEnumerator PlayParticleLevelUpSystem()
    {
        playerLevelupParticles.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        playerLevelupParticles.SetActive(false);
    }

}
