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
		KeyPadControl m_KeyPad = null;
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

		// 数据集合
		public struct PlayerData
		{
			public PLAYER_TYPE type;
			public PLAYER_STATE state;
			public Global.Attribute attrib;
		}
		PlayerData m_Data;
		public PlayerData Data {
			get { return m_Data; }
			set { m_Data = value; }
		}

		// Use this for initialization
		void Start () 
		{
			_InitData();

			m_KeyPad = Global.GetKeyPad();
			m_TempVec3 = new Vector3();
			m_TempAnlge = new Vector3();
			m_AnimationManager = gameObject.GetComponent<AnimationManagerPlayer>();
		}
		
		// Update is called once per frame
		void Update () 
		{
			if( m_KeyPad.Dir != KeyPadControl.DIR.D_NONE )
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
		}

		void _DirProcess()
		{
			m_TempVec3.Set(0, 0, 0);
			m_TempAnlge.Set(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

			// 朝向和位置都要变
			switch( m_KeyPad.Dir )
			{
			case KeyPadControl.DIR.D_UP:
				{
					m_TempVec3.z = -1.0f;
					m_TempAnlge.y = 180;
				}
				break;
			case KeyPadControl.DIR.D_DOWN:
				{
					m_TempVec3.z = 1.0f;
					m_TempAnlge.y = 0;
				}
				break;
			case KeyPadControl.DIR.D_LEFT:
				{
					m_TempVec3.x = 1.0f;
					m_TempAnlge.y = 90;
				}
				break;
			case KeyPadControl.DIR.D_RIGHT:
				{
					m_TempVec3.x = -1.0f;
					m_TempAnlge.y = 270;
				}
				break;
			case KeyPadControl.DIR.D_LEFTUP:
				{
					m_TempVec3.x = 1.0f;
					m_TempVec3.z = -1.0f;
					m_TempAnlge.y = 135;
				}
				break;
			case KeyPadControl.DIR.D_LEFTDOWN:
				{
					m_TempVec3.x = 1.0f;
					m_TempVec3.z = 1.0f;
					m_TempAnlge.y = 45;
				}
				break;
			case KeyPadControl.DIR.D_RIGHTUP:
				{
					m_TempVec3.x = -1.0f;
					m_TempVec3.z = -1.0f;
					m_TempAnlge.y = 235;
				}
				break;
			case KeyPadControl.DIR.D_RIGHTDOWN:
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
		/// Determines whether this instance is dead.
		/// </summary>
		/// <returns>
		/// <c>true</c> if this instance is dead; otherwise, <c>false</c>.
		/// </returns>
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