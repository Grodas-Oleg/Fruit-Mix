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
            NextScene(false);

            EventBus.OnNextScene += NextScene;
            EventBus.OnBlend += OnBlend;
        }

        private void OnBlend() => FruitSpawner.Instance.gameObject.SetActive(false);


        private void NextScene(bool flag)
        {
            if (flag)
            {
                _currentSceneIndex++;
                if (_currentSceneIndex > _recipies.Count - 1) _currentSceneIndex = 0;
                EventBus.OnNextLevel?.Invoke(_currentSceneIndex);
            }

            BlenderController.SetNewRecipe(_recipies[_currentSceneIndex]);
            FruitSpawner.Instance.gameObject.SetActive(true);
        }

        private void OnDestroy() => EventBus.OnNextScene -= NextScene;
    }
}