/// <summary>
/// 类名: Item
/// 描述: 物品类，物品数据将储存在该类中。同时也将作为更复杂的如装备，材料等物品的基类
/// </summary>

using UnityEngine;
using System.Collections;

namespace ActionGame
{
	public abstract class Item : MonoBehaviour {
		protected int m_Id;
		protected string m_Name;
		protected int m_Price;
		protected string m_Info;
		protected Texture m_ItemTex;

		/// <summary>
		/// 处理被拾取的情况
		/// </summary>
		public abstract void OnPick();

		/// <summary>
		/// 处理装备掉落
		/// </summary>
		public abstract void OnGroundDrop();
    }
}