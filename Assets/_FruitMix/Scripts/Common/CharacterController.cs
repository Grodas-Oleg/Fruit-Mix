using _FruitMix.Scripts.EventLayer;
using _FruitMix.Scripts.UI;
using _FruitMix.Scripts.Utilities;
using DG.Tweening;
using UnityEngine;

namespace _FruitMix.Scripts.Common
{
    public class CharacterController : MonoBehaviour
    {
        private const float SUCCESS_TIME = 1.833f;
        private const float FAIL_TIME = 4f;
        private const float DELAY = 3f;

        [SerializeField] private CocktailColorView _view;
        [SerializeField] private Animator _animator;

        private static readonly int Fail = Animator.StringToHash("Fail");
        private static readonly int Success = Animator.StringToHash("Success");

        private void Awake()
        {
            _view.transform.localScale = Vector3.zero;
            EventBus.OnBlendComplete += Action;
        }

        private void Start() => ShowUI(true);

        private void Action(bool flag)
        {
            ShowUI(false);
            
            _animator.Play(flag ? Success : Fail);
            
            DOVirtual.DelayedCall(flag ? SUCCESS_TIME : FAIL_TIME + DELAY,
                () =>
                {
                    ScreenFade.Fade(() => EventBus.OnNextScene?.Invoke(flag));
                    ShowUI(true);
                });
        }

        private void ShowUI(bool flag)
        {
            if (flag)
                DOVirtual.DelayedCall(3f, () => _view.transform.DOScale(1.2f, .7f)
                    .OnComplete(() => _view.transform.DOScale(1f, .3f)));
            else
                _view.transform.DOScale(0, .5f);
        }
    }
}