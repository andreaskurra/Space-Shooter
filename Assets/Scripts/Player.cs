using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [Header ("Player Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0, 1)] float shootSoundVol = 0.25f; 
    [SerializeField] [Range(0, 1)] float deathVolume = 0.7f;


    [Header ("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] public float ProjectileFiringPeriod = 0.5f;
    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    void Start()
    {
        SetUpBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

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
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathVolume);
        FindObjectOfType<Level>().LoadGameOver();

    }

    public int GetHealth()
    {
        return health;
    }
    private void SetUpBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }
    private void Move()
    {
        var deltaX = Input.GetAxis("Mouse X");
        var deltaY = Input.GetAxis("Mouse Y");
        //var deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        //var deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        deltaX *= Time.deltaTime;
        deltaX *= moveSpeed;
        deltaY *= Time.deltaTime;
        deltaY *= moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
        //transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition.x), Camera.main.ScreenToWorldPoint(Input.mousePosition.y));

    }

    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }

        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }
    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSoundVol);
            yield return new WaitForSeconds(ProjectileFiringPeriod);
        }
    }
}
