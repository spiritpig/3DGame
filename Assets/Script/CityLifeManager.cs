/// <summary>
/// 类名： CityLifeManager
/// 用途： 地下城管理类，负责管理玩家在地下城中的行为
/// </summary>

using UnityEngine;
using System.Collections;

namespace ActionGame
{
	public class CityLifeManager : MonoBehaviour, IManager {

		public enum CLMANAGER_STATE
		{
			CMS_WORK
		}
		CLMANAGER_STATE m_State = CLMANAGER_STATE.CMS_WORK;

		private PlayerControl m_Player = null;
		public PlayerControl Player {
			get { return m_Player; }
		}

		// 城市中，玩家的出生点
		Transform m_CitySpawn;
		public Transform CitySpawn {
			get {
				return m_CitySpawn;
			}
		}

		public static CityLifeManager Inst = null;

		// Use this for initialization
		void Start () 
		{
			m_CitySpawn = transform.FindChild("CitySpawn");
			m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
			m_Player.Init();

			Inst = this;
		}
		
		// Update is called once per frame
		void Update () 
		{
			switch(m_State)
			{
            case CLMANAGER_STATE.CMS_WORK:
				{
				}
				break;
			}
		}

		public void OnAwake()
		{
			gameObject.SetActive(true);
			m_State = CLMANAGER_STATE.CMS_WORK;
		}

		public void OnSleep()
		{
			gameObject.SetActive(false);
		}
	}
}