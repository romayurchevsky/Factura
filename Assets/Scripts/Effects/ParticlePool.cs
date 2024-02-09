using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Effects
{
	public class ParticlePool : MonoBehaviour
	{
		[Header("Components")]
		[SerializeField] private Transform particlesContainer;
		[SerializeField] private List<ParticleObject> particles = new List<ParticleObject>();

		#region Actions
		public static Action<Vector3, Vector3, ParticleType> PlayParticleAction = default;
		#endregion

		#region Variables
		private List<ParticleObject> particleObjects = new List<ParticleObject>();
		#endregion

		private void Awake()
		{
			PrepareParticles();
		}

		private void OnEnable()
		{
			PlayParticleAction += PlayParticle;
		}

		private void OnDisable()
		{
			PlayParticleAction -= PlayParticle;
		}

		private void PlayParticle(Vector3 _position, Vector3 _normal, ParticleType _particleType)
		{
			ParticleObject particle = GetFreeParticle(_particleType);
			particle.transform.position = _position;
			particle.Play(_normal);
		}

		private ParticleObject GetFreeParticle(ParticleType _particleType)
		{
			ParticleObject result = particleObjects.Find((_part) => _part.GetParticleType == _particleType && !_part.IsBusy);
			if (result == null)
			{
				result = CreateParticle(_particleType);
			}

			return result;
		}

		private void PrepareParticles()
		{
			foreach (var particle in particleObjects)
			{
				for (int i = 0; i < 10; i++)
				{
					CreateParticle(particle.GetParticleType);
				}
			}
		}

		private ParticleObject CreateParticle(ParticleType _particleType)
		{
			ParticleObject result = Instantiate(particles.Find((_prefab) => _prefab.GetParticleType == _particleType), particlesContainer);
			particleObjects.Add(result);
			return result;
		}
	}
}
