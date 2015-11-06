/// <summary>
/// 类名： PlayerControl
/// 用途： 管理玩家的行为和状态
/// </summary>

using UnityEngine;
using System.Collections;

namespace ActionGame
{
	public class PlayerControl : MonoBehaviour {
		Vector3 m_TempVec3, m_FrontVec, m_TempAnlge, m_MagicOriginPos;
		EnemyControl m_CurTarget = null;
		MagicBallControl m_Magicball = null;
		ParticleSystem m_LevelUpEffect;
		GameObject m_FrontObj = null;
		float m_RotateSpeed = 10.0f, m_CurAtkTime = 0.0f;
		Global.DIR m_Dir = Global.DIR.D_NONE;
		AnimationManagerPlayer m_AnimationManager;
		CharacterController m_Controller;

		/// <summary>
		/// 玩家状态
		/// </summary>
		public enum PLAYER_STATE
		{
			PS_IDLE,
			PS_WALK,
			PS_WAIT_ANIMAT,			// 等待动画播放完状态
			PS_ROTATE_TO_TARGET,
			PS_MOVE_TO_TARGET,
			PS_PRE_ATTACK,
			PS_ATTACK,
			PS_LEVELUP,
			PS_Death
		}

		// 类型
		public enum PLAYER_TYPE
		{
			PT_MAGE
		}

		public struct LevelData
		{
			public int level;			// 当前等级
			public int exp;				// 经验值
			public int levelUpExp;		// 升级所需经验值
		}

		// 数据集合
		public class PlayerData
		{
			public PLAYER_TYPE type;
			public PLAYER_STATE state;
			public Global.Attribute attrib;
			public LevelData levelData;
			public bool isInCity;

			public void LevelUp()
			{
				levelData.level += 1;
				levelData.levelUpExp = levelData.levelUpExp + levelData.levelUpExp*(levelData.level/10 + 1);
				levelData.exp = 0;

				attrib.hp += (float)((int)(100.0f*(1+Mathf.Log(levelData.level))));
				attrib.maxHp = attrib.hp;
				attrib.eng += (float)((int)(10.0f*(1+Mathf.Log(levelData.level))));
				attrib.maxEng = attrib.eng;
				attrib.atkMag += (float)((int)(attrib.atkMag/2*(Mathf.Log(levelData.level))));
				attrib.atkPhy += (float)((int)(attrib.atkPhy/2 * 0.07f));
			}
		}
		PlayerData m_Data;
		public PlayerData Data {
			get { return m_Data; }
			set { m_Data = value; }
		}

		// Player初始化，有我们自己完成
		public void Init() 
		{
			_InitData();

			m_Magicball = transform.FindChild("MagicBall").gameObject.GetComponent<MagicBallControl>();
			m_Magicball.gameObject.SetActive(false);
			m_MagicOriginPos = m_Magicball.transform.localPosition;

			m_LevelUpEffect = transform.FindChild("LevelUpEffect").gameObject.GetComponent<ParticleSystem>();
			m_LevelUpEffect.gameObject.SetActive(false);

			m_FrontObj = transform.FindChild("FrontObj").gameObject;

			m_TempVec3 = new Vector3();
			m_TempAnlge = new Vector3();
			m_AnimationManager = gameObject.GetComponent<AnimationManagerPlayer>();
			m_Controller = gameObject.GetComponent<CharacterController>();
		}
		
