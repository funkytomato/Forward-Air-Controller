using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyForwardForceScript : MonoBehaviour
{

    public float ForceStrength = 1.0f;
    Transform direction = null;

    // Start is called before the first frame update
    void Start()
    {
        direction = transform;    
    }

    // Update is called once per frame
    void Update()
    {
        if (this.direction != null)
        {
            this.GetComponent<Rigidbody>().AddForce(direction.forward * ForceStrength);
        }
    }
}
