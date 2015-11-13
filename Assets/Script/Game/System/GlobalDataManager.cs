﻿/// <summary>
/// 类名: GlobalDataManager
/// 描述: 全局数据管理类，管理游戏内所有的怪物信息，物品信息等数据, 相当于一个游戏数据的数据仓库
/// 	 如生成一个物品是，就需要从该类中提取数据
/// </summary>

using UnityEngine;
using System.Collections;

namespace ActionGame
{
	public class GlobalDataManager : MonoBehaviour {
		ItemListManager m_ItemListManager;
		public ItemListManager ItemListManager {
			get {
				return m_ItemListManager;
			}
		}

		public static GlobalDataManager Inst;

		// Use this for initialization
		void Awake () 
		{
			m_ItemListManager = new ItemListManager();
			m_ItemListManager.Init();

			Inst = this;
		}

		public Item GetItem(int id)
		{
			if(id == -1)
			{
				return null;
			}
			else
			{
				return m_ItemListManager.GetItem(id);
			}
		}
	}
}