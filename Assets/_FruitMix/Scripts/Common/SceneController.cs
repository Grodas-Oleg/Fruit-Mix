using System.Collections.Generic;
using _FruitMix.Scripts.Core;
using _FruitMix.Scripts.EventLayer;
using _FruitMix.Scripts.Holders;
using UnityEngine;

namespace _FruitMix.Scripts.Common
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private List<CocktailRecipeHolder> _recipies;

        private int _currentSceneIndex;

        private void Start()
        {
            NextScene();

            EventBus.OnNextScene += NextScene;
        }

        private void NextScene()
        {
            BlenderController.SetNewRecipe(_recipies[_currentSceneIndex]);

            if (_currentSceneIndex <= _recipies.Count - 1)
                _currentSceneIndex++;
            else
                _currentSceneIndex = 0;
        }

        private void OnDestroy() => EventBus.OnNextScene -= NextScene;
    }
}