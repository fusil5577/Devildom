using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour
{
        public GameObject BossMonsterPrefab;
        public Transform[] spawnPoints;


        void Start()
    {
        StartCoroutine(SpawnBossRoutine());
    }
    IEnumerator SpawnBossRoutine()
    {
        while (true)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Vector3 spawnPosition = spawnPoints[spawnIndex].position;

            GameObject newBoss = Instantiate(BossMonsterPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
