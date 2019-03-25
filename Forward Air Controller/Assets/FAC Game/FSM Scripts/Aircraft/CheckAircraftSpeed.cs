using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Aeroplane;

using FlightKit;

public class CheckAircraftSpeed : StateMachineBehaviour
{

    private GameObject _airplane;
    private Rigidbody _airplaneRB;
    private float _currentSpeed = 0.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var userControl = GameObject.FindObjectOfType<AirplaneUserControl>();
        if (userControl == null)
        {
            Debug.LogError("FLIGHT KIT CheckAircraftSpeed: an AeroplaneUserControl component is missing in the scene");
            return;
        }

        _airplane = userControl.gameObject;

        _airplaneRB = _airplane.GetComponent<Rigidbody>();
        _currentSpeed = _airplane.GetComponent<AeroplaneController>().ForwardSpeed;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _currentSpeed = _airplane.GetComponent<AeroplaneController>().ForwardSpeed;

        animator.SetFloat("CurrentSpeed", _currentSpeed);
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
