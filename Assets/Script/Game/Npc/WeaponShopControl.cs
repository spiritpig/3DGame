/// <summary>
/// 类名: WeaponShopControl
/// 描述: 武器店老板控制脚本
/// </summary>

using UnityEngine;
using System.Collections;

namespace ActionGame
{
	public class WeaponShopControl : MonoBehaviour {
		EquipShopPanel m_EquipShopPanel;
		CapsuleCollider m_Collider;
		Vector3 m_MousePos;

		void Start () 
		{
			m_EquipShopPanel = GameObject.Find("MainUICanvas/EquipShopPanel").GetComponent<EquipShopPanel>();
			m_EquipShopPanel.gameObject.SetActive(false);

			m_Collider = GetComponent<CapsuleCollider>();
			m_MousePos = new Vector3();
		}
		
		void Update () 
		{
#if UNITY_EDITOR
			if(Input.GetMouseButtonDown(0))
			{
				m_MousePos = Input.mousePosition;
#elif UNITY_ANDROID || UNITY_IPHONE
			if(Input.touchCount > 0 && Input.touches[0] != null)
			{
				m_MousePos.x = Input.touches[0].position.x;
				m_MousePos.y = Input.touches[0].position.y;
				m_MousePos.z = 0.0f;
#endif
				// 判断玩家和NPC的距离是否靠近了可交互的距离， 若在距离内则射线检测判断有没点到NPC
				float dist = Vector3.Distance(DungonManager.Inst.GetPlayerPosition() ,gameObject.transform.position);
				if(dist <= Global.g_NpcInteractRange)
				{
					Ray ray = Camera.main.ScreenPointToRay(m_MousePos);
					RaycastHit hit;
					if(m_Collider.Raycast(ray, out hit, Camera.main.farClipPlane))
					{
						_ProcessClick();
					}
				}
			}
		}

		void _ProcessClick()
		{
			if(!m_EquipShopPanel.gameObject.activeSelf)
			{
				m_EquipShopPanel.ActiveByCurrentState();
			}
		}
	}
}