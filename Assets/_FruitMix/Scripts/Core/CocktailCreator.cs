using System.Collections.Generic;
using System.Linq;
using _FruitMix.Scripts.Holders;
using UnityEngine;

namespace _FruitMix.Scripts.Core
{
    public class CocktailCreator : MonoBehaviour
    {
        [SerializeField] private CocktailRecipeHolder _limeCocktailRecipeHolder;
        [SerializeField] private List<Fruit> _addedFruits;

        [SerializeField] private Color _requiredColor;
        [SerializeField] private Color _recivedColor;

        [SerializeField] private Color _min;
        [SerializeField] private Color _max;
       

        private void Awake() => _requiredColor = _limeCocktailRecipeHolder.GetRequiredColor();

        [ContextMenu("Make cocktail")]
        private void Mix()
        {
            var list = _addedFruits.Select(fruit => FruitColorModel.I.GetFruitColor(fruit)).ToList();
            foreach (var color in list) _recivedColor += color;
            _recivedColor /= list.Count;
        }

        [ContextMenu("Compare Colors")]
        private void CompareColors()
        {
            var thresholdPercent = new Color(_requiredColor.r * .1f, _requiredColor.g * .1f, _requiredColor.b * .1f);

            var minThreshold = new Color(
                _requiredColor.r - thresholdPercent.r,
                _requiredColor.g - thresholdPercent.g,
                _requiredColor.b - thresholdPercent.b);
            _min = minThreshold;

            var maxThreshold = new Color(
                _requiredColor.r + thresholdPercent.r,
                _requiredColor.g + thresholdPercent.g,
                _requiredColor.b + thresholdPercent.b);
            _max = maxThreshold;

            

            if (_recivedColor.r >= minThreshold.r &&
                _recivedColor.g >= minThreshold.g &&
                _recivedColor.b >= minThreshold.b)
            {
                Debug.Log(true);
            }
            else if (_recivedColor.r <= maxThreshold.r &&
                     _recivedColor.g <= maxThreshold.g &&
                     _recivedColor.b <= maxThreshold.b)
            {
                Debug.Log(true);
            }
            else
            {
                Debug.Log(false);
            }
        }
    }
}