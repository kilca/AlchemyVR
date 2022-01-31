using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCClient : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;

    public Transform toPoint;

    public bool isWalking;



    void GetPotion() {

        anim.SetTrigger("hasPotion");
    }

    // Start is called before the first frame update
    void Start()
    {
        toPoint = GameObject.FindGameObjectWithTag("ToPoint").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        agent.SetDestination(toPoint.position);


    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(agent.velocity.magnitude);
        isWalking = (agent.velocity.magnitude > 1.0f);
        anim.SetBool("isWalking", isWalking);

    }
}
