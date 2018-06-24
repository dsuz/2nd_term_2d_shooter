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
    /// <summary>弾の連射間隔（単位：秒）</summary>
    [SerializeField] float m_fireInterval = 0.05f;
    /// <summary>連射間隔を制御するためのタイマー</summary>
    float m_bulletTimer;
    AudioSource m_audioSource;

    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
        m_audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");   // 垂直方向の入力を取得する
        float v = Input.GetAxisRaw("Vertical");     // 水平方向の入力を取得する
        Vector2 dir = new Vector2(h, v).normalized; // 進行方向の単位ベクトルを作る (dir = direction) 
        m_rb2d.velocity = dir * m_moveSpeed;        // 単位ベクトルにスピードをかけて速度ベクトルにする

        // 左クリックまたはスペースで弾を発射する
        /*
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(m_bulletPrefab, this.transform.position, Quaternion.identity);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
            m_audioSource.Play();   // 発射音を鳴らす
        }
        */

        // 左クリックまたはスペースを押したままにしている間、弾を連射する
        if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
        {
            // タイマーがリセットされた直後は弾を発射できる
            if (m_bulletTimer == 0f)
            {
                Instantiate(m_bulletPrefab, this.transform.position, Quaternion.identity);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
                m_audioSource.Play();   // 発射音を鳴らす
            }

            m_bulletTimer += Time.deltaTime;    // Time.deltaTime は「前回 Update() が実行されてから経過した時間」である。これを積算することにより m_bulletTimer は「発射ボタンを押し続けている時間」になる

            // m_bulletTimer = 「発射ボタンを押し続けている時間」が m_fireInterval を超えたら、タイマーをリセットする。これで次回の Update() 実行時には弾を発射できる
            if (m_bulletTimer > m_fireInterval)
            {
                m_bulletTimer = 0f;
            }
        }
        else
        {
            // 発射ボタンを話したらタイマーをリセットする
            if (m_bulletTimer > 0f)
            {
                m_bulletTimer = 0f;
            }
        }
    }
}
