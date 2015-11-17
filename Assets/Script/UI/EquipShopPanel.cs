/// <summary>
/// 类名: EquipShopPanel
/// 描述: 装备商店面板，负责管理装备商店的UI界面
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ActionGame
{
	public class EquipShopPanel : PanelBase {
		Transform m_EquipTypeGroup;
	
		void Start()
		{
			m_EquipTypeGroup = transform.FindChild("EquipType");
			m_Content = transform.FindChild("EquipContent").GetComponent<EquipShopContentControl>();
			m_Content.Init();
			// 链接ToggleGroup
			(m_Content as EquipShopContentControl).LinkToggleGroup(m_EquipTypeGroup);

			gameObject.SetActive(false);
		}
	}
}