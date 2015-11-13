/// <summary>
/// 类名: HudControl
/// 描述: 控制怪物头顶HUD的自动生成,及其跟随运动
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace ActionGame
{
	public class HpHudControl : MonoBehaviour {
		Transform m_FollowObj;
		EnemyControl m_Enemy;
		Vector3 m_CurPos, m_Offset, m_Size;
		GameObject m_HudObj;
		public GameObject HudObj {
			get {
				return m_HudObj;
			}
		}

		// Use this for initialization
		void Awake () 
		{
			m_FollowObj = transform;
			m_Enemy = gameObject.GetComponent<EnemyControl>();

			m_CurPos = new Vector3();
			m_Size = gameObject.GetComponent<CharacterController>().bounds.size;
			// 偏移量根据，怪物自身的大小而定
			m_Offset = new Vector3(0, m_Size.y + 1.0f, 0);

			GameObject go = Resources.Load("Prefab/UI/EnemyHpBar") as GameObject;
			m_HudObj = Instantiate(go);
			m_HudObj.transform.SetParent( GameObject.Find("HpHudCanvas").transform );
			m_HudObj.transform.localScale = Vector3.one;
		}
		
		// Update is called once per frame
		void Update () 
		{
			m_CurPos = Camera.main.WorldToScreenPoint(m_FollowObj.transform.position + m_Offset);
			m_HudObj.transform.position = m_CurPos;
			m_HudObj.GetComponent<ValBarControl>().OnValueChange(m_Enemy.Data.attrib.hp/m_Enemy.Data.attrib.maxHp, "");
		}

		public void SetHpHudActive(bool flag)
		{
			m_HudObj.SetActive(flag);
		}
	}
}