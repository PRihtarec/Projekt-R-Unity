using UnityEngine;

public class VentCloseTrigger : MonoBehaviour
{
    [SerializeField] private VentGame ventGame;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player") && ventGame != null && ventGame.isVentOpen())
        {
            Debug.Log("Vent se zatvara!");
            ventGame.StartCoroutine(ventGame.MoveVent(1, 0f));
            ventGame.ventToggle();
        }
    }
}