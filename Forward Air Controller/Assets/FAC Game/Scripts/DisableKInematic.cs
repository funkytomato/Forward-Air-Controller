using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Missiles;

public class DisableKInematic : MonoBehaviour
{
    [Tooltip("The minimum value when to disable kinematic.")]
    public float switchOffValue = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Rigidbody>())
        {
            if (GetComponent<HitPoint>())
            {
                if (GetComponent<HitPoint>().hitPoint <= switchOffValue && GetComponent<Rigidbody>().isKinematic != false)
                {
                    GetComponent<Rigidbody>().isKinematic = false;
                }
            }
        }
    }
}
