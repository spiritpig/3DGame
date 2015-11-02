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
			ES_IDLE,
			ES_BACK_IDLE,		// 回到初始位置
			ES_BODY_ROTATE,
			ES_CHASE,
			ES_PRE_ATTACK,		// 该状态用于叠加攻击时间
			ES_ATTACK,
			ES_BEATTACK,
			ES_DEATH,
			ES_DEATH_END
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
				m_TempFloat = 0.0f, m_RotateSpeed = 10.0f,
				m_CurAtkTime = 0.0f;
		GameObject m_SelectedPlane = null;
		CharacterController m_Controller;
		AnimationManagerEnemy m_AnimationManager;

		// Use this for initialization
		void Start () 
		{
			m_Controller = GetComponent<CharacterController>();
			m_AnimationManager = gameObject.GetComponent<AnimationManagerEnemy>();
			m_NextState = ENEMYSTATE.ES_IDLE;
			m_TempVec3 = new Vector3();
			m_Dir = new Vector3();
			m_SelectedPlane = transform.FindChild("SelectedPlane").gameObject;
			m_SelectedPlane.SetActive(false);

			_InitData();
		}
		
		// Update is called once per frame
		void Update () 
		{
			switch( m_Data.state )
			{
			case ENEMYSTATE.ES_IDLE:
				{
					if(_IsPlayerInRange())
					{
						_OnChaseStart();
					}

					// 顺便计算剩余的攻击冷却时间
					if(m_CurAtkTime > 0.0f)
					{
						m_CurAtkTime -= Time.deltaTime;
					}
				}
				break;

			case ENEMYSTATE.ES_BACK_IDLE:
				{
					m_Controller.SimpleMove(m_Dir*m_Data.attrib.movSp);

					// 若回到了起点，继续搜索
					if(_IsBackCentr())
					{
						_OnIdle();
					}

					// 顺便计算剩余的攻击冷却时间
					if(m_CurAtkTime > 0.0f)
					{
						m_CurAtkTime -= Time.deltaTime;
					}
				}
				break;

			case ENEMYSTATE.ES_BODY_ROTATE:
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

					// 顺便计算剩余的攻击冷却时间
					if(m_CurAtkTime > 0.0f)
					{
						m_CurAtkTime -= Time.deltaTime;
					}
				}
				break;

			// 发现玩家了，往死里追
			case ENEMYSTATE.ES_CHASE:
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
						if(m_CurAtkTime < 0.0f)
						{
							_OnAttack();
						}
						else
						{
							_OnPreAttack();
						}
						break;
					}

					// 顺便计算剩余的攻击冷却时间
					if(m_CurAtkTime > 0.0f)
					{
						m_CurAtkTime -= Time.deltaTime;
					}

					// 朝着 玩家移动
					m_Dir = PlayingManager.Inst.Player.transform.position - transform.position;
					m_Dir.Normalize();
					m_Controller.SimpleMove(m_Dir*m_Data.attrib.movSp);
					transform.LookAt(PlayingManager.Inst.Player.transform.position);
				}
				break;
			
			case ENEMYSTATE.ES_PRE_ATTACK:
				{
					m_CurAtkTime -= Time.deltaTime;
					if(m_CurAtkTime < 0.0f)
					{
						_OnAttack();
					}
				}
				break;

			case ENEMYSTATE.ES_ATTACK:
				{
					// 递减攻击时间
					m_CurAtkTime -= Time.deltaTime;

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

			case ENEMYSTATE.ES_BEATTACK:
				{
					if(!m_AnimationManager.IsPlaying())
					{
						// 既然被打了，必须打回去
						_OnChaseStart();
					}
				}
				break;

			case ENEMYSTATE.ES_DEATH:
				{
					if(!m_AnimationManager.IsPlaying())
					{
						m_Data.state = ENEMYSTATE.ES_DEATH_END;
					}
				}
				break;
			}
		}

		void OnDestroy()
		{
			Destroy(gameObject.GetComponent<HudControl>().HudObj);
		}

		void _InitData()
		{
			m_Data.type = ENEMYTYPE.ET_WOLFMAN;
			m_Data.state = ENEMYSTATE.ES_IDLE;
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
			m_Data.attrib.gainExp = 60;
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
			m_Data.state = ENEMYSTATE.ES_IDLE;
			m_AnimationManager.animationProcessor = m_AnimationManager.Idle;
		}

		/// <summary>
		/// 处理玩家进入攻击范围的情况
		/// </summary>
		void _OnChaseStart()
		{
			_PrepareBodyRotate(ENEMYSTATE.ES_CHASE);
			m_TempVec3 = PlayingManager.Inst.Player.transform.position - transform.position;
			m_Dir = m_TempVec3;
			m_Dir.Normalize();
			m_CurChaseTime = m_Data.chaseData.maxTime;

			m_AnimationManager.animationProcessor = m_AnimationManager.Walk;
		}

		/// <summary>
		/// 处理当追击结束时的情况
		/// </summary>
		void _OnChaseEnd()
		{
			_PrepareBodyRotate(ENEMYSTATE.ES_BACK_IDLE);
			m_TempVec3 = m_Data.searchData.centrPoint - transform.position;
			m_Dir = m_TempVec3;
			m_Dir.Normalize();
		}

		void _OnAttack()
		{
			m_Data.state = ENEMYSTATE.ES_ATTACK;
			m_CurAtkTime = m_Data.attrib.atkSp;
			m_AnimationManager.animationProcessor = m_AnimationManager.Attack;
		}

		/// <summary>
		/// 若是攻击的冷却时间未到，则切换到该状态
		/// </summary>
		void _OnPreAttack()
		{
			m_Data.state = ENEMYSTATE.ES_PRE_ATTACK;
			m_AnimationManager.animationProcessor = m_AnimationManager.Idle;
		}

		void _OnBeAttack()
		{
			m_Data.state = ENEMYSTATE.ES_BEATTACK;

			m_AnimationManager.animationProcessor = null;
			m_AnimationManager.TakeDamage();
		}

		void _OnDeath()
		{
			m_Data.state = ENEMYSTATE.ES_DEATH;
			
			// 死亡动画，只播放一次
			m_AnimationManager.animationProcessor = null;
			m_AnimationManager.Death();
		}

		/// <summary>
		/// 准备怪物转身
		/// </summary>
		/// <param name="nextState">Next state.</param>
		void _PrepareBodyRotate(ENEMYSTATE nextState)
		{
			m_Data.state = ENEMYSTATE.ES_BODY_ROTATE;
			m_NextState = nextState;
		}

		/// <summary>
		/// 处理被攻击的情况
		/// </summary>
		/// <returns>
		/// 若本次攻击导致怪物死亡，则返回true，否则返回false
		/// </returns>
		public bool BeAttack(float val)
		{
			m_Data.attrib.hp -= val;
			// 血量为0,已跪
			if(m_Data.attrib.hp <= 0.0f)
			{
				m_Data.attrib.hp = 0.0f;
				_OnDeath();
				return true;
			}

			_OnBeAttack();
			return false;
		}

		/// <summary>
		/// 处理被攻击的情况
		/// </summary>
		public void BeSelected()
		{
			gameObject.tag = "Target";
			m_SelectedPlane.SetActive(true);
			m_SelectedPlane.GetComponent<SelectedPlaneControl>().OnSelected();
		}

		/// <summary>
		/// 处理当玩家的目标从该怪物切换到其他怪物的情况
		/// </summary>
		public void AfterBeSelected()
		{
			gameObject.tag = "Enemy";
			m_SelectedPlane.SetActive(false);
			m_SelectedPlane.GetComponent<SelectedPlaneControl>().OnFree();
		}
	}
}