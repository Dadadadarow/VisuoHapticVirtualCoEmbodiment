using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter;
using Es.InkPainter.Sample;

namespace DrawingSystem
{
    public class OnTriggerPainter : MonoBehaviour
    {
        [SerializeField] private Brush _brush;
        [SerializeField] private int _waitForFrames;
        private int _currentWaitCount;

        [SerializeField] private GameObject _canvas;
        private InkCanvas _inkCanvas;


        private void Awake()
        {
            _inkCanvas = _canvas.GetComponent<InkCanvas>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            ++_currentWaitCount;
        }

        public void OnTriggerStay(Collider other)
        {
            if (_currentWaitCount < _waitForFrames)
            {
                return;
            }

            _currentWaitCount = 0;

            if (other.gameObject.GetComponent<InkCanvas>() != null)
            {
                var point = new Vector3(transform.position.x, transform.position.y, _canvas.transform.position.z);
                _inkCanvas.Paint(_brush, point);
            }
        }
    }

}
