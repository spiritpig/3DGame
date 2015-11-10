/// <summary>
/// 类名： DungonItemManager
/// 用途： 控制地下城区块的行为
/// </summary>

using UnityEngine;
using System.Collections.Generic;

namespace ActionGame
{
	public enum DungonType
	{
		DT_WOLF
	}

	public class DungonItemManager : MonoBehaviour {
		Dictionary<DungonType, DungonItemControl> m_ItemDict;

		// Use this for initialization
		void Awake () 
		{
			m_ItemDict = new Dictionary<DungonType, DungonItemControl>();
			foreach(DungonItemControl item in transform.GetComponentsInChildren( typeof(DungonItemControl) ))
			{
				m_ItemDict.Add(item.Type,item);
			}
		}

		/// <summary>
		/// 暂时使用硬代码完成该函数，未来需要将地下城管理起来，
		/// </summary>
		public bool IsSelectedDungonWolf()
		{
			return m_ItemDict[DungonType.DT_WOLF].IsSelected;
		}
	}
}