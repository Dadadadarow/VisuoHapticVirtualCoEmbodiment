using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Mirror;

public class BallManager : NetworkBehaviour
{
    [SerializeField] private Transform respawnPoint;

    [SyncVar] private Vector3 syncPosition;
    [SyncVar] private Quaternion syncRotation;

    // Start is called before the first frame update
    public void Reset()
    {
        this.transform.position = respawnPoint.position;
        Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        rb.isKinematic = false;
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
        TransmitTransform();
    }

    private void TransmitTransform()
    {
        if (isServer)
        {
            syncPosition = transform.position;
            syncRotation = transform.rotation;
        }
        else if (isClient)
        {
            transform.position = syncPosition;
            transform.rotation = syncRotation;
        }
    }
}
