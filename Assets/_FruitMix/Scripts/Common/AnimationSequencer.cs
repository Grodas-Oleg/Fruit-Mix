using _FruitMix.Scripts.Core;
using _FruitMix.Scripts.EventLayer;
using _FruitMix.Scripts.Utilities;
using DG.Tweening;
using UnityEngine;

namespace _FruitMix.Scripts.Common
{
    public class AnimationSequencer : Singleton<AnimationSequencer>
    {
        [SerializeField] private BlenderController _blender;
        [SerializeField] private GameObject _blenderLid;
        [SerializeField] private Transform _jumpPoint;

        private bool _isBlenderOpened;
        private Sequence _sequence;
        private Tween _tween;

        protected override void OnAwake()
        {
            EventBus.OnFruitSended += AnimationSequence;
            EventBus.OnBlend += CloseLid;
        }

        private void CloseLid() => LidStatus(false);

        private void LidStatus(bool flag)
        {
            _isBlenderOpened = flag;
            _blenderLid.transform.DOLocalRotate(flag ? new Vector3(0, 0, -90) : new Vector3(0, 0, 0), 1f);
        }

        private void AnimationSequence(FruitHolder fruitHolder)
        {
            _sequence = DOTween.Sequence();

            if (!_isBlenderOpened) _sequence.AppendCallback(() => LidStatus(true));

            _sequence.AppendCallback(() =>
            {
                fruitHolder.transform.parent = _blender.transform;
                fruitHolder.transform.DOJump(_jumpPoint.position, .3f, 1, 1.5f).SetEase(Ease.InQuart)
                    .OnComplete(() =>
                    {
                        _blender.transform.DOShakePosition(3f, .005f);
                        _blender.AddFruit(fruitHolder.Fruit);
                    });
            });

            _tween?.Kill();
            _tween = DOVirtual.DelayedCall(3f, () => LidStatus(false));
        }

        private void OnDestroy()
        {
            EventBus.OnFruitSended -= AnimationSequence;
            EventBus.OnBlend -= CloseLid;
        }
    }
}