using UnityEngine;

public class FrameRateController : MonoBehaviour
{
    void Start()
    {
        // フレームレートを30に設定
        Application.targetFrameRate = 60;
    }
}