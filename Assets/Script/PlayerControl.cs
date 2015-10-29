/// <summary>
/// 类名： PlayerControl
/// 用途： 管理玩家的行为和状态
/// </summary>

using UnityEngine;
using System.Collections;

namespace ActionGame
{
	public class PlayerControl : MonoBehaviour {
		Vector3 m_TempVec3, m_TempAnlge;
		Global.DIR m_Dir = Global.DIR.D_NONE;
		AnimationManagerPlayer m_AnimationManager;

		/// <summary>
		/// 玩家状态
		/// </summary>
		public enum PLAYER_STATE
		{
			PS_IDLE,
			PS_WALK,
			PS_RUN,
			PS_DEAD
		}

		// 类型
		public enum PLAYER_TYPE
		{
			PT_MAGE
		}

		public class LevelData
		{
			public int level;			// 当前等级
			public int exp;				// 经验值
			public int levelUpExp;		// 升级所需经验值

			void LevelUp()
			{
				if(exp >= levelUpExp)
				{
					level += 1;
					levelUpExp = levelUpExp + levelUpExp*(level/10 + 1);
				}
			}
		}

		// 数据集合
		public struct PlayerData
		{
			public PLAYER_TYPE type;
			public PLAYER_STATE state;
			public Global.Attribute attrib;
			public LevelData levelData;
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

			m_TempVec3 = new Vector3();
			m_TempAnlge = new Vector3();
			m_AnimationManager = gameObject.GetComponent<AnimationManagerPlayer>();
		}
		
		// Update is called once per frame
		void Update () 
		{
			CalcCurDir();

			if( m_Dir != Global.DIR.D_NONE )
			{
				_DirProcess();
				m_AnimationManager.m_CurAnimationProcessor = m_AnimationManager.Run;
				m_Data.state = PLAYER_STATE.PS_RUN;
			}
			else
			{
				m_AnimationManager.m_CurAnimationProcessor = m_AnimationManager.Idle;
				m_Data.state = PLAYER_STATE.PS_IDLE;
			}
		}

		void _InitData()
		{
			m_Data.type = PLAYER_TYPE.PT_MAGE;
			m_Data.state = PLAYER_STATE.PS_IDLE;

			m_Data.attrib.charName = "Mage";
			m_Data.attrib.hp = 100.0f;
			m_Data.attrib.maxHp = 100.0f;
			m_Data.attrib.eng = 100.0f;
			m_Data.attrib.maxEng = 100.0f;
			m_Data.attrib.atkPhy = 20.0f;
			m_Data.attrib.atkMag = 40.0f;
			m_Data.attrib.defPhy = 20.0f;
			m_Data.attrib.defMag = 40.0f;
			m_Data.attrib.atkSp = 1.0f;
			m_Data.attrib.movSp = Global.g_PlayerMoveSpeed;
			m_Data.attrib.atkRange = 5.0f;

			m_Data.levelData = new LevelData();
			m_Data.levelData.level = 1;
			m_Data.levelData.exp = 0;
			m_Data.levelData.levelUpExp = 100;
		}

		void _DirProcess()
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

			transform.position += m_TempVec3 *(m_Data.attrib.movSp*Time.deltaTime);
			transform.eulerAngles = m_TempAnlge;
		}

		/// <summary>
		/// 根据当前的轴距确定角色的行进方向
		/// </summary>
		void CalcCurDir()
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
		/// Determines whether this instance is dead.
		/// </summary>
		public bool IsDead()
		{
			return m_Data.state == PLAYER_STATE.PS_DEAD;
		}

		public void OnBeAttack(float atkVal)
		{
			m_Data.attrib.hp -= atkVal;
			if(m_Data.attrib.hp < 0)
			{
				m_Data.attrib.hp = 0;
			}

			// 血条做出反应
			PlayingManager.Inst.AttribPanel.OnHpBarChange( m_Data.attrib.hp / m_Data.attrib.maxHp,
			               m_Data.attrib.hp.ToString() + "/" + m_Data.attrib.maxHp.ToString() );
		}
	}
}