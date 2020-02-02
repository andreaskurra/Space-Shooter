using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] int health = 200;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] List<GameObject> addOns;
    [SerializeField] float addOnFallSpeed = 1.5f;
    private void OnTriggerEnter2D(Collider2D other)
    {

        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {   

       

        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        var addOnPower = addOns[0];
        //Debug.Log(addOns.Count);
        GameObject addOn = Instantiate(addOnPower, transform.position, Quaternion.identity);
        addOn.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -addOnFallSpeed);
        Destroy(explosion, durationOfExplosion);

    }
}
