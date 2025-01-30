using System.Collections.Generic;
using UnityEngine;

public class PolugaScript : MonoBehaviour
{
    public AudioSource odbrojavanjeSource;
    private bool isClickable = true; //cooldown za klikanje
    [SerializeField] private Animator animator; //animator za upravljanje polugom

    [SerializeField] private float cooldownTime = 30f; // vrijeme pauze (mijenjati nakon testiranja)

    private string activateTrigger = "electricityOn";  //trigeri za animacije
    private string deactivateTrigger = "electricityOff";

    private string lightObjectName = "EndGameLights"; //ime prefab taga svijetla koje koristim

    private List<Animator> lightAnimators = new();

    void Start()
    {
        //ucitavanje svih animatora svijetla
        GameObject[] lights = GameObject.FindGameObjectsWithTag(lightObjectName);

        foreach (GameObject light in lights)
        {
            Animator lightAnimator = light.GetComponent<Animator>();
            if (lightAnimator != null)
            {
                lightAnimators.Add(lightAnimator);
            }
            else
            {
                Debug.LogWarning("Animator nije pronađen na objektu: " + light.name);
            }
        }
    }

    public void ActivateTrigger()
    {
        if (!isClickable)
        {
            Debug.Log("Trigger is on cooldown.");
            return; //ako je cooldown aktivan nema interakcije
        }
        odbrojavanjeSource.Play();
        animator.SetTrigger(activateTrigger);
        foreach (Animator lightAnimator in lightAnimators)
        {
            lightAnimator.SetTrigger(activateTrigger);
        }


        Debug.Log($"Poluga aktivirana. Cooldown vrijeme: {cooldownTime} sekundi.");

        isClickable = false;

        // Cooldown za polugu, ne moze se kliknuti
        Invoke(nameof(ResetTrigger), cooldownTime+1f);

        //Gasi animator
        Invoke(nameof(ResetAnimator), cooldownTime);
    }

    private void ResetAnimator()
    {
        if (animator != null)
        {
            animator.SetTrigger(deactivateTrigger);
            foreach (Animator lightAnimator in lightAnimators)
            {
                lightAnimator.SetTrigger(deactivateTrigger);
            }
            Debug.Log("Poluga opet spuštena");
        }
    }

    private void ResetTrigger()
    {
        isClickable = true; // Ponovno omogućuje klikanje
        odbrojavanjeSource.Stop();
        Debug.Log("Poluga se opet može podignuti");
    }
}