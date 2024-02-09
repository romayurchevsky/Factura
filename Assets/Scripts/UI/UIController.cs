using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.UserInterface
{
	public class UIController : MonoBehaviour
	{
		public static List<IInterfacePanel> InterfacePanels { get; set; } = new List<IInterfacePanel>();
		public static Action<UIPanelType> ShowUIPanelAloneAction;
		public static Action<UIPanelType> ShowUIPanelAlongAction;

		public static UIPanelType CurrentPanel { get; private set; }

		private void OnEnable()
		{
			ShowUIPanelAloneAction += ShowPanelAlone;
			ShowUIPanelAlongAction += ShowPanelAlong;
		}

		private void OnDisable()
		{
			ShowUIPanelAloneAction -= ShowPanelAlone;
			ShowUIPanelAlongAction -= ShowPanelAlong;
		}

		private void ShowPanelAlone(UIPanelType _uIPanelType)
		{
			foreach (IInterfacePanel item in InterfacePanels)
			{
				if (item.UIPanelType == _uIPanelType)
				{
					item.Show();
				}
				else
				{
					item.Hide();
				}
			}
			CurrentPanel = _uIPanelType;
		}

		private void ShowPanelAlong(UIPanelType _uIPanelType)
		{
			InterfacePanels.Find(_somePanel => _somePanel.UIPanelType == _uIPanelType)?.Show();
		}
	}
}
