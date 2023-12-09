using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Picker3D.LevelSystem
{
    public class LevelManager : MonoBehaviour
    {
        private const string LevelKey = "Level";

        [SerializeField] private GameObject[] levels;

        private GameObject _currentLevelObject;
        
        /// <summary>
        /// Level is encapsulated.
        /// If the current level number is greater than the number of created level objects, this method returns a random level number.
        /// </summary>
        public int Level
        {
            get => PlayerPrefs.GetInt(LevelKey, 1) > levels.Length ? Random.Range(1, levels.Length) : PlayerPrefs.GetInt(LevelKey, 1);
            set => PlayerPrefs.SetInt(LevelKey, value);
        }

        private void Start()
        {
            _currentLevelObject = levels[Level - 1];
            _currentLevelObject.SetActive(true);
        }

        private void OnNextLevelHandler()
        {
            Level++;
            _currentLevelObject.SetActive(false);
            _currentLevelObject = levels[Level - 1];
            _currentLevelObject.SetActive(true);
        }
    }
}
