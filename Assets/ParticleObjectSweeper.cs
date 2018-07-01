using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Particle System が追加されたオブジェクトに追加することで、再生が終了したらオブジェクトを破棄するようにする
/// </summary>
public class ParticleObjectSweeper : MonoBehaviour
{
    /// <summary>Particle System の再生時間</summary>
    float m_duration;
    /// <summary>タイマー</summary>
    float m_timer;

    void Start()
    {
        // Particle System の再生時間を取得する
        m_duration = GetComponent<ParticleSystem>().main.duration;
    }

    void Update()
    {
        // タイマーで時間を測り、再生時間を超えたらオブジェクトを消す
        m_timer += Time.deltaTime;
        if (m_timer > m_duration)
        {
            Destroy(this.gameObject);
        }
    }
}
