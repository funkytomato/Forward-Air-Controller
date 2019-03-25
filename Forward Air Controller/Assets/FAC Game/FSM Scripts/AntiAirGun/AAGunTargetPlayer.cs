using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Missiles;

public class AAGunTargetPlayer : StateMachineBehaviour
{
    private GameObject player;
    private Missiles.AntiAirProjectileSpawner antiAirProjectileSpawner;

    private TurretElevation turretElevation;
    private TurretRotation turretRotation;
    private Transform turretEyes;

    public float maximumTurnSpeed = 30f;
    public float maximumElevationSpeed = 30f;

    private float _maxElevationAngle = 182f;
    private float _minElevationAngle = 0f;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        player = GameObject.FindGameObjectWithTag("Player");
        antiAirProjectileSpawner = animator.GetComponent<Missiles.AntiAirProjectileSpawner>();

        turretRotation = animator.GetComponent<TurretRotation>();
        turretElevation = animator.GetComponent<TurretElevation>();
        turretEyes = TurretEyes.Instance.transform;

        antiAirProjectileSpawner.isEnabled = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CalculateBearing(animator);
        CalculateElevation(animator);
    }


    private void CalculateBearing(Animator animator)
    {
        if (player != null)
        {
            Vector3 playerPosRelToTurret = turretRotation.rotationComponent.InverseTransformPoint(player.transform.position);
            float angle = Mathf.Atan2(playerPosRelToTurret.x, playerPosRelToTurret.z) * Mathf.Rad2Deg;

            float turnSpeed = 0f;
            if (angle > 0f)
            {
                turnSpeed = Mathf.Min(angle, maximumTurnSpeed * Time.deltaTime);
            }
            else if (angle < 0f)
            {
                turnSpeed = Mathf.Max(angle, -maximumTurnSpeed * Time.deltaTime);
            }

            turretRotation.rotationComponent.transform.Rotate(0f, turnSpeed, 0f);

            animator.SetFloat("angleToTargetDeg", angle);
        }
    }

    private void CalculateElevation(Animator animator)
    {

        if (player != null)
        {
            //Get the relative position between the turret position and the target position.
            Vector3 playerPosRelToTurret = turretElevation.gunBarrel.InverseTransformPoint(player.transform.position);

            //Calculate the angle between the forward facing gunbarrel and the relative target position
            float angle = 90f - Mathf.Atan2(playerPosRelToTurret.z, playerPosRelToTurret.y) * Mathf.Rad2Deg;

            //Clamp the elevation between the minimum and maximum angles
            //float clampedAngle = Mathf.Clamp(angle, _minElevationAngle, _maxElevationAngle);
            float clampedAngle = ClampAngle(angle, _minElevationAngle, _maxElevationAngle);

            Debug.Log("AAGunTargetPlayer CalculateElevation - Angle: " + angle + " clampedAngle: " + clampedAngle);

            //Calculate the elevation speed of the guns
            float turnSpeed = 0f;
            //if (angle > 0f)
            if (clampedAngle > 0f)
            {
                turnSpeed = Mathf.Max(angle, -maximumTurnSpeed * Time.deltaTime);
            }
            else
            {
                turnSpeed = Mathf.Min(angle, maximumTurnSpeed * Time.deltaTime);
            }


            //Articulate the gun barrels elevatin towards the target
            turretElevation.gunBarrel.transform.Rotate(-turnSpeed, 0f, 0f);

            //Set the animator's statemachine property elevationToPlayerDeg
            animator.SetFloat("elevationToTargetDeg", clampedAngle);
        }
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }
}
