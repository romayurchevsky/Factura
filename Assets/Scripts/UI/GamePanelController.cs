using Scripts.CommonCode;
using Scripts.PlayerCode;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UserInterface
{
	public class GamePanelController : MonoBehaviour, IInterfacePanel
	{
		[Header("Panel Setting")]
		[SerializeField] private UIPanelType uiPanelType = UIPanelType.Game;

		[Header("Components")]
		[SerializeField] private GameStorage gameStorage;
		[SerializeField] private PlayerStorage playerStorage;
		[SerializeField] private Transform panelContainer;
		[SerializeField] private Button gameplayButton;

		private void Awake()
		{
			Init();
			ConfigureButton();
		}

		private void OnEnable()
		{
			GameManager.StartLevelAction += LevelStartReaction;
        }

		private void OnDisable()
		{
			GameManager.StartLevelAction -= LevelStartReaction;
        }

		private void LevelStartReaction()
        {
			gameplayButton.gameObject.SetActive(true);
		}

		private void ConfigureButton()
		{
			gameplayButton.onClick.RemoveAllListeners();
			gameplayButton.onClick.AddListener(() =>
			{
				gameplayButton.gameObject.SetActive(false);
				GameManager.StartGameplayAction?.Invoke();
			});
		}

		#region IInterfacePanel
		public UIPanelType UIPanelType { get => uiPanelType; }

		public void Hide()
		{
			panelContainer.gameObject.SetActive(false);
		}

		public void Show()
		{
			panelContainer.gameObject.SetActive(true);
		}

		public void Init()
		{
			UIController.InterfacePanels.Add(this);
		}
		#endregion
	}

}
