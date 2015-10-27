using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AttributePanel : MonoBehaviour {
	GameObject m_HpBar, m_MpBar;

	// Use this for initialization
	void Start () 
	{
		m_HpBar = GameObject.Find("Canvas/AttribPanel/HpBar");
		m_MpBar = GameObject.Find("Canvas/AttribPanel/MpBar");
	}

	public void OnHpBarChange(float rate, string hpStr)
	{
		m_HpBar.GetComponent<ValBarControl>().OnValueChange(rate, hpStr);
	}

	public void OnMpBarChange(float rate, string mpStr)
	{
		m_MpBar.GetComponent<ValBarControl>().OnValueChange(rate, mpStr);
	}
}
