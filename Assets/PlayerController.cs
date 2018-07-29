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
    /// <summary>レーザーのプレハブ</summary>
    [SerializeField] GameObject m_laserPrefab;
    /// <summary>レーザーのインスタンス</summary>
    GameObject m_laserInstance;
    /// <summary>ゴッドモード（True の時、無敵モードになる）</summary>
    [SerializeField] bool m_godMode = false;
    /// <summary>メッセージ表示用の Text</summary>
    [SerializeField] Text m_messageText;
    /// <summary>やられた時の爆発エフェクト</summary>
    [SerializeField] GameObject m_explosionPrefab;
    /// <summary>やられた時の爆発音</summary>
    [SerializeField] AudioClip m_explosionAudioClip;

    /// <summary>弾をチャージした時間</summary>
    float m_chargeTime;
    /// <summary>最大チャージ時間</summary>
    [SerializeField] float m_maxChargeTime = 1.5f;
    /// <summary>弾の最大倍率</summary>
    [SerializeField] float m_maxChargeMagnification = 20.0f;
    /// <summary>チャージのゲージ</summary>
    [SerializeField] PowerGaugeController m_powerGauge;

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

        // 左クリックまたはスペースを押している間、チャージ時間を増やす
        if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
        {
            // チャージ時間が最大チャージ時間を超えないように測る
            m_chargeTime = Mathf.Min(m_chargeTime + Time.deltaTime, m_maxChargeTime);
            // パワーゲージをセットする
            m_powerGauge.SetPowerGauge(m_chargeTime / m_maxChargeTime);
        }

        if (Input.GetButtonUp("Fire1") || Input.GetKeyUp(KeyCode.Space))
        {
            // チャージ時間に応じた弾の倍率を計算する
            float scale = m_chargeTime * (m_maxChargeMagnification - 1.0f) / m_maxChargeTime + 1.0f;
            GameObject go = Instantiate(m_bulletPrefab, this.transform.position, Quaternion.identity);  // インスペクターから設定した m_bulletPrefab をインスタンス化する
            go.transform.localScale *= scale;    // チャージ時間に応じて弾を大きくする
            m_audioSource.Play();   // 発射音を鳴らす
            m_chargeTime = 0f;    // チャージ時間をリセットする
            m_powerGauge.SetPowerGauge(0f); // ゲージをリセットする
        }

        // 右クリックを押している間、レーザーを出す
        if (Input.GetButtonDown("Fire2"))
        {
            m_laserInstance = Instantiate(m_laserPrefab, this.transform.position, Quaternion.identity);
            m_laserInstance.transform.SetParent(this.transform);
        }

        // 右クリックを離したら、レーザーを消す
        if (Input.GetButtonUp("Fire2"))
        {
            Destroy(m_laserInstance);
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

    // オブジェクトが破棄 (destroy) される時にタイトルに戻る
    private void OnDestroy()
    {
        Initiate.Fade("Title", Color.black, 1f);
    }
}
