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

			// 预更新一次人物状态栏
			m_PlayerData = DungonManager.Inst.Player.Data;
			OnHpBarChange();
			OnMpBarChange();
			OnExpBarChange();
		}

		public void OnHpBarChange()
		{
			m_PlayerData = DungonManager.Inst.Player.Data;
			m_HpBar.GetComponent<ValBarControl>().OnValueChange(
				m_PlayerData.attrib.hp/m_PlayerData.attrib.maxHp, 
				m_PlayerData.attrib.hp.ToString() + "/" + m_PlayerData.attrib.maxHp.ToString() );
		}

		public void OnMpBarChange()
		{
			m_PlayerData = DungonManager.Inst.Player.Data;
			m_MpBar.GetComponent<ValBarControl>().OnValueChange(
				m_PlayerData.attrib.eng/m_PlayerData.attrib.maxEng, 
				m_PlayerData.attrib.eng.ToString() + "/" + m_PlayerData.attrib.maxEng.ToString() );
		}

		public void OnExpBarChange()
		{
			m_PlayerData = DungonManager.Inst.Player.Data;
			m_AttribExpBar.GetComponent<ValBarControl>().OnValueChange(
				(float)m_PlayerData.levelData.exp/m_PlayerData.levelData.levelUpExp, 
				m_PlayerData.levelData.exp.ToString() + "/" + m_PlayerData.levelData.levelUpExp.ToString() );
		}
	}
}