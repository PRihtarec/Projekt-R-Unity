using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTriggerHallway : MonoBehaviour
{
    [SerializeField] private GameObject monster;
    private MonsterController monsterController;
    [SerializeField] private Animator lights24;
    [SerializeField] private Animator lights3;
    
    [SerializeField] private Animator alarm;
    [SerializeField] private Animator alarm1;
    [SerializeField] private Animator alarm2;
    [SerializeField] private Animator alarm3;
    [SerializeField] private Animator alarm4;
    [SerializeField] private Camera turnOffMonsterCamera;  // za jumpscare
    [SerializeField] private Light turnOffMonsterLight;
    [SerializeField] private ParticleSystem turnOffBloodBurstEffect;

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
                if (turnOffMonsterCamera != null)  // gasimo kameru na cudovistu (palimo u DetekcijaIgraca.cs)
                {
                turnOffMonsterCamera.enabled = false;
                }
                if(turnOffMonsterLight != null)
                {
                turnOffMonsterLight.enabled = false;
                }
                if(turnOffBloodBurstEffect != null){
                    turnOffBloodBurstEffect.Stop();
                }
                monsterController = monster.GetComponent<MonsterController>();

                monsterController.setAggro(true); // Immediately set aggro
            
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
