using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyGenerationTrigger : MonoBehaviour
{
    /// <summary>既に敵を生成したか</summary>
    bool m_isGenerated = false;
    /// <summary>敵が生成される場所</summary>
    [SerializeField] Transform[] m_spawnPoints;
    /// <summary>生成する敵のプレハブ</summary>
    [SerializeField] GameObject m_enemyPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !m_isGenerated)
        {
            GenerateEnemies();
            m_isGenerated = true;
        }
    }

    void GenerateEnemies()
    {
        // 各 spawn point に敵を spawn する
        foreach (var spawnPoint in m_spawnPoints)
        {
            Instantiate(m_enemyPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
