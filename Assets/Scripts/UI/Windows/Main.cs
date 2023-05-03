
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class Main : Window
    {
        [SerializeField] private Button _play;
        [SerializeField] private Button _setting;
        [SerializeField] private Button _exit;

        private void Start()
        {
            _play.onClick.AddListener(ClickButtonPlay);
            // _setting.onClick.AddListener(ClickButtonSetting);
            // _exit.onClick.AddListener(ClickButtonExit);
        }

        private void ClickButtonPlay()
        {
            UIManager.Open<LevelSelection>();
        }

        // private void ClickButtonSetting()
        // {
        //     UIManager.Open<SettingPopup>();
        // }

        // private void ClickButtonExit()
        // {
        //     UIManager.Open<ExitPopup>();
        // }
    }
}