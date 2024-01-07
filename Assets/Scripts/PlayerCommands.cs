using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using CoEmbodimentSystem;
using DrawingSystem;

public class PlayerCommands : NetworkBehaviour
{
    private FusionWeightController _fusionWeightController;

    private CanvasCleaner _canvasCleaner;

    void Start()
    {
        _fusionWeightController = FindObjectOfType<FusionWeightController>();
        _canvasCleaner = FindObjectOfType<CanvasCleaner>();

        if (hasAuthority)
        {
            _fusionWeightController.SetPlayerCommands(this);
            _canvasCleaner.SetPlayerCommands(this);
        }
    }

    /*
        MirrorFusionWeightController
            public void ChangeHostWeight(int val)
            This changes Host's fusion-weight.
    */

    [Command]
    public void CmdChangeHostWeight(int val)
    {
        RpcChangeHostWeight(val);
    }

    [ClientRpc]
    public void RpcChangeHostWeight(int val)
    {
        _fusionWeightController.ChangeHostWeight(val);
    }

    /*
        MirrorCanvasCleaner
            public void ClearCanvas()
            This clears the canvas.
    */

    [Command]
    public void CmdClearCanvas()
    {
        RpcClearCanvas();
    }

    [ClientRpc]
    public void RpcClearCanvas()
    {
        _canvasCleaner.ClearCanvas();
    }
}
