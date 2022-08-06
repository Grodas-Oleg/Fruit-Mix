using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _FruitMix.Scripts.Holders
{
    [CreateAssetMenu(fileName = "FruitColorModelHolder", menuName = "Holders/FruitColorModelHolder")]
    public class FruitColorModel : ScriptableObject
    {
        [SerializeField] private List<FruitColorContainer> _fruitColorContainers;

        public Color GetFruitColor(Fruit fruit) =>
            _instance._fruitColorContainers.FirstOrDefault(x => x.Fruit.Equals(fruit)).Color;

        private static FruitColorModel _instance;
        public static FruitColorModel I => _instance == null ? LoadDefs() : _instance;

        private static FruitColorModel LoadDefs() =>
            _instance = Resources.Load<FruitColorModel>("FruitColorModelHolder");
    }

    [Serializable]
    public struct FruitColorContainer
    {
        [SerializeField] private Fruit fruit;
        [SerializeField] private Color _color;
        public Color GetColor(Fruit fruit) => this.fruit.Equals(fruit) ? _color : default;
        public Fruit Fruit => fruit;
        public Color Color => _color;
    }

    [Serializable]
    public enum Fruit
    {
        Banana,
        Apple,
        Citrus,
        RedBerry,
        Tomato,
        Cucumber,
        Eggplant
    }
}