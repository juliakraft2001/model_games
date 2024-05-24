using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Julia
{
    public class Spielmanager : MonoBehaviour
    {
        public GameObject blasePrefab;
        private float spawnTimer = 0.5f; // Längere Zeit bis zum ersten Spawn
        private float spawnInterval = 0.7f; // Längere Intervalle zwischen den Spawns
        private int blasenProSpawn = 2; // Weniger Blasen pro Spawn

        void Start()
        {
            // Beginne die Blasen kontinuierlich zu spawnen
            StartCoroutine(SpawnBlasenKontinuierlich());
        }

        IEnumerator SpawnBlasenKontinuierlich()
        {
            while (true)
            {
                for (int i = 0; i < blasenProSpawn; i++)
                {
                    SpawnBlase();
                }
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        void SpawnBlase()
        {
            float xPosition = Random.Range(-8f, 8f);
            float zPosition = Random.Range(-8f, 8f);
            Vector3 spawnPosition = new Vector3(xPosition, -5f, zPosition);
            Instantiate(blasePrefab, spawnPosition, Quaternion.identity);
        }
    }
}
