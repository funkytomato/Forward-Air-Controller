using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Missiles;

public class DisplayHitPoint : MonoBehaviour
{

    public Text textComponent;


    //public float CurrentDamage = 0f;

    private HitPoint hitPointObject;


    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<HitPoint>() != null)
        {
            hitPointObject = GetComponent<HitPoint>();
        }
        else
        {
            Debug.Log("DisplayHitPoint Start - HitPoint script not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //textComponent.text = CurrentDamage.ToString();
        textComponent.text = hitPointObject.hitPoint.ToString();

        //if (CurrentDamage < 0f)
        if (hitPointObject.hitPoint < 0f)
        {
            textComponent.color = Color.red;
        }
    }
}
