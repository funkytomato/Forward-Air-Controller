﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateAircraftIdle : MonoBehaviour
{

    public GameObject character;

    SCAirCrafts_Actions actions;

    // Start is called before the first frame update
    void Start()
    {
        actions = character.GetComponent<SCAirCrafts_Actions>();

        actions.SendMessage("Idle", SendMessageOptions.DontRequireReceiver);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
