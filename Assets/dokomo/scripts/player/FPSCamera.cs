using UnityEngine;
 
public class FPSCamera : MonoBehaviour
{
    [Header("振動設定")]
    [SerializeField] private float pitchAmplitude = -1f; // 上下軸の振幅
    [SerializeField] private float pitchFrequency = 1f; // 上下軸の周期（Hz）
    [SerializeField] private float yawAmplitude = 1f;   // 左右軸の振幅
    [SerializeField] private float yawFrequency = 2f;   // 左右軸の周期（Hz）
    [SerializeField] private float rollAmplitude = 1f; // ロール軸の振幅
    [SerializeField] private float rollFrequency = 2f;   // ロール軸の周期（Hz）
    [SerializeField] GameObject viewmodel;

    [Header("リセット設定")]
    [SerializeField] private float resetSpeed = 2f; // 回転をリセットするスピード

    private Quaternion initialCameraRotation;
    private Quaternion currentRotation;
    private float rotationTimer = 0f;
    private CharacterController characterController;
    private bool isMoving = false;

    void Start()
    {
        initialCameraRotation = transform.localRotation;
        currentRotation = initialCameraRotation;
        characterController = GetComponentInParent<CharacterController>();
    }

    void FixedUpdate()
    {
        HandleCameraRotation();
    }

    private void HandleCameraRotation()
    {
        gew98anim gew98anim = viewmodel.GetComponent<gew98anim>();
        // プレイヤーが動いているかどうかを確認
        float speed = characterController.velocity.magnitude;
        isMoving = speed > 0.1f; // 動いているかどうかを判断するためのしきい値

        if (characterController.isGrounded && isMoving && gew98anim.zooming==0)
        {
            rotationTimer += Time.deltaTime;

            // 各軸の振動を計算
            float pitch = Mathf.Sin(rotationTimer * pitchFrequency * Mathf.PI * 2f) * pitchAmplitude;
            float yaw = Mathf.Cos(rotationTimer * yawFrequency * Mathf.PI * 2f) * yawAmplitude;
            float roll = Mathf.Sin(rotationTimer * rollFrequency * Mathf.PI * 2f) * rollAmplitude;

            // カメラの回転を更新
            Quaternion pitchRotation = Quaternion.Euler(pitch, 0, 0);
            Quaternion yawRotation = Quaternion.Euler(0, yaw, 0);
            Quaternion rollRotation = Quaternion.Euler(0, 0, roll);
            currentRotation = initialCameraRotation * pitchRotation * yawRotation * rollRotation;
            transform.localRotation = currentRotation;
        }
        else
        {
            // プレイヤーが止まっているときのスムーズな回転リセット
            transform.localRotation = Quaternion.Slerp(transform.localRotation, initialCameraRotation, Time.deltaTime * resetSpeed);
        }
    }
}
