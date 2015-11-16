/// <summary>
/// 类名: EquipShopLeftBtnControl
/// 描述: 装备商店做滚动按钮控制脚本
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class EquipShopLeftBtnControl : BasePressButton {
	ScrollRect m_EquipArea = null;
	Vector2 m_Velocity;

	// Use this for initialization
	void Start () 
	{
		m_EquipArea = transform.parent.GetComponentInChildren<ScrollRect>();
		m_Velocity = new Vector2();
		m_Velocity.x = m_EquipArea.scrollSensitivity*100;
	}

	public override void OnPress()
	{
		if(m_EquipArea.velocity.x < -m_Velocity.x)
		{
			m_EquipArea.velocity = Vector2.zero;
        }
        m_EquipArea.velocity += m_Velocity;
		Debug.Log(m_EquipArea.velocity);
	}

	public override void OnDown()
	{
		m_EquipArea.movementType = ScrollRect.MovementType.Clamped;
	}
	
	public override void OnUp()
	{
		m_EquipArea.movementType = ScrollRect.MovementType.Elastic;
		m_EquipArea.StopMovement();
    }
}
