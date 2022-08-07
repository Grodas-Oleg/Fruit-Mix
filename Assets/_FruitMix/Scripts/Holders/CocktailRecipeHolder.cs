using System.Collections.Generic;
using UnityEngine;

namespace _FruitMix.Scripts.Holders
{
    [CreateAssetMenu(fileName = "CocktailRecipeHolder", menuName = "Holders/CocktailRecipeHolder")]
    public class CocktailRecipeHolder : ScriptableObject
    {
        [SerializeField] private List<Fruit> _fruits;
        public IEnumerable<Fruit> Fruits => _fruits;
    }
}