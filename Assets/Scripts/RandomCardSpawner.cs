using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;  

    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned.");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        transform.position = spawnPoints[randomIndex].position;
        transform.rotation = spawnPoints[randomIndex].rotation;
    }
}
