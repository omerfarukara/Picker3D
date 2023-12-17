using System;
using Picker3D.Managers;
using Picker3D.Player;
using Picker3D.Scripts.Helpers;
using Picker3D.UI;
using UnityEngine;
using UnityEngine.Serialization;
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

        private void OnNextLevelHandler()
        {
            Level++;
            _currentPlayedLevelCount++;
            LevelSpawn();
        }
        
        private void OnCompleteStageHandler()
        {
            CurrentPlayedStage++;
        }

        private void LevelSpawn()
        {
            PlayerController.Instance.ResetPosition();
            CurrentPlayedStage = 0;
            levelObjectPrefab.Build(levelContentData.GetLevelObjectData(Level - 1), _currentPlayedLevelCount);
        }
    }
}