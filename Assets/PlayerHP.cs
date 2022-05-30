using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public int maxHealth = 100;
    public int health = 100;
    public HealthBar healthBar;

    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            takeDamage(5);
        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        if(health <= 0)
        {
            this.death();
        }
    }

    void death()
    {
        //Destroy(this.gameObject);
        Debug.Log("Player DEATH");
    }

}
