using _FruitMix.Scripts.Common;
using UnityEngine.Events;

namespace _FruitMix.Scripts.EventLayer
{
    public static class EventBus
    {
        public static UnityAction<FruitHolder> OnFruitSended;
        public static UnityAction OnFruitAdded;
        public static UnityAction OnBlend;
        public static UnityAction<bool> OnBlendComplete;
        public static UnityAction<bool> OnNextScene;

        static EventBus()
        {
        }

        public static void Clear()
        {
        }
    }
}