using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetekcijaIgraca : MonoBehaviour
{
    private void OnTriggerEnter(Collider PlayerTrigger)
    {
        Debug.Log("Čudovište se sudarilo s" + PlayerTrigger.gameObject.name );
    if (PlayerTrigger.CompareTag("player"))
        {
            
            Destroy(PlayerTrigger.transform.root.gameObject); //Ubija igrača i to cijeli folder s kamerom
            Debug.Log("Ubilo te!");
            
        }
    }
}
