using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.EnemyCode
{
    public class EnemyAnimator : MonoBehaviour
    {
		[Header("Animator")]
		[SerializeField] private Animator animator;

		#region Variables
		private readonly int idleKey = Animator.StringToHash("Idle");
		private readonly int attackKey = Animator.StringToHash("Attack");
		private readonly int moveKey = Animator.StringToHash("Move");
		private readonly int dieKey = Animator.StringToHash("Die");
		#endregion

		public void Attack()
		{
			animator.SetTrigger(attackKey);
		}

		public void Idle()
		{
			animator.SetTrigger(idleKey);
		}

		public void Move()
		{
			animator.SetTrigger(moveKey);
		}		
		
		public void Die()
		{
			animator.SetTrigger(dieKey);
		}
	}
}
