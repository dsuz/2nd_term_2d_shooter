using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// チャージゲージを制御するスクリプト。Slider のオブジェクトに追加して使う
/// </summary>
public class PowerGaugeController : MonoBehaviour
{
    Slider m_slider;

    /// <summary>
    /// ゲージを移動する
    /// </summary>
    /// <param name="ratio"></param>
    public void SetPowerGauge(float ratio)
    {
        m_slider.value = ratio;
    }

    void Start()
    {
        m_slider = GetComponent<Slider>();
    }
}
