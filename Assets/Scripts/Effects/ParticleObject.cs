using System.Collections;
using UnityEngine;

namespace Scripts.Effects
{
	public class ParticleObject : MonoBehaviour
	{
		[Header("Parameters")]
		[SerializeField] private ParticleType particleType;

		[Header("Components")]
		[SerializeField] private ParticleSystem particleEffect;

		#region get/set
		public ParticleType GetParticleType => particleType;
		public bool IsBusy { get; private set; }
		#endregion

		private void Awake()
		{
			particleEffect.gameObject.SetActive(false);
		}

		public void Play(Vector3 _normal)
		{
			StartCoroutine(WaitToDeadParticle(_normal));
		}

		private IEnumerator WaitToDeadParticle(Vector3 _normal)
		{
			IsBusy = true;
			transform.rotation = Quaternion.FromToRotation(Vector3.up, _normal);
			particleEffect.gameObject.SetActive(true);
			particleEffect.Play();
			yield return new WaitForSecondsRealtime(particleEffect.main.duration);
			particleEffect.gameObject.SetActive(false);
			IsBusy = false;
		}
	}
}
