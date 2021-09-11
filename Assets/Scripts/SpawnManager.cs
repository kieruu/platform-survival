using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    public Vector2 spawnRange;

    private int m_EnemyCount;
    private int m_Waves;
    private HudManager m_HudManager;


    void Awake()
    {
        m_Waves = 0;
        enabled = false;

    }

    void Start()
    {
        m_HudManager = GameObject
            .Find("HUD")
            .GetComponent<HudManager>();
        m_HudManager.UpdateWaveText(m_Waves);
    }

    void Update()
    {
        m_EnemyCount = FindObjectsOfType<EnemyController>().Length;
        if (m_EnemyCount == 0)
        {
            m_Waves++;
            SpawnEnemy();
            SpawnPowerup();

            m_HudManager.UpdateWaveText(m_Waves);
        }
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < m_Waves; i++)
        {
            SpawnEntity(enemyPrefab);
        }
    }

    private void SpawnPowerup()
    {
        SpawnEntity(powerupPrefab);
    }
    private void SpawnEntity(GameObject entity)
    {
        Vector3 spawnPos = new Vector3(
                Random.Range(spawnRange.x, spawnRange.y),
                entity.transform.position.y,
                Random.Range(spawnRange.x, spawnRange.y));

        Instantiate<GameObject>(
            entity,
            spawnPos,
            entity.transform.rotation);
    }

    public void StartSpawning()
    {
        enabled = true;
    }
}
