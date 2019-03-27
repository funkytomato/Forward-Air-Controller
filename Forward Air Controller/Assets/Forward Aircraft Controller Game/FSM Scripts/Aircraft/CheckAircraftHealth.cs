using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Missiles;

public class CheckAircraftHealth : StateMachineBehaviour
{

    private GameObject _airplane;
    //private HitPoint _hitPoints;

    [Tooltip("The HealthManager for the player.")]
    private HealthManager _healthManager;

    //private float CurrentHealth = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        GameObject healthManagerObject = GameObject.FindWithTag("LevelScripts");
        if (healthManagerObject != null)
        {
            _healthManager = healthManagerObject.GetComponent<HealthManager>();
        }

        if (_healthManager == null)
        {
            Debug.Log("Cannot find 'HealthManager' script");
        }

        //var playerObject = GameObject.FindWithTag("Player");
        //_airplane = playerObject;

        //_hitPoints = playerObject.GetComponent<HitPoint>();

        //CurrentHealth = _hitPoints;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //_hitPoints = _airplane.GetComponent<HitPoint>();

        //animator.SetFloat("CurrentHealth", _hitPoints.hitPoint);
        animator.SetFloat("CurrentHealth", _healthManager.GetHealth());
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
