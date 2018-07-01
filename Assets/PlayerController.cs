using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    /// <summary>プレイヤーの移動速度</summary>
    [SerializeField] float m_moveSpeed = 5f;
    Rigidbody2D m_rb2d;
    /// <summary>弾のプレハブ</summary>
    [SerializeField] GameObject m_bulletPrefab;
    /// <summary>ゴッドモード（True の時、無敵モードになる）</summary>
    [SerializeField] bool m_godMode = false;
    /// <summary>メッセージ表示用の Text</summary>
    [SerializeField] Text m_messageText;
    /// <summary>やられた時の爆発エフェクト</summary>
    [SerializeField] GameObject m_explosionPrefab;
    /// <summary>やられた時の爆発音</summary>
    [SerializeField] AudioClip m_explosionAudioClip;

    AudioSource m_audioSource;

    void Start()
    {
        m_rb2d = GetComponent<Rigidbody2D>();
        m_audioSource = GetComponent<AudioSource>();
        m_messageText.text = "";    // メッセージを消す
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
            m_audioSource.Play();   // 発射音を鳴らす
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!m_godMode)
        {
            if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
            {
                // 演出を実行する
                m_messageText.text = "GAME OVER";   // メッセージを表示する
                Instantiate(m_explosionPrefab, this.transform.position, Quaternion.identity);   // 爆発エフェクトを生成する
                m_audioSource.PlayOneShot(m_explosionAudioClip);    // 爆発音を鳴らす

                // 後始末
                m_godMode = true;   // オブジェクトの破棄を遅らせるので、プレイヤーの当たり判定を消す
                GetComponent<Renderer>().enabled = false;   // オブジェクトの破棄を遅らせるので、プレイヤーの表示を消す
                Destroy(this.gameObject, m_explosionAudioClip.length);  // 爆発音が鳴り終わったらオブジェクトを破棄する
            }
        }
    }
}
