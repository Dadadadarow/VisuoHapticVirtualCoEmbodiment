using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoEmbodimentSystem.PlatformDependent
{
    public class OculusQuestButtonInputForWeightController : MonoBehaviour
    {
        [SerializeField] private float _threshold;

        [SerializeField] private FusionWeightController _weightController;

        // Update is called once per frame
        void Update()
        {
            if (_weightController.gameObject.activeSelf)
            {
                if (OVRInput.Get(OVRInput.RawButton.X))
                {
                    _weightController.SetButtonInput(FusionWeightController.ButtonInput.GuiButton, true);
                }
                else
                {
                    _weightController.SetButtonInput(FusionWeightController.ButtonInput.GuiButton, false);
                }

                Vector2 rightThumbStickVal = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);

                if (rightThumbStickVal.x > _threshold)
                {
                    _weightController.SetButtonInput(FusionWeightController.ButtonInput.InclimentButton, true);
                }
                else if (rightThumbStickVal.x < -1 * _threshold)
                {
                    _weightController.SetButtonInput(FusionWeightController.ButtonInput.DeclimentButton, true);
                }
                else
                {
                    _weightController.SetButtonInput(FusionWeightController.ButtonInput.InclimentButton, false);
                    _weightController.SetButtonInput(FusionWeightController.ButtonInput.DeclimentButton, false);
                }
            }
        }
    }
}