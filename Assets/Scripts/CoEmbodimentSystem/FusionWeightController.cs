using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace CoEmbodimentSystem
{
    public class FusionWeightController : NetworkBehaviour
    {
        public enum ButtonInput
        {
            GuiButton,

            InclimentButton,

            DeclimentButton,
        }

        private Dictionary<ButtonInput, bool> _buttonInputs = new Dictionary<ButtonInput, bool>();

        public int HostWeightPercentage { get; private set; }

        private int _currentWeight;

        private PlayerCommands _playerCommands;

        [SerializeField] private int _defaultHostWeightPercentage;
        [SerializeField] private GameObject _guiCanvas;
        [SerializeField] private Text _selfWeightValue;

        void Start()
        {
            HostWeightPercentage = _defaultHostWeightPercentage;
            _buttonInputs.Add(ButtonInput.GuiButton, false);
            _buttonInputs.Add(ButtonInput.InclimentButton, false);
            _buttonInputs.Add(ButtonInput.DeclimentButton, false);

            _guiCanvas.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (isServer)
            {
                _currentWeight = HostWeightPercentage;
            }
            else
            {
                _currentWeight = 100 - HostWeightPercentage;
            }

            if (_buttonInputs[ButtonInput.GuiButton])
            {
                _guiCanvas.SetActive(true);

                if (_buttonInputs[ButtonInput.InclimentButton])
                {
                    if (_currentWeight < 100)
                    {
                        _currentWeight++;
                        if (isServer)
                        {
                            _playerCommands.CmdChangeHostWeight(_currentWeight);
                        }
                        else
                        {
                            _playerCommands.CmdChangeHostWeight(100 - _currentWeight);
                        }
                    }
                }
                if (_buttonInputs[ButtonInput.DeclimentButton])
                {
                    if (_currentWeight > 0)
                    {
                        _currentWeight--;
                        if (isServer)
                        {
                            _playerCommands.CmdChangeHostWeight(_currentWeight);
                        }
                        else
                        {
                            _playerCommands.CmdChangeHostWeight(100 - _currentWeight);
                        }
                    }
                }

                _selfWeightValue.text = _currentWeight.ToString();
            }
            else
            {
                _guiCanvas.SetActive(false);
            }
        }

        public void SetButtonInput(ButtonInput buttonInput, bool isPressed)
        {
            _buttonInputs[buttonInput] = isPressed;
        }

        public void ChangeHostWeight(int val)
        {
            HostWeightPercentage = val;
        }

        public void SetPlayerCommands(PlayerCommands playerCommands)
        {
            _playerCommands = playerCommands;
        }
    }
}