using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAGunAttackPlayer : StateMachineBehaviour
{
    private Missiles.AntiAirProjectileSpawner antiAirProjectileSpawner;

    private GameObject player;
    private Transform turretEyes;
    private TurretElevation turretElevation;
    private TurretRotation turretRotation;

    public float maximumTurnSpeed = 30f;
    public float maximumElevationSpeed = 30f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        antiAirProjectileSpawner = animator.GetComponent<Missiles.AntiAirProjectileSpawner>();

        turretRotation = animator.GetComponent<TurretRotation>();
        turretElevation = animator.GetComponent<TurretElevation>();
        turretEyes = TurretEyes.Instance.transform;

        antiAirProjectileSpawner.isEnabled = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (player)
        {
            CalculateBearing(animator);
            CalculateElevation(animator);

            EnableMissileSpawner();

        }
    }


    void CalculateBearing(Animator animator)
    {

        if (player != null)
        {
            Vector3 playerPosRelToTurret = turretRotation.rotationComponent.InverseTransformPoint(player.transform.position);
            float angle = Mathf.Atan2(playerPosRelToTurret.x, playerPosRelToTurret.z) * Mathf.Rad2Deg;
            //Debug.Log("AttackPlayer OnStateUpdate - Angle to player: " + angle);


            float turnSpeed = 0f;
            if (angle > 0f)
            {
                turnSpeed = Mathf.Min(angle, maximumTurnSpeed * Time.deltaTime);
            }
            else if (angle < 0f)
            {
                turnSpeed = Mathf.Max(angle, -maximumTurnSpeed * Time.deltaTime);
            }


            //Debug.Log("AttackPlayer OnStateUpdate - turnSpeed: " + turnSpeed);
            turretRotation.rotationComponent.transform.Rotate(0f, turnSpeed, 0f);


            animator.SetFloat("angleToTargetDeg", angle);
        }


    }

    private void CalculateElevation(Animator animator)
    {
    
        if (player != null)
        {

            Vector3 playerPosRelToTurret = turretElevation.gunBarrel.InverseTransformPoint(player.transform.position);

            float angle = 90 - Mathf.Atan2(playerPosRelToTurret.z, playerPosRelToTurret.y) * Mathf.Rad2Deg;
            //Debug.Log("TargetPlayer CalculateElevation - Angle to player: " + angle);

            float turnSpeed = 0f;
            if (angle > 0f)
            {
                turnSpeed = Mathf.Min(angle, maximumElevationSpeed * Time.deltaTime);
            }
            else if (angle < 0f)
            {
                turnSpeed = Mathf.Max(angle, -maximumElevationSpeed * Time.deltaTime);
            }


            //Debug.Log("TargetPlayer CalculateElevation - turnSpeed: " + turnSpeed);

            turretElevation.gunBarrel.transform.Rotate(-turnSpeed, 0f, 0f);

            animator.SetFloat("elevationToTargetDeg", angle);
        }
    }

    void EnableMissileSpawner()
    {
        antiAirProjectileSpawner.isEnabled = true;
    }
}
