using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BallManager : MonoBehaviour
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
}
