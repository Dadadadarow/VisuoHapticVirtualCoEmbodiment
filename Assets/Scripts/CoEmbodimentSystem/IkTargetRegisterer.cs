using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace CoEmbodimentSystem
{
    public class IkTargetRegisterer : NetworkBehaviour
    {
        void Start()
        {
            var ikTargetControllers = GetComponentsInChildren<IkTargetController>();
            bool isHost;

            if (isServer)
            {
                isHost = (hasAuthority) ? true : false;
            }
            else
            {
                isHost = (hasAuthority) ? false : true;
            }

            foreach (var controller in ikTargetControllers)
            {
                controller.Register(isHost, hasAuthority);
            }
        }
    }
}