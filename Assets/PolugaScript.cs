using UnityEngine;

public class PolugaScript : MonoBehaviour
{
    private bool isClickable = true; //cooldown za klikanje
    [SerializeField] private Animator animator; //animator za upravljanje polugom

    [SerializeField] private float cooldownTime = 30f; // vrijeme pauze (mijenjati nakon testiranja)

    private string activateTrigger = "electricityOn";  //trigeri za animacije
    private string deactivateTrigger = "electricityOff";

    public void ActivateTrigger()
    {
        if (!isClickable)
        {
            Debug.Log("Trigger is on cooldown.");
            return; //ako je cooldown aktivan nema interakcije
        }

        animator.SetTrigger(activateTrigger);

        Debug.Log("Trigger activated!");

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
            Debug.Log("Poluga opet ne radi");
        }
    }

    private void ResetTrigger()
    {
        isClickable = true; // Ponovno omogućuje klikanje
        Debug.Log("Poluga se opet može podignuti");
    }
}