		// Update is called once per frame
		void Update () 
		{
			_CalcCurDir();
			_CalcCurState();

			switch(m_Data.state)
			{
			case PLAYER_STATE.PS_IDLE:
				{
					m_AnimationManager.animationProcessor = m_AnimationManager.Idle;

					if(m_CurAtkTime >= 0.0f)
					{
						m_CurAtkTime -= Time.deltaTime;
					}
				}
				break;

			case PLAYER_STATE.PS_WAIT_ANIMAT:
				{
					if(!m_AnimationManager.IsPlaying())
					{
						_OnIdle();
					}

					if(m_CurAtkTime >= 0.0f)
					{
						m_CurAtkTime -= Time.deltaTime;
	                  }
                }
                break;
                    
                case PLAYER_STATE.PS_WALK:
				{
					_MoveProcess();
					m_AnimationManager.animationProcessor = m_AnimationManager.Walk;

					if(m_CurAtkTime >= 0.0f)
					{
						m_CurAtkTime -= Time.deltaTime;
					}
				}
				break;

			case PLAYER_STATE.PS_ROTATE_TO_TARGET:
				{
					m_TempVec3 = m_CurTarget.transform.position - transform.position;
					Quaternion lookRotate = Quaternion.LookRotation(m_TempVec3);

					// 将玩家转向目标
					if(Quaternion.Angle( transform.rotation, lookRotate ) > 1.0f)
					{
						transform.rotation = Quaternion.Slerp( transform.rotation, lookRotate, 
					                                      		m_RotateSpeed*Time.deltaTime );
					}
					else
					{
						if(IsTargetInAtkRange())
						{
							m_Data.state = PLAYER_STATE.PS_PRE_ATTACK;
							m_AnimationManager.OnAttack();
							m_AnimationManager.animationProcessor = null;
						}
						else
						{
							m_Data.state = PLAYER_STATE.PS_MOVE_TO_TARGET;
						}
					}
				}
				break;

			case PLAYER_STATE.PS_MOVE_TO_TARGET:
				{
					// 若是目标处于攻击范围内，开始攻击
					// 若是没有，则先移动到攻击范围内
					if(IsTargetInAtkRange())
					{
						m_Data.state = PLAYER_STATE.PS_PRE_ATTACK;
						m_AnimationManager.OnAttack();
						m_AnimationManager.animationProcessor = null;
					}
					else
					{
						m_TempVec3 = m_CurTarget.transform.position - transform.position;
						m_TempVec3.Normalize();
						transform.position += m_TempVec3 *(m_Data.attrib.movSp*Time.deltaTime);
					}
				}
				break;

			case PLAYER_STATE.PS_PRE_ATTACK:
				{
					m_CurAtkTime -= Time.deltaTime;

					// 攻击动画播放完成后，释放魔法球
					if(m_AnimationManager.IsAttack1End())
					{
						m_Data.state = PLAYER_STATE.PS_ATTACK;
						m_Magicball.gameObject.SetActive(true);
						m_Magicball.Reset(m_MagicOriginPos, m_CurTarget);
					}
				}
				break;

			case PLAYER_STATE.PS_ATTACK:
				{
					m_CurAtkTime -= Time.deltaTime;

					// 此处交由MagicBall自行完成,移动。
					// 玩家只需判断目标是否死亡
					if(m_CurTarget.Data.state == EnemyControl.ENEMYSTATE.ES_DEATH)
					{
						_OnAddExp();

						m_Magicball.gameObject.SetActive(false);
						_OnIdle();
						break;
					}

					// 以及目标是否被击中
					if(m_Magicball.GetComponent<MagicBallControl>().IsHitTarget)
                    {
						// 当目标死亡了，经验加上去
						if(m_CurTarget.BeAttack(m_Data.attrib.atkPhy))
						{
							_OnAddExp();
						}

						m_Magicball.gameObject.SetActive(false);
						// 若攻击动画未播完，等待其播完后，再继续
						if(m_AnimationManager.IsPlaying())
						{
							_OnWaitAnimation();
						}
						else
						{
							_OnIdle();
						}
						break;
					}
				}
				break;

			case PLAYER_STATE.PS_LEVELUP:
				{
					if(!m_LevelUpEffect.isPlaying)
					{
					_OnIdle();
					}
				}
				break;
			}
		}

		void _InitData()
		{
			m_Data = new PlayerData();
			m_Data.type = PLAYER_TYPE.PT_MAGE;
			m_Data.state = PLAYER_STATE.PS_IDLE;
			m_Data.isInCity = true;

			// 属性数据
			m_Data.attrib.charName = "Mage";
			m_Data.attrib.hp = 100.0f;
			m_Data.attrib.maxHp = 100.0f;
			m_Data.attrib.eng = 100.0f;
			m_Data.attrib.maxEng = 100.0f;
			m_Data.attrib.atkPhy = 20.0f;
			m_Data.attrib.atkMag = 40.0f;
			m_Data.attrib.defPhy = 20.0f;
			m_Data.attrib.defMag = 40.0f;
			m_Data.attrib.atkSp = 1.2f;
			m_Data.attrib.movSp = Global.g_PlayerMoveSpeed;
			m_Data.attrib.atkRange = 10.0f;

			// 等级数据
			m_Data.levelData.level = 1;
			m_Data.levelData.exp = 0;
			m_Data.levelData.levelUpExp = 100;
		}

