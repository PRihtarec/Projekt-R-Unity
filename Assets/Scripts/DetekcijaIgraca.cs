using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetekcijaIgraca : MonoBehaviour
{
    [SerializeField] private Camera monsterCamera;
    [SerializeField] private Animator monsterAnimator;
    [SerializeField] private Light monsterLight;
    [SerializeField] private ParticleSystem bloodBurstEffect;
    [SerializeField] private FadeToBlackAndLoadMenu fadeManager;
    [SerializeField] private GameObject youDiedScreen;
    private void Start()
    {
        if(monsterCamera != null){
            monsterCamera.enabled = false;
        } 
        if (monsterLight != null)
        {
            monsterLight.enabled = false;
        }
        if (bloodBurstEffect != null)
        {
            bloodBurstEffect.Stop();
        }
    }

    private void OnTriggerEnter(Collider PlayerTrigger)
    {
        Debug.Log("Čudovište se sudarilo s " + PlayerTrigger.gameObject.name);

        if (PlayerTrigger.CompareTag("player"))
        {
            // jumpscare
            if (monsterCamera != null)
            {
                Debug.Log("Enabling monster camera.");
                monsterCamera.enabled = true;
            }

            if (monsterAnimator != null)
            {
                monsterAnimator.SetTrigger("Bite");
            }

            if (monsterLight != null)
            {
                monsterLight.enabled = true;
            }

            if (bloodBurstEffect != null)
            {
                bloodBurstEffect.Play();
            }            
            Destroy(PlayerTrigger.transform.root.gameObject);
            Debug.Log("Player destroyed.");

            if (youDiedScreen != null)
            {
                youDiedScreen.SetActive(true);
            }

            if (fadeManager != null)
            {
                fadeManager.TriggerFadeAndLoadMenu();
            }
        }
        
    }
}
