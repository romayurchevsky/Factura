using Scripts.CommonCode;
using Scripts.Effects;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.PlayerCode
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform gun;
        [SerializeField] private Transform bulletPoint;
        [SerializeField] private Slider uiHealth;
        [SerializeField] private Collider detector;

        [Header("Parameters")]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float currentHorizontalSensetivity;
        [SerializeField] private float rotationLimit;
        [SerializeField] private float reloadTime;

        #region Variables
        private float rotationY;
        private bool canShoot = false;
        private bool reload = false;
        private bool canMove = false;
        private float timeTmp = 0;
        private int health = 100;
        #endregion

        private void OnEnable()
        {
            GameManager.StartGameplayAction += StartGameplayReaction;
            GameManager.PlayerDamageAction += PlayerDamageReaction;
            GameManager.StartResultAction += StartResulReaction;
            GameManager.StartLobbyAction += StartLobbyReaction;
            GameManager.StartLevelAction += StartLevelReaction;
        }

        private void OnDisable()
        {
            GameManager.StartGameplayAction -= StartGameplayReaction;
            GameManager.PlayerDamageAction -= PlayerDamageReaction;
            GameManager.StartResultAction -= StartResulReaction;
            GameManager.StartLobbyAction -= StartLobbyReaction;
            GameManager.StartLevelAction -= StartLevelReaction;
        }

        private void Update()
        {
            if (canMove) MovePlayer();
            if (canShoot) Shooting();
        }

        private void StartLobbyReaction()
        {
            detector.enabled = false;
            HeaslthUIStatus(false);
            transform.position = Vector3.zero;
            gun.eulerAngles = Vector3.zero;
            CameraController.SetLobbyAction?.Invoke();
        }

        private void StartLevelReaction()
        {
            CameraController.SetFollowAction?.Invoke(transform);
        }

        private void StartGameplayReaction()
        {
            health = 100;
            reload = false;
            canMove = true;
            canShoot = true;
            HeaslthUIStatus(true);
            detector.enabled = true;
        }

        private void StartResulReaction(bool _result)
        {
            canMove = false;
            canShoot = false;
            HeaslthUIStatus(false);
        }

        private void MovePlayer()
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
            gun.localEulerAngles = new Vector3(gun.localEulerAngles.x, GetHorizontalControl(), gun.localEulerAngles.z);
        }

        private float GetHorizontalControl()
        {
            if (Input.GetMouseButton(0))
            {
                var deltaX = Input.GetAxis("Mouse X");
                rotationY += deltaX * currentHorizontalSensetivity;
            }

            rotationY = Mathf.Clamp(rotationY, -rotationLimit, rotationLimit);
            return rotationY;
        }

        private void Shooting()
        {
            if (!reload)
            {
                BulletPool.SpawnAndPlaceAction?.Invoke(bulletPoint.position, new Vector3(bulletPoint.eulerAngles.x, gun.eulerAngles.y, gun.eulerAngles.z));
                timeTmp = 0;
                reload = true;
            }
            else
            {
                timeTmp += Time.deltaTime;
                if (timeTmp >= reloadTime)
                {
                    reload = false;
                }
            }
        }

        private void PlayerDamageReaction(int _damage)
        {
            health -= _damage;
            uiHealth.value = health;
            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            GameManager.StartResultAction.Invoke(false);
        }

        private void HeaslthUIStatus(bool _status)
        {
            uiHealth.gameObject.SetActive(_status);
            if (_status)
            {
                uiHealth.value = health;
            }
        }
    }
}
