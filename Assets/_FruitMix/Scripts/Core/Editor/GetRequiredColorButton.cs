using UnityEditor;
using UnityEngine;

namespace _FruitMix.Scripts.Core.Editor
{
    [CustomEditor(typeof(BlenderController)), CanEditMultipleObjects]
    public class GetRequiredColorButton : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GUILayout.Space(20);
            BlenderController blenderController = (BlenderController) target;
            if (GUILayout.Button("Get Required Color"))
            {
                blenderController.GetRequiredColor();
            }
        }
    }
}