using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CoEmbodimentSystem
{
    public class FusionBodyTransformCalculator : MonoBehaviour
    {
        private Transform _hostIkTarget;
        private Transform _clientIkTarget;
        public Vector3 _hostForceTarget;

        [SerializeField] private FusionWeightController _fusionWeightController;

        private Quaternion _previousRotation;
        private Quaternion _currentRotation;
        private Quaternion _currentReversedRotation;

        private bool _isFirstFrame = true;

        [SerializeField] private BodyPart _bodyPart;
        public BodyPart ThisBodyPart { get { return _bodyPart; }}

        private bool _isFusionStarted;

        // SomatoShiftの固定IP
        public string IP = "192.168.8.49";

        public float sendValueRight = 10f;
        public float sendValueLeft = 10f;
        public float sendValueGainRight = 0f;
        public float sendValueGainLeft = 0f;
        // public double sendValue = 0;
        public string Address = "/bind/values";
        public int Port = 9000;
        private float time;
        private float lastSendTime = 0;
        // private double maxAbs = 0;

        void Start()
        {
            // client = new OscClient(IP, Port);
        }

        // Update is called once per frame
        void Update()
        {
            if (_fusionWeightController.gameObject.activeSelf)
            {
                if (_hostIkTarget != null && _clientIkTarget != null)
                {
                    CalculateTransform();
                    if (_isFusionStarted)
                    {
                        return;
                    }
                    _isFusionStarted = true;
                    this.GetComponent<WristCorrector>().StartCorrectingWrist();
                }
                else
                {
                    if (!_isFusionStarted)
                    {
                        return;
                    }
                    _isFusionStarted = false;
                    this.GetComponent<WristCorrector>().StopCorrectingWrist();
                }
            }
            else
            {
                if (_isFusionStarted)
                {
                    _isFusionStarted = false;
                    this.GetComponent<WristCorrector>().StopCorrectingWrist();
                }
            }
        }

        public void SetIkTarget(Transform target, bool isHost)
        {
            if (isHost)
            {
                _hostIkTarget = target;
            }
            else
            {
                _clientIkTarget = target;
            }
        }

        private void CalculateTransform()
        {
            int hostWeight = _fusionWeightController.HostWeightPercentage;
            this.transform.position = (
                _hostIkTarget.position * (float)hostWeight
                + _clientIkTarget.position * (float)(100 - hostWeight)
                ) / 100f;
            
            if (_bodyPart == BodyPart.LeftHand || _bodyPart == BodyPart.RightHand)
            {
                _currentRotation = Quaternion.Lerp(_clientIkTarget.rotation, _hostIkTarget.rotation, (float)(hostWeight / 100f));
                _currentReversedRotation = Quaternion.AngleAxis(180, _currentRotation * this.transform.forward) * _currentRotation;

                if (_isFirstFrame)
                {
                    _previousRotation = _currentRotation;
                    _isFirstFrame = false;
                    this.transform.rotation = _currentRotation;
                }
                else
                {
                    this.transform.rotation = GetNearestQuaternion(_currentRotation, _currentReversedRotation, _previousRotation);
                }

                _previousRotation = this.transform.rotation;
            }

            if (_bodyPart == BodyPart.RightHand)
            {
                time += Time.deltaTime;
                // 前回送信から0.0025秒以上経過していたらメッセージを送る（400Hz）
                _hostForceTarget = this.transform.position - _clientIkTarget.position;
                sendValueRight = _hostForceTarget.y;
                sendValueGainRight = _hostForceTarget.magnitude;
            }

            if (_bodyPart == BodyPart.LeftHand)
            {
                time += Time.deltaTime;
                // 前回送信から0.0025秒以上経過していたらメッセージを送る（400Hz）
                _hostForceTarget = this.transform.position - _clientIkTarget.position;
                sendValueLeft = _hostForceTarget.y;
                sendValueGainLeft = _hostForceTarget.magnitude;
            }
        }

        static public Quaternion GetNearestQuaternion(Quaternion rot1, Quaternion rot2, Quaternion refRot)
        {
            return (Mathf.Abs(Quaternion.Dot(rot1, refRot)) > Mathf.Abs(Quaternion.Dot(rot2, refRot))) ? rot1 : rot2;
        }
    }
}
