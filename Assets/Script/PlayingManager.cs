/// <summary>
/// 类名： PlayingManager
/// 用途： 主场景管理类，负责管理主场景的行为
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ActionGame
{
	public class PlayingManager : MonoBehaviour {
		public static PlayingManager Inst;
		private PlayerControl m_Player = null;
		private EnemyManager m_EnemyManager = null;
		public PlayerControl Player {
			get { return m_Player; }
		}
		private AttributePanel m_AttribPanel;
		public AttributePanel AttribPanel {
			get { return m_AttribPanel; }
		}

		// Use this for initialization
		void Start () 
		{
			m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
			m_EnemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
			m_AttribPanel = GameObject.Find("Canvas/AttribPanel").GetComponent<AttributePanel>();
			Button atkBtn = GameObject.Find("Canvas/AttackBtn").GetComponent<Button>();
			atkBtn.onClick.AddListener(ProcessAttack);

			m_Player.Init();
			m_EnemyManager.Init();
			Inst = this;
		}
		
		public bool IsGameOver()
		{
			return m_Player.IsDead();
		}

		/// <summary>
		/// 处理攻击行为
		/// </summary>
		void ProcessAttack()
		{
			m_Player.OnAttack( m_EnemyManager.GetNearestEnemy(m_Player.transform.position) );
		}
	}
}