using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class calls RegisterIdle on IdleSequence instance if it finds one.
/// To avoid registering idle every time an airplane collides with a platform,
/// the airplane has to be in collision with the platform for few seconds for taking off to be registered.
/// </summary>
public class IdlePublisher : MonoBehaviour
{

    /// <summary>
    /// The event is fired once the user-controlled airplane has taken off the ground.
    /// Useful for tweening in effects and HUD.
    /// </summary>
    public static event GameActions.SimpleAction OnIdleEvent;

    /// <summary>
    /// Minimum time in seconds that can be considered landing and not a crash.
    /// </summary>
    //private const float MIN_LANDING_DURATION = 1f;

    /// <summary>
    /// Minimum time in seconds between two possible landings, everithing smaller is ignored.
    /// </summary>
    //private const float MIN_TIME_BETWEEN_LANDINGS = 10f;

    private float _collisionEnterTime = -1;

    void OnCollisionEnter(Collision collision)
    {
        // Time since last landing.
        //float duration = Time.time - _collisionEnterTime;
       
        // // Last landing was long time ago or did not exist.
        //bool validLanding = duration > MIN_TIME_BETWEEN_LANDINGS || _collisionEnterTime < 0;
        //if (validLanding && collision.gameObject.CompareTag(Tags.TakeOffPlatform))
        //{
        //    _collisionEnterTime = Time.time;
        //}
    }

    void OnCollisionExit(Collision collision)
    {
        //float duration = Time.time - _collisionEnterTime;
        //if (duration > MIN_LANDING_DURATION && collision.gameObject.CompareTag(Tags.TakeOffPlatform))
        //{
        //    if (OnTakeOffEvent != null)
        //    {
        //        OnTakeOffEvent();
        //    }
        //}
    }
}
