/*******************************************************
 * 													   *
 * Asset:		 	Missile and Rocket behavior        *
 * Script:		 	BaseMissile.cs  				   *
 *                                                     *
 * Page: https://www.facebook.com/NLessStudio/         *
 * 													   *
 *******************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Missiles
{
    public enum TargetType
    {
        targetFerstEnemy = 0,
        targetNearestEnemy = 1,
        targetRandomEnemy = 2
    }
    [RequireComponent(typeof(Collider))]
    public abstract class BaseMissile : MonoBehaviour
    {

        [Header("Missile Variables")]

        [Tooltip("The projectile will go for the object that has this tag")]
        public string tagTargetName = "Enemy";              //Tag for the target.
        [Tooltip("The projectile damage.")]
        public float damageAmount = 10;
        [Tooltip("If damage OnTriggerEnter.")]
        public bool damageOnTrigger = true;
        [Tooltip("If damage OnCollisionEnter.")]
        public bool damageOnCollision = false;
        [Tooltip("If using gravity.")]
        public bool useGravity = false;
        [Tooltip("Explode if the target is close less or equal than distanceToExplode.")]
        public float distanceToExplode = 0;
        [Tooltip("Target the first enemy in scene, target a random enemy in scene, target the nearest enemy in scene between minDistance and maxDistance.")]
        public TargetType typeOfTarget = 0;
        [Tooltip("If type of target = target nearest enemy the target must be between the maximum and minimum distances for the projectile to consider it a target.")]
        public float minDistance = 0;                   //This is the minimum distance to change target.
        [Tooltip("If type of target = target nearest enemy the target must be between the maximum and minimum distances for the projectile to consider it a target.")]
        public float maxDistance = 100;
        [Range(1f, 30f)]
        [Tooltip("The projectile  life time, if reach 0 destroy it.")]
        public float lifeTime = 10f;                //the projectile  life time.
        [Range(0f, 15f)]
        [Tooltip("The time this projectile  wait to mark the targett,if exit a target.")]
        public float timeToSetTarget = 0;               //the time this projectile  wait to mark the target

        [Range(0f, 400f)]
        [Tooltip("This is the projectile start speed after activate projectile.")]
        public float startSpeed = 60;                   //the projectile  start speed.

        [Range(0f, 400f)]
        [Tooltip("This is the maximum speed the projectile can reach.")]
        public float MaxSpeed = 60f;                //the projectile  maximum speed.

        [Tooltip("This is the acceleration speed.")]
        public float accelerationSpeed = 2f;

        [Range(10, 300f)]
        [Tooltip("This is the turn speed,the larger the number, the faster it will rotate.")]
        public float turnSpeed = 180f;              //the projectile  turn speed

        [Tooltip("This is the projectile current speed.")]
        public float currentSpeed = 0;              //missile current speed 

        [Tooltip("This is the projectile accurancy.")]
        [Range(0f, 10f)]
        public float missileStability = 0;

        [Tooltip("Drag the prefab of the explosion here.")]
        public GameObject explosionPrefab;          //explosion prefab
        [Tooltip("Drag the sound of the missile here (this is the active sound of the missile, the explosion sounds is recommended to put in explosion prefabs).")]
        public AudioSource missileEngineSound;
        [Tooltip("The current selected target.")]
        public GameObject SelectedTarget = null;    //projectile  target

        internal Vector3 direction;                 //vector direction for the target
        internal bool addTarget = false;            //true if have target, false if not
        internal float time = 0;                    //time variable
        internal float offSet;                      //projectile accuracy
        internal GameObject[] enemys;               //var to keep list of enemy in scene
        internal bool activate = false;             //to activate only once  time

        internal Vector3 diference;
        private GameObject nearestTarget = null;    //nearest target
        //Get the ferst target in scene when the time to get a target reach 0;
        public virtual void TargetEnemy()
        {
            if (SelectedTarget == null && timeToSetTarget <= time)
            {
                SelectedTarget = GameObject.FindGameObjectWithTag(tagTargetName);
            }
        }
        //target the closest enemy in the scene in realtime
        public virtual void TargetNearestEnemy(float minDistance, float maxDistance)
        {
            if (timeToSetTarget <= time)
            {
                enemys = GameObject.FindGameObjectsWithTag(tagTargetName);
                float distance = Mathf.Infinity;
                //calculate squared distances
                minDistance = minDistance * minDistance;
                maxDistance = maxDistance * maxDistance;
                foreach (GameObject target in enemys)
                {
                    diference = target.transform.position - transform.position;
                    float currentDistance = diference.sqrMagnitude;
                    if (currentDistance < distance && currentDistance >= minDistance && currentDistance <= maxDistance)
                    {
                        nearestTarget = target;
                        distance = currentDistance;
                    }
                }
                SelectedTarget = nearestTarget;                   //catch the nearest enemy
            }
        }
        ////target a random enemy
        public virtual void TargetRandomEnemy()
        {
            if (SelectedTarget == null && timeToSetTarget <= time)
            {
                enemys = GameObject.FindGameObjectsWithTag(tagTargetName);
                if (enemys.Length > 0)
                {
                    SelectedTarget = enemys[Random.Range(0, enemys.Length)];
                }
            }
        }
        //follow the target in 3D space if exist,if not go ahead.
        public virtual void PersuitTarget3D()
        {

            if (SelectedTarget != null)
            {
                direction = SelectedTarget.transform.position - transform.position + Random.insideUnitSphere * offSet;
                direction.Normalize();
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
            }
            transform.Translate(Vector3.forward * currentSpeed * Time.fixedDeltaTime);
        }
        //follow the target in ZX plane if exist,if not go ahead.
        public virtual void PersuitTargetZX()
        {

            if (SelectedTarget)
            {
                direction = SelectedTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);

            }
            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        }
        //Accelerate speed not more than maxima speed or desaccelerate if accel is negative.
        public virtual void Accelerate(float accel)
        {
            if (currentSpeed < 0)
                currentSpeed = 0;
            if (currentSpeed < MaxSpeed)
                currentSpeed += accel * Time.fixedDeltaTime;
        }

        public virtual void ExplodeByDistance(float distanceToExplode)
        {
            if (SelectedTarget && distanceToExplode > 0)
            {
                //calculate squared distances
                distanceToExplode = distanceToExplode * distanceToExplode;
                diference = SelectedTarget.transform.position - transform.position;
                float currentDistance = diference.sqrMagnitude;
                if (distanceToExplode >= currentDistance)
                {
                    //this avoids duplicating the collision
                    if (activate) { return; }
                    MissileContact();
                }
            }

        }
        //destroy the projectile  if the time life is over
        public virtual void EndLifeByTime()
        {
            time += Time.fixedDeltaTime;
            if (lifeTime < time)
                MissileContact();
        }
        // destroy the projectile
        public virtual void MissileContact()
        {
            activate = true;
            if (explosionPrefab)
            {
                Instantiate(explosionPrefab, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
        public virtual void UseGravity(bool useGravity)
        {
            if (GetComponent<Rigidbody>())
                GetComponent<Rigidbody>().useGravity = useGravity;
        }
        public virtual void RemoveForces(bool removeForces)
        {
            GetComponent<Rigidbody>().isKinematic = removeForces;
        }

        //Apply MissileContact when the projectile impact another object, used for missiles which are triggers. 
        public virtual void OnTriggerEnter(Collider other)
        {
            if (damageOnTrigger)
            {
                //this avoids duplicating the collision
                if (activate) { return; }
                // if the missile got hit with it's or whit the player, ignore it
                //if (tag == "Missile" && other.gameObject.tag == "Missile" || other.gameObject.tag == "Player") { return; }
                if (tag == "Missile" && other.gameObject.tag == "Missile" || other.gameObject.tag == "Player" ||
                    other.gameObject.tag == "LevelBounds" || other.gameObject.tag == "RadarSphere" ||
                    other.gameObject.name == "GlobalWeatherZone") { return; }

                if (other.gameObject.GetComponent<HitPoint>() != null)
                {   // if the hit object has the Health script on it, deal damage

                    other.gameObject.GetComponent<HitPoint>().ApplyDamage(damageAmount);
                }
                // destroy the object
                MissileContact();
            }
        }

        // this is used for things that explode on impact and are NOT triggers
        public virtual void OnCollisionEnter(Collision other)
        {
            if (damageOnCollision)
            {
                //this avoids duplicating the collision
                if (activate) { return; }
                // If the missile was hit by him or by the player, ignore it
                //if (tag == "Missile" && other.gameObject.tag == "Missile" || other.gameObject.tag == "Player")
                //return;
                if (tag == "Missile" && other.gameObject.tag == "Missile" || other.gameObject.tag == "Player" || other.gameObject.tag == "Respawn")
                    return;

                // if the hit object has the Enemy_Controller script on it, deal damage
                if (other.gameObject.GetComponent<HitPoint>() != null)
                {
                    other.gameObject.GetComponent<HitPoint>().ApplyDamage(damageAmount);
                }
                MissileContact();  // destroy the object every time it hits something
            }
        }
    }
}
