using Scripts.CommonCode;
using Scripts.PlayerCode;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UserInterface
{
	public class LobbyPanelController : MonoBehaviour, IInterfacePanel
	{
		[Header("Panel Setting")]
		[SerializeField] private UIPanelType uiPanelType = UIPanelType.Lobby;

		[Header("Components")]
		[SerializeField] private PlayerStorage playerStorage;
		[SerializeField] private Transform panelContainer;
		[SerializeField] private Button startButton;

		private void Awake()
		{
			Init();
			ConfigureButton();
		}

		private void ConfigureButton()
		{
			startButton.onClick.RemoveAllListeners();
			startButton.onClick.AddListener(() =>
			{
				GameManager.StartLevelAction?.Invoke();
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

