using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    /// <summary>ボスのライフ</summary>
    [SerializeField] int m_life = 10;
    /// <summary>ボスの生死フラグ</summary>
    bool m_isDead = false;

    void Start()
    {

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
        }

        // ライフが 0 になったらやられたことにする
        if (m_life <= 0)
        {
            m_isDead = true;
            Debug.Log(gameObject.name + " is Dead.");
        }
    }
}
