/// <summary>
/// 类名: PackagePanel
/// 描述: 背包面板
/// </summary>
	
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ActionGame
{
	public class PackagePanel : PanelBase {

		void Start () 
		{
			Button Btn = GameObject.Find("MainUICanvas/InfoPanel/PackageBtn").GetComponent<Button>();
			Btn.onClick.AddListener(ActiveByCurrentState);
			m_Content = transform.GetComponentInChildren<PackageContentControl >();
			m_Content.Init();
			
			gameObject.SetActive(false);
		}
	}
}