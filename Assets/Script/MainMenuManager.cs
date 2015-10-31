/// <summary>
/// 类名： MainMenuManager
/// 用途： 主菜单管理类，管理主菜单的状态切换
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ActionGame
{
	public class MainMenuManager : MonoBehaviour {

		public enum MENUSTATE
		{
			MS_NONE,
			MS_STARTGAME
		}
		MENUSTATE m_State = MENUSTATE.MS_NONE;
		Button m_StartBtn;
		
		// Use this for initialization
		void Start () 
		{
			m_StartBtn = GameObject.Find("Canvas/StartGame").GetComponent<Button>();
			m_StartBtn.onClick.AddListener( OnClick );
		}

		/// <summary>
		/// 开始按钮监听器
		/// </summary>
		public void OnClick()
		{
			m_State = MENUSTATE.MS_STARTGAME;

			// 销毁所有的GameObject
			foreach(GameObject go in GameObject.FindObjectsOfType(typeof(GameObject)))
			{
				if( go != gameObject )
				{
					Destroy(go);
				}
			}
			Application.LoadLevel(Global.g_LoadingScene);
		}
		
		public bool IsStartGame()
		{
			return m_State == MENUSTATE.MS_STARTGAME;
		}
	}
}
