using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTriggerHallway : MonoBehaviour
{
    [SerializeField] private GameObject monster;
    private MonsterControllerHallway monsterController;
    [SerializeField] private Animator lights24;
    [SerializeField] private Animator lights3;

    [SerializeField] private Animator alarm;
    [SerializeField] private Animator alarm1;
    [SerializeField] private Animator alarm2;
    [SerializeField] private Animator alarm3;
    [SerializeField] private Animator alarm4;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player")){
            lights24.SetTrigger("lightsoff");
            lights3.SetTrigger("chase");
            //alarm.SetTrigger("startalarm");
            StartCoroutine(TriggerAlarmAfterDelay(alarm,3f));
            StartCoroutine(TriggerAlarmAfterDelay(alarm1,3f));
            StartCoroutine(TriggerAlarmAfterDelay(alarm2,3f));
            StartCoroutine(TriggerAlarmAfterDelay(alarm3,3f));
            StartCoroutine(TriggerAlarmAfterDelay(alarm4,3f));
            if (!monster.activeSelf)
                {
                monster.SetActive(true); // Activate the monster
                }
                /*
                monsterController = monster.GetComponent<MonsterControllerHallway>();

                if (monsterController != null)
                {
                monsterController.setAggro(true); // Set aggro
                }
                else
                {
                   Debug.LogError("MonsterControllerHallway nije pronađen na objektu 'monster'. Provjeri da li je komponenta dodana.");
                }*/
            
            }
    }
    private IEnumerator TriggerAlarmAfterDelay(Animator alarm, float delaytime)
{
    // Wait for 2 seconds
    yield return new WaitForSeconds(delaytime);


    // Trigger the alarm after the delay
    alarm.SetTrigger("startalarm");
}
}
