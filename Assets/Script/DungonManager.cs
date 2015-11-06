/// <summary>
/// 类名： DungonManager
/// 用途： 地下城管理类，负责管理玩家在地下城中的行为
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ActionGame
{
	public class DungonManager : MonoBehaviour, IManager {
		public static DungonManager Inst;
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

		public enum DMANAGER_STATE
		{
			DMS_SLEEP,
			DMS_AWAKE,
			DMS_NORMAL,
			DMS_WIN
		}
		DMANAGER_STATE m_State = DMANAGER_STATE.DMS_SLEEP;
		public DMANAGER_STATE State {
			get { return m_State; }
			set { m_State = value; }
		}
		Transform m_PlayerSpawn;

		// Use this for initialization
		void Start () 
		{
			m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
			m_EnemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
			m_AttribPanel = GameObject.Find("MainUICanvas/AttribPanel").GetComponent<AttributePanel>();
			m_DamageHudControl = GameObject.Find("DamageHudCanvas").GetComponent<DamageHudControl>();
			Button atkBtn = GameObject.Find("MainUICanvas/AttackBtn").GetComponent<Button>();
			atkBtn.onClick.AddListener(ProcessAttack);

			m_PlayerSpawn = transform.FindChild("Tutorial").FindChild("PlayerSpawn");

			// 需要控制初始化时机的脚本在此处初始化
			m_EnemyManager.Init();
			m_DamageHudControl.Init();
			Inst = this;

			OnSleep();
		}

		void Update()
		{
			switch(m_State)
			{
			case DMANAGER_STATE.DMS_WIN:
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
		/// 当玩家进入地下城时，被唤醒
		/// </summary>
		public void OnAwake()
		{
			gameObject.SetActive(true);
			m_EnemyManager.gameObject.SetActive(true);	
			m_State = DMANAGER_STATE.DMS_NORMAL;
			m_Player.transform.position = m_PlayerSpawn.position;
			m_Player.transform.rotation = m_PlayerSpawn.rotation;
			m_Player.Data.isInCity = false;
        }

		/// <summary>
		/// 当玩家进入地下城时，被唤醒
		/// </summary>
		public void OnSleep()
		{
			gameObject.SetActive(false);
			m_EnemyManager.gameObject.SetActive(false);
        }
    }
}