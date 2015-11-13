/// <summary>
/// 类名: ItemDropManager
/// 描述: 物品掉落管理类，怪物死亡时，物品掉落的过程由该类完成。
/// </summary>

using UnityEngine;
using System.Collections.Generic;

namespace ActionGame
{
	public class ItemDropManager {
		Dictionary<int, float> m_DropRateDict;

		public void Init () 
		{
			m_DropRateDict = new Dictionary<int, float>();

			// Test
			m_DropRateDict.Add(0, 0.5f);
			m_DropRateDict.Add(1, 0.4f);
		}

		/// <summary>
		/// 返回一个掉落物品ID，若无掉落则返回-1
		/// </summary>
		public int GetADropItem(EnemyControl.ENEMYTYPE type)
		{
			int id = Random.value > 0.5f ? 1 : 0;
			if(Random.value > m_DropRateDict[id])
			{
				return id;
			}
			else
			{
				return -1;
			}
		}
	}
}