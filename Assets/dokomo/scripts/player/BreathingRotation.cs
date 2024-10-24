using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathingRotation : MonoBehaviour
{
    // 呼吸の周期と振幅
    [SerializeField] float breathCycle = 2.0f; // 呼吸の周期（秒）
    [SerializeField] float breathCycle2 = 2.0f; // 呼吸の周期（秒）
    [SerializeField] float breathAmplitude = 10.0f; // 呼吸の振幅（度）

    // 初期回転
    private Quaternion initialRotation;

    void Start()
    {
        // 初期回転を保存
        initialRotation = transform.localRotation;
    }

    void FixedUpdate()
    {
        // 呼吸をシミュレート
        float breathOffset = Mathf.Sin(Time.time / breathCycle * Mathf.PI * 2) * breathAmplitude;
        float breathOffset2 = Mathf.Sin(Time.time / breathCycle2 * Mathf.PI * 2) * breathAmplitude;

        // 初期回転に呼吸の変化を加えて新しい回転を計算
        Quaternion newRotation = initialRotation * Quaternion.Euler(Vector3.right * breathOffset)* Quaternion.Euler(Vector3.up * breathOffset2);

        // 新しい回転に適用
        transform.localRotation = newRotation;
    }
}