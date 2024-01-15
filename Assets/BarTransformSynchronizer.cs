using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BarTransformSynchronizer : NetworkBehaviour
{
    [SyncVar] public float height;
    [SyncVar] public float tiltAngle;
    [SerializeField] private Transform _synchronizeTarget_right;
    [SerializeField] private Transform _synchronizeTarget_left;
    private Quaternion initialRotation; // 初期回転
    private void Start()
    {
        // 初期の高さと傾きを保存
        initialRotation = this.transform.rotation;
    }
    // Update is called once per frame
    void Update()
    {
        tiltAngle = -Mathf.Atan2(_synchronizeTarget_left.position.y - _synchronizeTarget_right.position.y, 1.1f) * Mathf.Rad2Deg;
        height = (_synchronizeTarget_left.position.y + _synchronizeTarget_right.position.y) / 2;

        this.transform.position = new Vector3(0, height, this.transform.position.z);
        // 常にx座標は0
        this.transform.rotation = Quaternion.Euler(0, 0, tiltAngle) * initialRotation;
    }
}
