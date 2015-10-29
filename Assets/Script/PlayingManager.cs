/// <summary>
/// 类名： PlayingManager
/// 用途： 主场景管理类，负责管理主场景的行为
/// </summary>

using UnityEngine;
using System.Collections;

namespace ActionGame
{
	public class PlayingManager : MonoBehaviour {
		public static PlayingManager Inst;
		private PlayerControl m_Player = null;
		public PlayerControl Player {
			get { return m_Player; }
		}
		private AttributePanel m_AttribPanel;
		public AttributePanel AttribPanel {
			get {
				return m_AttribPanel;
			}
			set {
				m_AttribPanel = value;
			}
		}

		// Use this for initialization
		void Start () 
		{
			m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
			m_Player.Init();
			m_AttribPanel = GameObject.Find("Canvas/AttribPanel").GetComponent<AttributePanel>();
			Inst = this;
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