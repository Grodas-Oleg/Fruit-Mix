using _FruitMix.Scripts.EventLayer;
using _FruitMix.Scripts.Holders;
using DG.Tweening;
using UnityEngine;

namespace _FruitMix.Scripts.Common
{
    public class FruitHolder : MonoBehaviour
    {
        [SerializeField] private Fruit fruit;
        [SerializeField] private Rigidbody _rigidbody;

        private Vector3 _startScale;
        public Fruit Fruit => fruit;

        private void Awake() => _startScale = transform.localScale;

        private void OnEnable()
        {
            transform.localScale = Vector3.zero;
            _rigidbody.isKinematic = true;
            transform.DOScale(_startScale, 1f);
        }

        private void OnMouseDown()
        {
            _rigidbody.isKinematic = false;
            EventBus.OnFruitSended?.Invoke(this);
        }
    }
}