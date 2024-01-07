using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DrawingSystem
{
    public class TransformSynchronizer : MonoBehaviour
    {
        [SerializeField] private Transform _synchronizeTarget;

        // Update is called once per frame
        void Update()
        {
            this.transform.position = _synchronizeTarget.position;
            this.transform.rotation = _synchronizeTarget.rotation;
        }
    }
}
