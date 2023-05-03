
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class LevelSelection : Window
    {
        [SerializeField] private Button _exit;

        [SerializeField] private List<Button> _levels = new List<Button>();

        public event Action<int> LoadSelectedLevel;

        private void Start()
        {
            _exit.onClick.AddListener(ClickButtonExit);

            foreach (var level in _levels)
            {
                level.onClick.AddListener(() => {  ClickButtonSelectLevel(level);});	
            }
        }

        private void ClickButtonExit()
        {
            UIManager.Open<Main>();
        }

        private void ClickButtonSelectLevel(Button selectedLevel)
        {
            var numberSelectedLevel = _levels.IndexOf(selectedLevel);
			
            LoadSelectedLevel?.Invoke(numberSelectedLevel);
        }
        
        public void OnInitBlockLevels(int passedLevels)
        {
            for (var i = _levels.Count - 1; i > passedLevels; i--)
            {
                var image = _levels[i].gameObject.transform.GetChild(1);
                image.gameObject.SetActive(true);
            }
        }

        public void OnOpenLevel(int numberLevel)
        {
            if (numberLevel >= _levels.Count) 
                return;
			
            var image = _levels[numberLevel].gameObject.transform.GetChild(1);
            image.gameObject.SetActive(false);
        }
    }
}