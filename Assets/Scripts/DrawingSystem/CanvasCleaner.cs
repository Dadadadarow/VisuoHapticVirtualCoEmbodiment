using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter;
using Mirror;

namespace DrawingSystem
{
    public class CanvasCleaner : NetworkBehaviour
    {
        [SerializeField] InkCanvas _inkCanvas;

        public enum ButtonInput
        {
            GuiButton,

            ClearButton,
        }

        private Dictionary<ButtonInput, bool> _buttonInputs = new Dictionary<ButtonInput, bool>();

        private bool _isGuiActive;

        private PlayerCommands _playerCommands;

        // Start is called before the first frame update
        void Start()
        {
            _buttonInputs.Add(ButtonInput.GuiButton, false);
            _buttonInputs.Add(ButtonInput.ClearButton, false);
        }

        // Update is called once per frame
        void Update()
        {
            if (_buttonInputs[ButtonInput.GuiButton])
            {
                if (_buttonInputs[ButtonInput.ClearButton])
                {
                    _playerCommands.CmdClearCanvas();
                }
            }
        }

        public void SetButtonInput(ButtonInput buttonInput, bool isPressed)
        {
            _buttonInputs[buttonInput] = isPressed;
        }

        public void ClearCanvas()
        {
            _inkCanvas.ResetPaint();
        }

        public void SetPlayerCommands(PlayerCommands playerCommands)
        {
            _playerCommands = playerCommands;
        }
    }
}
