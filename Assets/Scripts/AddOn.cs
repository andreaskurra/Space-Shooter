using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AddOn : MonoBehaviour
{
    [SerializeField] float PlayerShootRateAdd = 0.05f;
    [SerializeField] float PlayerShootRateMax = 0.15f;
    [SerializeField] GameObject playerLaser;
    float timeToColor;
    string addOnTag;

    SpriteRenderer sr;
    Color defaultColor;

    void Start()
    {
        
    }
    private void Update()
    {
        //Debug.Log(addOnTag);
        timeToColor = FindObjectOfType<Player>().getTimeToColor();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger)
        {
            addOnTag = this.tag;
            if (addOnTag == "Attack Speed")
            {
                if (FindObjectOfType<Player>().ProjectileFiringPeriod >= PlayerShootRateMax)
                {
                    FindObjectOfType<Player>().ProjectileFiringPeriod -= PlayerShootRateAdd;
                    sr = FindObjectOfType<Player>().GetComponent<SpriteRenderer>();
                    object[] parms = new object[4] { 0.5184412f, 0.6226415f, 0f, sr }; //yellowish color
                    StartCoroutine(ColorSwitch(parms));
                }
            }
            else if (addOnTag == "Health Restore")
            {
                if (FindObjectOfType<Player>().health < 300)
                {
                    FindObjectOfType<Player>().health += 100;
                    sr = FindObjectOfType<Player>().GetComponent<SpriteRenderer>();
                    object[] parms = new object[4] { 0.2053667f, 0.6132076f, 0.2076596f, sr }; //green color
                    StartCoroutine(ColorSwitch(parms));

                }


            }
        }
       // this.GetComponent<SpriteRenderer>().enabled = false; // Make the game object invisible -- working but it bugs.
        
    }
    private IEnumerator ColorSwitch(object[] parms)
    {   
        float r = (float)parms[0];
        float g = (float)parms[1];
        float b = (float)parms[2];
        SpriteRenderer sr = (SpriteRenderer)parms[3];
        defaultColor = sr.color;
        sr.color = new Color(r, g, b);
        yield return new WaitForSeconds(timeToColor);
        sr.color = defaultColor;
        Destroy(gameObject);
    }
}
