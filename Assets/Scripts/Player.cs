using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [Header ("Player Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.7f;
    [SerializeField] public int health = 200;
    [SerializeField] public int maxHealth = 300;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0, 1)] float shootSoundVol = 0.25f; 
    [SerializeField] [Range(0, 1)] float deathVolume = 0.7f;
    [SerializeField] float fingerPadding = 2f;
    [Header ("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] public float ProjectileFiringPeriod = 0.5f;
    [Header("Getting Shot")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] float timeToColor = 1f;
    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    SpriteRenderer sr;
    Color defaultColor;

    

    public Color getColor()
    {
        return GetComponent<SpriteRenderer>().color;
    }

    public float getTimeToColor()
    {
        return timeToColor;
    }

    void Start()
    {
      
        SetUpBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        CheckDamageVFX();
    }

    private void CheckDamageVFX()
    {
        float h = health * 100 / maxHealth;
        if (h < 100f && h >= 50f)
        {
            Transform sd = transform.Find("Small Damage");
            Transform dd = transform.Find("Damage Dust");
            Transform df = transform.Find("Damage Fire");
            sd.GetComponent<SpriteRenderer>().enabled = true;
            dd.gameObject.SetActive(true);
            df.gameObject.SetActive(false);
        }
        else if (h >= 0 && h <= 33f )
        {
            Transform bd = transform.Find("Big Damage");
            Transform df = transform.Find("Damage Fire");
            Transform dd = transform.Find("Damage Dust");
            dd.gameObject.SetActive(false);
            df.gameObject.SetActive(true);
            bd.GetComponent<SpriteRenderer>().enabled = true;

        }
        
        else
        {
            Transform sd = transform.Find("Small Damage");
            Transform bd = transform.Find("Big Damage");
            Transform dd = transform.Find("Damage Dust");
            Transform df = transform.Find("Damage Fire");
            sd.GetComponent<SpriteRenderer>().enabled = false;
            bd.GetComponent<SpriteRenderer>().enabled = false;
            dd.gameObject.SetActive(false);
            df.gameObject.SetActive(false);
        }
        
         
    
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
       
        if (health > 0)
        {
            StartCoroutine(SwitchColor());
            
        }
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {   
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
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
        if (Input.GetMouseButton(0))
        {
            var pos = Input.mousePosition;
            pos = Camera.main.ScreenToWorldPoint(pos);
            pos.x = Mathf.Clamp(pos.x, xMin, xMax); 
            pos.y = Mathf.Clamp(pos.y + fingerPadding, yMin, yMax);
            transform.position = Vector2.MoveTowards(transform.position, pos, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector2(newXPos, newYPos);
        }

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
    IEnumerator SwitchColor()
    {
       
        sr = GetComponent<SpriteRenderer>();
        defaultColor = sr.color;
        sr.color = new Color(1f, 0.4858491f, 0.4858491f);
        yield return new WaitForSeconds(timeToColor);
        sr.color = defaultColor;

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

    public void SavePlayer()
    {
        SystemSave.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SystemSave.LoadPlayer();

        health = data.health;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;

    }

}
