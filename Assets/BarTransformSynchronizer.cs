using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BarTransformSynchronizer : NetworkBehaviour
{
    [SyncVar] private float syncHeight;
    [SyncVar] private float syncTiltAngle;
    [SerializeField] private Transform _synchronizeTarget_right;
    [SerializeField] private Transform _synchronizeTarget_left;
    private Quaternion initialRotation; // 初期回転

    private float height;
    private float tiltAngle;

    private void Start()
    {
        // 初期の高さと傾きを保存
        initialRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        TransmitTransform();
    }

    private void TransmitTransform()
    {
        if (isServer)
        {
            // サーバー側の処理
            syncHeight = height;
            syncTiltAngle = tiltAngle;
        }
        else if (isClient)
        {
            // クライアント側の処理
            height = syncHeight;
            tiltAngle = syncTiltAngle;
        }
    }

    void Update()
    {
        tiltAngle = -Mathf.Atan2(_synchronizeTarget_left.position.y - _synchronizeTarget_right.position.y, 1.1f) * Mathf.Rad2Deg;
        height = (_synchronizeTarget_left.position.y + _synchronizeTarget_right.position.y) / 2;

        // 常にx座標は0
        Vector3 newPosition = new Vector3(0, height, 0);
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(0, 0, tiltAngle) * initialRotation;
    }
}
