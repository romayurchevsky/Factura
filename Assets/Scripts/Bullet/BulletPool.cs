using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Effects
{
    public class BulletPool : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform ñontainer;
        [SerializeField] private BulletController baseBullet;

        [Header("Parameters")]
        [SerializeField] private int startPoolCount = 20;

        #region Acrions
        public static Action<Vector3, Vector3> SpawnAndPlaceAction;
        #endregion

        #region Variables
        private List<BulletController> bulletControllers = new List<BulletController>();
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
                AddBullet();
            }
        }

        private void AddBullet()
        {
            var newBullet = Instantiate(baseBullet, ñontainer);
            bulletControllers.Add(newBullet);
            newBullet.Init();
        }

        private void SpawnAndPlace(Vector3 _spawnPoint, Vector3 _rotaion)
        {
            GetFreeBullet().SpawnAndPlace(_spawnPoint, _rotaion);
        }

        private BulletController GetFreeBullet()
        {
            var someBullet = bulletControllers.Find(_someBullet => !_someBullet.IsBusy);
            if (someBullet != null)
            {
                return someBullet;
            }

            AddBullet();
            return GetFreeBullet();
        }
    }
}
