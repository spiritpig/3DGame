/// <summary>
/// 类名: StatePanel
/// 描述: 控制UI界面的显示和隐藏，用于需要按钮点击隐藏或显示的UI界面
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ActionGame
{
	public class StatePanel : PanelBase {

		void Start()
		{
			Button stateBtn = GameObject.Find("MainUICanvas/InfoPanel/StateBtn").GetComponent<Button>();
			stateBtn.onClick.AddListener(ActiveByCurrentState);
			m_Content = transform.GetComponentInChildren<StateContentControl>();
			m_Content.Init();

			gameObject.SetActive(false);
		}
	}
}