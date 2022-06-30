using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewTypes;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _forceMultiplyer;
    [SerializeField] private float _spearAngle;
    private InputSystem _inputSystem;
    private TouchInfo _touchInfo;
    private Vector3 _target;

    void Awake()
    {
        _inputSystem = new InputSystem();
    }
    void Start()
    {
    }

    void Update()
    {
        _touchInfo = _inputSystem.ReadInput();

        if (_touchInfo.Phase == TouchPhase.Began)
        {
            _target = new Vector3(_touchInfo.StartPosWorld.x, Spear.Instance.transform.position.y + Spear.Instance.StartAngle, _touchInfo.StartPosWorld.z);
            //Vector3 target = new Vector3(_touchInfo.StartPosWorld.x, _touchInfo.StartPosWorld.y, _touchInfo.StartPosWorld.z);
            Spear.Instance.LookAt(_target);
        }
        if (_touchInfo.Phase == TouchPhase.Moved || _touchInfo.Phase == TouchPhase.Stationary)
        {
            _target = new Vector3(_touchInfo.DirectionWorld.x, Spear.Instance.transform.position.y + Spear.Instance.StartAngle, _touchInfo.DirectionWorld.z);
            //Vector3 target = new Vector3(_touchInfo.DirectionWorld.x, _touchInfo.DirectionWorld.y, _touchInfo.DirectionWorld.z);
            Spear.Instance.LookAt(_target);
        }
        if (_touchInfo.Phase == TouchPhase.Ended)
        {   
            _target = new Vector3(_touchInfo.DirectionWorld.x, Spear.Instance.transform.position.y + Spear.Instance.StartAngle, _touchInfo.DirectionWorld.z);
            //Vector3 target = new Vector3(_touchInfo.DirectionWorld.x, _touchInfo.DirectionWorld.y, _touchInfo.DirectionWorld.z);
            Spear.Instance.Throw(_target);
        }
    }
}
