/// <summary>
/// 类名: PackagePanel
/// 描述: 控制UI界面的显示和隐藏，用于需要按钮点击隐藏或显示的UI界面
/// </summary>

using UnityEngine;
using System.Collections;

namespace ActionGame
{
	public class StatePanel : MonoBehaviour {
		bool m_ActiveFlag = false;
		StateContent m_StateContent = null;

		// Use this for initialization
		void Start () 
		{
			m_StateContent = GameObject.FindGameObjectWithTag("UI_StateContent").GetComponent<StateContent>();
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
			RefreshStateData();
		}

		void RefreshStateData()
		{
			m_StateContent.RefreshData();
		}
	}
}