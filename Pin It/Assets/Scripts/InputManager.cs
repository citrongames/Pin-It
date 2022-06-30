using UnityEngine;
using NewTypes;

public class InputSystem
{
    private int _fakeTouchId = 99;
    private TouchInfo _touchInfo;
    public TouchInfo TouchInfo {get => _touchInfo;}

    private Camera _camera;

    public InputSystem()
    {
        _touchInfo = new TouchInfo(Vector3.zero, Vector3.zero, false, TouchPhase.Canceled);
        _camera = Camera.main;
    }

    public TouchInfo ReadInput()
    {
        _touchInfo.Phase = TouchPhase.Canceled;
        //If on mobile
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            TouchEvents(touch.fingerId, touch.position, touch.phase);
        }
        //else emulate with mouse button
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                TouchEvents(_fakeTouchId, Input.mousePosition, TouchPhase.Began);
            }
            else if (Input.GetMouseButton(0))
            {
                TouchEvents(_fakeTouchId, Input.mousePosition, TouchPhase.Moved);
            }
            if (Input.GetMouseButtonUp(0))
            {
                TouchEvents(_fakeTouchId, Input.mousePosition, TouchPhase.Ended);
            }
        }

        return _touchInfo;
    }

    private void TouchEvents(int touchId, Vector3 touchPos, TouchPhase touchPhase)
    {    
        _touchInfo.Phase = touchPhase;
        _touchInfo.IsInteractableUI = false;
        switch (touchPhase)
        {
            case TouchPhase.Began:
                _touchInfo.StartPos = touchPos;
                _touchInfo.StartPosWorld = _camera.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, Camera.main.farClipPlane));
                break;
            case TouchPhase.Stationary:
            case TouchPhase.Moved:
                _touchInfo.Direction = touchPos;
                _touchInfo.DirectionWorld = _camera.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, Camera.main.farClipPlane));       
                break;
            case TouchPhase.Ended:
                break;
        }
    }
}
