using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Missiles;

public class AircraftCollisionController : MonoBehaviour
{
    [Tooltip("Drag the prefab of the explosion here.")]
    public GameObject explosionPrefab;          //explosion prefab

    [Tooltip("The damage caused hitting this object")] 
    public int damageAmount = 10;


    internal bool activate = false;             //to activate only once  time

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("CollisionController OnTriggerEnter - tag: " + tag + "collision: " + collision.gameObject.name);

        //this avoids duplicating the collision
        if (activate) { return; }

        // We are destructable and we are touching terrain or levelbounds gameobjects, so ignore them
        if (tag == "CanDestroy" && collision.gameObject.tag == "Terrain" || collision.gameObject.tag == "Weather" || collision.gameObject.name == "GlobalWeatherZone" || collision.gameObject.name == "WeatherMakerSoundZone" || collision.gameObject.tag == "Missile" || /*collision.gameObject.tag == "AAProjectile" ||*/ collision.gameObject.tag == "LevelBounds") { return; }

        Debug.Log("CollisionController OnTriggerEnter - tag: " + tag + "collision: " + collision.gameObject.name);

        //Apply damage to the gameobject collided with
        if (collision.gameObject.GetComponent<HitPoint>() != null)
        {   
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

        //Debug.Log("AircraftCollisionController ContactExplosion: " + gameObject.ToString());


        if (gameObject.GetComponent<HitPoint>() != null)
        {
            if (gameObject.GetComponent<HitPoint>().hitPoint <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
