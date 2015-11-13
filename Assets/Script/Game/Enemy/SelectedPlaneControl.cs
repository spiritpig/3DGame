/// <summary>
/// 类名： SelectedPlaneControl
/// 用途： 控制怪物选择面的行为
/// </summary>

using UnityEngine;
using System.Collections;

namespace ActionGame
{
	public class SelectedPlaneControl : MonoBehaviour {
		bool m_IsSelected = false;
		float m_RotateSpeed = 1.0f;

		/// <summary>
		/// 被选中处理
		/// </summary>
		public void OnSelected()
		{
			m_IsSelected = true;
		}

		/// <summary>
		/// 未被选中处理
		/// </summary>
		public void OnFree()
		{
			m_IsSelected = false;
			transform.rotation = Quaternion.identity;
		}
		
		// Update is called once per frame
		void Update () 
		{
			if(m_IsSelected)
			{
				transform.Rotate(0.0f, transform.rotation.y + m_RotateSpeed, 0.0f);
			}
		}
	}
}