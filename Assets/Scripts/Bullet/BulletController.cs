using Scripts.CommonCode;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Effects
{
    public class BulletController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform body;

        [Header("Parameters")]
        [SerializeField] private int damagePower = 50;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float lifetime = 2;

        #region get/set
        public bool IsBusy { get; private set; } = false;
        #endregion

        #region Variables
        private float timeTmp = 0;
        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals(Tags.Enemy))
            {
                GameManager.EnemyDamageAction?.Invoke(other.transform, damagePower);
                DestroyBullet();
            }
        }

        public void Init()
        {
            Hide();
        }

        public void SpawnAndPlace(Vector3 _spawnPoint, Vector3 _rotaion)
        {
            IsBusy = true;
            transform.position = _spawnPoint;
            transform.eulerAngles = _rotaion;
            body.gameObject.SetActive(true);
            StartCoroutine(MoveProcess());
        }

        public void Hide()
        {
            StopAllCoroutines();
            body.gameObject.SetActive(false);
            IsBusy = false;
        }

        private IEnumerator MoveProcess()
        {
            timeTmp = 0;
            while (timeTmp < lifetime)
            {
                timeTmp += Time.deltaTime;
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
                yield return null;
            }

            Hide();
        }
     
        private void DestroyBullet()
        {
            ParticlePool.PlayParticleAction?.Invoke(transform.position, transform.position, ParticleType.Impact);
            CameraController.ShakeCameraAction?.Invoke();
            Hide();
        }
    }
}
