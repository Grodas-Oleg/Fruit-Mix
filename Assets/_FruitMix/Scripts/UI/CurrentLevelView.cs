using _FruitMix.Scripts.EventLayer;
using TMPro;
using UnityEngine;

namespace _FruitMix.Scripts.UI
{
    public class CurrentLevelView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        private void Awake() => EventBus.OnNextLevel += index =>
        {
            var value = index + 1;
            _text.SetText(value.ToString());
        };
    }
}