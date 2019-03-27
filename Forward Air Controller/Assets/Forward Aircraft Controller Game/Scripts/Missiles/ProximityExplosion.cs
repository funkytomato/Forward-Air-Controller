using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Missiles;
using FlightKit;

public class ProximityExplosion : MonoBehaviour
{
    [Tooltip("Drag the prefab of the explosion here.")]
    public GameObject explosionPrefab;          //explosion prefab

    [Tooltip("The damage caused hitting this object")]
    public float damageAmount = 10;

    [Tooltip("The radius of affect from this object")]
    public float radius = 10.0f;

    [Tooltip("The force of the explosion from this object")]
    public float power = 1.0f;


    Rigidbody playerRb;


    internal bool activate = false;             //to activate only once  time

    // Start is called before the first frame update
    void Start()
    {
        var userControl = FindObjectOfType<AirplaneUserControl>();
        playerRb = userControl.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("ProximityExplosion OnTriggerEnter - tag: " + tag + "collision: " + collision.gameObject.name);


        //this avoids duplicating the collision
        if (activate) { return; }


        //If any part of a plane is detected we treat it as one
        if (collision.gameObject.name == "Wings" || collision.gameObject.name == "Body" || collision.gameObject.name == "Legs")
        {
            //Project exploding force onto aircraft rigidbody

            //explodeForce();
        }

        // We are destructable and we are touching terrain or levelbounds gameobjects, so ignore them.  We want AA Guns to be able destroy each other if player flies low enough
        if (tag == "CanDestroy" && collision.gameObject.tag == "Terrain" || 
            tag == "AAProjectile" && collision.gameObject.tag == "Terrain" ||
            tag == "AAProjectile" && collision.gameObject.tag == "AAProjectile" ||
            tag == "AAProjectile" && collision.gameObject.tag == "TakeOffPlatform" ||
            tag == "AAProjectile" && collision.gameObject.name == "Aircraft_AntiAirGun" ||
            tag == "AAProjectile" && collision.gameObject.name == "Aircraft_AntiAir4Guns" ||
            collision.gameObject.tag == "Missile" /*|| collision.gameObject.tag == "Player"*/ ||
             collision.gameObject.tag == "LevelBounds" || collision.gameObject.tag == "Untagged") { return; }


        if (collision.gameObject.GetComponent<HitPoint>() != null)
        {   // if the hit object has the Health script on it, deal damage

            collision.gameObject.GetComponent<HitPoint>().ApplyDamage(damageAmount);

        }

        ContactExplosion();
    }

    // destroy the projectile
    public virtual void ContactExplosion()
    {
        activate = true;
        if (explosionPrefab)
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }



    // Applies an explosion force to all nearby rigidbodies
    private void explodeForce()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {

            if (playerRb != null)
            {
                //Debug.Log("Rigidbody: " + playerRb.ToString());
                playerRb.AddExplosionForce(power, explosionPos, radius, 0F);
            }

        }
    }
}
