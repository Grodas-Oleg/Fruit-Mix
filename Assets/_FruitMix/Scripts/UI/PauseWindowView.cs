using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _FruitMix.Scripts.UI
{
    public class PauseWindowView : BaseView
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _restartButton;

        private void Awake()
        {
            _resumeButton.onClick.AddListener(() => GameUI.SwitchPauseWindow(false));
            _hiderButton.onClick.AddListener(() => GameUI.SwitchPauseWindow(false));
            _restartButton.onClick.AddListener(() =>
            {
                var scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            });
        }

        private void OnEnable() => Time.timeScale = 0f;

        protected override void OnDisable()
        {
            base.OnDisable();
            Time.timeScale = 1f;
        }
    }
}