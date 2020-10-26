using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{

    private Animator animator;
    public PlayerStats playerStats;
    private EnemyController controller;
    public string hurtAnimationString,deathAnimationString;
    public Canvas canvas;
    public TMPro.TextMeshProUGUI damageValueText;

    //Health
    [SerializeField]
    int currentHealth, maxHealth = 100;
    public int Health
    {
        get { return currentHealth; }
        private set { currentHealth = value; }
    }

    //Strenght
    [SerializeField]
    int strenght = 10;
    public int Strenght
    {
        get { return strenght; }
        private set { strenght = value; }
    }

    //defence
    [SerializeField]
    int defence = 1;
    public int Defence
    {
        get { return defence; }
        private set { defence = value; }
    }

    //exp
    public float givingExp;
    public float Exp
    {
        get { return givingExp; }
        private set { givingExp = value; }
    }

    // Level
    [SerializeField]
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
        playerStats = GameObject.Find("main_character_knight").GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
        //ApplyLevel();
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("o"))
        {
            ApplyDamage(12);
        }

        if (Input.GetKeyDown("p"))
        {
            ApplyLevel(12);
        }
    }

    void Die()
    {
        animator.Play(deathAnimationString);
        Debug.Log("Enemy has died and gives exp: "+this.Exp);
        Collider2D[] colliders = this.GetComponents<Collider2D>();
        gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        Destroy(gameObject,5f);
        foreach (Collider2D collider in colliders) // will disable enemy colliders
        {
            collider.enabled = false;
        }
        GetComponent<EnemyController>().enabled = false; //stops enemy attacks and folowing
    }

    public void ApplyDamage(int dmg)
    {
        animator.Play("Hurt");
        dmg -= this.Defence;
        dmg = Mathf.Clamp(dmg, 1, int.MaxValue);
        // apply damage to health
        AffectHealth(-dmg);
        DisplayDamage(-dmg * 2);
        //Debug.Log("Enemies health is" + this.Health);
        if (currentHealth <= 0)
        {

            
            playerStats.ApplyExp(Exp);
            Die();
        }
           
    }

    void LevelUp()
    {
        this.Level += 1;
        this.Defence += 1;
        this.Strenght += 1;
        this.maxHealth += 10;
        this.currentHealth = maxHealth; // Returns full hp after lvlup
        //Debug.Log("Level up");
    }

    int Attack()
    {
        //Debug.Log("Enemy has attacked for " + this.Strenght + " Damage");
        return this.Strenght;
    }

    // aplly possitive or negative effect to hp
    void AffectHealth(int value)
    {
        currentHealth += value;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    void ApplyLevel(int value)
    {
        for(int i = 0; i < value; i++)
        {
            //Debug.Log("Level up now");
            LevelUp();
        }

    }

    void DisplayDamage(int value)
    {    // display damage
        TMPro.TextMeshProUGUI damageValueClone = Instantiate(damageValueText, (this.transform.position), Quaternion.identity);
        damageValueClone.text = value.ToString(); //convert to string cause text is string
        damageValueClone.transform.SetParent(canvas.transform, false); //sets clone as a child of canvas

    }
}
