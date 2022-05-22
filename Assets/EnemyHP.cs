using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int hpAmount;

    void Update()
    {
        if (hpAmount <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
