using UnityEngine.Events;

namespace _FruitMix.Scripts.EventLayer
{
    public static class EventBus
    {
        public static UnityAction OnFruitAdded;
        public static UnityAction OnBlend;

        static EventBus()
        {
        }

        public static void Clear()
        {
        }
    }
}