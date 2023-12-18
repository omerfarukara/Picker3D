using Picker3D.Managers;
using Picker3D.Player;
using Picker3D.Scripts.Helpers;
using Picker3D.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Picker3D.LevelSystem
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        private const string LevelKey = "Level";

        [SerializeField] internal LevelContentData levelContentData;
        [SerializeField] private LevelObject levelObjectPrefab;

        private LevelObject _currentLevelObject;

        private int _currentPlayedLevelCount = 0;
        public int CurrentPlayedStage { get; private set; }

        /// <summary>
        /// Level is encapsulated.
        /// If the current level number is greater than the number of created level objects, this method returns a random level number.
        /// </summary>
        public int Level
        {
            get => PlayerPrefs.GetInt(LevelKey, 1) > levelContentData.LevelCount
                ? Random.Range(1, levelContentData.LevelCount)
                : PlayerPrefs.GetInt(LevelKey, 1);
            set => PlayerPrefs.SetInt(LevelKey, value);
        }

        private void OnEnable()
        {
            UIManager.OnNextLevelButtonClicked += OnNextLevelHandler;
            GameManager.OnPassedStage += OnCompleteStageHandler;
        }

        private void OnDisable()
        {
            UIManager.OnNextLevelButtonClicked -= OnNextLevelHandler;
            GameManager.OnPassedStage -= OnCompleteStageHandler;
        }

        private void Start()
        {
            LevelSpawn();
        }

        /// <summary>
        /// It is called when the current one is finished and the new level is loaded.
        /// </summary>
        private void OnNextLevelHandler()
        {
            Level++;
            _currentPlayedLevelCount++;
            LevelSpawn();
        }

        /// <summary>
        /// It is called when a stage is completed and the number of stages completed in the current level is increased.
        /// </summary>
        private void OnCompleteStageHandler()
        {
            CurrentPlayedStage++;
        }

        /// <summary>
        /// It is called when a level spawned.
        /// </summary>
        private void LevelSpawn()
        {
            PlayerController.Instance.ResetPosition();
            CurrentPlayedStage = 0;

            if (_currentLevelObject == null)
            {
                _currentLevelObject = Instantiate(levelObjectPrefab);
            }

            _currentLevelObject.Build(levelContentData.GetLevelObjectData(Level - 1), _currentPlayedLevelCount);
        }
        
        /// <summary>
        /// It checks whether the stages in the current level are finished or not and returns a boolean.
        /// </summary>
        public bool AllStageIsComplete()
        {
            int currentLevelStageCount = levelContentData.levelObjectsData[Level - 1].levelStagesData.Length;
            bool allStageIsComplete = currentLevelStageCount == CurrentPlayedStage;
            return allStageIsComplete;
        }
    }
}