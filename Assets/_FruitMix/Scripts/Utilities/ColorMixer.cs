using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _FruitMix.Scripts.Utilities
{
    public static class ColorMixer
    {
        public static Color GetMixedColor(List<Color> colors)
        {
            Color newColor = new Color(0, 0, 0, 1);
            newColor = colors.Aggregate(newColor, (current, color) => current + color);
            return newColor / colors.Count;
        }
    }
}