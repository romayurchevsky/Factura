using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Scripts.CommonCode
{
	public class CameraController : MonoBehaviour
	{
		[Header("Main Camera")]
		[SerializeField] private Camera mainCamera;

		[Header("Virtual Cameras")]
		[SerializeField] private CinemachineVirtualCamera playerCamera;
		[SerializeField] private CinemachineVirtualCamera lobbyCamera;
		[SerializeField] private float shakeDuration;

		#region get/set
		public Camera GetCamera => mainCamera;
		#endregion

		#region Actions
		public static Action<Transform> SetFollowAction;
		public static Action SetLobbyAction;
		public static Action ShakeCameraAction;
		#endregion

		#region Variables
		private CinemachineBasicMultiChannelPerlin shakeChanel;
		#endregion

		private void Awake()
        {
			shakeChanel = playerCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
		}

        private void OnEnable()
		{
			SetFollowAction += SetFollow;
			ShakeCameraAction += ShakeCamera;
			SetLobbyAction += SetLobbyCamera;
		}

		private void OnDisable()
		{
			SetFollowAction -= SetFollow;
			ShakeCameraAction -= ShakeCamera;
			SetLobbyAction -= SetLobbyCamera;
		}

		private void SetFollow(Transform _transform)
		{
			playerCamera.Priority = 10;
			lobbyCamera.Priority = 0;

			playerCamera.Follow = _transform;
		}

		private void ShakeCamera()
        {
			StopAllCoroutines();
			StartCoroutine(ShakeProcess());
        }

		private IEnumerator ShakeProcess()
        {
			shakeChanel.m_AmplitudeGain = 1;
			yield return new WaitForSeconds(shakeDuration);
			shakeChanel.m_AmplitudeGain = 0;
		}

		private void SetLobbyCamera()
        {
			playerCamera.Priority = 0;
			lobbyCamera.Priority = 10;
		}
	}
}