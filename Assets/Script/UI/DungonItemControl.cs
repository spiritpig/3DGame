/// <summary>
/// 类名： DungonItemControl
/// 用途： 控制地下城区块的行为
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace ActionGame
{
	public class DungonItemControl : MonoBehaviour, IPointerClickHandler
	{
		Sprite m_OriginSpr;
		public Sprite m_SelectedSpr;
		Image m_Img;
		float m_ScaleRate = 1.2f;
		bool m_IsSelected;
		public bool IsSelected {
			get {
				return m_IsSelected;
			}
		}
		DungonType m_Type;
		public DungonType Type {
			get { return m_Type; }
		}

		// Use this for initialization
		void Awake () 
		{
			m_Img = GetComponent<Image>();
			m_OriginSpr = m_Img.sprite;
			m_Type = DungonType.DT_WOLF;
			m_IsSelected = false;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			m_IsSelected = !m_IsSelected;
			if(m_IsSelected)
			{
				m_Img.transform.localScale *= 1.2f;
			}
			else
			{
				m_Img.transform.localScale = Vector3.one;
			}
		}
	}
}
