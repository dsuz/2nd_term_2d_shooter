using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>敵の共通動作をコントロールするクラス</summary>
public class EnemyController : MonoBehaviour
{
    /// <summary>敵が発射する弾のプレハブ</summary>
    [SerializeField] GameObject m_bulletPrefab;
    /// <summary>敵が弾を撃つ間隔</summary>
    [SerializeField] float m_shotInterval = 1.5f;
    /// <summary>タイマーとして使う変数</summary>
    float m_timer;

    void Start()
    {

    }

    void Update()
    {
        m_timer += Time.deltaTime;  // タイマーに、前フレームからの経過時間を足していく
        
        if (m_timer > m_shotInterval)   // タイマーがインターバルを超えたら
        {
            Instantiate(m_bulletPrefab, this.transform.position, Quaternion.identity);  // 弾を発射して
            m_timer = 0;    // タイマーをリセットする
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BulletController>())  // 衝突相手のオブジェクトに BulletController が追加されていたら
        {
            Destroy(gameObject);    // 自分自身を消す
        }
    }
}
