using System;
using Picker3D.UI;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Picker3D.LevelSystem
{
    public class LevelManager : MonoBehaviour
    {
        private const string LevelKey = "Level";

        [SerializeField] private LevelContentData levelContentData;
        [SerializeField] private LevelObject levelObjectPrefab;

        private LevelObject _currentLevelObject;

        private int _currentPlayedLevelCount = 0;

        /// <summary>
        /// Level is encapsulated.
        /// If the current level number is greater than the number of created level objects, this method returns a random level number.
        /// </summary>
        private int Level
        {
            get => PlayerPrefs.GetInt(LevelKey, 1) > levelContentData.LevelCount
                ? Random.Range(1, levelContentData.LevelCount)
                : PlayerPrefs.GetInt(LevelKey, 1);
            set => PlayerPrefs.SetInt(LevelKey, value);
        }

        private void OnEnable()
        {
            UIManager.OnNextLevelButtonClicked += OnNextLevelHandler;
        }

        private void OnDisable()
        {
            UIManager.OnNextLevelButtonClicked -= OnNextLevelHandler;
        }

        private void Start()
        {
            levelObjectPrefab.Build(levelContentData.GetLevelObjectData(Level - 1), _currentPlayedLevelCount);
        }

        private void OnNextLevelHandler()
        {
            Level++;
            _currentPlayedLevelCount++;
            levelObjectPrefab.Build(levelContentData.GetLevelObjectData(Level - 1), _currentPlayedLevelCount);
        }
    }
}