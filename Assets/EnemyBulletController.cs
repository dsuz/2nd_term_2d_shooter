using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>敵の弾を制御するクラス</summary>
public class EnemyBulletController : MonoBehaviour
{
    /// <summary>弾が飛ぶ速度</summary>
    [SerializeField] float m_speed = 0.5f;
    /// <summary>弾が飛ぶ範囲</summary>
    [SerializeField] float m_border = 10f;
    Rigidbody2D m_rb2d;

    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();

        GameObject m_player = GameObject.FindGameObjectWithTag("Player");
        Vector2 direction = (m_player.transform.position - this.transform.position).normalized;     // プレイヤーのいる方向の単位ベクトルを計算する
        m_rb2d.AddForce(direction, ForceMode2D.Impulse);    // プレイヤーに向かって弾を飛ばす
    }

    void Update()
    {
        // 縦方向または横方向に -10 ~ 10 の範囲から飛び出したら弾を消す
        if (Mathf.Abs(this.transform.position.x) > m_border
            || Mathf.Abs(this.transform.position.y) > m_border)
        {
            Destroy(this.gameObject);
        }
    }
}
