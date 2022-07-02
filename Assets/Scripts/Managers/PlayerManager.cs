using Controllers;
using Data.ValueObject;
using Signals;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        [Header("Data")] public PlayerData PlayerData;
        
        #endregion
        
        #region Serialized Variables
       [Space] [SerializeField] private PlayerMovementController movementController;
       [SerializeField] private PlayerPhysicsController physicsController;
       [SerializeField] private PlayerAnimationController animationController;
       #endregion
       #endregion

       private void Awake()
       {
           GetPlayerData();
       }
       private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").PlayerData;

       private void SendPLayerDataToControllers()
       {
           movementController.SetMovementData(PlayerData.PlayerMovementData);
           animationController.SetAnimationData();
           physicsController.SetPhysicsData();
       }
       
       
       private void OnEnable()
       {
           SubscribeEvents();
       }
       void SubscribeEvents()
       {
           InputSignals.Instance.onInputTaken += OnDeactiveMovement;
           InputSignals.Instance.onInputReleased += OnDeactiveMovement;
       }
       void UnSubscribeEvents()
       {
           InputSignals.Instance.onInputTaken -= OnDeactiveMovement;
           InputSignals.Instance.onInputReleased -= OnDeactiveMovement;
       }
       private void OnDisable()
       {
           UnSubscribeEvents();
       }
       
       private void OnActivateMovement()
        { 
            movementController.EnableMovement();
        }

        private void OnDeactiveMovement()
        {
            movementController.DeactiveMovement();
        }

        private void GetInputValue()
        {
            movementController.UpdateInputValue(4);
        }
        
    }
}