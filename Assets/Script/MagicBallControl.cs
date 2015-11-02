/// <summary>
/// 类名： MagicBallControl
/// 用途： 管理魔法球的行为和状态
/// </summary>

using UnityEngine;
using System.Collections;

namespace ActionGame
{
	public class MagicBallControl : MonoBehaviour { 
		GameObject m_Target = null;
		bool m_IsHitTarget = false;
		public bool IsHitTarget {
			get {
				return m_IsHitTarget;
			}
		}

		Vector3 m_TempVec3;
		float m_Speed = 20.0f;

		void FixedUpdate()
		{
			if(!m_IsHitTarget)
			{
				m_TempVec3 = m_Target.transform.position - transform.position;
				m_TempVec3.y = 0.0f;
				m_TempVec3.Normalize();
				
				transform.position += m_TempVec3*m_Speed*Time.deltaTime;
			}
		}

		public void Reset(Vector3 pos, EnemyControl Target)
		{
			m_Target = Target.gameObject;
			transform.localPosition = pos;
			m_IsHitTarget = false;
		}

		public void OnTriggerEnter(Collider colli)
		{
			if(colli.gameObject.Equals(m_Target))
			{
				m_IsHitTarget = true;
			}
			else
			{
				m_IsHitTarget = false;
			}
		}
	}
}