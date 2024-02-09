using Scripts.CommonCode;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UserInterface
{
	public class ResultPanelController : MonoBehaviour, IInterfacePanel
	{
		[Header("Panel Setting")]
		[SerializeField] private UIPanelType uiPanelType = UIPanelType.Result;

		[Header("Components")]
		[SerializeField] private Transform panelContainer;
		[SerializeField] private Button lobbyButton;
		[SerializeField] private TMP_Text resultText;

		private void Awake()
		{
			Init();
			ConfigureButton();
		}

		private void OnEnable()
		{
			GameManager.StartResultAction += StartResultReaction;
		}

		private void OnDisable()
		{
			GameManager.StartResultAction -= StartResultReaction;
		}

		private void StartResultReaction(bool _result)
		{
			PreperePanel(_result);
			UIController.ShowUIPanelAlongAction?.Invoke(UIPanelType.Result);
		}

		private void ConfigureButton()
		{
			lobbyButton.onClick.RemoveAllListeners();
			lobbyButton.onClick.AddListener(() =>
			{
				GameManager.StartLobbyAction?.Invoke();
			});

		}

		private void PreperePanel(bool _result)
        {
			resultText.text = _result ? "you win" : "you lose";
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

