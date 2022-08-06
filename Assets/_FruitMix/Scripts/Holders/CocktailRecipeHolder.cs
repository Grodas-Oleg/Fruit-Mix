using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _FruitMix.Scripts.Holders
{
    [CreateAssetMenu(fileName = "CocktailRecipeHolder", menuName = "Holders/CocktailRecipeHolder")]
    public class CocktailRecipeHolder : ScriptableObject
    {
        [SerializeField] private List<Fruit> _fruits;

        private Color _requiredColor;
        private List<Color> _fruitColors;

        public Color GetRequiredColor()
        {
            foreach (var fruit in _fruits) _fruitColors.Add(FruitColorModel.I.GetFruitColor(fruit));
            _requiredColor = _fruitColors.Aggregate(_requiredColor, (current, color) => current + color);
            return _requiredColor /= _fruitColors.Count;
        }
    }
}