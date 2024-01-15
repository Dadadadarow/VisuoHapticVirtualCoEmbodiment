using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TaskManager : NetworkBehaviour
{
    // // Start is called before the first frame update
    [SyncVar] GameObject syncBall;
    // [SyncVar] Vector3 ballPosition;
    public GameObject originBall;
    public Transform barPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Instantiate(originBall, new Vector3(barPosition.position.x, barPosition.position.y+0.1f, barPosition.position.z), Quaternion.identity);
            syncBall = Instantiate(originBall, new Vector3(barPosition.position.x, barPosition.position.y+0.5f, barPosition.position.z), Quaternion.identity) as GameObject;
        }
        // ballPosition = this.transform.position;
    }
}
