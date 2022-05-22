using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float secondsDelayBetweenSpawning;
    
    
    private float lastTime;
    // Start is called before the first frame update
    void Start()
    {
        lastTime = secondsDelayBetweenSpawning;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastTime > secondsDelayBetweenSpawning)
        {
            var enemy = Instantiate(enemyPrefab, new Vector3(0,0,0), Quaternion.Euler(0, 0, 0));
            lastTime = Time.time;
        }
    }
}
