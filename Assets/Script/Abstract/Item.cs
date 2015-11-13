/// <summary>
/// 类名: Item
/// 描述: 物品类，物品数据将储存在该类中。同时也将作为更复杂的如装备，材料等物品的基类
/// </summary>

using UnityEngine;
using System.Collections;

namespace ActionGame
{
	public abstract class Item {
		protected int m_Id = 1;
		public int Id {
			get {
				return m_Id;
			}
		}

		protected string m_Name = "1";
		protected int m_Price = 100;
		protected string m_Info = "11";
		protected int m_PosInPackage = -1;

		protected Texture m_ItemTex = null;
		public Texture ItemTex {
			get { return m_ItemTex; }
			set { m_ItemTex = value; }
		}

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