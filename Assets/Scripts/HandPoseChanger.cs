using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPoseChanger : MonoBehaviour
{
    [SerializeField] private PoseContainer[] _handBones;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHandPose()
    {
        foreach (PoseContainer bone in _handBones)
        {
            bone.SwitchPose();
        }
    }
}
