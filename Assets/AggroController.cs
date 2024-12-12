using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private MonsterController monsterController;
    float distanceToPlayer;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        monsterController = gameObject.GetComponent<MonsterController>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if (distanceToPlayer < 1){
            if (!monsterController.getAggro()){
                monsterController.setAggro(true);
            }
        }

    }
}
