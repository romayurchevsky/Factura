using Scripts.PlayerCode;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UserInterface
{
	public class PromoPanelController : MonoBehaviour, IInterfacePanel
	{
		[Header("Base")]
		[SerializeField] private UIPanelType uiPanelType = UIPanelType.Promo;
		[SerializeField] private Transform panelContainer;
		[SerializeField] private PlayerStorage playerStorageSO;

		[Header("Loading Image")]
		[SerializeField] private Slider loadingSlider;

		private void Awake()
		{
			Init();
		}

		private IEnumerator LoadingProcess()
		{
			loadingSlider.value = 0f;
			while (loadingSlider.value < loadingSlider.maxValue)
			{
				loadingSlider.value += Time.deltaTime;
				yield return null;
			}

			playerStorageSO.LoadPlayer();
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
			StartCoroutine(LoadingProcess());
		}

		public void Init()
		{
			UIController.InterfacePanels.Add(this);
		}
		#endregion
	}
}
