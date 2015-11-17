/// <summary>
/// 类名: EquipShopItemControl
/// 描述: 装备商店物品项控制类
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ActionGame
{
	public class EquipShopItemControl : MonoBehaviour{
		RawImage m_Icon;
		Text m_Name;
		Text m_Info;
		Text m_Price;
		Item m_SelfItem;
		Button m_BuyBtn;
		public Item SelfItem {
			get {
				return m_SelfItem;
			}
		}

		bool m_IsBuy = false;
		public bool IsBuy {
			get {
				return m_IsBuy;
			}
		}

		public void Init () 
		{
			m_Icon = transform.FindChild("Icon").GetChild(0).GetComponent<RawImage>();
			m_Name = transform.FindChild("Name").GetComponent<Text>();
			m_Info = transform.FindChild("Info").GetComponent<Text>();
			m_Price = transform.FindChild("Price").GetComponent<Text>();
			m_BuyBtn = transform.FindChild("BuyBtn").GetComponent<Button>();
			m_BuyBtn.onClick.AddListener(OnClick);
		}

		public void SetItemData(Item item)
		{
			m_Icon.texture = item.ItemTex;
			m_Name.text = item.Name;
			m_Info.text = item.Info;
			m_Price.text = item.Price.ToString();

			m_SelfItem = item; 
		}

		public void OnClick()
		{
			DungonManager.Inst.Player.OnBuy(m_SelfItem);
		}
	}
}