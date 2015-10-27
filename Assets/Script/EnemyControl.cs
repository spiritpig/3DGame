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
			ET_BACK_IDLE,		// 回到初始位置
			ET_BODY_ROTATE,
			ET_WALK,
			ET_CHASE,
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

		// 怪物追击数据
		public struct EnemyChaseData
		{
			public float maxTime;
		}

		// 怪物数据，怪唔得基本数据都在这了
		public struct EnemyData
		{
			public ENEMYTYPE type;
			public ENEMYSTATE state;
			public float moveSpeed;
			public EnemySearchData searchData;
			public EnemyChaseData chaseData;
			public Global.Attribute attrib;
		}
		private EnemyData m_Data;
		public EnemyData Data {
			get { return m_Data; }
		}
		

		// 平面上移动方向
		Vector3 m_Dir, m_PrevDir, m_TargetPostion, m_TempVec3;
		Vector2 m_TempVec2;
		ENEMYSTATE m_NextState;
		float m_CurBreakTime = 0.0f, m_CurChaseTime = 0.0f, 
				m_TempFloat = 0.0f, m_RotateSpeed = 6.0f;
		CharacterController m_Controller;
		AnimationManagerEnemy m_AnimationManager;

		// Use this for initialization
		void Start () 
		{
			m_Controller = GetComponent<CharacterController>();
			m_AnimationManager = gameObject.GetComponent<AnimationManagerEnemy>();
			m_NextState = ENEMYSTATE.ET_IDLE;

			_InitData();
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
						m_TempVec2.x = Random.Range( -m_Data.searchData.range, m_Data.searchData.range );
						m_TempVec2.y = Random.Range( -m_Data.searchData.range, m_Data.searchData.range );
						m_TargetPostion.x = m_Data.searchData.centrPoint.x + m_TempVec2.x;
						m_TargetPostion.y = m_Data.searchData.centrPoint.y;
						m_TargetPostion.z = m_Data.searchData.centrPoint.z + m_TempVec2.y;
						
						//	计算运动方向
						m_PrevDir = m_Dir;
						m_Dir = m_TargetPostion - transform.position;
						m_Dir.Normalize();

						// 计算转向向量 备用
						m_TempVec3 = m_TargetPostion - transform.position;
						m_Data.state = ENEMYSTATE.ET_BODY_ROTATE;
						m_NextState = ENEMYSTATE.ET_WALK;

						// 切换到行走动画
						m_AnimationManager.m_CurAnimationProcessor = m_AnimationManager.Walk;
					}
				}
				break;

			case ENEMYSTATE.ET_BACK_IDLE:
				{
					m_Controller.SimpleMove(m_Dir*m_Data.moveSpeed);

					// 若回到了起点，继续搜索
					if(_IsBackCentr())
					{
						_OnIdle();
					}
				}
				break;

			case ENEMYSTATE.ET_BODY_ROTATE:
				{
					// 继续转头
					Quaternion rotate = Quaternion.LookRotation( m_TempVec3 );
					if( Quaternion.Angle( transform.rotation, rotate ) > 0.001f )
					{
						transform.rotation = Quaternion.Slerp( transform.rotation, rotate, 
					                                      		m_RotateSpeed*Time.deltaTime );
					}
					else
					{
						m_Data.state = m_NextState;
					}
				}
				break;

			case ENEMYSTATE.ET_WALK:
				{
					// 若玩家在攻击范围内，则追上他
					if( _IsPlayerInRange() )
					{
						_OnChaseStart();
						break;
					}

					// 继续更新怪物的移动
					if(_IsInSearchRange())
					{
						m_Controller.SimpleMove(m_Dir*m_Data.moveSpeed);
					}
					else
					{
						_AdjustPosition();
						m_CurBreakTime = Random.Range(m_Data.searchData.breakTime.x, 
					                              		m_Data.searchData.breakTime.y);

						_OnIdle();
                    }
				}
				break;

			// 发现玩家了，往死里追
			case ENEMYSTATE.ET_CHASE:
				{
					m_CurChaseTime -= Time.deltaTime;
					if(m_CurChaseTime <= 0.0f)
					{
						_OnChaseEnd();
						break;
					}

					// 如果接近玩家了，则转换为攻击状态
					if(!_IsOutOfAtkRange())
					{
						_OnAttack();
						break;
					}

					// 朝着 玩家移动
					m_Dir = PlayingManager.Inst.Player.transform.position - transform.position;
					m_Dir.Normalize();
					m_Controller.SimpleMove(m_Dir*m_Data.moveSpeed);
					transform.LookAt(PlayingManager.Inst.Player.transform.position);
				}
				break;

			case ENEMYSTATE.ET_ATTACK:
				{
					if(_IsOutOfAtkRange())
					{
						_OnChaseStart();
						break;
					}

					
					// 当次攻击动画接近尾声，判定为攻击生效
					if(m_AnimationManager.IsAttackEnd())
					{
						PlayingManager.Inst.Player.OnBeAttack(m_Data.attrib.atkPhy);
					}
				}
				break;
			}
		}

		void _InitData()
		{
			m_Data.type = ENEMYTYPE.ET_WOLFMAN;
			m_Data.state = ENEMYSTATE.ET_IDLE;
			m_Data.moveSpeed = 1.0f;
			m_Data.searchData.centrPoint = new Vector3();
			m_Data.searchData.centrPoint = transform.position;
			m_Data.searchData.breakTime = new Vector3(0.5f, 3.0f);
			m_Data.searchData.range = 8.0f;
			m_Data.chaseData.maxTime = 5.0f;

			m_Data.attrib.hp = 100.0f;
			m_Data.attrib.maxHp = 100.0f;
			m_Data.attrib.eng = 100.0f;
			m_Data.attrib.maxEng = 100.0f;;
			m_Data.attrib.atkPhy = 20.0f;
			m_Data.attrib.atkMag = 40.0f;
			m_Data.attrib.defPhy = 20.0f;
			m_Data.attrib.defMag = 40.0f;
			m_Data.attrib.atkSp = 1.0f;
			m_Data.attrib.movSp = Global.g_PlayerMoveSpeed;
			m_Data.attrib.atkRange = 2.5f;
		}

		/// <summary>
		/// 检测怪物是否在搜索范围内s
		/// </summary>
		bool _IsInSearchRange()
		{
			m_TempFloat = Vector3.Distance(m_Data.searchData.centrPoint, transform.position);
			return m_TempFloat < m_Data.searchData.range;
		}

		/// <summary>
		/// 判断怪物是否回到搜索的起点
		/// </summary>
		bool _IsBackCentr()
		{
			m_TempFloat = Vector3.Distance(m_Data.searchData.centrPoint, transform.position);
			return m_TempFloat < 0.5f;
		}

		/// <summary>
		/// 搜索玩家，若玩家出现在范围内，则吵着玩家冲过去
		/// </summary>
		bool _IsPlayerInRange()
		{
			m_TempFloat = Vector3.Distance( PlayingManager.Inst.Player.transform.position, transform.position );
			// 若玩家接近了怪物，别犹豫冲过去
			if( m_TempFloat <= m_Data.searchData.range )
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// 判断是否超出了攻击范围
		/// </summary>
		bool _IsOutOfAtkRange()
		{
			m_TempFloat = Vector3.Distance(PlayingManager.Inst.Player.transform.position, 
			                               transform.position);
			return m_TempFloat > m_Data.attrib.atkRange;
		}

		/// <summary>
		/// 处理玩家回到初始态的情况
		/// </summary>
		void _OnIdle()
		{
			// 切换到静止态动画
			m_Data.state = ENEMYSTATE.ET_IDLE;
			m_AnimationManager.m_CurAnimationProcessor = m_AnimationManager.Idle;
		}

		/// <summary>
		/// 处理玩家进入攻击范围的情况
		/// </summary>
		void _OnChaseStart()
		{
			_PrepareBodyRotate(ENEMYSTATE.ET_CHASE);
			m_TempVec3 = PlayingManager.Inst.Player.transform.position - transform.position;
			m_Dir = m_TempVec3;
			m_Dir.Normalize();
			m_CurChaseTime = m_Data.chaseData.maxTime;

			m_AnimationManager.m_CurAnimationProcessor = m_AnimationManager.Walk;
		}

		/// <summary>
		/// 处理当追击结束时的情况
		/// </summary>
		void _OnChaseEnd()
		{
			_PrepareBodyRotate(ENEMYSTATE.ET_BACK_IDLE);
			m_TempVec3 = m_Data.searchData.centrPoint - transform.position;
			m_Dir = m_TempVec3;
			m_Dir.Normalize();
		}

		void _OnAttack()
		{
			m_Data.state = ENEMYSTATE.ET_ATTACK;
			m_AnimationManager.m_CurAnimationProcessor = m_AnimationManager.Attack;
		}

		/// <summary>
		/// 在怪物移动出，搜索范围后，将其位置修正到搜索范围圈内。
		/// 避免下一次移动时，未移动、便判定为不可移动。
		/// </summary>
		void _AdjustPosition()
		{
			// Dir为当前位置到目标点的位置，而修正时需要有中心点到当前未知的方向
			// 所以，需要重新计算。
			m_Dir = transform.position - m_Data.searchData.centrPoint;
			m_Dir.Normalize();

			m_TempVec3 = m_Dir*(m_Data.searchData.range - 0.001f);
			m_TempVec3 += m_Data.searchData.centrPoint;
			transform.position = m_TempVec3;
		}

		void _PrepareBodyRotate(ENEMYSTATE nextState)
		{
			m_Data.state = ENEMYSTATE.ET_BODY_ROTATE;
			m_NextState = nextState;
		}
	}
}