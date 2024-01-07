using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoEmbodimentSystem
{
    public class IkTargetController : MonoBehaviour
    {
        private Transform _referenceTracker;

        [SerializeField] private string _trackerName;
        [SerializeField] private BodyPart _bodyPart;

        private bool _hasAuthority;

        void Start()
        {
            _referenceTracker = GameObject.Find(_trackerName).transform;
        }

        void Update()
        {
            if (_hasAuthority)
            {
                this.transform.position = _referenceTracker.position;
                this.transform.rotation = _referenceTracker.rotation;
            }
        }

        public void Register(bool isHost, bool hasAuthority)
        {
            _hasAuthority = hasAuthority;

            foreach (var calc in FindObjectsOfType<FusionBodyTransformCalculator>())
            {
                if (calc.ThisBodyPart == this._bodyPart)
                {
                    calc.SetIkTarget(this.transform, isHost);
                }
            }
        }
    }
}
