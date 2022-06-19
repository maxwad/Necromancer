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
    private float spawnOffset = 5f;
    private Coroutine spawnCoroutine;
    private float waitNextEnemyTimeFast = 0.1f;
    private float waitNextEnemyTimeSlow = 0.5f;
    private WaitForSeconds waitNextEnemyFast;
    private WaitForSeconds waitNextEnemySlow;
    private WaitForSeconds waitNextEnemy;

    //private BattleMap battleMap;

    private bool[,] battleMap;

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
        battleMap = GetComponent<BattleMap>().battleArray;
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

            if (randomPosition != Vector3.zero)
            {
                int randomIndex = 0;
                bool finded = false;

                while (finded == false && commonQuantity != 0)
                {
                    randomIndex = Random.Range(0, enemiesList.Count);
                    if (enemiesQuantityList[randomIndex] != 0)
                    {
                        finded = true;
                    }

                }
                //for (int i = 0; i < enemiesQuantityList.Count; i++)
                //{
                //    currentProbably[i] = Mathf.Round((enemiesQuantityList[i] / commonQuantity) * 100);
                //}

                GameObject enemy = Instantiate(enemiesList[randomIndex], randomPosition, Quaternion.identity);
                enemy.transform.SetParent(enemiesContainer.transform);
                enemiesOnTheMap.Add(enemy);
                enemiesQuantityList[randomIndex]--;
            }
        }
    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 randomPosition = Vector3.zero;
        bool finded = false;
        int randomIndex;

        randomIndex = Random.Range(0, spawnPositions.Count);
        randomPosition = spawnPositions[randomIndex].transform.position;

        //while (finded == false)
        //{
        //    randomIndex = Random.Range(0, spawnPositions.Count);

        //    if (spawnPositions[randomIndex].transform.localScale.z == 1)
        //    {
        //        randomPosition = spawnPositions[randomIndex].transform.position;
        //        finded = true;
        //    }
        //}

        int tempX = Mathf.RoundToInt(Random.Range((randomPosition.x - spawnOffset) * 10, (randomPosition.x + spawnOffset + 1) * 10) / 10);
        int tempY = Mathf.RoundToInt(Random.Range((randomPosition.y - spawnOffset) * 10, (randomPosition.y + spawnOffset + 1) * 10) / 10);

        if (tempX <= 0 + 1 || tempX >= battleMap.GetLength(0) - 1 ||
            tempY <= 0 + 1 || tempY >= battleMap.GetLength(1) - 1)
        {
            return Vector3.zero;
        }

        if (battleMap[tempX, tempY] == false)
        {
            return Vector3.zero;
        }

        int checkGap = 2;
        if ((tempX - checkGap) <= 0 || (tempX + checkGap) >= battleMap.GetLength(0) ||
           (tempY - checkGap) <= 0 || (tempY + checkGap) >= battleMap.GetLength(1))
        {
            return Vector3.zero;
        }
        else
        {
            for (int x = tempX - checkGap / 2; x <= tempX + checkGap / 2; x++)
            {
                for (int y = tempY - checkGap / 2; y <= tempY + checkGap / 2; y++)
                {
                    if (battleMap[tempX, tempY] == false) return Vector3.zero;
                }
            }
        }



        //Debug.Log(new Vector3(Mathf.RoundToInt(tempX), Mathf.RoundToInt(tempY), randomPosition.z));
        return new Vector3(tempX, tempY, randomPosition.z);
    }
}
