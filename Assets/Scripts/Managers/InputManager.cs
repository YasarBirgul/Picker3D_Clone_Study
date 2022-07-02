using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Data.ValueObject;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
         [Header("Data")] public InputData InputData;
        #endregion

        #region Serialized Variables
        
        #endregion
        [SerializeField] private bool isReadyForTouch,isFirstTimeTouchTaken;
        #endregion
        
        
        #region Private Variables

        private bool _isTouching;
        private float _currentVelocity;
        private Vector2? _mousePosition;
        private Vector3 _moveVector;
        
        #endregion


        private void Awake()
        {
            InputData = GetInputData();
        }


        private InputData GetInputData() => Resources.Load<CD_Input>("Data/CD_Input").InputData;

        #region Event Subsription 

        private void OnEnable()
        {
            SubscribeEvents(); 
        }
        void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
        }
        void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
        }
        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion
        
        
        
        private void Update()
        {
            if (!isReadyForTouch) return;

            if (Input.GetMouseButtonUp(0) && !IsPointerOverUIElement())
            {
                _isTouching = false;
                InputSignals.Instance.onInputReleased?.Invoke();
            }


            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
            {
                _isTouching = true;
                InputSignals.Instance.onInputTaken?.Invoke();
                if (!isFirstTimeTouchTaken)
                {
                    isFirstTimeTouchTaken = true;
                    InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
                }

                _mousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0) && !IsPointerOverUIElement())
            {
                if (_isTouching)
                {
                    if (_mousePosition != null)
                    {
                        Vector2 mouseDeltaPos = (Vector2) Input.mousePosition - _mousePosition.Value;


                        if (mouseDeltaPos.x > InputData.HorizontalInputSpeed)
                            _moveVector.x = InputData.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                        else if (mouseDeltaPos.x < -InputData.HorizontalInputSpeed)
                            _moveVector.x = -InputData.HorizontalInputSpeed / 10f * -mouseDeltaPos.x;
                        else
                            _moveVector.x = Mathf.SmoothDamp(_moveVector.x, 0f, ref _currentVelocity,
                                InputData.ClampSpeed);

                        _mousePosition = Input.mousePosition;

                        InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
                        {
                            Xvalue = _moveVector.x,
                            ClampValues = new Vector2(InputData.ClampSides.x, InputData.ClampSides.y)
                        });
                    }
                }
            }
        }

        private void OnEnableInput()
        {
            isReadyForTouch = true;
        }

        private void OnDisableInput()
        {
            isReadyForTouch = false;
        }

        private void OnPlay()
        {
            isReadyForTouch = true;
        }

        private bool IsPointerOverUIElement()
        {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }

        private void OnReset()
        {
            _isTouching = false;
            isReadyForTouch = false;
            isFirstTimeTouchTaken = false;
        }
    }
}