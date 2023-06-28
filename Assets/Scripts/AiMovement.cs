using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiMovement : MonoBehaviour
{
    private NavMeshAgent enemyAgent;
    private float dist;
    public float followThreshhold;
    public Animator anim;

    [SerializeField]
    Transform targetTransform;
    // Start is called before the first frame update
    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, targetTransform.position);

        if(dist < followThreshhold)
        {
            enemyAgent.isStopped = false;
            enemyAgent.SetDestination(targetTransform.position);
            anim.SetBool("isFollow", true);
            if(dist < 1f)
            {
                anim.SetBool("isAtk", true);
                anim.SetBool("isFollow", false);

            }
            else
            {
                anim.SetBool("isAtk", false);
                anim.SetBool("isFollow", true);

            }
        }
        else
        {
            enemyAgent.isStopped = true;
            anim.SetBool("isFollow", false);
            anim.SetBool("isAtk", false);
        }
 
    }
    


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject, 2);
        }
    }
}
