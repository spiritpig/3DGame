﻿/// <summary>
/// 类名： AnimationManagerPlayer
/// 用途： 管理怪物的动画播放
/// </summary>

using UnityEngine;
using System.Collections;

namespace ActionGame
{
	public class AnimationManagerEnemy : MonoBehaviour {
		
		public delegate void AnimationProcessor();
		public AnimationProcessor m_CurAnimationProcessor = null;
		Animation m_AnimationComponent;
		struct AnimatData
		{
			public AnimationClip Animation;
			public float AnimatTime;
		}
		AnimatData m_Idle, m_Run, m_Attack;
		bool m_IsAttacked = false;
		
		// Use this for initialization
		void Start () 
		{
			m_AnimationComponent = gameObject.GetComponent<Animation>();
			
			DataInit();
		}
		
		// Update is called once per frame
		void Update () 
		{
			// 有动作的时候，执行该动作
			if( m_CurAnimationProcessor != null )
			{
				m_CurAnimationProcessor();
			}
		}
		
		/// <summary>
		/// 数据初始化
		/// </summary>
		void DataInit()
		{
			m_Idle = new AnimatData();
			m_Idle.AnimatTime = Global.g_PlayerIdleAnimatTime;
			m_Idle.Animation = m_AnimationComponent.GetClip( "Idle" );
			
			m_Run = new AnimatData();
			m_Run.AnimatTime = Global.g_PlayerRunAnimatTime;
			m_Run.Animation = m_AnimationComponent.GetClip( "Walk" );

			m_Attack = new AnimatData();
			m_Attack.AnimatTime = Global.g_PlayerRunAnimatTime;
			m_Attack.Animation = m_AnimationComponent.GetClip( "Attack1" );
		}
		
		/// <summary>
		/// 各种Animation Processor 函数
		/// </summary>
		public void Idle()
		{
			m_AnimationComponent.CrossFade( m_Idle.Animation.name );
			m_AnimationComponent[ m_Idle.Animation.name ].speed = m_Idle.AnimatTime;
		}
		
		public void Walk()
		{
			m_AnimationComponent.Play( m_Run.Animation.name );
			m_AnimationComponent[ m_Run.Animation.name ].speed = m_Run.AnimatTime;
		}

		public void Attack()
		{
			m_AnimationComponent.Play( m_Attack.Animation.name );
			m_AnimationComponent[ m_Attack.Animation.name ].speed = m_Attack.AnimatTime;
		}

		/// <summary>
		// 判断攻击是否结束
		/// </summary>
		public bool IsAttackEnd()
		{
			if(m_CurAnimationProcessor == Attack &&
			   !m_IsAttacked &&
			   m_AnimationComponent[ m_Attack.Animation.name ].normalizedTime >= 0.9f)
			{
				Debug.Log( "BEAttack" );
				m_IsAttacked = true;
				return true;
			}
			m_IsAttacked = false;
			return false;
		}
	}
}