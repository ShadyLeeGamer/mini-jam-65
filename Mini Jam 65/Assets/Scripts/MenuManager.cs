using System.Collections;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public float spawnDelay;
    public GameObject newBomb;
    public Transform[] spawnPoints;
    public int boomCount;

    void Start()
    {
        SpawnNewBomb();
    }

    public void SpawnNewBomb()
    {
        if(boomCount % 2 == 0)
            StartCoroutine(SpawningNewBomb());
    }

    IEnumerator SpawningNewBomb()
    {
        yield return new WaitForSeconds(spawnDelay);

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform randomSpawnPoint = spawnPoints[randomIndex];
        int randomIndex2 = Random.Range(0, spawnPoints.Length);
        Transform randomSpawnPoint2 = spawnPoints[randomIndex2];

        Instantiate(newBomb, randomSpawnPoint.position, Quaternion.identity);
        Instantiate(newBomb, randomSpawnPoint2.position, Quaternion.identity);
    }
}