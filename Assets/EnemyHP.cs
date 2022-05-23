using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public float hpAmount;

    public void takeDamage(float dmgAmount)
    {
		hpAmount -= dmgAmount;
        if (hpAmount <= 0)
        {
            Destroy(this.gameObject);
        }
    }

	
}
