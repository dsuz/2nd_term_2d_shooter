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
    /// <summary>マージン</summary>
    [SerializeField] float m_margin = 0.1f;

    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();

        if (m_moveType == MoveType.StraightDown || m_moveType == MoveType.LShapeChasePlayer)
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
        if (this.transform.position.y < m_border || Mathf.Pow(this.transform.position.x, 2) > Mathf.Pow(m_border, 2))
        {
            Destroy(this.gameObject);
        }

        if (m_moveType == MoveType.FollowPlayer || m_moveType == MoveType.LShapeChasePlayer)
        {
            // プレイヤーを追いかける
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player)
            {
                if (m_moveType == MoveType.FollowPlayer)
                {
                    Vector2 direction = (player.transform.position - this.transform.position).normalized;
                    m_rb2d.AddForce(direction * m_speed, ForceMode2D.Force);
                }
                else if (m_moveType == MoveType.LShapeChasePlayer && Mathf.Abs(m_rb2d.velocity.y) > 0.1f)   // 縦にある程度の速さで動いている時
                {
                    // y 軸がプレイヤーと同じくらいになったら
                    if (Mathf.Abs(this.transform.position.y - player.transform.position.y) < m_margin)
                    {
                        m_rb2d.velocity = Vector2.zero; // いったん止める
                        Vector2 dir = Vector2.zero;
                        if (player.transform.position.x - this.transform.position.x > 0)
                        {
                            dir = Vector2.right;    // プレイヤーの方向
                        }
                        else
                        {
                            dir = Vector2.left;     // プレイヤーの方向
                        }
                        m_rb2d.AddForce(dir * m_speed, ForceMode2D.Impulse);    // プレイヤーの方向に力を加える
                    }
                }
            }
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
    /// <summary>最初まっすぐ下へ移動し、プレイヤーと同じ Y 軸になったらプレイヤーの方向に 90 度曲がる</summary>
    LShapeChasePlayer,
}