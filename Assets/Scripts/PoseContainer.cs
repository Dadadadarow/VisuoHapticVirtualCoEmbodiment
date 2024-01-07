using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseContainer : MonoBehaviour
{
    [SerializeField] private Transform[] _poses;

    private int _currentPoseIndex = 1;

    private void Start()
    {
        SwitchPose();
    }
    public void SwitchPose()
    {
        if (_currentPoseIndex == 0)
        {
            _currentPoseIndex = 1;
        }
        else
        {
            _currentPoseIndex = 0;
        }
        this.transform.localPosition = _poses[_currentPoseIndex].localPosition;
        this.transform.localRotation = _poses[_currentPoseIndex].localRotation;
    }
}
