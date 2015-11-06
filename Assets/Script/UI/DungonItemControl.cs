/// <summary>
/// 类名： DungonItemControl
/// 用途： 控制地下城区块的行为
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace ActioGame
{
	public class DungonItemControl : MonoBehaviour, IPointerClickHandler
	{
		Sprite m_OriginSpr;
		public Sprite m_SelectedSpr;
		Image m_Img;
		float m_ScaleRate = 1.2f;
		bool m_IsSelected = false;

		// Use this for initialization
		void Start () 
		{
			m_Img = GetComponent<Image>();
			m_OriginSpr = m_Img.sprite;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if(m_IsSelected)
			{
				m_Img.transform.localScale *= 1.2f;
			}
			else
			{
				m_Img.transform.localScale = Vector3.one;
			}

			m_IsSelected = !m_IsSelected;
		}
	}
}