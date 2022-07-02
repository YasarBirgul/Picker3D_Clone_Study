using System;
using Data.ValueObject;
using Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private Rigidbody rigidbody;

        #endregion

        [Header("Data")] [ShowInInspector] private PlayerMovementData playerMovementData;
        [ShowInInspector] private bool _isReadyToMove;
        [ShowInInspector] private float _inputValue;
        #endregion
        public void SetMovementData(PlayerMovementData dataMovementData)
        {
            playerMovementData = dataMovementData;
        }

        public void EnableMovement()
        {
            _isReadyToMove = true;
        }

        public void DeactiveMovement()
        {
            _isReadyToMove = false;
        }

        private void FixedUpdate()
        {
            if (_isReadyToMove)
            {
                Move();
            }
            else
            {
                Stop();
            }
        }

        public void UpdateInputValue(float inputParams)
        {
            _inputValue = inputParams;
        }
        
        private void Move()
        {
            rigidbody.velocity = new Vector3(_inputValue * playerMovementData.SidewaysSpeed, rigidbody.velocity.y,
                _inputValue * playerMovementData.ForwardSpeed);
            rigidbody.angularVelocity = Vector3.zero;
        }
        
        private void Stop()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;

        }
        
    }
}