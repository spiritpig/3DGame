/// <summary>
/// 类名: ValBarControl
/// 描述: 值滚动条控制脚本，用于控制滚动条的进度
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ActionGame
{
	public class ValBarControl : MonoBehaviour {
		Scrollbar m_ScrollBar;
		Image m_Content;
		Text m_ValText;

		// Use this for initialization
		void Start () 
		{
			m_ScrollBar = GetComponent<Scrollbar>();
			m_Content = m_ScrollBar.handleRect.gameObject.GetComponent<Image>();
			m_ValText = gameObject.GetComponentInChildren<Text>();

			m_Content.material.SetFloat("_MaskWidth", 1.0f);
			m_Content.material.SetTexture("_MainTex", m_Content.sprite.texture);
			m_Content.material.SetColor("_Color", m_Content.color);
		}
		
		public void OnValueChange(float val, string valStr)
		{
			m_Content.material.SetFloat("_MaskWidth", val);
			m_ValText.text = valStr;
		}
	}
}