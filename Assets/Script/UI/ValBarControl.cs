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
		public float initVal = 1.0f;

		// Use this for initialization
		void Start () 
		{
			Init();
		}

		public void Init()
		{
			m_ScrollBar = GetComponent<Scrollbar>();
			m_Content = m_ScrollBar.handleRect.gameObject.GetComponent<Image>();
			m_ValText = gameObject.GetComponentInChildren<Text>();
			
			// 加载遮罩材质，并设置参数
			Material mat = Resources.Load("Material/MaskMat") as Material;
			m_Content.material = Instantiate(mat) as Material;
			m_Content.material.SetFloat("_MaskWidth", initVal);
			m_Content.material.SetTexture("_MainTex", m_Content.sprite.texture);
            m_Content.material.SetColor("_Color", m_Content.color);
		}
		
		public void OnValueChange(float val, string valStr)
		{
            m_Content.material.SetFloat("_MaskWidth", val);

			// 字体字符串为空，则代表不需要设置valStr
			if(valStr == "")
			{
				return;
			}

			// 否则尝试设置ValText
			if( m_ValText != null )
			{
				m_ValText.text = valStr;
			}
			else
			{
				m_ValText = gameObject.GetComponentInChildren<Text>();
				m_ValText.text = valStr;
			}
		}
	}
}