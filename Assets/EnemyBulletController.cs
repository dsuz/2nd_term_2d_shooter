using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    [SerializeField] float m_speed = 0.5f;
    Rigidbody2D m_rb2d;

    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();

        // プレイヤーの位置を取得する
        GameObject m_player = GameObject.FindGameObjectWithTag("Player");
        Vector2 direction = (m_player.transform.position - this.transform.position).normalized;
        m_rb2d.AddForce(direction, ForceMode2D.Impulse);
    }
}
