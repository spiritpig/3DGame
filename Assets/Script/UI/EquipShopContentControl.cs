/// <summary>
/// 类名: EquipShopContentControl
/// 描述: 装备商店内容控制类
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace ActionGame
{
	public class EquipShopContentControl : MonoBehaviour, IPanelContentControl {

		List<EquipShopItemControl> m_UIItemList;
		Dictionary<int, Item> m_GlobalItemList;
		
		// Use this for initialization
		public void Init() 
		{
			m_GlobalItemList = GlobalGameDataManager.Inst.GetItemDict();

			if(m_GlobalItemList.Count > 0)
			{
				RectTransform content = GetComponentInChildren<ScrollRect>().content;
				GameObject equipItem = Resources.Load("Prefab/UI/EquipItem") as GameObject;

				foreach(Item item in m_GlobalItemList.Values)
				{

				}
			}
		}
		
		public void RefreshData()
		{
        }
	}
}

