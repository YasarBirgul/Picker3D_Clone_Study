using Controllers;
using Data.ValueObject;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public LevelData LevelData;

        #region Serialize Variables

        [Space] [SerializeField] private GameObject levelHolder;
        [SerializeField] private LevelLoaderCommand levelLoaderCommand;
        [SerializeField] private ClearActiveLevelCommand clearActiveLevelCommand;
        #endregion

        #region Private Variables

        [ShowInInspector] private int _levelID;

        #endregion

        #endregion

        #endregion
        
        private void Awake()
        {
            _levelID = GetActiveLevel();
            LevelData = GetLevelData();
        }

        private int GetActiveLevel()
        {
            if (ES3.FileExists()) return 0;
            return ES3.KeyExists("Level") ? ES3.Load<int>("Level") : 0;

        }
        private LevelData GetLevelData()=> Resources.Load<CD_Level>("Data/CD_Level").Levels[_levelID];

        #region Event Subsription
        private void OnEnable()
                {
                    SubscribeEvents();
                }
                void SubscribeEvents()
                {
                    CoreGameSignals.Instance.onLevelInitialize += OnInitializeLevel;
                    CoreGameSignals.Instance.onClearActiveLevel += OnClearActiveLevel;
                }
                void UnSubscribeEvents()
                {
                    CoreGameSignals.Instance.onLevelInitialize -= OnInitializeLevel;
                    CoreGameSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
                }
                private void OnDisable()
                {
                    UnSubscribeEvents();
                }

        #endregion
        private void Start()
        {
            OnInitializeLevel();
        }
        void OnInitializeLevel()
        {
            levelLoaderCommand.InitializeLevel(_levelID,levelHolder);
        }

        private void OnClearActiveLevel()
        {
            clearActiveLevelCommand.ClearActiveLevel(levelHolder.transform);
        }
    }
}