/// <summary>
/// 类名： PlayingManager
/// 用途： 主场景管理类，负责管理主场景的行为
/// </summary>

using UnityEngine;
using System.Collections;

namespace ActionGame
{
	public class PlayingManager : MonoBehaviour {
		private PlayerControl m_Player = null;
		public PlayerControl Player {
			get { return m_Player; }
		}

		// Use this for initialization
		void Start () 
		{
			m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
		}
		
		// Update is called once per frame
		void Update () 
		{
		}

		public bool IsGameOver()
		{
			return m_Player.IsDead();
		}
	}
}