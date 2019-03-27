/*******************************************************
 * 													   *
 * Asset:		 	Missile and Rocket behavior        *
 * Script:		 	MissileSpawner.cs  				   *
 *                                                     *
 * Page: https://www.facebook.com/NLessStudio/         *
 * 													   *
 *******************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Missiles
{
    public class AntiAirProjectileSpawner : MonoBehaviour
    {

        public bool isEnabled = false;

        public enum AxisToRotate { AroundX, AroundY, AroundZ, noRotate }
        [Header("Variables")]

        //[Tooltip("This is the proximity of the AA Guns.")]
        //public GameObject radar;

        [Tooltip("This is the projectile to spawn.")]
        public GameObject missile;
        [Tooltip("This is the projectile number to spawn.")]
        public int number;
        [Tooltip("This is the spawn time per tic.")]
        public float spawnTicTime = 0.1f;
        [Tooltip("This is the rotation speed for the spawner.")]
        public float rotationSpeed = 10.0f;

        [Tooltip("This is the axis to rotate the spawner.")]
        public AxisToRotate way = AxisToRotate.noRotate;

        [Tooltip("This is prefabs if have a launch effect.")]
        public GameObject launchExplotion;

        [Tooltip("This is the launch site of projectiles.")]
        public Transform launchPointA;

        [Tooltip("This is the launch site of projectiles.")]
        public Transform launchPointB;

        [Tooltip("This is the launch site of projectiles.")]
        public Transform launchPointC;

        [Tooltip("This is the launch site of projectiles.")]
        public Transform launchPointD;

        [HideInInspector]
        public int ricochetNumber = 0;      //this is from ricochet missile use

        //private vars
        private GameObject newMissile;

        private float bulletsPerSecond = 1f;
        private float elapsedTime = 0f;

        private Transform[] possibleTargets;

        void Start()
        {

            //InvokeRepeating("SpawnMisile", 0f, spawnTicTime);

            //Collider[] colliders = Physics.OverlapSphere(radar.transform.position, radar.GetComponent<SphereCollider>().radius);
            //int i = 0;
            //while (i<colliders.Length)
            //{
            //    InvokeRepeating("SpawnMisile", 0f, spawnTicTime);
            //}
        }

        private void Update()
        {

            if (isEnabled)
            {
                //InvokeRepeating("SpawnMisile", 0f, spawnTicTime);
                if (Time.time - elapsedTime > 1f / spawnTicTime)
                {
                    SpawnMisile();
                    elapsedTime = Time.time;
                }
            }
        }

        void SpawnMisile()
        {
            switch (way)
            {
                case AxisToRotate.AroundX:
                    transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed, 10);
                    break;
                case AxisToRotate.AroundY:
                    transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed, 10);
                    break;
                case AxisToRotate.AroundZ:
                    transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed, 10);
                    break;
                case AxisToRotate.noRotate:
                    break;
            }
            if (number > 0)
            {
                if (launchExplotion)
                { 
                    Instantiate(launchExplotion, launchPointA.transform.position, launchPointA.transform.rotation);
                    newMissile = Instantiate(missile, launchPointA.transform.position, launchPointA.transform.rotation);

                    if (launchPointB != null && launchPointC != null && launchPointD != null)
                    {
                        Instantiate(launchExplotion, launchPointB.transform.position, launchPointB.transform.rotation);
                        Instantiate(launchExplotion, launchPointC.transform.position, launchPointC.transform.rotation);
                        Instantiate(launchExplotion, launchPointD.transform.position, launchPointD.transform.rotation);

                        newMissile = Instantiate(missile, launchPointB.transform.position, launchPointB.transform.rotation);
                        newMissile = Instantiate(missile, launchPointC.transform.position, launchPointC.transform.rotation);
                        newMissile = Instantiate(missile, launchPointD.transform.position, launchPointD.transform.rotation);
                    }
                }



                //if ricochet
                if (newMissile.GetComponent<Ricochet>()) { newMissile.GetComponent<Ricochet>().numberOfJump = ricochetNumber; }
                number--;
            }
            else
            {

                if (gameObject.tag != "Respawn")
                {
                    Debug.Log("MissileSpawner spawnMissile - gameObject destroyed: " + gameObject.name);
                    Destroy(gameObject);
                }
            }
        }


        //void GetInactiveInRadius()
        //{
        //    foreach (GameObject obj in possibleTargets)
        //    {
        //        if (Vector3.Distance(transform.position, obj.transform.position) < distance)
        //            obj.SetActive(true);
        //    }
        //}
    }
}
