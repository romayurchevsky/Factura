using System;
using Scripts.PlayerCode;
using Scripts.UserInterface;
using UnityEngine;

namespace Scripts.CommonCode
{
	public class GameManager : MonoBehaviour
	{
		[Header("Storages")]
		[SerializeField] private PlayerStorage playerStorage;
		[SerializeField] private GameStorage gameStorage;

		#region Actions
		public static Action StartGameAction;
		public static Action PlayerLoadedAction;
		public static Action StartLobbyAction;
		public static Action StartLevelAction;
		public static Action StartGameplayAction;
		public static Action<bool> StartResultAction;
		public static Action<int> PlayerDamageAction;
		public static Action<Transform, int> EnemyDamageAction;
		#endregion

		private void OnEnable()
		{
			StartGameAction += StartGame;
			PlayerLoadedAction += PlayerLoaded;
			StartLobbyAction += StartLobby;
			StartLevelAction += StartLevel;
		}

		private void Start()
		{
			StartGameAction?.Invoke();
		}

		private void OnDisable()
		{
			StartGameAction += StartGame;
			PlayerLoadedAction += PlayerLoaded;
			StartLobbyAction += StartLobby;
			StartLevelAction += StartLevel;
		}

		private void OnDestroy()
		{
			SavePlayer();
		}

		private void OnApplicationQuit()
		{
			SavePlayer();
		}

		private void OnApplicationPause(bool _pause)
		{
			if (_pause)
			{
				playerStorage.SavePlayer();
			}
		}

		private void SavePlayer()
		{
			playerStorage.SavePlayer();
		}

		private void PlayerLoaded()
		{
			StartLobbyAction?.Invoke();
		}

		private void StartLobby()
		{
			UIController.ShowUIPanelAloneAction?.Invoke(UIPanelType.Lobby);
		}

		private void StartLevel()
		{
			UIController.ShowUIPanelAloneAction?.Invoke(UIPanelType.Game);
		}

		private void StartGame()
		{
			UIController.ShowUIPanelAloneAction?.Invoke(UIPanelType.Promo);
		}
	}
}
