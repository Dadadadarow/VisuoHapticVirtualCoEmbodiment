using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DrawingSystem
{
    public class OculusQuestButtonInputForCanvasCleaner : MonoBehaviour
    {
        [SerializeField] private float _threshold;

        [SerializeField] private CanvasCleaner _canvasCleaner;

        // Update is called once per frame
        void Update()
        {
            if (_canvasCleaner.gameObject.activeSelf)
            {
                if (OVRInput.Get(OVRInput.RawButton.A))
                {
                    _canvasCleaner.SetButtonInput(CanvasCleaner.ButtonInput.GuiButton, true);
                }
                else
                {
                    _canvasCleaner.SetButtonInput(CanvasCleaner.ButtonInput.GuiButton, false);
                }

                if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > _threshold)
                {
                    _canvasCleaner.SetButtonInput(CanvasCleaner.ButtonInput.ClearButton, true);
                }
                else
                {
                    _canvasCleaner.SetButtonInput(CanvasCleaner.ButtonInput.ClearButton, false);
                }
            }
        }
    }
}