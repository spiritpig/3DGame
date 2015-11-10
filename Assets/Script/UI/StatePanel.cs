/// <summary>
/// 类名: PackagePanel
/// 描述: 控制UI界面的显示和隐藏，用于需要按钮点击隐藏或显示的UI界面
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ActionGame
{
	public class StatePanel : MonoBehaviour {
		bool m_ActiveFlag = false;
		public StateContent m_StateContent = null;

		void Start()
		{
			Button stateBtn = GameObject.Find("MainUICanvas/InfoPanel/StateBtn").GetComponent<Button>();
			stateBtn.onClick.AddListener(ActiveByCurrentState);
			m_StateContent.Init();

			gameObject.SetActive(false);
		}

		/// <summary>
		/// 根据当前状态，切换面板的活动状态
		/// </summary>
		public void ActiveByCurrentState()
		{
			m_ActiveFlag = !m_ActiveFlag;
			gameObject.SetActive( m_ActiveFlag );

			// 刷新状态信息的显示数据
			if( m_ActiveFlag )
			{
				_RefreshStateData();
			}
		}

		/// <summary>
		/// 刷新面板的数据
		/// </summary>
		void _RefreshStateData()
		{
			m_StateContent.RefreshData();
		}
	}
}