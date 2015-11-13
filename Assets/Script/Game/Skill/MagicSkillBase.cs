/// <summary>
/// 类名： MagicSkillBase
/// 用途： 魔法师的第一个技能
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace ActionGame
{
	public class MagicSkillBase : MonoBehaviour {
		SkillBtnCoolDownControl m_CoolDownControl; 
		List<Global.SkillObjData> m_IceBallObjs;
		float m_ShowingTime, m_ShowingTimeMax, m_Cd, m_MaxCd;
		Vector3 m_BaseOri, m_TempVec3;

		bool m_IsShowing = true, m_IsAllObjDone;
		public bool IsShowing {
			get {
				return m_IsShowing;
			}
		}

		void Awake () 
		{
			m_IceBallObjs = new List<Global.SkillObjData>();
			for(int i=0; i<transform.childCount; ++i)
			{
				Global.SkillObjData data = new Global.SkillObjData();
				data.time = 1.0f;
				data.timeMax = data.time;
				data.moveSpeed = 15.0f;
				data.transObj = transform.GetChild(i);
				data.skillObj = data.transObj.GetComponent<SkillObj>();
				m_IceBallObjs.Add(data);
			}
			Debug.Log(m_IceBallObjs.Count);

			m_BaseOri = new Vector3();
			m_TempVec3 = new Vector3(0, 0.0f, 1);
			m_IsShowing = false;
			m_ShowingTime = 3.0f;
			m_ShowingTimeMax = m_ShowingTime;
			m_Cd = 0;
			m_MaxCd = 8.0f;

			m_CoolDownControl = GameObject.Find("MainUICanvas/Skill1Btn").GetComponent<SkillBtnCoolDownControl>();
			m_CoolDownControl.Init(m_MaxCd);
		}

		public void UpdateCd()
		{
			if(m_Cd > 0)
			{
				m_Cd -= Time.deltaTime;
			}
		}
		
		void Update () 
		{
			UpdateCd();

			if(m_IsShowing)
			{
				m_ShowingTime -= Time.deltaTime;
				if(m_ShowingTime <= 0.0f)
				{
					m_IsShowing = false;
					gameObject.SetActive(false);
					return;
				}

				m_IsAllObjDone = true;
				foreach(Global.SkillObjData data in m_IceBallObjs)
				{
					data.time -= Time.deltaTime;
					if(data.time >= 0.0f)
					{
						m_IsAllObjDone = false;

						if(data.skillObj.IsHitEnemy)
						{
							data.skillObj.OnAttack(data.atk);
						}

						// 计算出该技能对象的运动朝向,然后，愉快的移动起来
						m_TempVec3 = data.transObj.rotation*m_BaseOri;
						data.transObj.position += m_TempVec3*data.moveSpeed*Time.deltaTime;
					}
				}

				if(m_IsAllObjDone)
				{
					m_IsShowing = false;
					gameObject.SetActive(false);
				}
			}
		}

		public void SkillShow()
		{
			// 冷却中，请稍后
			if(m_Cd > 0)
			{
				return;
			}

			m_IsShowing = true;
			m_IsAllObjDone = false;
			foreach(Global.SkillObjData data in m_IceBallObjs)
			{
				data.time = data.timeMax;
				m_ShowingTime = m_ShowingTimeMax;
				data.skillObj.Reset();
			}

			DungonManager.Inst.Player.GetOriVec3(out m_BaseOri);
			m_TempVec3 = DungonManager.Inst.Player.transform.position;
			m_TempVec3.y = transform.position.y;
			transform.position = m_TempVec3;

			// 展示技能按钮的冷却效果
			m_Cd = m_MaxCd;
			m_CoolDownControl.OnCdStart();
		}

		public bool IsCoolDown()
		{
			return m_Cd > 0;
		}
	}
}