using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretElevation : MonoBehaviour
{

    public Animator animator;
    private Transform _target;
    public Transform gunBarrel;

    public float speed = 1.0f;

    //public GameObject m_target = null;
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
        if (_target)
        {

            //Vector3 targetPosition = new Vector3(Target.transform.position.x,
            //                                        Target.transform.position.y,
            //                                        Target.transform.position.z);

            //turretElevation.transform.LookAt(targetPosition);


            //if (m_lastKnownPosition != Target.transform.position)
            //{
            //    m_lastKnownPosition = Target.transform.position;
            //    m_lookAtRotation = Quaternion.LookRotation(m_lastKnownPosition - transform.position);
            //}

            //if (turretElevation.transform.rotation != m_lookAtRotation)
            //{
            //    //turretElevation.transform.rotation = Quaternion.RotateTowards(transform.rotation, m_lookAtRotation, speed * Time.deltaTime);
            //    turretElevation.transform.LookAt(Target);
            //}
        }
    }

    //public void SetTarget(GameObject target)
    //{
    //    player = target;
    //}

    void OnDrawGizmos()
    {
        if (_target)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(gunBarrel.transform.position, _target.transform.position);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(gunBarrel.transform.position, gunBarrel.transform.position + gunBarrel.transform.forward * 5f);
        }
    }
}
