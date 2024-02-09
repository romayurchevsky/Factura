using System;
using UnityEngine;

namespace Scripts.CommonCode
{
	[Serializable]
	public class GameBaseParameters
	{
		[field: SerializeField] public float MinShootStrength { get; private set; }
		[field: SerializeField] public float MaxShooStrength { get; private set; }
		[field: SerializeField] public float BulletMoveSpeed { get; private set; }
	}
}
