/// <summary>
/// 类名： PlayerControl
/// 用途： 管理玩家的行为和状态
/// </summary>

using UnityEngine;
using System.Collections;

namespace ActionGame
{
	public class PlayerControl : MonoBehaviour {
		float m_MoveSpeed;
		Vector3 m_TempVec3, m_TempAnlge;
		KeyPadControl m_KeyPad = null;
		AnimationManagerPlayer m_AnimationManager;

		/// <summary>
		/// 玩家状态
		/// </summary>
		public enum PLAYER_STATE
		{
			PS_None,
			PS_Walk,
			PS_Run,
			PS_Dead
		}
		PLAYER_STATE m_State;
		public PLAYER_STATE M_State {
			get { return m_State; }
			set { m_State = value; }
		}

		// Use this for initialization
		void Start () 
		{
			m_MoveSpeed = Global.g_PlayerMoveSpeed;
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
				DirProcess();
				m_AnimationManager.m_CurAnimationProcessor = m_AnimationManager.Run;
			}
			else
			{
				m_AnimationManager.m_CurAnimationProcessor = m_AnimationManager.Idle;
			}
		}

		void DirProcess()
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

			transform.position += m_TempVec3 *(m_MoveSpeed*Time.deltaTime);
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
			return m_State == PLAYER_STATE.PS_Dead;
		}
	}
}