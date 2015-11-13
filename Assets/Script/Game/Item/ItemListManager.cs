/// <summary>
/// 类名: ItemListManager
/// 描述: 物品列表管理类,相当于一个物品的数据仓库。
/// 储存游戏内，所有可能出现的物品 
/// </summary>

using UnityEngine;
using System.Collections.Generic;

namespace ActionGame
{
	public class ItemListManager {
		int m_CurId = -1;
		Dictionary<int, Item> m_ItemDict;

		public void Init()
		{
			m_ItemDict = new Dictionary<int, Item>();

			// 测试物品
			Item item = new ItemTest();
			item.ItemTex = Resources.Load("Texture/armor0-icon") as Texture;
			AddItem(item);

			item = new ItemTest();
			item.ItemTex = Resources.Load("Texture/sword2-icon") as Texture;
			AddItem(item);
		}

		public void AddItem(Item item)
		{
			m_ItemDict.Add(++m_CurId, item);
		}

		public Item GetItem(int id)
		{
			return m_ItemDict[id];
		}               
	}
}