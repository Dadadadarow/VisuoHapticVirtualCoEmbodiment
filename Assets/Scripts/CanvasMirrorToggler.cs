using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMirrorToggler : MonoBehaviour
{
    [SerializeField] private GameObject _mirror;
    [SerializeField] private GameObject _canvas;

    private bool _isDrawingMode;

    // Start is called before the first frame update
    void Start()
    {
        _mirror.SetActive(false);
        _canvas.SetActive(true);

        _isDrawingMode = true;
    }

    public void Toggle()
    {
        _isDrawingMode = !_isDrawingMode;

        _canvas.SetActive(_isDrawingMode);
        _mirror.SetActive(!_isDrawingMode);
    }
}
