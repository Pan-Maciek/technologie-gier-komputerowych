using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float spawnTime;
    public float TimeToLive = 1;
    public Vector3 velocity;
    
    void Start() 
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), FindObjectOfType<Player>().GetComponent<Collider2D>());
        spawnTime = Time.time;
    }
    void FixedUpdate()
    {
        if (Time.time - spawnTime > TimeToLive) {
            Destroy(gameObject);       
        }
        transform.Translate(velocity);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name.Contains("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHP>().hpAmount--;
            Debug.Log(collision.gameObject.GetComponent<EnemyHP>().hpAmount);
            // Destroy(collision.gameObject);    
        }
        
        Destroy(gameObject);
    }
}
