using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InheritParent : MonoBehaviour
{

    public Transform newParent;

    // Start is called before the first frame update
    void Start()
    {
        //Sets "newParent" as the new parent of the player GameObject.
       //transform.SetParent(newParent);

        //Same as above, except this makes the player keep its local orientation rather than its global orientation.
        //player.transform.SetParent(Parent, false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("InheritParent Update - transform rotation: " + transform.rotation + "newParent rotation: " + newParent.rotation);
        transform.rotation = newParent.rotation;

        Debug.Log("InheritParent Update - transform position: " + transform.position + "newParent position: " + newParent.position);

        //transform.position = newParent.position;   

        SetParent(newParent);  
    }

    // Set the GameObject's parent
    void SetParent(Transform newParent)
    {
        //Sets "newParent" as the new parent of the player GameObject.
        //transform.SetParent(newParent);

        //Same as above, except this makes the player keep its local orientation rather than its global orientation.
        transform.SetParent(newParent, false);
    }
}