		void _MoveProcess()
		{
			m_TempVec3.Set(0, 0, 0);
			m_TempAnlge.Set(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

			// 朝向和位置都要变
			switch( m_Dir )
			{
			case Global.DIR.D_UP:
				{
					m_TempVec3.z = -1.0f;
					m_TempAnlge.y = 180;
				}
				break;
			case Global.DIR.D_DOWN:
				{
					m_TempVec3.z = 1.0f;
					m_TempAnlge.y = 0;
				}
				break;
			case Global.DIR.D_LEFT:
				{
					m_TempVec3.x = 1.0f;
					m_TempAnlge.y = 90;
				}
				break;
			case Global.DIR.D_RIGHT:
				{
					m_TempVec3.x = -1.0f;
					m_TempAnlge.y = 270;
				}
				break;
			case Global.DIR.D_LEFTUP:
				{
					m_TempVec3.x = 1.0f;
					m_TempVec3.z = -1.0f;
					m_TempAnlge.y = 135;
				}
				break;
			case Global.DIR.D_LEFTDOWN:
				{
					m_TempVec3.x = 1.0f;
					m_TempVec3.z = 1.0f;
					m_TempAnlge.y = 45;
				}
				break;
			case Global.DIR.D_RIGHTUP:
				{
					m_TempVec3.x = -1.0f;
					m_TempVec3.z = -1.0f;
					m_TempAnlge.y = 235;
				}
				break;
			case Global.DIR.D_RIGHTDOWN:
				{
					m_TempVec3.x = -1.0f;
					m_TempVec3.z = 1.0f;
					m_TempAnlge.y = 315;
				}
				break;
			}

			// 向前方打一条射线，判断移动是否会撞到障碍物
			if(!Physics.Raycast(transform.position, m_TempVec3, m_Data.attrib.movSp*Time.deltaTime))
			{
				// 为发生碰撞，则产生移动
				m_Controller.SimpleMove(m_TempVec3*m_Data.attrib.movSp);
			}
			transform.eulerAngles = m_TempAnlge;
		}

		/// <summary>
		/// 根据当前的轴距确定角色的行进方向
		/// </summary>
		void _CalcCurDir()
		{
			float vertAxis = ETCInput.GetAxis("Vertical");
			float horiAxis = ETCInput.GetAxis("Horizontal");

			if(vertAxis > 0.0f)
			{
				if(horiAxis.CompareTo(0.0f) == 0)
				{
					m_Dir = Global.DIR.D_UP;
				}
				else
				if(horiAxis < 0.0f)
				{
					m_Dir = Global.DIR.D_LEFTUP;
				}
				else
				{
					m_Dir = Global.DIR.D_RIGHTUP;
				}
			}
			else
			if(vertAxis < 0.0f)
			{
				if(horiAxis.CompareTo(0.0f) == 0)
				{
					m_Dir = Global.DIR.D_DOWN;
				}
				else
					if(horiAxis < 0.0f)
				{
					m_Dir = Global.DIR.D_LEFTDOWN;
				}
				else
				{
					m_Dir = Global.DIR.D_RIGHTDOWN;
				}
			}
			else
			{
				if(horiAxis > 0.0f)
				{
					m_Dir = Global.DIR.D_RIGHT;
				}
				else
				if(horiAxis < 0.0f)
				{
					m_Dir = Global.DIR.D_LEFT;
				}
				else
				{
					m_Dir = Global.DIR.D_NONE;
				}
			}
		}

		/// <summary>
		/// 计算当前的状态
		/// </summary>
		void _CalcCurState()
		{
			if(m_Data.state == PLAYER_STATE.PS_WALK ||
			   m_Data.state == PLAYER_STATE.PS_IDLE)
			{
				if(m_Dir == Global.DIR.D_NONE)
				{
					m_Data.state = PLAYER_STATE.PS_IDLE;
				}
				else
				{
					m_Data.state = PLAYER_STATE.PS_WALK;
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		void _OnAddExp()
		{
			m_Data.levelData.exp += m_CurTarget.Data.attrib.gainExp;

			// 若经验满了，则升级
			if(m_Data.levelData.exp >= m_Data.levelData.levelUpExp)
			{
				m_Data.LevelUp();

				m_Data.state = PLAYER_STATE.PS_LEVELUP;
				m_LevelUpEffect.gameObject.SetActive(true);
				m_LevelUpEffect.Play(true);

				// 升级了血量和MP都有提升，需要更新显示
				DungonManager.Inst.AttribPanel.OnHpBarChange();
				DungonManager.Inst.AttribPanel.OnMpBarChange();
			}

			// 更新UI
			DungonManager.Inst.AttribPanel.OnExpBarChange();
		}

		/// <summary>
		/// 处理玩家回到初始状态的情况
		/// </summary>
		void _OnIdle()
		{
			m_Data.state = PLAYER_STATE.PS_IDLE;
			m_AnimationManager.animationProcessor = m_AnimationManager.Idle;
		}

		/// <summary>
		/// 处理玩家攻击动画尚未播完即击中敌人的情况
		/// </summary>
        void _OnWaitAnimation()
		{
			m_Data.state = PLAYER_STATE.PS_WAIT_ANIMAT;
		}

		/// <summary>
		/// 玩家是否死亡
		/// </summary>
		public bool IsDeath()
		{
			return m_Data.state == PLAYER_STATE.PS_Death;
		}

		/// <summary>
		/// 判断目标是否在玩家的共计范围内
		/// </summary>
		public bool IsTargetInAtkRange()
		{
			float m_Dist = Vector3.Distance(transform.position, m_CurTarget.transform.position);
			return m_Dist <= m_Data.attrib.atkRange;
		}

		/// <summary>
		/// 判断目标是否在玩家面朝方向的正面
		/// 备注： 预留，未来可能有用
		/// </summary>
		public bool IsTargetInFront()
		{
			if(m_CurTarget)
			{
				return false;
			}

			m_TempVec3 = m_CurTarget.transform.position - transform.position;
			m_FrontVec = m_FrontObj.transform.position - transform.position;
			float val = Vector3.Dot(m_FrontVec, m_TempVec3);

			return val > 0;
		}

		/// <summary>
		/// 判断是否可以开始攻击
		/// </summary>
		public bool CanStartAttack()
		{
			// 攻击的CD时间未到，再等会
			// 或者在城镇中，不宜杀生
			if(m_Data.isInCity || m_CurAtkTime > 0.0f)
			{
				return false;
			}

			if( m_Data.state == PLAYER_STATE.PS_IDLE ||
			   m_Data.state == PLAYER_STATE.PS_WALK )
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// 被攻击函数，减少血量
		/// </summary>
		public void BeAttack(float val)
		{
			m_Data.attrib.hp -= val;
			if(m_Data.attrib.hp < 0)
			{
				m_Data.attrib.hp = 0;
			}

			// 更新UI
			DungonManager.Inst.AttribPanel.OnHpBarChange();
			DungonManager.Inst.DamageHudControl.UseDamageHud(transform, (int)val);
		}

		/// <summary>
		/// 处理攻击行为
		/// 参数： 离玩家最近的怪物，由外部提供
		/// </summary>
		public void OnAttack(EnemyControl Enemy)
		{
			if(Enemy == null)
			{
				return;
			}

			if(m_CurTarget != null)
			{
				m_CurTarget.AfterBeSelected();
			}

			m_CurTarget = Enemy;
			m_CurTarget.BeSelected();
			m_Data.state = PLAYER_STATE.PS_ROTATE_TO_TARGET;
			m_CurAtkTime = m_Data.attrib.atkSp;
			m_AnimationManager.animationProcessor = m_AnimationManager.Walk;
		}
	}
}