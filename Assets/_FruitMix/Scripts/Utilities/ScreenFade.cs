using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace _FruitMix.Scripts.Utilities
{
    public class ScreenFade : Singleton<ScreenFade>
    {
        [SerializeField] private CanvasGroup _canvas;

        public static void Fade(UnityAction action)
        {
            DOTween.Sequence()
                .AppendCallback(() => Instance._canvas.gameObject.SetActive(true))
                .Append(Instance._canvas.DOFade(1, 1f))
                .AppendCallback(() => action?.Invoke())
                .AppendInterval(1f)
                .Append(Instance._canvas.DOFade(0, 1f))
                .AppendCallback(() => Instance._canvas.gameObject.SetActive(false));
        }
    }
}