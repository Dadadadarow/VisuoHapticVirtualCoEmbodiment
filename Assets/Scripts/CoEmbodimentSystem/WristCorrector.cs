using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoEmbodimentSystem
{
    public class WristCorrector : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotationAngle;

        private bool _isCorrecting;

        private void LateUpdate()
        {
            if (_isCorrecting)
            {
                this.transform.localRotation = this.transform.localRotation * Quaternion.Euler(_rotationAngle.x, _rotationAngle.y, _rotationAngle.z);
            }
        }

        public void StartCorrectingWrist()
        {
            _isCorrecting = true;
        }

        public void StopCorrectingWrist()
        {
            _isCorrecting = false;
        }
    }
}
