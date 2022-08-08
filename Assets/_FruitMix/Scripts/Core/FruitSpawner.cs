using System;
using System.Collections.Generic;
using System.Linq;
using _FruitMix.Scripts.Common;
using _FruitMix.Scripts.EventLayer;
using _FruitMix.Scripts.Holders;
using _FruitMix.Scripts.Utilities;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;

namespace _FruitMix.Scripts.Core
{
    public class FruitSpawner : Singleton<FruitSpawner>
    {
        [SerializeField] private List<FruitSpawnerContainer> _spawnerContainers;

        protected override void OnAwake()
        {
            foreach (var fruitSpawnerContainer in _spawnerContainers)
            {
                var fruit = fruitSpawnerContainer.Pool.Spawn(fruitSpawnerContainer.Transform.position,
                    Quaternion.identity, transform);
                fruitSpawnerContainer.SetLastSpawnedFruit(fruit);
            }

            EventBus.OnFruitSended += SpawnNewFruit;
            EventBus.OnBlend += DespawnAllUsedFruits;
        }

        private void SpawnNewFruit(FruitHolder fruitHolder)
        {
            DOVirtual.DelayedCall(1f, () =>
            {
                foreach (var fruitSpawnerContainer in _spawnerContainers.Where(fruitSpawnerContainer =>
                    fruitSpawnerContainer.Fruit == fruitHolder.Fruit))
                {
                    fruitSpawnerContainer.SpawnedFruits.Add(fruitSpawnerContainer.LastSpawnedFruit);

                    var fruit = fruitSpawnerContainer.Pool.Spawn(fruitSpawnerContainer.Transform.position,
                        Quaternion.identity, transform);

                    fruitSpawnerContainer.SetLastSpawnedFruit(fruit);
                }
            });
        }

        private void DespawnAllUsedFruits()
        {
            foreach (var fruitSpawnerContainer in _spawnerContainers)
            {
                if (fruitSpawnerContainer.SpawnedFruits.Count <= 0) continue;

                foreach (var go in fruitSpawnerContainer.SpawnedFruits)
                {
                    fruitSpawnerContainer.Pool.Despawn(go);
                }

                fruitSpawnerContainer.SpawnedFruits.Clear();
            }
        }

        private void OnDestroy()
        {
            EventBus.OnFruitSended -= SpawnNewFruit;
            EventBus.OnBlend -= DespawnAllUsedFruits;
        }
    }

    [Serializable]
    public class FruitSpawnerContainer
    {
        [SerializeField] private Fruit _fruit;
        [SerializeField] private LeanGameObjectPool _pool;
        [SerializeField] private Transform _transform;
        [SerializeField] private List<GameObject> _spawnedFruits;
        [SerializeField] private GameObject _lastSpawnedFruit;
        public Fruit Fruit => _fruit;
        public LeanGameObjectPool Pool => _pool;
        public Transform Transform => _transform;
        public List<GameObject> SpawnedFruits => _spawnedFruits;
        public GameObject LastSpawnedFruit => _lastSpawnedFruit;

        public void SetLastSpawnedFruit(GameObject go) => _lastSpawnedFruit = go;
    }
}