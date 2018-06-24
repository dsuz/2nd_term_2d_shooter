using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>オブジェクトを動かすためのクラス</summary>
public class ObjectMovingController : MonoBehaviour
{
    /// <summary>動き方のタイプ</summary>
    public MoveType m_moveType = MoveType.StraightDown;
    /// <summary>動く速さ（正確には加える力）</summary>
    public float m_speed = 1.5f;
    Rigidbody2D m_rb2d;
    /// <summary>ここより下に行ったら敵を消す</summary>
    float m_border = -10f;

    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();

        if (m_moveType == MoveType.StraightDown)
        {
            m_rb2d.AddForce(Vector2.down * m_speed, ForceMode2D.Impulse);   // 下向きの力を加える
        }
        else if (m_moveType == MoveType.FollowPlayer)
        {
            if (m_rb2d.drag == 0)
            {
                m_rb2d.drag = 0.5f; // 設定し忘れていた場合は、あまり早くなりすぎないようにここを調整する。
            }
        }
    }

    void Update()
    {
        // 画面外の下の方に行ったら敵を消してしまう
        if (this.transform.position.y < m_border)
        {
            Destroy(this.gameObject);
        }

        if (m_moveType == MoveType.FollowPlayer)
        {
            // プレイヤーを追いかける
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector2 direction = (player.transform.position - this.transform.position).normalized;
            m_rb2d.AddForce(direction * m_speed, ForceMode2D.Force);
        }
    }
}

/// <summary>動き方の種類</summary>
public enum MoveType
{
    /// <summary>まっすぐ下へ移動する</summary>
    StraightDown,
    /// <summary>プレイヤーを追いかける</summary>
    FollowPlayer,
}