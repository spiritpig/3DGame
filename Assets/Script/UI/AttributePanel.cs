/// <summary>
/// 类名: AttributePanel
/// 描述: 画面左上角状态栏，用于控制其中各滚动条的滚动
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ActionGame
{
	public class AttributePanel : MonoBehaviour {
		GameObject m_HpBar, m_MpBar, m_AttribExpBar;
		PlayerControl.PlayerData m_PlayerData;

		// Use this for initialization
		void Start () 
		{
			m_HpBar = gameObject.transform.FindChild("HpBar").gameObject;
			m_MpBar = gameObject.transform.FindChild("MpBar").gameObject;
			m_AttribExpBar = gameObject.transform.FindChild("AttribExpBar").gameObject;

			m_PlayerData = PlayingManager.Inst.Player.Data;
			//OnHpBarChange(m_PlayerData.attrib.hp/m_PlayerData.attrib.maxHp,
			//              "" );
		}

		public void OnHpBarChange(float rate, string hpStr)
		{
			m_HpBar.GetComponent<ValBarControl>().OnValueChange(rate, hpStr);
		}

		public void OnMpBarChange(float rate, string mpStr)
		{
			m_MpBar.GetComponent<ValBarControl>().OnValueChange(rate, mpStr);
		}

		public void OnExpBarChange(float rate, string mpStr)
		{
			m_AttribExpBar.GetComponent<ValBarControl>().OnValueChange(rate, mpStr);
		}
	}
}