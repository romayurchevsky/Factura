using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.EnemyCode
{
    public class EnemyPool : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform ñontainer;
        [SerializeField] private EnemyController baseEnemy;

        [Header("Parameters")]
        [SerializeField] private int startPoolCount = 20;

        #region Actions
        public static Action<Vector3, Vector3> SpawnAndPlaceAction;
        #endregion

        #region Variables
        private List<EnemyController> enemyControllers = new List<EnemyController>();
        #endregion

        private void Awake()
        {
            PreparePool();
        }

        private void OnEnable()
        {
            SpawnAndPlaceAction += SpawnAndPlace;
        }

        private void OnDisable()
        {
            SpawnAndPlaceAction -= SpawnAndPlace;
        }

        private void PreparePool()
        {
            for (int i = 0; i < startPoolCount; i++)
            {
                AddEnemy();
            }
        }

        private void AddEnemy()
        {
            var newEnemy = Instantiate(baseEnemy, ñontainer);
            enemyControllers.Add(newEnemy);
            newEnemy.Init();
        }

        private void SpawnAndPlace(Vector3 _spawnPoint, Vector3 _spawnRotation)
        {
            GetFreeEnemy().SpawnAndPlace(_spawnPoint, _spawnRotation);
        }

        private EnemyController GetFreeEnemy()
        {
            var someEnemy = enemyControllers.Find(_someEnemy => !_someEnemy.IsBusy);
            if (someEnemy != null)
            {
                return someEnemy;
            }

            AddEnemy();
            return GetFreeEnemy();
        }
    }
}
