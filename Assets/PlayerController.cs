using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>プレイヤーの移動速度</summary>
    [SerializeField] float m_moveSpeed = 5f;
    Rigidbody2D m_rb2d;
    /// <summary>弾のプレハブ</summary>
    [SerializeField] GameObject m_bulletPrefab;

    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");   // 垂直方向の入力を取得する
        float v = Input.GetAxisRaw("Vertical");     // 水平方向の入力を取得する
        Vector2 dir = new Vector2(h, v).normalized; // 進行方向の単位ベクトルを作る (dir = direction) 
        m_rb2d.velocity = dir * m_moveSpeed;        // 単位ベクトルにスピードをかけて速度ベクトルにする

        // 左クリックまたはスペースで弾を発射する
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(m_bulletPrefab, this.transform.position, Quaternion.identity);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
        }
    }
}
