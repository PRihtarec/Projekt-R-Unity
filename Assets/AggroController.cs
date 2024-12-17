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
    float walkingRange = 2f;
    float sprintingRange = 5f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        Player = GameObject.FindGameObjectWithTag("Player");
        monsterController = gameObject.GetComponent<MonsterController>();
        playerMovement = Player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
        float range = walkingRange;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            range = sprintingRange;
        }
        if (CheckPlayerInRange(range, player, gameObject) && playerMovement.IsPlayerMovingByInput())
        {
            monsterController.setAggro(true);
        }
    }

       public bool CheckPlayerInRange(float range, GameObject player, GameObject mainObject)
    {
        distanceToPlayer = Vector3.Distance(mainObject.transform.position, player.transform.position);
        return (distanceToPlayer < range);

    }
}
