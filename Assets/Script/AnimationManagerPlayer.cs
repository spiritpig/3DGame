/// <summary>
/// 类名： AnimationManagerPlayer
/// 用途： 管理玩家的动画播放
/// </summary>

using UnityEngine;
using System.Collections;

namespace ActionGame
{
	public class AnimationManagerPlayer : MonoBehaviour {

		public delegate void AnimationProcessor();
		public AnimationProcessor animationProcessor = null;

		Animation m_AnimationComponent;
		struct AnimatData
		{
			public AnimationClip Animation;
			public float AnimatTime;
		}
		AnimatData m_Idle, m_Walk, m_Attack;

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
			if( animationProcessor != null )
			{
				animationProcessor();
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

			m_Walk = new AnimatData();
			m_Walk.AnimatTime = Global.g_PlayerRunAnimatTime;
			m_Walk.Animation = m_AnimationComponent.GetClip( "Run" );

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
			m_AnimationComponent.Play( m_Walk.Animation.name );
			m_AnimationComponent[ m_Walk.Animation.name ].speed = m_Walk.AnimatTime;
		}

		public void OnAttack()
		{
			m_AnimationComponent.Play( m_Attack.Animation.name );
			m_AnimationComponent[ m_Attack.Animation.name ].speed = m_Attack.AnimatTime;
		}

		public bool IsPlaying()
		{
			return m_AnimationComponent.isPlaying;
		}

		public bool IsAttack1End()
		{
			if(m_AnimationComponent.IsPlaying("Attack1") &&
			   m_AnimationComponent[ m_Attack.Animation.name ].normalizedTime > 0.8f)
			{
				return true;
			}

			return false;
		}
	}
}