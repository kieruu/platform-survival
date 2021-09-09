using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Vector2 spawnRange;

    private int m_EnemyCount;
    private int m_Waves;

    // Start is called before the first frame update
    void Start()
    {
        m_Waves = 1;
        SpawnEnemy();
    }

    void Update()
    {
        m_EnemyCount = FindObjectsOfType<EnemyController>().Length;
        if (m_EnemyCount == 0)
        {
            m_Waves++;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < m_Waves; i++)
        {
            Vector3 spawnPos = new Vector3(
                Random.Range(spawnRange.x, spawnRange.y),
                enemyPrefab.transform.position.y,
                Random.Range(spawnRange.x, spawnRange.y));

            Instantiate<GameObject>(
                enemyPrefab, 
                spawnPos, 
                enemyPrefab.transform.rotation);
        }
        
    }
}
