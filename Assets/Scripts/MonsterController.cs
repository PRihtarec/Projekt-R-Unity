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

    public GameObject drugoCudoviste;

    private GameObject player;
    private GameObject Player;
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
    public float walkingSpeed = 2f;
    public float runningSpeed = 4f;
    private bool previousAggroState;
    private bool first;
    public GameObject body;


    void Start()
    {
        first=true;
        previousAggroState = aggro;
        aggroController = gameObject.GetComponent<AggroController>();
        hodanjeSource.Play();
        player = GameObject.FindGameObjectWithTag("player");
        Player = GameObject.FindGameObjectWithTag("Player");
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
        agent.speed = walkingSpeed;
        GotoNextPoint();
    }

    void Update()
    {
       // UnityEngine.Debug.Log(aggro);
        if (player == null || gameObject == null){
            return;
        }
        if (aggro != previousAggroState){
            if(aggro)
            {
            //    hodanjeSource.Pause();
             //   brzoHodanjeSource.Play();
             ///   muzikaSource.Pause();
             //   chaseMuzikaSource.Play();
            }
            else
            {
              //  brzoHodanjeSource.Pause();
             //   hodanjeSource.Play();
            //    chaseMuzikaSource.Pause();
            //    muzikaSource.Play();
            }
        }
        previousAggroState = aggro;
        if(isPlayerInSafeRoom()&&first){
            chaseMuzikaSource.Pause();
            muzikaSource.Play();
            drugoCudoviste.SetActive(false);
            first=false;
        }
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
        UnityEngine.Debug.Log("stavio desTinaciju na" + finalDestination.position);

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

     //   aggro = ifAggro;

        if (!aggro && ifAggro)
        {
            agent.speed = runningSpeed;
            animator.SetBool("isWalking", false);
            animator2.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
            animator2.SetBool("isRunning", true);
                            hodanjeSource.Pause();
                brzoHodanjeSource.Play();
                muzikaSource.Pause();
                chaseMuzikaSource.Play();
        }
        if (!ifAggro && aggro)
        {
            agent.speed = walkingSpeed;
            animator.SetBool("isRunning", false);
            animator2.SetBool("isRunning", false);
            animator.SetBool("isWalking", true);
            animator2.SetBool("isWalking", true);
             brzoHodanjeSource.Pause();
                hodanjeSource.Play();
                chaseMuzikaSource.Pause();
                muzikaSource.Play();
            
            GotoNextPoint();

        }
        aggro = ifAggro;
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
                if (!isPlayerInSafeRoom()){
                setAggro(true);
                }
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
     //   if (safe)
      //  {
      //      setAggro(false);
      //  }
        return safe;
    }
    public bool isInViewOfPlayer()
    {
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(gameObject.transform.position);

        // Check if the object is in the camera's viewport
        
        Debug.Log((viewportPos.z > 0 &&
                        viewportPos.x > 0 && viewportPos.x < 1 &&
                        viewportPos.y > 0 && viewportPos.y < 1) && HasLineOfSight(body.transform, Player.transform) && getPlayerDistance()<=15);
        return (viewportPos.z > 0 &&
                        viewportPos.x > 0 && viewportPos.x < 1 &&
                        viewportPos.y > 0 && viewportPos.y < 1) && HasLineOfSight(body.transform, Player.transform) && getPlayerDistance()<=15;


    }
public static bool HasLineOfSight(Transform objectA, Transform objectB)
{
    if (objectA == null || objectB == null)
    {
        Debug.LogWarning("One or both objects are null!");
        return false;
    }

    Vector3 direction = (objectB.position - objectA.position).normalized;
    float distance = Vector3.Distance(objectA.position, objectB.position);

    // Perform a raycast and collect all hits along the ray
    RaycastHit[] hits = Physics.RaycastAll(objectA.position, direction, distance);

    // Sort hits by distance to ensure we process the closest objects first
    System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

    foreach (RaycastHit hit in hits)
    {
        // If the hit object is the target or one of its children, return true
        if (hit.transform == objectB || hit.transform.IsChildOf(objectB))
        {
            
            return true; // Target is reached without obstruction
            
        }
        else
        {
            Debug.Log($"Hit object before target: {hit.transform.name}");
            return false; // There is something blocking the way
        }
    }

    return false; // No objects hit, meaning no clear sight
}

}