using _FruitMix.Scripts.Holders;
using UnityEngine;

namespace _FruitMix.Scripts.Core
{
    public class FruitHolder : MonoBehaviour
    {
        [SerializeField] private Fruit fruit;
        
        public Fruit Fruit => fruit;
        private Transform _transform;

        private void Awake() => _transform = transform;

        private void OnMouseDown() => BlenderController.AddFruit(this);

        [ContextMenu("Reset")]
        private void ResetPosition()
        {
            transform.position = _transform.position;
        }
    }
}