using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FlightKit;
using DigitalRuby.WeatherMaker;

public class RigidBodyApplyWindForce : MonoBehaviour
{
    public Transform obj;
    Rigidbody myRigidbody;
    GameObject weatherMakerObject;
    WeatherMakerScript weatherMakerScript;
    WeatherMakerWindScript weatherMakerWindScript;
    IWindManager windManager;

    public GameObject windObject;

    // Start is called before the first frame update
    void Start()
    {


        weatherMakerObject = GameObject.FindGameObjectWithTag("WeatherMaker");
        if (weatherMakerObject != null)
        {
            //Debug.Log("WeatherMaker: " + weatherMakerObject.ToString());

            weatherMakerScript = weatherMakerObject.gameObject.GetComponent<WeatherMakerScript>();
            //weatherMakerWindScript = weatherMakerObject.GetComponent<WeatherMakerWindScript>();
            windManager = weatherMakerScript.WindManager;
            weatherMakerWindScript = FindObjectOfType<WeatherMakerWindScript>();
            //WeatherMakerWindScript.Instance
            //Debug.Log("WeatherMaker instance:" + weatherMakerScript.ToString() + "instance: " + weatherMakerScript.InstanceClientId +
                        //"WeatherMakerWindScript: " + weatherMakerWindScript.ToString());
        }


        myRigidbody = GetComponent<Rigidbody>();


        //var userControl = FindObjectOfType<AirplaneUserControl>();
        //myRigidbody = userControl.GetComponent<Rigidbody>();



        //Debug.Log("WindZone: " + windZone.ToString());
        Debug.Log("Aircraft velocity: " + myRigidbody.velocity);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("WeatherMaker instance:" + weatherMakerScript.ToString() + "instance: " + weatherMakerScript.InstanceClientId +
        //"WeatherMakerWindScript: " + weatherMakerWindScript.ToString() + "WindManager: " + windManager.ToString());



        //Debug.Log("WindZone: " + weatherMakerScript.WindManager);
        //Debug.Log("Aircraft velocity: " + myRigidbody.velocity);

        //Get the windzone intensity
        //windScript = windManager

        //myRigidbody.AddForce(weatherMakerWindScript.CurrentWindVelocity);
        //weatherMakerScript = windObject.GetComponent<WeatherMakerScript>();
        //float windMain = weatherMakerScript.GetComponent<WindZone>().windMain;
        //Vector3 force = 
        //myRigidbody.AddForce();
    }
}
