using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public AudioSource sniffSource;
    public AudioSource roarSource;
    public AudioSource muzikaSource;
    public AudioSource chaseMuzikaSource;
    public AudioSource hodanjeSource;
    public AudioSource brzoHodanjeSource;

    private GameObject player;
    private NavMeshAgent agent;
    private Animator animator;
    private Animator animator2;
    GameObject[] destinations;
    private bool aggro;
    private int destinationIndex;
    private bool hasSniffed;
    public float sniffDetectionRange = 5f;
    public bool interrupt;
    private AggroController aggroController;
    private Coroutine sniffCoroutine;
    public Camera mainCamera;

    void Start()
    {
        aggroController = gameObject.GetComponent<AggroController>();
        hodanjeSource.Play();
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
            isPlayerInSafeRoom();
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f && !hasSniffed)
            {
                ArrivedAtLocation();
            }
        }


        if (aggro && sniffCoroutine != null)
        {
            InterruptSniffAndAttack();
        }
    }

    void GotoNextPoint()
    {
        if (destinations.Length == 0)
            return;
        GameObject destinationRoom = destinations[destinationIndex];
        int finalDestinationIndex = Random.Range(0, destinationRoom.transform.childCount);
        Transform finalDestination = destinationRoom.transform.GetChild(finalDestinationIndex);

        agent.destination = finalDestination.position;

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

        if (ifAggro && !aggro)
        {
            StartCoroutine(PerformRoarBeforeAggro());
        }

        aggro = ifAggro;

        if (aggro)
        {
            agent.speed = 4;
            animator.SetBool("isWalking", false);
            animator2.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
            animator2.SetBool("isRunning", true);
            hodanjeSource.Pause();
            brzoHodanjeSource.Play();
            muzikaSource.Pause();
            chaseMuzikaSource.Play();
        }
        else
        {
            agent.speed = 2;
            animator.SetBool("isRunning", false);
            animator2.SetBool("isRunning", false);
            animator.SetBool("isWalking", true);
            animator2.SetBool("isWalking", true);
            brzoHodanjeSource.Pause();
            hodanjeSource.Play();
            chaseMuzikaSource.Pause();
            muzikaSource.Play();

        }
    }
    private IEnumerator PerformRoarBeforeAggro()
    {

        agent.isStopped = true;


        animator.SetTrigger("Roar");
        animator2.SetTrigger("Roar");
        roarSource.Play();


        yield return new WaitForSeconds(4.5f);


        agent.isStopped = false;

        if (aggro)
        {
            agent.SetDestination(player.transform.position);
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
        sniffSource.Play();

        hasSniffed = true;


        sniffCoroutine = StartCoroutine(WaitForSniffAndCheckPlayer());
    }

    private IEnumerator WaitForSniffAndCheckPlayer()
    {
        yield return new WaitForSeconds(4.5f);

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= sniffDetectionRange && !aggroController.IsWallBetween(player, gameObject))
        {
            float aggroChance = (sniffDetectionRange - distanceToPlayer) / sniffDetectionRange * 100;
            float randomValue = Random.Range(0f, 100f);
            if (randomValue < aggroChance)
            {
                animator.SetTrigger("Roar");
                animator2.SetTrigger("Roar");
                setAggro(true);
            }
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

    private void InterruptSniffAndAttack()
    {

        if (sniffCoroutine != null)
        {
            StopCoroutine(sniffCoroutine);
            sniffCoroutine = null;
        }


        animator.ResetTrigger("Sniff");
        animator2.ResetTrigger("Sniff");


        StartCoroutine(PerformRoarBeforeAggro());


        aggro = true;
    }
    public float getPlayerDistance()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }
    public bool isPlayerInSafeRoom()
    {
        bool safe = player.transform.position.x >= -83 && player.transform.position.x < -76 && player.transform.position.z >= -1 && player.transform.position.z <= 10;
        if (safe)
        {
            setAggro(false);
        }
        return safe;
    }
    public bool isInViewOfPlayer()
    {
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(gameObject.transform.position);

        // Check if the object is in the camera's viewport
        return viewportPos.z > 0 &&
                        viewportPos.x > 0 && viewportPos.x < 1 &&
                        viewportPos.y > 0 && viewportPos.y < 1;


    }
}