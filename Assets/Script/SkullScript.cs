using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullScript : MonoBehaviour
{
    float destroyTime = 10f;
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
 

 
}
