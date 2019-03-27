using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Missiles;

public class HealthManager : MonoBehaviour
{

    public Text textComponent;


    static int Health = 100;

    private HitPoint _wingsHealth;
    private HitPoint _bodyHealth;
    private HitPoint _wheelsHealth;

    // Start is called before the first frame update
    void Start()
    {

        GameObject _wingsObject = GameObject.Find("Wings");
        if (_wingsObject != null)
        {
            _wingsHealth = _wingsObject.GetComponent<HitPoint>();
        }


        GameObject _wheelsObject = GameObject.Find("Legs");
        if (_wheelsObject != null)
        {
            _wheelsHealth = _wheelsObject.GetComponent<HitPoint>();
        }

        GameObject _bodyObject = GameObject.Find("Body");
        if (_bodyObject != null)
        {
            _bodyHealth = _bodyObject.GetComponent<HitPoint>();
        }

        Health = Mathf.RoundToInt(_wingsHealth.hitPoint + _wheelsHealth.hitPoint + _bodyHealth.hitPoint);
        UpdateHealth();    
    }

    // Update is called once per frame
    void Update()
    {

        //Health is body, wings and wheels total health
        Health = Mathf.RoundToInt(_wingsHealth.hitPoint + _wheelsHealth.hitPoint + _bodyHealth.hitPoint);


        textComponent.text = Health.ToString();

        if (Health < 0f)
        {
            textComponent.color = Color.red;
        }
    }

    public int GetHealth()
    {
        return Health;
    }

    void UpdateHealth()
    {
        textComponent.text = Health.ToString();
    }
}
