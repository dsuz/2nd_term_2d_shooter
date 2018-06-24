using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵を生成するクラス
/// </summary>
public class EnemyGenerationController : MonoBehaviour
{
    /// <summary>敵が生成される場所</summary>
    [SerializeField] Transform[] m_spawnPoints;
    /// <summary>生成する敵のプレハブ</summary>
    [SerializeField] GameObject[] m_enemyPrefabs;
    /// <summary>敵が生成される間隔（秒）</summary>
    [SerializeField] float m_enemyGenerationInterval = 3.0f;
    float m_timer;

    void Update()
    {
        m_timer += Time.deltaTime;

        if (m_timer > m_enemyGenerationInterval)
        {
            // 複数の敵のプレハブから、どの敵を生成するかランダムに選ぶ
            int i = Random.Range(0, m_enemyPrefabs.Length);
            GameObject enemy = m_enemyPrefabs[i];

            // 各 Spawn Point に敵を生成する
            foreach (Transform spawnPoint in m_spawnPoints)
            {
                Instantiate(enemy, spawnPoint.position, Quaternion.identity);
            }

            m_timer = 0f;
        }
    }
}
