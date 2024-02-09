using Scripts.CommonCode;
using Scripts.EnemyCode;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.LevelCode
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private List<Transform> enemyPoints = new List<Transform>();

        private void OnEnable()
        {
            GameManager.StartLevelAction += StarLevelReaction;
        }

        private void OnDisable()
        {
            GameManager.StartLevelAction -= StarLevelReaction;
        }

        private void StarLevelReaction()
        {
            for (int i = 0; i < enemyPoints.Count; i++)
            {
                EnemyPool.SpawnAndPlaceAction?.Invoke(enemyPoints[i].position, enemyPoints[i].eulerAngles);
            }
        }
    }
}
