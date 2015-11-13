/// <summary>
/// 类名： SkillManager
/// 用途： 技能管理类
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ActionGame
{
	public class SkillManager : MonoBehaviour {
		MagicSkillBase m_SkillOne;
		MagicSkillBase m_CurActiveSkill = null;
		public MagicSkillBase ActiveSkill {
			get { return m_SkillOne; }
		}

		void Start () 
		{
			m_SkillOne = transform.FindChild("SkillIceBall").GetComponent<MagicSkillBase>();
			m_SkillOne.gameObject.SetActive(false);
		}

		public bool OnReleaseMagicOne()
		{
			if(!m_SkillOne.IsShowing && !m_SkillOne.IsCoolDown())
			{
				m_SkillOne.gameObject.SetActive(true);
				m_SkillOne.SkillShow();
				m_CurActiveSkill = m_SkillOne;
				return true;
			}
			return false;
		}
		
		void Update () 
		{
			// 若是活动的技能已然停止，则活动技能变量也需要赋空
			if(m_CurActiveSkill != null)
			{
				m_CurActiveSkill = m_CurActiveSkill.IsShowing ? m_CurActiveSkill : null;
			}
			m_SkillOne.UpdateCd();
		}
	}
}