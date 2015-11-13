/// <summary>
/// 类名： SkillBtnCoolDownControl
/// 用途： 技能按钮的冷却控制脚本
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ActionGame
{
	[RequireComponent(typeof(Image))]
	public class SkillBtnCoolDownControl : MonoBehaviour {
		float m_CdTime, m_MaxCdTime;
		Image m_CdImg; 
		Text m_CdText;

		// Use this for initialization
		public void Init (float CdTime) 
		{
			m_CdTime = 0.0f;
			m_MaxCdTime = CdTime;
			m_CdImg = transform.FindChild("cdImg").GetComponent<Image>();
			m_CdText = transform.FindChild("cdTime").GetComponent<Text>();
		}
		
		// Update is called once per frame
		void Update () 
		{
			if(m_CdTime > 0)
			{
				m_CdTime -= Time.deltaTime;
				// cd时间到，一切归零
				if(m_CdTime <= 0.0f)
				{
					m_CdImg.material.SetFloat("_MaskHeight", 0);
					m_CdText.text = "";
				}
				else
				{
					m_CdImg.material.SetFloat("_MaskHeight", m_CdTime/m_MaxCdTime);
					m_CdText.text = ((int)m_CdTime).ToString();
				}
			}
		}

		public void OnCdStart()
		{
			m_CdTime = m_MaxCdTime;
			m_CdImg.material.SetFloat("_MaskHeight", 1.0f);
			m_CdText.text = ((int)m_CdTime).ToString();
		}
	}
}