using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    private Animator animator;
    private Animator animator2;
    GameObject[] destinations;
    private bool aggro;
    private int destinationIndex;
    private bool hasSniffed;  
    public float playerDetectionRange = 5f; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        agent = GetComponent<NavMeshAgent>();
        Transform childTransform = transform.Find("Creep_mesh");
        Transform childTransform2 = transform.Find("Creep_mesh_lod1");
        animator = childTransform.GetComponent<Animator>();
        animator2 = childTransform2.GetComponent<Animator>();
        destinations = GameObject.FindGameObjectsWithTag("destination");
        aggro = false;
        destinationIndex = Random.Range(0, destinations.Length);
        hasSniffed = false;  
        agent.autoBraking = false;
        agent.isStopped = false;
        animator.SetBool("isWalking", true);
        animator2.SetBool("isWalking", true);
        agent.speed = 2;
        GotoNextPoint();
    }

    void Update()
    {
        if (aggro)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f && !hasSniffed)
            {
                
                ArrivedAtLocation();
            }
        }
    }

    void GotoNextPoint()
    {
        if (destinations.Length == 0)
            return;

        agent.destination = destinations[destinationIndex].transform.position;

        int newDestinationIndex = Random.Range(0, destinations.Length);
        while (destinationIndex == newDestinationIndex)
        {
            newDestinationIndex = Random.Range(0, destinations.Length);
        }
        destinationIndex = newDestinationIndex;
        hasSniffed = false;  
    }

    public void setAggro(bool ifAggro)
    {
        aggro = ifAggro;

        if (aggro)
        {
            agent.speed = 3;
            animator.SetBool("isWalking", false);
            animator2.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
            animator2.SetBool("isRunning", true);
        }
        else
        {
            agent.speed = 2;
            animator.SetBool("isRunning", false);
            animator2.SetBool("isRunning", false);
            animator.SetBool("isWalking", true);
            animator2.SetBool("isWalking", true);
        }
    }

    public bool getAggro()
    {
        return aggro;
    }

    private void ArrivedAtLocation()
    {
     
        agent.isStopped = true;

     
        animator.SetBool("isWalking", false);
        animator2.SetBool("isWalking", false);
        animator.SetTrigger("Sniff");
        animator2.SetTrigger("Sniff");

      
        hasSniffed = true;

        
        StartCoroutine(WaitForSniffAndCheckPlayer());
    }

    private IEnumerator WaitForSniffAndCheckPlayer()
    {
    
        yield return new WaitForSeconds(4.5f); 

       
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= playerDetectionRange)
        {
          
            animator.SetTrigger("Roar");
            animator2.SetTrigger("Roar");
            setAggro(true);

          
            yield return new WaitForSeconds(4.5f); 
        }

   
        agent.isStopped = false;

      
        animator.SetBool("isWalking", true);
        animator2.SetBool("isWalking", true);

        
        if (!aggro)
        {
            GotoNextPoint();
        }
    }
}
