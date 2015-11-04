﻿/// <summary>
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
		float m_Speed = 10.0f;

		void Update()
		{
			if(!m_IsHitTarget)
			{
				// 通过距离判断是否打到
				float dist = Vector3.Distance(m_Target.transform.position, transform.position);
				if(dist <= 2.0f)
				{
					m_IsHitTarget = true;
					// 击中目标，产生震屏效果
					ThirdPersonCamera.OnShakeCamera();
					return;
				}

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
	}
}