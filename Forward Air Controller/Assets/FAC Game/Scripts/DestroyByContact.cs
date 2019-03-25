using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Missiles;

public class DestroyByContact : MonoBehaviour
{

    [Tooltip("Drag the prefab of the explosion here.")]
    public GameObject explosionPrefab;          //explosion prefab

    [Tooltip("The damage caused hitting this object.")]
    public float damageAmount = 10;

    [Tooltip("The time before destroying the gameobject.")]
    public float duration = 1.0f;

    [Tooltip("The value of destroying this target.")]
    public int scoreValue;

    [Tooltip("The ScoreManager for the player.")]
    private ScoreManager scoreManager;


    //To activate only once at a time
    internal bool activate = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject scoreManagerObject = GameObject.FindWithTag("LevelScripts");
        if (scoreManagerObject != null)
        {
            scoreManager = scoreManagerObject.GetComponent<ScoreManager>();
        }

        if (scoreManager == null)
        {
            Debug.Log("Cannot find 'ScoreManager' script");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {


        //this avoids duplicating the collision
        if (activate) { return; }

        // We are destructable and we are touching terrain or levelbounds gameobjects, so ignore them
        if (tag == "CanDestroy" && other.gameObject.tag == "Terrain" || other.gameObject.tag == "LevelBounds" || other.gameObject.tag == "AAProjectile") { return; }


        Debug.Log("DestroyByTarget OnTriggerEnter - " + gameObject.ToString());
        if (gameObject.GetComponent<HitPoint>().hitPoint <= 0f)
        {


            //Create big explosion as gameobject destroyed
            activate = true;
            if (explosionPrefab)
            {
                Instantiate(explosionPrefab, transform.position, transform.rotation);
            }

            //Wait one second before destroying game object
            Destroy(gameObject, duration);

            scoreManager.AddScore(scoreValue);
        }
    }
}
