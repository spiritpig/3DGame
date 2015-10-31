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
		public List<GameObject> m_EnemyList;

		// Use this for initialization
		public void Init () 
		{
			foreach(GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
			{
				m_EnemyList.Add(go);
			}
		}

		public GameObject GetNearestEnemy(Vector3 pos)
		{
			float disTemp, disMin;
			GameObject nearestEnemy = m_EnemyList[0];
			disMin = Vector3.Distance(pos, nearestEnemy.transform.position);;

			for(int i=1; i<m_EnemyList.Count; ++i)
			{
				disTemp = Vector3.Distance(pos, m_EnemyList[i].transform.position);
				if(disTemp < disMin)
				{
					disMin = disTemp;
					nearestEnemy = m_EnemyList[i];
				}
			}

			return nearestEnemy;
		}
	}
}