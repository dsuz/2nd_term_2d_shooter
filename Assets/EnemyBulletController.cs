using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>敵の弾を制御するクラス</summary>
public class EnemyBulletController : MonoBehaviour
{
    /// <summary>弾が飛ぶ速度</summary>
    [SerializeField] float m_speed = 0.5f;
    Rigidbody2D m_rb2d;

    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();

        GameObject m_player = GameObject.FindGameObjectWithTag("Player");
        Vector2 direction = (m_player.transform.position - this.transform.position).normalized;     // プレイヤーのいる方向の単位ベクトルを計算する
        m_rb2d.AddForce(direction, ForceMode2D.Impulse);    // プレイヤーに向かって弾を飛ばす
    }
}
