// <summary>
/// 类名： DungonEntryControl
/// 用途： 地下城入口控制，负责相应玩家要进入地下城的行为
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ActionGame
{
	public class DungonEntryControl : MonoBehaviour {
		GameObject m_DungonEntryUI;
		CanvasRenderer m_DungonEntryUIRenerer;
		Image m_DungonEntryUIBg;
		DungonItemManager m_DungonItemManager;
		bool m_IsEnterRange = false;

		// Use this for initialization
		void Awake () 
		{
			m_DungonEntryUI = GameObject.Find("DungonEntryCanvas");
			m_DungonEntryUIRenerer = m_DungonEntryUI.transform.FindChild("DungonEntryBG").GetComponent<CanvasRenderer>();
			m_DungonEntryUIBg = m_DungonEntryUIRenerer.gameObject.GetComponent<Image>();
			m_DungonItemManager = GameObject.Find("DungonEntryCanvas/DungonItemList").GetComponent<DungonItemManager>();

			Button enterBtn =m_DungonEntryUI.transform.FindChild("Enter").GetComponent<Button>();
			enterBtn.onClick.AddListener(OnEnterDungon);
			Button cancelBtn =m_DungonEntryUI.transform.FindChild("Cancel").GetComponent<Button>();
			cancelBtn.onClick.AddListener(OnReturnCity);

			m_DungonEntryUI.SetActive(false);
		}

		public void OnEnterDungon()
		{
			if(m_DungonItemManager.IsSelectedDungonWolf())
			{
				m_DungonEntryUI.SetActive(false);
				DungonManager.Inst.OnAwake();
				CityLifeManager.Inst.OnSleep();
				CityLifeManager.Inst.Player.gameObject.SetActive(true);
			}
		}

		public IEnumerator UIFadeIn()
		{
			float curAlpha = 0.0f;

			while(m_DungonEntryUIRenerer.GetAlpha() < 1.0f)
			{
				curAlpha += 0.1f;
				m_DungonEntryUIRenerer.SetAlpha(curAlpha);

				yield return null;
			}
		}

		public void OnReturnCity()
		{
			m_DungonEntryUI.SetActive(false);
			CityLifeManager.Inst.Player.gameObject.SetActive(true);
		}

		public void OnTriggerEnter(Collider colli)
		{
			if(colli.gameObject.tag == "Player" && !m_IsEnterRange)
			{
				m_IsEnterRange = true;

				m_DungonEntryUI.SetActive(true);
				m_DungonEntryUIRenerer.SetAlpha(0.0f);
				//StartCoroutine("UIFadeIn");
				m_DungonEntryUIBg.CrossFadeAlpha(1, 0.5f, true);
				CityLifeManager.Inst.Player.gameObject.SetActive(false);
			}
		}

		public void OnTriggerExit(Collider colli)
		{
			m_IsEnterRange = false;
		}
	}
}