/// <summary>
/// 类名: Item3DListControl
/// 描述: 3D预置物品管理，负责管理表示掉落到地上的物品。
/// </summary>

using UnityEngine;
using System.Collections.Generic;

namespace ActionGame
{
	public class Item3DListControl : MonoBehaviour {
		List<ItemTest3DControl> m_Item3DList;
		ItemTest3DControl m_TempData;
		int m_CurUseCount;

		// Use this for initialization
		void Awake () 
		{
			m_CurUseCount = 0;

			m_Item3DList = new List<ItemTest3DControl>();
			foreach(ItemTest3DControl item3D in transform.GetComponentsInChildren<ItemTest3DControl>())
			{
				m_Item3DList.Add(item3D);
			}
		}

		void Update()
		{
			for(int i=0; i<m_CurUseCount; )
			{
				// 使用完毕的3D物品和最后一个使用中的3D物品交换位置
				// 同时，下标不移动。因为，还需要更新交换过来的3D物品
				if(m_Item3DList[i].IsPicked)
				{
					m_Item3DList[i].gameObject.SetActive(false);
					_Swap(i, m_CurUseCount-1);
					--m_CurUseCount;
				}
				// 若是未产生交换则增加下标
				else
				{
					++i;
				}
			}
		}

		public void SetAItem(Item item, Vector3 pos)
		{
			if(m_CurUseCount >= m_Item3DList.Count)
			{
				Debug.Log("物品掉落不够用了");
				return;
			}

			if(item == null)
			{
				return;
			}

			m_Item3DList[m_CurUseCount].SetItem3D(item, pos);
			++m_CurUseCount;
		}

		/// <summary>
		/// 交换I和J处的元素
		/// </summary>
		void _Swap(int i, int j)
		{
			m_TempData = m_Item3DList[i];
			m_Item3DList[i] = m_Item3DList[j];
			m_Item3DList[j] = m_TempData;
		}
	}
}