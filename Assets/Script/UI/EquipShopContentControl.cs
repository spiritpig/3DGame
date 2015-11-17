/// <summary>
/// 类名: EquipShopContentControl
/// 描述: 装备商店内容控制类
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace ActionGame
{
	public class EquipShopContentControl : MonoBehaviour, IPanelContentControl {

		AudioClip m_ChangeTabClip;
		ScrollRect m_EquipItemScrollRect;
		List<EquipShopItemControl> m_UIItemList;
		Dictionary<int, Item> m_GlobalItemList;

		public enum EQUIP_TYPEON
		{
			ETO_WEAPON,
			ETO_ARMOR,
			ETO_HEAD,
			ETO_SHOE
		}
		EQUIP_TYPEON m_EquipTypeOn = EQUIP_TYPEON.ETO_WEAPON;

		public GameObject m_WeaponContent, m_ArmorContent,
							m_HeadContent, m_ShoeContent;
		
		// Use this for initialization
		public void Init() 
		{
			m_ChangeTabClip = Resources.Load("Sound/Change_Tab") as AudioClip;
			m_EquipItemScrollRect = transform.GetComponentInChildren<ScrollRect>();
			m_UIItemList = new List<EquipShopItemControl>();
			m_GlobalItemList = GlobalGameDataManager.Inst.GetItemDict();

			if(m_GlobalItemList.Count > 0)
			{
				GameObject equipItem = Resources.Load("Prefab/UI/EquipItem") as GameObject;
				EquipShopItemControl tempControl = null;

				// 填充装备商店的数据
				foreach(Item item in m_GlobalItemList.Values)
				{
					if(item.EquipType == Item.EQUIP_TYPE.EPT_WEAPON)
					{
						// 添加子项
						GameObject weaponItem = Instantiate(equipItem, Vector3.zero, Quaternion.identity) as GameObject;
						weaponItem.transform.parent = m_WeaponContent.transform;
						weaponItem.transform.localScale = Vector3.one;

						// 填数据
						tempControl = weaponItem.GetComponent<EquipShopItemControl>();
						tempControl.Init();
						tempControl.SetItemData(item);
						m_UIItemList.Add(tempControl);
					}
					else
					if(item.EquipType == Item.EQUIP_TYPE.EPT_ARMOR)
					{
						// 添加子项
						GameObject armorItem = Instantiate(equipItem, Vector3.zero, Quaternion.identity) as GameObject;
						armorItem.transform.parent = m_ArmorContent.transform;
						armorItem.transform.localScale = Vector3.one;

						// 填数据
						tempControl = armorItem.GetComponent<EquipShopItemControl>();
						tempControl.Init();
						tempControl.SetItemData(item);
						m_UIItemList.Add(tempControl);
					}
					else
					if(item.EquipType == Item.EQUIP_TYPE.EPT_HEAD)
					{
						// 添加子项
						GameObject headItem = Instantiate(equipItem, Vector3.zero, Quaternion.identity) as GameObject;
						headItem.transform.parent = m_HeadContent.transform;
						headItem.transform.localScale = Vector3.one;

						// 填数据
						tempControl = headItem.GetComponent<EquipShopItemControl>();
						tempControl.Init();
						tempControl.SetItemData(item);
						m_UIItemList.Add(tempControl);
					}
					else
					if(item.EquipType == Item.EQUIP_TYPE.EPT_FOOT)
					{
						// 添加子项
						GameObject shoeItem = Instantiate(equipItem, Vector3.zero, Quaternion.identity) as GameObject;
						shoeItem.transform.parent = m_ShoeContent.transform;
						shoeItem.transform.localScale = Vector3.one;

						// 填数据
						tempControl = shoeItem.GetComponent<EquipShopItemControl>();
						tempControl.Init();
						tempControl.SetItemData(item);
						m_UIItemList.Add(tempControl);
					}
				}
			}

			m_ArmorContent.SetActive(false);
			m_HeadContent.SetActive(false);
			m_ShoeContent.SetActive(false);

			// 根据内容的长度，调整Content区域的长度
			_ContentWidthAdjsut();
		}

		/// <summary>
		/// 将Toggle控件的开启和关闭，与装备界面的物品展示区绑定起来
		/// 当某一toggle开启或关闭时，这边也会做出响应
		/// </summary>
		public void LinkToggleGroup(Transform toggleGroup)
		{
			Toggle tempToggle = toggleGroup.GetChild(0).GetComponent<Toggle>();
			tempToggle.onValueChanged.AddListener(OnWeaponTypeOn);

			tempToggle = toggleGroup.GetChild(1).GetComponent<Toggle>();
			tempToggle.onValueChanged.AddListener(OnArmorTypeOn);

			tempToggle = toggleGroup.GetChild(2).GetComponent<Toggle>();
			tempToggle.onValueChanged.AddListener(OnHeadTypeOn);

			tempToggle = toggleGroup.GetChild(3).GetComponent<Toggle>();
			tempToggle.onValueChanged.AddListener(OnShoeTypeOn);
		}
		
		public void RefreshData()
		{
        }

		public void OnWeaponTypeOn(bool flag)
		{
			_OnEquipTypeOnChange(EQUIP_TYPEON.ETO_WEAPON);
		}

		public void OnArmorTypeOn(bool flag)
		{
			_OnEquipTypeOnChange(EQUIP_TYPEON.ETO_ARMOR);
		}

		public void OnHeadTypeOn(bool flag)
		{
			_OnEquipTypeOnChange(EQUIP_TYPEON.ETO_HEAD);
		}

		public void OnShoeTypeOn(bool flag)
		{
			_OnEquipTypeOnChange(EQUIP_TYPEON.ETO_SHOE);
		}

		void _OnEquipTypeOnChange(EQUIP_TYPEON type)
		{
			AudioSource.PlayClipAtPoint(m_ChangeTabClip, transform.position);

			m_WeaponContent.SetActive(false);
			m_ArmorContent.SetActive(false);
			m_HeadContent.SetActive(false);
			m_ShoeContent.SetActive(false);

			// 根据类型，选择展示的界面
			switch(type)
			{
			case EQUIP_TYPEON.ETO_WEAPON:
				{
					m_WeaponContent.SetActive(true);
					m_EquipItemScrollRect.content = m_WeaponContent.transform as RectTransform;
				}
				break;

			case EQUIP_TYPEON.ETO_ARMOR:
				{
					m_ArmorContent.SetActive(true);
					m_EquipItemScrollRect.content = m_ArmorContent.transform as RectTransform;
				}
				break;

			case EQUIP_TYPEON.ETO_HEAD:
				{
					m_HeadContent.SetActive(true);
					m_EquipItemScrollRect.content = m_HeadContent.transform as RectTransform;
				}
				break;

			case EQUIP_TYPEON.ETO_SHOE:
				{
					m_ShoeContent.SetActive(true);
					m_EquipItemScrollRect.content = m_ShoeContent.transform as RectTransform;
				}
				break;
			}
		}

		void _ContentWidthAdjsut()
		{
			int count = m_WeaponContent.transform.childCount;
			float size = 0.0f, floatCount;

			// 设定是每页放6个
			if(count > 6)
			{
				size = (m_WeaponContent.transform as RectTransform).rect.size.x;
				// 为了每关需要精确的匹配长度，所以，需要算出不到1页的长度所站的比例（小数）
				floatCount = count/6 + (float)((count+1)/2%3)/3;
				// 在原始的长度基础上，按倍数扩展
				(m_WeaponContent.transform as RectTransform).SetSizeWithCurrentAnchors(
					RectTransform.Axis.Horizontal, size*floatCount);
			}

			count = m_ArmorContent.transform.childCount;
			if(count > 6)
			{
				size = (m_ArmorContent.transform as RectTransform).rect.size.x;
				// 为了每关需要精确的匹配长度，所以，需要算出不到1页的长度所站的比例（小数）
				floatCount = count/6 + (float)((count+1)/2%3)/3;
				// 在原始的长度基础上，按倍数扩展
				(m_ArmorContent.transform as RectTransform).SetSizeWithCurrentAnchors(
					RectTransform.Axis.Horizontal, size*floatCount);
			}

			count = m_HeadContent.transform.childCount;
			if(count > 6)
			{
				size = (m_HeadContent.transform as RectTransform).rect.size.x;
				// 为了每关需要精确的匹配长度，所以，需要算出不到1页的长度所站的比例（小数）
				floatCount = count/6 + (float)((count+1)/2%3)/3;
				// 在原始的长度基础上，按倍数扩展
				(m_HeadContent.transform as RectTransform).SetSizeWithCurrentAnchors(
					RectTransform.Axis.Horizontal, size*floatCount);
			}

			count = m_ShoeContent.transform.childCount;
			if(count > 6)
			{
				size = (m_ShoeContent.transform as RectTransform).rect.size.x;
				// 为了每关需要精确的匹配长度，所以，需要算出不到1页的长度所站的比例（小数）
				floatCount = count/6 + (float)((count+1)/2%3)/3;
				// 在原始的长度基础上，按倍数扩展
				(m_ShoeContent.transform as RectTransform).SetSizeWithCurrentAnchors(
					RectTransform.Axis.Horizontal, size*floatCount);
			}
		}
	}
}

