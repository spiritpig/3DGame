/// <summary>
/// 类名: PanelBase
/// 描述: UI面板的基类，提供一些共有方法。
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ActionGame
{
	public abstract class PanelBase : MonoBehaviour {
		protected bool m_ActiveFlag = false;
		protected IPanelContentControl m_Content;
		public IPanelContentControl Content {
			get {
				return m_Content;
			}
		}
		
		/// <summary>
		/// 根据当前状态，切换面板的活动状态
		/// </summary>
		public virtual void ActiveByCurrentState()
		{
			m_ActiveFlag = !m_ActiveFlag;
			gameObject.SetActive( m_ActiveFlag );
			
			// 刷新状态信息的显示数据
			if( m_ActiveFlag )
			{
				transform.SetAsLastSibling();
				RefreshData();
			}
		}
		
		/// <summary>
		/// 刷新面板的数据
		/// </summary>
		public virtual void RefreshData()
		{
			if(m_Content != null)
			{
				m_Content.RefreshData();
			}
		}
	}
}