/// <summary>
/// 类名： GoalControl
/// 用途： 控制终点的状态
/// </summary>
/// 
using UnityEngine;
using System.Collections;

namespace ActionGame
{
	public class GoalControl : MonoBehaviour {
		public enum GOALSTATE
		{
			GS_NONE,
			GS_TOUCH
		}
		GOALSTATE m_State = GOALSTATE.GS_NONE;
		public GOALSTATE State {
			get { return m_State; }
		}

		void OnCollisionEnter(Collision info)
		{
			if( info.gameObject.tag == "Player" )
			{
				Debug.Log("Get Goal");
				m_State = GOALSTATE.GS_TOUCH;
			}
		}
	}
}