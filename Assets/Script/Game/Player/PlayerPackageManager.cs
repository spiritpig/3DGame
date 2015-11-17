/// <summary>
/// 类名: PlayerPackageManager
/// 描述: 玩家背包管理类，负责管理玩家的背包
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ActionGame
{
	public class PlayerPackageManager {
		List<Item> m_ItemList;
		public List<Item> ItemList {
			get { return m_ItemList; }
		}

		int m_PackageSize = 42;

		public void Init () 
		{
			m_ItemList = new List<Item>(42);
			for(int i=0; i<m_PackageSize; ++i)
			{
				m_ItemList.Add(null);
			}
		}

		/// <summary>
		/// 添加元素到背包中，不过需要找到背包中，下标最小的第一个空位置。
		/// </summary>
		public bool AddItem(Item item)
		{
			if(item != null)
			{
				int index = m_ItemList.FindIndex((Item obj) => {return obj == null;});
				if(index == -1)
				{
					Debug.Log("背包已经满了");
				}
				else
				{
					m_ItemList[index] = item;
					return true;
				}
			}

			return false;
		}

		public void RemoveItem(Item item)
		{
			if(item != null)
			{
				int index = m_ItemList.FindIndex((Item obj) => {return obj.Id == item.Id;});
				if(index == -1)
				{
					return;
				}
				else
				{
					m_ItemList[index] = null;
				}
			}
		}

		public void RemoveItemAt(int index)
		{
			if(index > -1 && index < m_PackageSize)
			{
				m_ItemList[index] = null;
			}
		}

		/// <summary>
		/// 交换两元素
		/// </summary>
		public void SwapItem(int i, int j)
		{
			Item temp = m_ItemList[i];
			m_ItemList[i] = m_ItemList[j];
			m_ItemList[j] = temp;
		}
	}
}