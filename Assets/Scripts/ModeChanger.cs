using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ModeChanger : NetworkBehaviour
{
    private HandPoseChanger[] _handPoseChangers;
    private CanvasMirrorToggler _modeToggler;

    // Start is called before the first frame update
    void Start()
    {
        _handPoseChangers = FindObjectsOfType<HandPoseChanger>();
        _modeToggler = FindObjectOfType<CanvasMirrorToggler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.A) && OVRInput.Get(OVRInput.RawButton.B) && OVRInput.Get(OVRInput.RawButton.X) && OVRInput.Get(OVRInput.RawButton.Y))
        {
            if (hasAuthority)
            {
                CmdSwitchMode();
            }
        }
    }

    [Command]
    public void CmdSwitchMode()
    {
        RpcSwitchMode();
    }

    [ClientRpc]
    public void RpcSwitchMode()
    {
        foreach (HandPoseChanger handPoseChanger in _handPoseChangers)
        {
            handPoseChanger.ChangeHandPose();
        }

        _modeToggler.Toggle();
    }
}
