using System.Collections.Generic;
using System.Linq;
using _FruitMix.Scripts.EventLayer;
using _FruitMix.Scripts.Holders;
using _FruitMix.Scripts.Utilities;
using DG.Tweening;
using UnityEngine;

namespace _FruitMix.Scripts.Core
{
    public class BlenderController : Singleton<BlenderController>
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private List<FruitHolder> _currentAddedFruits;
        [SerializeField] private GameObject _blenderLid;
        [SerializeField] private Transform _jumpEndPoint;

        public List<FruitHolder> CurrentAddedFruits => _currentAddedFruits;
        private Color _recivedColor;
        private Sequence _sequence;

        private static readonly int SideColor = Shader.PropertyToID("_SideColor");
        private static readonly int TopColor = Shader.PropertyToID("_TopColor");

        public static void AddFruit(FruitHolder fruit)
        {
            Instance._currentAddedFruits.Add(fruit);
            AnimationSequence(fruit);
            EventBus.OnFruitAdded?.Invoke();
        }

        private void SetLiquidColor(Color color)
        {
            _renderer.material.SetColor(SideColor, color);
            _renderer.material.SetColor(TopColor, color);
        }

        private static void AnimationSequence(FruitHolder fruitHolder)
        {
            Instance._sequence = DOTween.Sequence()
                .AppendCallback(() => Instance._blenderLid.transform.DOLocalRotate(new Vector3(0, 0, -90), 1f))
                .AppendCallback(() =>
                {
                    fruitHolder.transform.DOJump(Instance._jumpEndPoint.position, .3f, 1, 1.5f).SetEase(Ease.InQuart);
                    Instance.transform.DOShakePosition(3f, .01f);
                });
        }

        public static void StartBlend()
        {
            var list = Instance._currentAddedFruits
                .Select(fruitHolder => FruitColorModel.I.GetFruitColor(fruitHolder.Fruit))
                .ToList();
            foreach (var color in list) Instance._recivedColor += color;
            Instance._recivedColor /= list.Count;

            Instance.SetLiquidColor(Instance._recivedColor);

            Instance._currentAddedFruits.Clear();
            EventBus.OnBlend?.Invoke();
        }
    }
}