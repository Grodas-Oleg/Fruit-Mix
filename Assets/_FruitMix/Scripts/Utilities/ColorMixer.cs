using System;
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

        public static float CompareColor(Color color1, Color color2)
        {
            var colorR = (color1[0] / color2[0]);
            var colorG = (color1[1] / color2[1]);
            var colorB = (color1[2] / color2[2]);
            return (colorR + colorG + colorB) / 3;
        }
    }
}