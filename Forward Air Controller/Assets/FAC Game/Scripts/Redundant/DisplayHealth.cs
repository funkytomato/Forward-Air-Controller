using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Missiles;

public class DisplayHealth : MonoBehaviour
{

    public Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = GetComponent<HitPoint>().hitPoint.ToString(); 
    }
}
