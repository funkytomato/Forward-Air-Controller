/*******************************************************
 *                                                     *
 * Asset:           Missile and Rocket behavior        *
 * Script:          MissileScript.cs                   *
 *                                                     *
 * Page: https://www.facebook.com/NLessStudio/         *
 *                                                     *
 *******************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FlightKit;

namespace Missiles
{
    public class InterceptMissileScript : BaseMissile
    {

        //Calculate velocity
        //private Vector3 aircraftVelocity = Vector3.zero;
        private Vector3 _missilePrevPosition = Vector3.zero;
        private Vector3 _targetPrevPosition = Vector3.zero;

        // === variables you need ===
        //how fast our shots move
        float shotSpeed;
        //objects
        //GameObject shooter;
        //GameObject target;

        // === derived variables ===
        //positions
        //Vector3 shooterPosition = shooter.transform.position;
        Vector3 shooterPosition;
        Vector3 targetPosition;
        //velocities
        Rigidbody missileRb;
        Rigidbody targetRb;
        Vector3 shooterVelocity;
        Vector3 targetVelocity;


        //Vector3 shooterVelocity = shooter.rigidbody ? shooter.rigidbody.velocity : Vector3.zero;
        //Vector3 targetVelocity = target.rigidbody ? target.rigidbody.velocity : Vector3.zero;



        void Start()
        {
            UseGravity(useGravity);
            if (damageOnTrigger)
            {
                GetComponent<Collider>().isTrigger = true;
            }
            offSet = missileStability;
            currentSpeed = startSpeed;
            shotSpeed = currentSpeed;

            var userControl = FindObjectOfType<AirplaneUserControl>();
            targetRb = userControl.GetComponent<Rigidbody>();
            missileRb = GetComponent<Rigidbody>();

            Debug.Log("Aircraft velocity: " + targetRb.velocity);

            shooterPosition = transform.position;
            targetPosition = targetRb.transform.position;

            shooterVelocity = missileRb ? missileRb.velocity : Vector3.zero;
            targetVelocity = targetRb ? targetRb.velocity : Vector3.zero;

            //targetRb = SelectedTarget.GetComponent<Rigidbody>();

            //Vector3 vel = (transform.position - _missilePrevPosition) / Time.fixedDeltaTime;
            //_missilePrevPosition = transform.position;
            //shooterVelocity = vel;

            //vel = Vector3.zero;
            //vel = (SelectedTarget.transform.position - _targetPrevPosition) / Time.fixedTime;
            //_targetPrevPosition = transform.position;
            //targetVelocity = vel;

            //targetVelocity = SelectedTarget.GetComponent<>

            //shooterVelocity = missileRb ? missileRb.velocity : Vector3.zero;
            //targetVelocity = targetRb ? targetRb.velocity : Vector3.zero;

        }


        private void Update()
        {


        }

        void FixedUpdate()
        {

            Vector3 vel = (transform.position - _missilePrevPosition) / Time.deltaTime;
            _missilePrevPosition = transform.position;
            shooterVelocity = vel;


            //vel = Vector3.zero;
            //vel = (SelectedTarget.transform.position - _targetPrevPosition) / Time.deltaTime;
            //_targetPrevPosition = transform.position;
            //targetVelocity = vel;


            //var userControl = FindObjectOfType<AirplaneUserControl>();
            //targetRb = userControl.GetComponent<Rigidbody>();
            //missileRb = GetComponent<Rigidbody>();

            Debug.Log("Aircraft velocity: " + targetRb.velocity);

            //shooterPosition = transform.position;
            //targetPosition = targetRb.transform.position;

            shooterVelocity = missileRb ? shooterVelocity : Vector3.zero;
            targetVelocity = targetRb ? targetRb.velocity : Vector3.zero;





            //calculate intercept
            Vector3 interceptPoint = FirstOrderIntercept
            (
                shooterPosition,
                shooterVelocity,
                shotSpeed,
                targetPosition,
                targetVelocity
            );
            //now use whatever method to launch the projectile at the intercept point




            Debug.Log("missile velocity: " + shooterVelocity + "target velocity: " + targetVelocity);


            ExplodeByDistance(distanceToExplode);

            //EndLifeByTime();                    //destroy the projectile  if the time life is over

            switch (typeOfTarget)               //select the type of target
            {
                case TargetType.targetFerstEnemy:
                    TargetEnemy();
                    break;
                case TargetType.targetNearestEnemy:
                    TargetNearestEnemy(minDistance, maxDistance);
                    break;
                case TargetType.targetRandomEnemy:
                    TargetRandomEnemy();
                    break;
                default:
                    break;
            }

            //PersuitTarget3D();                    //follow the target in 3D space if exist,if not go ahead.
            InterceptTarget3D(interceptPoint);

            Accelerate(accelerationSpeed);        //Accelerate speed not more than maxima speed or desaccelerate if accel is negative.


        }


        //follow the target in 3D space if exist,if not go ahead.
        public void InterceptTarget3D(Vector3 interceptPoint)
        {

            if (SelectedTarget != null)
            {
                direction = SelectedTarget.transform.position - transform.position + Random.insideUnitSphere * offSet;
                direction.Normalize();
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(interceptPoint), turnSpeed * Time.deltaTime);
            }
            transform.Translate(Vector3.forward * currentSpeed * Time.fixedDeltaTime);

        }

        //first-order intercept using absolute target position
        public static Vector3 FirstOrderIntercept
        (
            Vector3 shooterPosition,
            Vector3 shooterVelocity,
            float shotSpeed,
            Vector3 targetPosition,
            Vector3 targetVelocity
        )
        {
            Vector3 targetRelativePosition = targetPosition - shooterPosition;
            Vector3 targetRelativeVelocity = targetVelocity - shooterVelocity;
            float t = FirstOrderInterceptTime
            (
                shotSpeed,
                targetRelativePosition,
                targetRelativeVelocity
            );
            return targetPosition + t * (targetRelativeVelocity);
        }
        //first-order intercept using relative target position
        public static float FirstOrderInterceptTime
        (
            float shotSpeed,
            Vector3 targetRelativePosition,
            Vector3 targetRelativeVelocity
        )
        {
            float velocitySquared = targetRelativeVelocity.sqrMagnitude;
            if (velocitySquared < 0.001f)
                return 0f;

            float a = velocitySquared - shotSpeed * shotSpeed;

            //handle similar velocities
            if (Mathf.Abs(a) < 0.001f)
            {
                float t = -targetRelativePosition.sqrMagnitude /
                (
                    2f * Vector3.Dot
                    (
                        targetRelativeVelocity,
                        targetRelativePosition
                    )
                );
                return Mathf.Max(t, 0f); //don't shoot back in time
            }

            float b = 2f * Vector3.Dot(targetRelativeVelocity, targetRelativePosition);
            float c = targetRelativePosition.sqrMagnitude;
            float determinant = b * b - 4f * a * c;

            if (determinant > 0f)
            { //determinant > 0; two intercept paths (most common)
                float t1 = (-b + Mathf.Sqrt(determinant)) / (2f * a),
                        t2 = (-b - Mathf.Sqrt(determinant)) / (2f * a);
                if (t1 > 0f)
                {
                    if (t2 > 0f)
                        return Mathf.Min(t1, t2); //both are positive
                    else
                        return t1; //only t1 is positive
                }
                else
                    return Mathf.Max(t2, 0f); //don't shoot back in time
            }
            else if (determinant < 0f) //determinant < 0; no intercept path
                return 0f;
            else //determinant = 0; one intercept path, pretty much never happens
                return Mathf.Max(-b / (2f * a), 0f); //don't shoot back in time
        }
    }
}
