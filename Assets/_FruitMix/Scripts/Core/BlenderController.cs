using System;
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
        private const float MAX_FILL = .66f;
        private const float MIN_FILL = .58f;

        [SerializeField] private CocktailRecipeHolder _limeCocktailRecipeHolder;

        [Space] [SerializeField] private Renderer _liquidRenderer;
        [SerializeField] private Color _requiredColor;
        [SerializeField] private Color _recivedColor;

        [Space] [SerializeField] private List<Fruit> _currentAddedFruits;

        public List<Fruit> CurrentAddedFruits => _currentAddedFruits;
        public static event Action<Color> OnRequiredColorChanged;

        private static readonly int SideColor = Shader.PropertyToID("_SideColor");
        private static readonly int TopColor = Shader.PropertyToID("_TopColor");
        private static readonly int Fill = Shader.PropertyToID("_Fill");

        protected override void OnAwake()
        {
            _liquidRenderer.material.SetFloat(Fill, MIN_FILL);

            EventBus.OnBlend += StartBlend;
        }

        private void Start() => _requiredColor = GetRequiredColor();

        public void AddFruit(Fruit fruit)
        {
            _currentAddedFruits.Add(fruit);
            EventBus.OnFruitAdded?.Invoke();
        }

        public Color GetRequiredColor()
        {
            var colors = _limeCocktailRecipeHolder.Fruits
                .Select(fruit => FruitColorModel.I.GetFruitColor(fruit))
                .ToList();

            _requiredColor = ColorMixer.GetMixedColor(colors);

            OnRequiredColorChanged?.Invoke(_requiredColor);
            return _requiredColor;
        }

        private void SetLiquidColor(Color color)
        {
            _liquidRenderer.material.DOColor(color, SideColor, 5f);
            _liquidRenderer.material.DOColor(color, TopColor, 5f);
            _liquidRenderer.material.DOFloat(MAX_FILL, Fill, 5f)
                .OnComplete(CompareColors);
        }

        private void StartBlend()
        {
            var colors = _currentAddedFruits
                .Select(fruit => FruitColorModel.I.GetFruitColor(fruit))
                .ToList();
            _recivedColor = ColorMixer.GetMixedColor(colors);
            _currentAddedFruits.Clear();
            SetLiquidColor(_recivedColor);
        }

        public void Reset()
        {
            _liquidRenderer.material.DOColor(Color.white, SideColor, 0f);
            _liquidRenderer.material.DOColor(Color.white, TopColor, 0f);
            _liquidRenderer.material.DOFloat(MIN_FILL, Fill, 0);
        }

        private void CompareColors()
        {
            var thresholdPercent = new Color(_requiredColor.r * .1f, _requiredColor.g * .1f, _requiredColor.b * .1f);

            var minThreshold = new Color(
                _requiredColor.r - thresholdPercent.r,
                _requiredColor.g - thresholdPercent.g,
                _requiredColor.b - thresholdPercent.b);

            var maxThreshold = new Color(
                _requiredColor.r + thresholdPercent.r,
                _requiredColor.g + thresholdPercent.g,
                _requiredColor.b + thresholdPercent.b);

            if (_recivedColor.r >= minThreshold.r &&
                _recivedColor.g >= minThreshold.g &&
                _recivedColor.b >= minThreshold.b)
            {
                EventBus.OnBlendComplete?.Invoke(true);
            }
            else if (_recivedColor.r <= maxThreshold.r &&
                     _recivedColor.g <= maxThreshold.g &&
                     _recivedColor.b <= maxThreshold.b)
            {
                EventBus.OnBlendComplete?.Invoke(true);
            }
            else
            {
                EventBus.OnBlendComplete?.Invoke(false);
            }
        }

        private void OnDestroy() => EventBus.OnBlend -= StartBlend;
    }
}