/// <summary>
/// 类名: EnemyManager
/// 描述: 管理怪物列表,对怪物实行统一管理
/// 注意: 此处需保证怪物在EnemyManager之前初始化完成
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ActionGame
{
	public class EnemyManager : MonoBehaviour {
		Item3DListControl m_Item3DListController;
		EnemyControl m_TempData;
		List<EnemyControl> m_EnemyList;
		public List<EnemyControl> EnemyList {
			get {
				return m_EnemyList;
			}
		}
		int m_CurUseCount = 0;

		// Use this for initialization
		public void Init () 
		{
			m_Item3DListController = GameObject.Find("Environment/Item3DList").GetComponent<Item3DListControl>();

			m_EnemyList = new List<EnemyControl>();
			foreach(EnemyControl enemy in transform.GetComponentsInChildren<EnemyControl>())
			{
				m_EnemyList.Add(enemy);
			}
			m_CurUseCount = m_EnemyList.Count;
		}

		public EnemyControl GetNearestEnemy(Vector3 pos)
		{
			if(m_EnemyList.Count <= 0)
			{
				return null;
			}

			float disTemp, disMin = float.MaxValue;
			EnemyControl nearestEnemy = null;

			// 找到最近的怪物并返回
			for(int i=0; i<m_EnemyList.Count; ++i)
			{
				// 死亡的怪物不参与运算
				if(m_EnemyList[i].Data.state == EnemyControl.ENEMYSTATE.ES_DEATH ||
				   m_EnemyList[i].Data.state == EnemyControl.ENEMYSTATE.ES_DEATH_END)
				{
					continue;
				}

				// 比较距离大小
				disTemp = Vector3.Distance(pos, m_EnemyList[i].transform.position);
				if(disTemp < disMin)
				{
					disMin = disTemp;
					nearestEnemy = m_EnemyList[i];
				}
			}

			return nearestEnemy;
		}

		void Update()
		{
			for(int i=0; i<m_CurUseCount; )
			{
				if(m_EnemyList[i].Data.state == EnemyControl.ENEMYSTATE.ES_DEATH_END)
				{
					// 怪物死亡了，产生掉落
					int itemId = DungonManager.Inst.ItemDropManager.GetADropItem(EnemyControl.ENEMYTYPE.ET_WOLFMAN);
					Item item = GlobalDataManager.Inst.GetItem(itemId);
					m_Item3DListController.SetAItem(item, m_EnemyList[i].GenItemDropPos());

					// 移动怪物到后方
					m_EnemyList[i].gameObject.SetActive(false);
					_Swap(i,m_CurUseCount-1);
					--m_CurUseCount;
				}
				else
				{
					++i;
				}
			}
		}

		void _Swap(int i, int j)
		{
			m_TempData = m_EnemyList[i];
			m_EnemyList[i] = m_EnemyList[j];
			m_EnemyList[j] = m_TempData;
		}
	}
}