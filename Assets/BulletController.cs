using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    /// <summary>弾の飛ぶ速度</summary>
    [SerializeField] float m_bulletSpeed = 10f;
    Rigidbody2D m_rb2d;
    /// <summary>ここまで飛んだら弾を消す</summary>
    [SerializeField] float m_bulletTravelDistance = 20f;
    AudioSource m_audioSource;

    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
        Vector3 dir = Vector2.up * m_bulletSpeed;   // 弾が飛ぶ速度ベクトルを計算する
        m_rb2d.velocity = dir;                      // 速度ベクトルを弾にセットする
        m_audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 弾の y 座標が m_bulletTravelDistance になるまで飛んだら、弾を消す
        if (this.transform.position.y > m_bulletTravelDistance)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            m_audioSource.Play();
        }
    }
}
