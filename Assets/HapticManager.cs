using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum HAPTICOPTIONS
{
    None = 0,
    Vibration = 1,
    Force = 2,
}

namespace CoEmbodimentSystem
{
    public class HapticManager : MonoBehaviour
    {
        public FusionBodyTransformCalculator fusionDataRight;
        public FusionBodyTransformCalculator fusionDataLeft;
        public HAPTICOPTIONS hapticOpt = 0;
        private float time;
        private float lastSendTime = 0f;
        private float vibrationDeadLine = 0.2f; //振動の不感帯
        public float vibrationGain = 1f; // 　生徒教師が離れている距離に対する、コントローラ振動のゲイン
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;
            
            if (time - lastSendTime > 0.0025f)
            {
                if (hapticOpt == HAPTICOPTIONS.Vibration)
                {
                    if (fusionDataRight.sendValueGainRight > vibrationDeadLine)
                    {
                        StartCoroutine(Vibrate(amplitude:(fusionDataRight.sendValueGainRight-vibrationDeadLine)*vibrationGain, controller:OVRInput.Controller.RTouch));
                    }
                    if (fusionDataLeft.sendValueGainLeft > vibrationDeadLine)
                    {
                        StartCoroutine(Vibrate(amplitude:(fusionDataLeft.sendValueGainLeft-vibrationDeadLine)*vibrationGain, controller:OVRInput.Controller.LTouch));
                    }
                }
            }
        }

        public static IEnumerator Vibrate(float frequency = 0.1f, float amplitude = 0.5f, OVRInput.Controller controller = OVRInput.Controller.Active)
        {
            //コントローラーを振動させる
            OVRInput.SetControllerVibration(frequency, amplitude, controller);
            // Debug.Log(amplitude);

            //指定された時間待つ
            // yield return new WaitForSeconds(duration);
            yield return new WaitForSeconds(0);

            //コントローラーの振動を止める
            // OVRInput.SetControllerVibration(0, 0, controller);
        }
    }
}
