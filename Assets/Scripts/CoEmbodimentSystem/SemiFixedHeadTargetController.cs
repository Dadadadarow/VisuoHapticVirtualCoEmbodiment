using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoEmbodimentSystem
{
    public class SemiFixedHeadTargetController : MonoBehaviour
    {
        [SerializeField] private Transform _headPosInput;
        [SerializeField] private Transform _defaultTransform;

        private void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            this.transform.position = new Vector3(_defaultTransform.position.x, _headPosInput.position.y, _defaultTransform.position.z);
            this.transform.rotation = _defaultTransform.rotation;
        }
    }
}