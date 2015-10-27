/// <summary>
/// 类名: EnemyControl
/// 描述: 控制怪物行为
/// </summary>

using UnityEngine;
using System.Collections;

namespace ActionGame
{
	public class EnemyControl : MonoBehaviour {

		public enum ENEMYSTATE
		{
			ET_IDLE,
			ET_PRE_WALK,
			ET_WALK,
			ET_ATTACK,
			ET_DEATH
		}

		public enum ENEMYTYPE
		{
			ET_WOLFMAN
		}

		// 怪物巡逻数据
		public struct EnemySearchData
		{
			public float range;
			// x代表最小时间， y代表最大时间
			public Vector2 breakTime;
			public Vector3 centrPoint;
		}

		// 怪物数据，怪唔得基本数据都在这了
		public struct EnemyData
		{
			public ENEMYTYPE type;
			public ENEMYSTATE state;
			public float moveSpeed;
			public EnemySearchData searchDetail;
		}
		private EnemyData m_Data;
		public EnemyData Data {
			get { return m_Data; }
		}
		

		// 平面上移动方向
		Vector3 m_Dir, m_PrevDir, m_TargetPostion, m_TempVec3;
		Vector2 m_TempVec2;
		float m_CurBreakTime = 0.0f, m_CurMoveDist = 0.0f;
		CharacterController m_Controller;
		AnimationManagerEnemy m_AnimationManager;

		// Use this for initialization
		void Start () 
		{
			m_Controller = GetComponent<CharacterController>();
			m_AnimationManager = gameObject.GetComponent<AnimationManagerEnemy>();
			// 设置为静止态动画

			_Init();
		}
		
		// Update is called once per frame
		void Update () 
		{
			switch( m_Data.state )
			{
			case ENEMYSTATE.ET_IDLE:
				{
					m_CurBreakTime -= Time.deltaTime;
					
					// 休息完成，继续搜索
					if(m_CurBreakTime <= 0.0f)
					{
						// 计算新的目标点
						m_TempVec2.x = Random.Range( -m_Data.searchDetail.range, m_Data.searchDetail.range );
						m_TempVec2.y = Random.Range( -m_Data.searchDetail.range, m_Data.searchDetail.range );
						m_TargetPostion.x = m_Data.searchDetail.centrPoint.x + m_TempVec2.x;
						m_TargetPostion.y = m_Data.searchDetail.centrPoint.y;
						m_TargetPostion.z = m_Data.searchDetail.centrPoint.z + m_TempVec2.y;
						
						//	计算运动方向
						m_PrevDir = m_Dir;
						m_Dir = m_TargetPostion - transform.position;
						m_Dir.Normalize();

						// 计算转向向量 备用
						m_TempVec3 = m_TargetPostion - transform.position;
						m_Data.state = ENEMYSTATE.ET_PRE_WALK;

						// 切换到行走动画
						m_AnimationManager.m_CurAnimationProcessor = m_AnimationManager.Walk;
					}
				}
				break;

			case ENEMYSTATE.ET_PRE_WALK:
				{
					Quaternion rotate = Quaternion.LookRotation( m_TempVec3 );

					if( Quaternion.Angle( transform.rotation, rotate ) > 0.001f )
					{
						transform.rotation = Quaternion.Slerp( transform.rotation, rotate, 4.0f*Time.deltaTime );
					}
					else
					{
						m_Data.state = ENEMYSTATE.ET_WALK;
					}
				}
				break;

			case ENEMYSTATE.ET_WALK:
				{
					if(_IsInSearchRange())
					{
						m_Controller.SimpleMove(m_Dir*m_Data.moveSpeed);
					}
					else
					{
						_AdjustPosition();
						m_CurBreakTime = Random.Range(m_Data.searchDetail.breakTime.x, m_Data.searchDetail.breakTime.y);

						// 切换到静止态动画
						m_Data.state = ENEMYSTATE.ET_IDLE;
						m_AnimationManager.m_CurAnimationProcessor = m_AnimationManager.Idle;
                    }
				}
				break;
			}
		}

		void _Init()
		{
			m_Data.type = ENEMYTYPE.ET_WOLFMAN;
			m_Data.state = ENEMYSTATE.ET_IDLE;
			m_Data.moveSpeed = 1.0f;
			m_Data.searchDetail.centrPoint = new Vector3();
			m_Data.searchDetail.centrPoint = transform.position;
			m_Data.searchDetail.breakTime = new Vector3(0.5f, 3.0f);
			m_Data.searchDetail.range = 4.0f;
		}

		/// <summary>
		/// 检测怪物是否在搜索范围内s
		/// </summary>
		bool _IsInSearchRange()
		{
			m_CurMoveDist = Vector3.Distance(m_Data.searchDetail.centrPoint, transform.position);
			return m_CurMoveDist < m_Data.searchDetail.range;
		}

		/// <summary>
		/// 在怪物移动出，搜索范围后，将其位置修正到搜索范围圈内。
		/// 避免下一次移动时，未移动、便判定为不可移动。
		/// </summary>
		void _AdjustPosition()
		{
			// Dir为当前位置到目标点的位置，而修正时需要有中心点到当前未知的方向
			// 所以，需要重新计算。
			m_Dir = transform.position - m_Data.searchDetail.centrPoint;
			m_Dir.Normalize();

			m_TempVec3 = m_Dir*(m_Data.searchDetail.range - 0.01f);
			m_TempVec3 += m_Data.searchDetail.centrPoint;
			transform.position = m_TempVec3;
		}
	}
}