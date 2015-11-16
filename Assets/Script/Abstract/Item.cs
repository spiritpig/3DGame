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
			get { return m_Id; }
			set { m_Id = value; }
		}

		protected string m_Name = "kong";
		public string Name {
			get { return m_Name; }
			set { m_Name = value; }
		}

		protected int m_Price = 100;
		public int Price {
			get { return m_Price; }
			set { m_Price = value; }
		}

		protected string m_Info = "11";
		public string Info {
			get { return m_Info; }
			set { m_Info = value; }
		}

		protected int m_Val;					// 物品的数值，对于药剂是恢复量，对于装备则是属性值，如攻击力，防御力等。
		public int Val {
			get { return m_Val; }
			set { m_Val = value; }
		}

		protected Texture m_ItemTex = null;
		public Texture ItemTex {
			get { return m_ItemTex; }
			set { m_ItemTex = value; }
		}

		public enum QUALITY
		{
			QT_WHITE,
			QT_GREEN,
			QT_BLUE,
			QT_PURPLE,
			QT_GOLD
		}
		protected QUALITY m_Quality = QUALITY.QT_WHITE;
		public QUALITY Quality {
			get { return m_Quality; }
			set { m_Quality = value; }
		}
		
		public enum ITEM_TYPE
		{
			IT_MAT,
			IT_EQUIP,
			IT_GEM,
			IT_POSION,
			IT_QUEST,
			IT_OTHER
		}
		protected ITEM_TYPE m_Type = 0;
		public ITEM_TYPE Type {
			get { return m_Type; }
			set { m_Type = value; }
		}

		public bool m_CanWear = false;
		public bool CanWear {
			get { return m_CanWear; }
			set { m_CanWear = value; }
		}
		
		public enum EQUIP_TYPE
		{
			EPT_NONE,
			EPT_HEAD,
			EPT_FOOT,
			EPT_WEAPON,
			EPT_SHIELD,
			EPT_ARMOR,
			EPT_NECKLACE
		}
		protected EQUIP_TYPE m_EquipType = EQUIP_TYPE.EPT_NONE;
		public EQUIP_TYPE EquipType {
			get { return m_EquipType; }
			set { m_EquipType = value; }
		}

		protected int m_Level;
		public int Level {
			get { return m_Level; }
			set { m_Level = value; }
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