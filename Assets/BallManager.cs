using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Mirror;

public class BallManager : NetworkBehaviour
{
    [SerializeField] private Transform respawnPoint;

    // Start is called before the first frame update
    public void Reset()
    {
        this.transform.position = respawnPoint.position;
        Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        rb.isKinematic = false;

        // サーバー側でReset()が呼び出された場合、クライアント側でもReset()を呼び出す
        if (isServer)
        {
            RpcReset();
        }
    }

    [ClientRpc]
    private void RpcReset()
    {
        Reset();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Reset();
        }
    }

    void FixedUpdate()
    {
        // 位置の同期を行わないため、この部分を削除
    }
}
