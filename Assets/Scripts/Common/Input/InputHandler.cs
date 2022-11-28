
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{

    private List<InputListener> _listeners = new();

    private InputInfo _inputInfo = new InputInfo();

    private InputInfo _prevInputInfo = new InputInfo();

    private void Update()
    {

        _prevInputInfo = _inputInfo;
        _inputInfo = new InputInfo();

        if (!IsPointerOverUIObject())
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                _inputInfo.IsLeftButtonDown = true;
            }

            var pos = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
            _inputInfo.xPos = pos.x;
            _inputInfo.yPos = pos.y;
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.Tab))
        {
            _inputInfo.IsKeyPressed = true;
            _inputInfo.IsTabPressed = true;
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1))
        {
            _inputInfo.IsKeyPressed = true;
            _inputInfo.Is1Pressed = true;
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2))
        {
            _inputInfo.IsKeyPressed = true;
            _inputInfo.Is2Pressed = true;
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha3))
        {
            _inputInfo.IsKeyPressed = true;
            _inputInfo.Is3Pressed = true;
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.A))
        {
            _inputInfo.IsKeyPressed = true;
            _inputInfo.IsAPressed = true;
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.W))
        {
            _inputInfo.IsKeyPressed = true;
            _inputInfo.IsWPressed = true;
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.D))
        {
            _inputInfo.IsKeyPressed = true;
            _inputInfo.IsDPressed = true;
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.S))
        {
            _inputInfo.IsKeyPressed = true;
            _inputInfo.IsSPressed = true;
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.E))
        {
            _inputInfo.IsKeyPressed = true;
            _inputInfo.IsEPressed = true;
        }

        if (UnityEngine.Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            _inputInfo.IsScrollUp = true;
        }

        if (UnityEngine.Input.GetKey(KeyCode.LeftShift))
        {
            _inputInfo.IsShiftPressed = true;
        }

        FireEvents();
    }

    private void FireEvents()
    {
        if (_inputInfo.IsLeftButtonDown)
        {
            InvokeHandlers((handler) => handler.OnClick(_inputInfo));
        }

        if (_prevInputInfo.xPos != _inputInfo.xPos || _prevInputInfo.yPos != _inputInfo.yPos)
        {
            InvokeHandlers((handler) => handler.OnMouseMove(_inputInfo));
        }

        if (_inputInfo.IsKeyPressed)
        {
            InvokeHandlers((handler) => handler.OnKeyPressed(_inputInfo));
        }

        if (_inputInfo.IsScrollUp)
        {
            InvokeHandlers((handler) => handler.OnScroll(_inputInfo));
        }
    }

    private void InvokeHandlers(Action<InputListener> Callback)
    {
        _listeners.ForEach(handler => {
            if (!handler.IsListenerDisabled)
            {
                Callback(handler);
            }
        });

    }

    public void AddListener(InputListener listener)
    {

        _listeners.Add(listener);
    }

    public void RemoveListener(InputListener listener)
    {
        _listeners.Remove(listener);
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}
