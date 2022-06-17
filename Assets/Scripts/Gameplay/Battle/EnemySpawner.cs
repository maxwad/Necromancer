using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private List<GameObject> enemiesList;
    private List<int> enemiesQuantityList;
    [SerializeField] private GameObject enemiesContainer;
    private List<GameObject> enemiesOnTheMap = new List<GameObject>();

    [SerializeField] private List<GameObject> spawnPositions;

    private bool canISpawn = false;
    private int enemyEnoughCount = 100;
    private Coroutine spawnCoroutine;
    private float waitNextEnemyTimeFast = 0.1f;
    private float waitNextEnemyTimeSlow = 0.3f;
    private WaitForSeconds waitNextEnemyFast;
    private WaitForSeconds waitNextEnemySlow;
    private WaitForSeconds waitNextEnemy;

    public void Initialize(List<GameObject> enemiesPrefabs, List<int> quantity)
    {
        enemiesList = enemiesPrefabs;
        enemiesQuantityList = quantity;
        waitNextEnemyFast = new WaitForSeconds(waitNextEnemyTimeFast);
        waitNextEnemySlow = new WaitForSeconds(waitNextEnemyTimeSlow);
        waitNextEnemy = waitNextEnemyFast;
    }

    private void Update()
    {
        if (enemiesOnTheMap.Count > enemyEnoughCount)
            waitNextEnemy = waitNextEnemySlow;
        else
            waitNextEnemy = waitNextEnemyFast;

    }
    public void ReadyToSpawnEnemy()
    {
        canISpawn = true;
        spawnCoroutine = StartCoroutine(SpawnEnemy());
    }

    public void StopSpawnEnemy()
    {
        canISpawn = false;
        foreach (Transform child in enemiesContainer.transform)
            Destroy(child.gameObject);

        enemiesOnTheMap.Clear();
    }

    private IEnumerator SpawnEnemy()
    {
        int commonQuantity;
        List<float> currentProbably = new List<float>();

        while (canISpawn == true)
        {
            yield return waitNextEnemy;

            //check current quantity
            commonQuantity = 0;
            for (int i = 0; i < enemiesQuantityList.Count; i++)
            {
                commonQuantity += enemiesQuantityList[i];                
            }

            if (commonQuantity == 0)
            {
                canISpawn = false;
                StopCoroutine(spawnCoroutine);
                break;
            }

            Vector3 randomPosition = GetSpawnPosition();
            int randomIndex = 0;
            bool finded = false;

            while (finded == false && commonQuantity != 0)
            {
                randomIndex = Random.Range(0, enemiesList.Count);
                if (enemiesQuantityList[randomIndex] != 0)
                {
                    finded = true;
                    enemiesQuantityList[randomIndex]--;
                }
                    
            }
            //for (int i = 0; i < enemiesQuantityList.Count; i++)
            //{
            //    currentProbably[i] = Mathf.Round((enemiesQuantityList[i] / commonQuantity) * 100);
            //}

            GameObject enemy = Instantiate(enemiesList[randomIndex], randomPosition, Quaternion.identity);
            enemy.transform.SetParent(enemiesContainer.transform);
            enemiesOnTheMap.Add(enemy);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 randomPosition = Vector3.zero;
        bool finded = false;
        int randomIndex;
        while (finded == false)
        {
            randomIndex = Random.Range(0, spawnPositions.Count);

            if (spawnPositions[randomIndex].transform.localScale.z == 1)
            {
                randomPosition = spawnPositions[randomIndex].transform.position;
                finded = true;
            }
        }
        return randomPosition;
    }
}
