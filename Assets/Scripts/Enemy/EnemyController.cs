using Scripts.CommonCode;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.EnemyCode
{
    public class EnemyController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform container;
        [SerializeField] private Transform body;
        [SerializeField] private Collider collider;
        [SerializeField] private Slider uiHealth;
        [SerializeField] private EnemyAnimator enemyAnimator;

        [Header("Parameters")]
        [SerializeField] private int damagePower;
        [SerializeField] private float moveSpeed;

        #region get/set
        public bool IsBusy { get; private set; } = false;
        #endregion

        #region Variables
        private int health = 100;
        private bool startAttack = false;
        private Transform player;
        #endregion

        private void OnEnable()
        {
            GameManager.StartResultAction += StartResultReaction;
            GameManager.EnemyDamageAction += EnemyDamageReaction;
        }

        private void OnDisable()
        {
            GameManager.StartResultAction -= StartResultReaction;
            GameManager.EnemyDamageAction -= EnemyDamageReaction;
        }

        private void Update()
        {
            if (startAttack) MoveToPlayer();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals(Tags.Player))
            {
                enemyAnimator.Attack();
                GameManager.PlayerDamageAction?.Invoke(damagePower);
            }
            if (other.tag.Equals(Tags.PlayerDetector))
            {
                startAttack = true;
                player = other.transform;
                enemyAnimator.Move();
            }
        }

        public void Init()
        {
            Hide();
        }

        public void SpawnAndPlace(Vector3 _spawnPoint, Vector3 _spawnRotation)
        {
            IsBusy = true;
            transform.position = _spawnPoint;
            transform.eulerAngles = _spawnRotation;
            container.gameObject.SetActive(true);
            collider.enabled = true;
            health = 100;
            uiHealth.value = health;
            startAttack = false;
            enemyAnimator.Idle();
            uiHealth.gameObject.SetActive(true);
        }

        public void Hide()
        {
            container.gameObject.SetActive(false);
            collider.enabled = false;
            IsBusy = false;
            startAttack = false;
        }

        private void EnemyDamageReaction(Transform _transform, int _damage)
        {
            if (_transform == this.transform)
            {
                Damage(_damage);
            }
        }

        private void Damage(int _damage)
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
            StopAllCoroutines();
            collider.enabled = false;
            startAttack = false;
            enemyAnimator.Die();
            uiHealth.gameObject.SetActive(false);
        }

        private void MoveToPlayer()
        {
            transform.position = Vector3.Lerp(transform.position, player.position, moveSpeed * Time.deltaTime);
            body.LookAt(player);
        }

        private void StartResultReaction(bool _result)
        {
            IsBusy = false;
            startAttack = false;
            enemyAnimator.Idle();
        }
    }
}
