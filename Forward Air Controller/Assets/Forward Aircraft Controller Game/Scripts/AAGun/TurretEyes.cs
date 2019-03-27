using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.;

public class TurretEyes : MonoBehaviour
{

    public Animator animator;
    private Transform _target;
    public float range = 100f;

    public static TurretEyes Instance;



    void Awake()
    {
        Instance = this;
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
            RaycastHit hit;

            if (Physics.Raycast(transform.position, _target.position - transform.position, out hit, range) &&
                                                                                                             hit.collider.tag == "Player" || 
                                                                                                             hit.collider.tag == "AAProjectile" ||
                                                                                                             hit.collider.tag == "WeatherMaker")
            {
                animator.SetBool("isTargetVisible", true);
            }
            else
                animator.SetBool("isTargetVisible", false);
        }
        else
        {
            animator.SetBool("isTargetVisible", false);
        }
    }

    void OnDrawGizmos()
    {
        if (_target)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, _target.position);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5f);
        }
    }
}
