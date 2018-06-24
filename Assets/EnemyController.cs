using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject m_bulletPrefab;
    [SerializeField] float m_shotInterval = 1.5f;
    float m_timer;


    void Start()
    {

    }

    void Update()
    {
        m_timer += Time.deltaTime;
        
        if (m_timer > m_shotInterval)
        {
            Instantiate(m_bulletPrefab, this.transform.position, Quaternion.identity);
            m_timer = 0;
        }
    }
}
