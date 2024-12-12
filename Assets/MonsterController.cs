using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MonsterController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private NavMeshAgent agent;
    private Animator animator;
    GameObject[] destinations;
    private bool aggro;
    private int destinationIndex;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        agent = GetComponent<NavMeshAgent>();
        Transform childTransform = transform.Find("Creep_mesh");
        animator = childTransform.GetComponent<Animator>();
        destinations = GameObject.FindGameObjectsWithTag("destination");
        aggro = false;
        destinationIndex = Random.Range(0, destinations.Length);
        agent.autoBraking = false;
        agent.isStopped = false;
        animator.SetBool("isWalking", true);
        agent.speed = 2;
        GotoNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (aggro){
            agent.SetDestination(player.transform.position);
        //    animator.SetBool("isWalking", true);
          //  agent.speed = 3;
        }
        else{
         //   animator.SetBool("isWalking", true);
          //  agent.speed = 2;
            if (!agent.pathPending && agent.remainingDistance < 0.5f){
                GotoNextPoint();
            }
        }
        
    }
            void GotoNextPoint() {
            // Returns if no points have been set up
            if (destinations.Length == 0)
                return;

            // Set the agent to go to the currently selected destination.
            agent.destination = destinations[destinationIndex].transform.position;

            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
            int newDestinationIndex = Random.Range(0, destinations.Length);
            while (destinationIndex == newDestinationIndex){
                newDestinationIndex = Random.Range(0, destinations.Length);
            }
            destinationIndex = newDestinationIndex;
        }
        public void setAggro (bool ifAggro){
            aggro = ifAggro;

            if (aggro){
                agent.speed=3;
                animator.SetBool("isRunning", true);
            }
            else{
                agent.speed=2;
                animator.SetBool ("isRunning", false);
            }
        }
        public bool getAggro(){
            return aggro;
        }
}
