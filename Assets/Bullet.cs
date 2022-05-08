using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float spawnTime;
    public float TimeToLive = 1;
    void Start() 
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), FindObjectOfType<Player>().GetComponent<Collider2D>());
        spawnTime = Time.time;
    }
    void Update()
    {
        if (Time.time - spawnTime > TimeToLive) {
            Destroy(gameObject);       
        }
    }
    void OnCollisionEnter2D(Collision2D collision){
        Destroy(gameObject);
    }
}
