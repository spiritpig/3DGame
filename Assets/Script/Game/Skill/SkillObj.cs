/// <summary>
/// 类名： GetIsColli
/// 用途： 简单脚本，判断对象是否产生碰撞
/// </summary>

using UnityEngine;
using System.Collections;

namespace ActionGame
{
	//[RequireComponent(typeof(BoxCollider))]
	public class SkillObj : MonoBehaviour {

		BoxCollider m_Collider;
		bool m_IsHitEnemy = false;
		public bool IsHitEnemy {
			get {
				return m_IsHitEnemy;
			}
		}
		EnemyControl m_Target;
		public EnemyControl Target {
			get {
				return m_Target;
			}
		}	

		void Awake () 
		{
			m_Collider = GetComponent<BoxCollider>();
			m_IsHitEnemy = false;
		}

		public void OnTriggerEnter(Collider colli)
		{
			if(colli.gameObject.tag == "Enemy" || 
			   colli.gameObject.tag == "Target")
			{
				m_IsHitEnemy = true;
				m_Target = colli.gameObject.GetComponent<EnemyControl>();
			}
		}

		/// <summary>
		/// 重置技能对象的属性，在释放技能时调用
		/// </summary>
		public void Reset()
		{
			m_IsHitEnemy = false;
			m_Target = null;
			transform.localPosition = Vector3.zero;
		}

		/// <summary>
		/// 触发攻击行为
		/// </summary>
		public void OnAttack(float atk)
		{
			if(!(m_Target == null || m_Target.IsDeath()))
			{
				// 若怪物死亡，则增加玩家的经验值
				if(m_Target.BeAttack(atk))
				{
					DungonManager.Inst.Player.OnAddExp(m_Target.Data.attrib.gainExp);
				}
				m_IsHitEnemy = false;
			}
		}
	}
}