/// <summary>
/// 类名: GlobalGameDataManager
/// 描述: 全局数据管理类，管理游戏内所有的怪物信息，物品信息等数据, 相当于一个游戏数据的数据仓库
/// 	 如生成一个物品是，就需要从该类中提取数据
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ActionGame
{
	public class GlobalGameDataManager : MonoBehaviour {
		ItemListManager m_ItemListManager;
		public ItemListManager ItemListManager {
			get {
				return m_ItemListManager;
			}
		}

		public static GlobalGameDataManager Inst;

		// Use this for initialization
		void Awake () 
		{
			m_ItemListManager = new ItemListManager();
			m_ItemListManager.Init();

			Inst = this;

			StartCoroutine( m_ItemListManager.ReadItemInfo() );
		}

		public Item GetItem(int id)
		{
			if(id == -1 || !m_ItemListManager.IsConstructDone)
			{
				return null;
			}
			else
			{
				return m_ItemListManager.GetItem(id);
			}
		}

		public Dictionary<int, Item> GetItemDict()
		{
			return m_ItemListManager.ItemDict;
		}
	}
}