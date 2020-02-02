using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOn : MonoBehaviour
{
    [SerializeField] float PlayerShootRateAdd = 0.05f;
    [SerializeField] float PlayerShootRateMax = 0.04f;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        //Debug.Log(FindObjectOfType<Player>().ProjectileFiringPeriod);
        if (FindObjectOfType<Player>().ProjectileFiringPeriod >= PlayerShootRateMax)
        {
            FindObjectOfType<Player>().ProjectileFiringPeriod -= PlayerShootRateAdd;
        }
    }
}
