using System;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion
        
        #region Serialized Variables

        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private Collider collider;
        [SerializeField] private Rigidbody rigidbody;
        
        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("StageArea"))
            {
                
            }
            
            if (other.CompareTag("MiniGameArea"))
            {
                
            }
        }

        public void SetPhysicsData()
        {
            
        }
        
    }
}