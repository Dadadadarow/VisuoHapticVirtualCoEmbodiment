using UnityEngine;

public class BarController : MonoBehaviour
{
    public float moveForce = 1f; // バーの移動力
    public float tiltSpeed = 50.0f; // バーの傾斜速度
    public float maxTiltAngle = 45.0f; // バーの最大傾斜角度

    private Rigidbody rb; // Rigidbodyの参照
    private Quaternion originalRotation; // 元の回転角

    void Start()
    {
        // Rigidbodyコンポーネントを取得
        rb = GetComponent<Rigidbody>();
        // 元の回転角を保存
        originalRotation = transform.rotation;
    }

    void Update()
    {
        // 上下の移動（物理的な力を使用）
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(Vector3.up * moveForce);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(Vector3.down * moveForce);
        }

        // 左右の傾斜
        float tiltAmount = 0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            tiltAmount = tiltSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            tiltAmount = -tiltSpeed * Time.deltaTime;
        }

        // バーの回転を更新
        Quaternion targetRotation = originalRotation * Quaternion.Euler(0, 0, tiltAmount);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxTiltAngle * Time.deltaTime);
    }
}
