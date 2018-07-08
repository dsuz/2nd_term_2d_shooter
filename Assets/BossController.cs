using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    /// <summary>ボスのライフ</summary>
    [SerializeField] int m_life = 10;
    /// <summary>ダメージを受けた時の爆発エフェクト</summary>
    [SerializeField] GameObject m_smallExplosion;
    /// <summary>やられた時の爆発エフェクト</summary>
    [SerializeField] GameObject m_bigExplosion;
    /// <summary>ボスの停止地点</summary>
    [SerializeField] Transform m_bossAnchor;
    /// <summary>ボスの出現時間（停止地点までたどり着く時間）</summary>
    [SerializeField] float m_movingTime = 20.0f;
    /// <summary>ボスの生死フラグ</summary>
    bool m_isDead = false;


    void Start()
    {
        // ボスを m_bossAnchor まで動かす
        iTween.MoveTo(this.gameObject, m_bossAnchor.position, m_movingTime);
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // もうやられていたら何もしない
        if (m_isDead) return;

        // 弾に当たったらライフを 1 減らす（レーザーは無効）
        if (collision.gameObject.GetComponent<BulletController>())
        {
            m_life -= 1;
            Debug.Log(gameObject.name + "'s life: " + m_life.ToString());
            // 爆発エフェクトを再生する
            Instantiate(m_smallExplosion, collision.gameObject.transform.position + new Vector3(0, 0, -5f), Quaternion.identity);   // position はカメラに寄せる
        }

        // ライフが 0 になったらやられたことにする
        if (m_life <= 0)
        {
            m_isDead = true;
            Debug.Log(gameObject.name + " is Dead.");
            // 爆発エフェクトを生成する
            GameObject go = Instantiate(m_bigExplosion, this.transform.position + new Vector3(0, 0, -5f), Quaternion.identity);   // position はカメラに寄せる
            // 爆発エフェクトのループをオンにする
            ParticleSystem[] psArray = go.transform.GetComponentsInChildren<ParticleSystem>();
            foreach(var ps in psArray)
            {
                var psMain = ps.main;
                psMain.loop = true;
            }
            // シーンを遷移させる
            string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;   // とりあえず現在のシーンに遷移させる
            GoNextScene(currentSceneName);
        }
    }
    
    /// <summary>
    /// フェードアウト/フェードインを伴ってシーンを遷移させる
    /// </summary>
    /// <param name="sceneName">遷移先のシーン名</param>
    void GoNextScene(string sceneName)
    {
        Initiate.Fade(sceneName, Color.black, 0.1f);
    }
}
