/// <summary>
/// 类名: StateContentControl
/// 描述: 控制状态面板中，状态信息部分的数据更新
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ActionGame
{
	public class StateContentControl : MonoBehaviour, IPanelContentControl {

		PlayerControl.PlayerData m_Data;
		Text m_NameText;
		Text m_LevelText;
		Text m_HpText;
		Text m_MpText;
		Text m_AtkPhyText;
		Text m_DefPhyText;
		Text m_AtkMagText;
		Text m_DefMagText;
		ValBarControl m_ExpBarControl;

		// Use this for initialization
		public void Init() 
		{
			m_NameText = gameObject.transform.FindChild("NameText").GetComponent<Text>();
			m_LevelText = gameObject.transform.FindChild("LevelText").GetComponent<Text>();
			m_HpText = gameObject.transform.FindChild("HpText").GetComponent<Text>();
			m_MpText = gameObject.transform.FindChild("MpText").GetComponent<Text>();
			m_AtkPhyText = gameObject.transform.FindChild("AtkPhyText").GetComponent<Text>();
			m_DefPhyText = gameObject.transform.FindChild("DefPhyText").GetComponent<Text>();
			m_AtkMagText = gameObject.transform.FindChild("AtkMagText").GetComponent<Text>();
			m_DefMagText = gameObject.transform.FindChild("DefMagText").GetComponent<Text>();
			m_ExpBarControl = gameObject.transform.FindChild("ExpBar").GetComponent<ValBarControl>();

			m_Data = DungonManager.Inst.Player.Data;
			m_ExpBarControl.Init();
		}
		
		public void RefreshData()
		{
			m_Data = DungonManager.Inst.Player.Data;

			m_NameText.text = "角色名: " + m_Data.attrib.charName;
			m_LevelText.text = "等级: " + m_Data.levelData.level;
			m_HpText.text = "HP: " + m_Data.attrib.hp + "/" + m_Data.attrib.maxHp;
			m_MpText.text = "MP: " + m_Data.attrib.eng + "/" + m_Data.attrib.maxEng;
			m_AtkPhyText.text = "物理攻击: " + m_Data.attrib.atkPhy;
			m_DefPhyText.text = "物理防御: " + m_Data.attrib.defPhy;
			m_AtkMagText.text = "魔法攻击: " + m_Data.attrib.atkMag;
			m_DefMagText.text = "魔法防御: " + m_Data.attrib.defMag;
			m_ExpBarControl.OnValueChange( (float)m_Data.levelData.exp / m_Data.levelData.levelUpExp,
			                             m_Data.levelData.exp.ToString() + "/" + m_Data.levelData.levelUpExp.ToString() );
		}
	}
}