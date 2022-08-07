using _FruitMix.Scripts.Core;
using UnityEngine;
using UnityEngine.UI;

namespace _FruitMix.Scripts.UI
{
    public class CocktailColorView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        private void Awake() => BlenderController.OnRequiredColorChanged += color => _image.color = color;
    }
}