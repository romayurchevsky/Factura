using Scripts.CommonCode;
using UnityEngine;

namespace Scripts.PlayerCode
{
	[CreateAssetMenu(menuName = "ScriptableObjects/PlayerStorage", fileName = "PlayerStorage")]
	public class PlayerStorage : ScriptableObject
	{
		[Header("Basic")]
		[SerializeField] private string playerPrefsSaveString = "_playerSave";

		[Header("ConcretePlayer")]
		[SerializeField] private Player concretePlayer = new Player();

		#region Geters/Seters
		public Player ConcretePlayer { get => concretePlayer; }
        public bool IsPlayerIsLoaded { get; private set; }
        #endregion

        public void SavePlayer()
		{
			if (IsPlayerIsLoaded)
			{
                PlayerPrefs.SetString(playerPrefsSaveString, JsonUtility.ToJson(concretePlayer));
            }
		}

		public void LoadPlayer()
		{
			var playerString = PlayerPrefs.GetString(playerPrefsSaveString, "");
			if (playerString != "")
			{
				concretePlayer = JsonUtility.FromJson<Player>(playerString);
			}
			else
			{
				concretePlayer = new Player();
            }

			IsPlayerIsLoaded = true;
            GameManager.PlayerLoadedAction?.Invoke();
		}
	}
}
