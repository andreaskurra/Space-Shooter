using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FloatingText : MonoBehaviour
{
    [SerializeField] float destroyTime = 3f;
    //public Vector3 Offset = new Vector3(0, 0.5f, 0);
    private void Start()
    {
        Destroy(gameObject, destroyTime);
        //transform.localPosition += Offset;
    }
}
