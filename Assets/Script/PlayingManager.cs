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
		private DamageHudControl m_DamageHudControl;
		public DamageHudControl DamageHudControl {
			get {
				return m_DamageHudControl;
			}
		}

		public enum PMANAGER_STATE
		{
			PMS_IDLE,
			PMS_WIN,
			PMS_CITY
		}
		PMANAGER_STATE m_State = PMANAGER_STATE.PMS_IDLE;

		// 主角位置枚举变量
		public enum PLAYER_LOCATION
		{
			PL_TUTORIAL,
			PL_CITY
		}
		PLAYER_LOCATION m_Location = PLAYER_LOCATION.PL_TUTORIAL;

		// 主城出生点
		public Transform m_CitySpwan;

		// Use this for initialization
		void Start () 
		{
			m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
			m_EnemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
			m_AttribPanel = GameObject.Find("Canvas/AttribPanel").GetComponent<AttributePanel>();
			m_DamageHudControl = GameObject.Find("DamageHudCanvas").GetComponent<DamageHudControl>();
			Button atkBtn = GameObject.Find("Canvas/AttackBtn").GetComponent<Button>();
			atkBtn.onClick.AddListener(ProcessAttack);

			// 需要控制初始化时机的脚本在此处初始化
			m_Player.Init();
			m_EnemyManager.Init();
			m_DamageHudControl.Init();
			Inst = this;
		}

		void Update()
		{
			switch(m_State)
			{
			case PMANAGER_STATE.PMS_IDLE:
				{
					if(m_EnemyManager.EnemyList.Count <= 0)
					{
						m_State = PMANAGER_STATE.PMS_WIN;
					}
				}
				break;

			case PMANAGER_STATE.PMS_WIN:
				{
					m_Location = PLAYER_LOCATION.PL_CITY;
					ChangeScene();
				}
				break;

			case PMANAGER_STATE.PMS_CITY:
				{
				}
				break;
			}
		}
		
		public bool IsGameOver()
		{
			return m_Player.IsDeath();
		}

		/// <summary>
		/// 处理攻击行为
		/// </summary>
		void ProcessAttack()
		{
			if(m_Player.CanStartAttack())
			{
				m_Player.OnAttack( m_EnemyManager.GetNearestEnemy(m_Player.transform.position) );
			}
		}

		/// <summary>
		/// 切换玩家所处的场景
		/// </summary>
		void ChangeScene()
		{
			switch(m_Location)
			{
			case PLAYER_LOCATION.PL_CITY:
				{
					m_Player.transform.position = m_CitySpwan.transform.position;
					m_State = PMANAGER_STATE.PMS_CITY;
				}
				break;
			}
		}
	}
}