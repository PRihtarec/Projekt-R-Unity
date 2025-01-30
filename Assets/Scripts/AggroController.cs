using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private GameObject Player;
    private MonsterController monsterController;
    private PlayerMovement playerMovement;
    float distanceToPlayer;
    public float crouchingRange = 1f;
    public float walkingRange = 2f;
    public float sprintingRange = 5f;
    public float wallRangeMultiplier = 0.3f;

    public GameObject flashlight;
    public Light flashlightLight;
    private float flashlightRange = 10f;
    private Flashlight flashlightScript;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        Player = GameObject.FindGameObjectWithTag("Player");
        monsterController = gameObject.GetComponent<MonsterController>();
        playerMovement = Player.GetComponent<PlayerMovement>();
        // flashlight = GameObject.FindGameObjectWithTag("flashlight");
        flashlightLight = flashlight.GetComponent<Light>();
        flashlightRange = flashlightLight.range;
        flashlightScript = Player.GetComponent<Flashlight>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null || gameObject == null)
        {
            return;
        }
        if (gameObject.name.Equals("CreepHalway"))
        {
            return;
        }
        distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
        float range = walkingRange;
        if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
        {
            range = sprintingRange;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            range = crouchingRange;
        }
        if (IsWallBetween(Player, gameObject))
        {
            range = range * wallRangeMultiplier;
            //       UnityEngine.Debug.Log("ZID JE IZMEDU");
        }
        if (CheckPlayerInRange(range, player, gameObject) && playerMovement.IsPlayerMovingByInput() && monsterController.getAggro() == false)
            if (!monsterController.isPlayerInSafeRoom())
            {
                {
                    monsterController.setAggro(true);
                }
            }
        if (CheckMonsterFlashlight() && monsterController.getAggro() == false)
        {
            if (!monsterController.isPlayerInSafeRoom()){
            monsterController.setAggro(true);
            }
        }

    }

    public bool CheckPlayerInRange(float range, GameObject player, GameObject mainObject)
    {
        distanceToPlayer = Vector3.Distance(mainObject.transform.position, player.transform.position);
        return (distanceToPlayer < range);

    }

    public bool CheckMonsterFlashlight()
    {
        Vector3 directionToTarget = gameObject.transform.position - flashlight.transform.position;
        float distanceToTarget = directionToTarget.magnitude;
        if (!flashlightScript.isFlashlightActive())
        {
            return false;
        }
        if (distanceToTarget <= flashlightRange)
        {
            directionToTarget.Normalize();

            // Check if within cone
            float angleToTarget = Vector3.Angle(flashlight.transform.forward, directionToTarget);
            if (angleToTarget <= flashlightLight.spotAngle / 2)
            {
                //    UnityEngine.Debug.Log("proso angle check");
                // Perform a raycast to confirm no obstruction
                if (Physics.Raycast(flashlight.transform.position, directionToTarget, out RaycastHit hit, flashlightRange))
                {
                    //         UnityEngine.Debug.Log($"hitalo je {hit.collider}");
                    return hit.transform.position.x == gameObject.transform.position.x && hit.transform.position.z == gameObject.transform.position.z;

                }
            }
        }
        return false;
    }
    public bool IsWallBetween(GameObject object1, GameObject object2, float radius = 0.1f)
    {
        Vector3 start = object1.transform.position;
        Vector3 end = object2.transform.position;

        // Calculate the direction and distance between the objects
        Vector3 direction = (end - start).normalized;
        float distance = Vector3.Distance(start, end);

        // Perform a capsule cast to check for objects directly in the way
        RaycastHit[] hits = Physics.SphereCastAll(start, radius, direction, distance);

        // Iterate through all hits to check for a wall
        foreach (RaycastHit hit in hits)
        {

            if (HasTagInHierarchy(hit.collider.gameObject, "Wall"))
            {
                //    Debug.Log($"Hit: {hit.collider.name}");
                //     Debug.DrawLine(start, end, Color.red, 1.0f);
                return true; // A wall or its parent is in between
            }
        }

        return false; // No wall detected
    }


    private bool HasTagInHierarchy(GameObject obj, string tag)
    {
        Transform current = obj.transform;

        while (current != null)
        {
            if (current.CompareTag(tag))
            {
                return true; // Tag found on this object or its parent
            }
            current = current.parent; // Move to the parent
        }

        return false; // Tag not found in the hierarchy
    }
    public static bool HasLineOfSight(Transform objectA, Transform objectB, LayerMask layerMask = default)
    {
        if (objectA == null || objectB == null)
        {
            Debug.LogWarning("One or both objects are null!");
            return false;
        }

        Vector3 direction = (objectB.position - objectA.position).normalized;
        float distance = Vector3.Distance(objectA.position, objectB.position);

        // Perform a raycast to check for obstructions
        if (Physics.Raycast(objectA.position, direction, out RaycastHit hit, distance, layerMask))
        {
            // If the hit object is not the target object, line of sight is blocked
            if (hit.transform != objectB)
            {
                return false;
            }
        }

        // No obstructions or target object hit
        return true;
    }
}
