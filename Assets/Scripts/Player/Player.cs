using System;
using UnityEngine;

namespace Scripts.PlayerCode
{
	[Serializable]
	public class Player
	{
		[field: SerializeField] public float Money { get; private set; } = 0;

		public void AddMoney(int _count)
        {
			Money += _count;
		}
	}
}
