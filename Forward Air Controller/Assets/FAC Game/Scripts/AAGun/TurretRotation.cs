using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotation : MonoBehaviour
{

    public Animator animator;
    private Transform _target;
    //public static TurretRotation Instance;
    public Transform rotationComponent;

    public float speed = 1.0f;
    Vector3 m_lastKnownPosition = Vector3.zero;
    Quaternion m_lookAtRotation;

    void Awake()
    {
        //Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            _target = playerObject.transform;
        }
        else
        {
            Debug.Log("TurretEyes can not find player gameobject");
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Vector3 targetPosition = new Vector3(Target.transform.position.x,
        //                                 transform.position.y,
        //                                 Target.transform.position.z);

        //rotationComponent.transform.LookAt(targetPosition);

        //if (Target)
        //{
        //    if (m_lastKnownPosition != Target.transform.position)
        //    {
        //        m_lastKnownPosition = Target.transform.position;
        //        m_lookAtRotation = Quaternion.LookRotation(m_lastKnownPosition - transform.position);
        //    }

        //    if (rotationComponent.transform.rotation != m_lookAtRotation)
        //    {

        //        //Need to rotate just on z axis
        //        //rotationComponent.transform.rotation = Quaternion.RotateTowards(rotationComponent.transform.rotation, m_lookAtRotation, speed * Time.deltaTime);
        //        //rotationComponent.transform.LookAt(Target);

 
        //    }
        //}
    }

    void OnDrawGizmos()
    {
        if (_target)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(rotationComponent.transform.position, _target.transform.position);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(rotationComponent.transform.position, rotationComponent.transform.position + rotationComponent.transform.forward * 5f);
        }
    }
}
