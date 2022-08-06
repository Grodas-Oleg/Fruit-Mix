using _FruitMix.Scripts.Core;
using _FruitMix.Scripts.EventLayer;
using _FruitMix.Scripts.Utilities;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _FruitMix.Scripts.UI
{
    public class GameUI : Singleton<GameUI>
    {
        // [SerializeField] private BaseView _startWindow;
        // [SerializeField] private BaseView _pauseWindow;
        // [SerializeField] private ButtonView _pauseButton;
        [SerializeField] private ButtonView _blendButton;
        [SerializeField] private Image _globalHider;

        // private void Start() => _pauseWindow.Hide();

        protected override void OnAwake()
        {
            FadeGlobal(false, false, 0.5f);

            // _pauseButton.Init(() => SwitchPauseWindow(true));
            _blendButton.Init(BlenderController.StartBlend);

            EventBus.OnFruitAdded += SwitchBlendButton;
            EventBus.OnBlend += SwitchBlendButton;

            // if (!EventBus.isFirstLaunch.Value)
            // {
            //     _startWindow.Show(true);
            //     EventBus.isFirstLaunch.Publish(true);
            // }
        }

        public static void SwitchBlendButton()
        {
            if (BlenderController.Instance.CurrentAddedFruits.Count > 0)
            {
                Instance._blendButton.Show();
            }
            else
            {
                Instance._blendButton.Hide();
            }
        }

        // public void HideStartWindow() => _startWindow.Hide();
        //
        // public void SwitchPauseWindow(bool flag)
        // {
        //     if (flag)
        //         _pauseWindow.Show();
        //     else
        //         _pauseWindow.Hide();
        // }

        private void FadeGlobal(bool @in, bool force, float delay = 0f, float fadeTime = 0.75f)
        {
            if (force)
            {
                _globalHider.raycastTarget = @in;
                var tempColor = _globalHider.color;
                tempColor.a = @in ? 1f : 0f;
                _globalHider.color = tempColor;
                return;
            }

            if (@in)
            {
                _globalHider.raycastTarget = true;
                _globalHider
                    .DOFade(1f, fadeTime)
                    .SetDelay(delay)
                    .SetUpdate(true);
            }
            else
            {
                _globalHider
                    .DOFade(0f, fadeTime)
                    .SetDelay(delay)
                    .SetUpdate(true)
                    .OnComplete(() => _globalHider.raycastTarget = false);
            }
        }
    }
}