/// <summary>
/// 类名: PackageContentControl
/// 描述: 控制背包的更新
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace ActionGame
{
	public class PackageContentControl : MonoBehaviour, IPanelContentControl {
		List<ItemUIControl> m_UIItemList;
		GridLayoutGroup m_ItemLayout;
		
		// Use this for initialization
		public void Init() 
		{
			m_UIItemList = new List<ItemUIControl>();
			foreach(ItemUIControl uiItem in GetComponentsInChildren<ItemUIControl>())
			{
				m_UIItemList.Add(uiItem);
			}

			m_ItemLayout = GetComponent<GridLayoutGroup>();
		}
		
		public void RefreshData()
		{
		}
	}
}