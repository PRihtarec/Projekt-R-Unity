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
    float crouchingRange = 1f;
    float walkingRange = 2f;
    float sprintingRange = 5f;

    private GameObject flashlight;
    private Light flashlightLight;
    private float flashlightRange = 10f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        Player = GameObject.FindGameObjectWithTag("Player");
        monsterController = gameObject.GetComponent<MonsterController>();
        playerMovement = Player.GetComponent<PlayerMovement>();
        flashlight = GameObject.FindGameObjectWithTag("flashlight");
        flashlightLight = flashlightLight.GetComponent<Light>();
        flashlightRange = flashlightLight.range;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (CheckPlayerInRange(range, player, gameObject) && playerMovement.IsPlayerMovingByInput())
        {
            monsterController.setAggro(true);
        }
        if (CheckMonsterFlashlight()){
            monsterController.setAggro(true);
        }
    }

       public bool CheckPlayerInRange(float range, GameObject player, GameObject mainObject)
    {
        distanceToPlayer = Vector3.Distance(mainObject.transform.position, player.transform.position);
        return (distanceToPlayer < range);

    }

    public bool CheckMonsterFlashlight(){
        Vector3 directionToTarget = gameObject.transform.position - flashlight.transform.position;
        float distanceToTarget = directionToTarget.magnitude;

        if (distanceToTarget <= flashlightRange)
        {
            directionToTarget.Normalize();

            // Check if within cone
            float angleToTarget = Vector3.Angle(flashlight.transform.forward, directionToTarget);
            if (angleToTarget <= flashlightLight.spotAngle / 2)
            {
                // Perform a raycast to confirm no obstruction
                if (Physics.Raycast(flashlight.transform.position, directionToTarget, out RaycastHit hit, flashlightRange))
                {
                    return hit.transform == gameObject.transform;
                    
                }
            }
        }
        return false;
    }
}
