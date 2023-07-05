using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnner : MonoBehaviour
{
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private Transform[] spawnPoint;

    [SerializeField] private GameObject whiteCLownPrefab;
    [SerializeField] private Transform whiteClownSpawnPoint;

    private void Start()
    {
        Instantiate(zombiePrefab, spawnPoint[1].position, Quaternion.identity);
        StartCoroutine(ZombieSpawnnerCouroutine());

        Instantiate(whiteCLownPrefab, whiteClownSpawnPoint.position, Quaternion.identity);
        StartCoroutine(WhiteCLownCoroutine());
    }

  

    public void ZombieSpawnner()
    {
        int randomIndex = Random.Range(0, spawnPoint.Length);
        var obj=Instantiate(zombiePrefab, spawnPoint[randomIndex].position, Quaternion.identity);
        obj.transform.SetParent(transform);
    }

    IEnumerator ZombieSpawnnerCouroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(16);
            ZombieSpawnner();
        } 
    }



    public void WhiteCLownSpawnner()
    {
        var obj = Instantiate(whiteCLownPrefab, whiteClownSpawnPoint.position, Quaternion.identity);
        obj.transform.SetParent(transform);
    }

    IEnumerator WhiteCLownCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(35);
            WhiteCLownSpawnner();
        }
    }
}
