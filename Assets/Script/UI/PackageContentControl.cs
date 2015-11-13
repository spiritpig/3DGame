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
		List<Item> m_PlayerItemList;
		GridLayoutGroup m_ItemLayout;
		
		// Use this for initialization
		public void Init() 
		{
			m_UIItemList = new List<ItemUIControl>();
			int i=0;
			foreach(ItemUIControl uiItem in GetComponentsInChildren<ItemUIControl>())
			{
				uiItem.ItemIndexInPackge = i;
				m_UIItemList.Add(uiItem);

				++i;
			}

			m_ItemLayout = GetComponent<GridLayoutGroup>();
			m_PlayerItemList = DungonManager.Inst.GetPlayerItemList();
		}

		public void RefreshData()
		{
			for(int i=0; i<m_PlayerItemList.Count; ++i)
			{
				// 空物品，则直接进行下一次循环
				if(m_PlayerItemList[i] != null)
				{
					// 若是已经有物品了，不需要作处理
					if(m_UIItemList[i].ItemTexture == null)
					{
						m_UIItemList[i].SetItem(m_PlayerItemList[i], i);
						if(m_PlayerItemList[i].ItemTex != null)
						{
							m_UIItemList[i].SetAlpha(1.0f);
						}
					}
				}
			}
		}
	}
}