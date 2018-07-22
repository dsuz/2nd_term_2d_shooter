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
    /// <summary>ぶつかった時にスクロールを止めるか</summary>
    [SerializeField] bool m_stopScroll = false;
    /// <summary>スクロールコントローラーを設定する。Stop Scroll にチェックを入れた時のみ必要になる</summary>
    [SerializeField] ScrollController m_scrollController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !m_isGenerated)
        {
            // スクロールを止める
            if (m_stopScroll)
            {
                StartCoroutine(StopScroll());
            }
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

    /// <summary>
    /// スクロールスピードを徐々に遅くする
    /// </summary>
    /// <returns></returns>
    IEnumerator StopScroll()
    {
        while (Mathf.Abs(m_scrollController.ScrollSpeed) > 0.05f)
        {
            m_scrollController.ScrollSpeed *= 0.99f;
            yield return null;
        }
        m_scrollController.ScrollSpeed = 0f;
    }
}
