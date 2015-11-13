
/// <summary>
/// 类名: ItemTest3DControl
/// 描述: 测试物品3D控制类
/// </summary>

using UnityEngine;
using System.Collections;

namespace ActionGame
{
	public class ItemTest3DControl : MonoBehaviour {
		Color m_TempColor;
		Item m_TestItem;
		MeshRenderer m_Renderer;
		bool m_IsPicked = false;
		public bool IsPicked {
			get {
				return m_IsPicked;
			}
		}

		// Use this for initialization
		void Awake () 
		{
			m_TempColor = Color.white;
			m_TestItem = null;
			m_Renderer = GetComponent<MeshRenderer>();
			gameObject.SetActive(false);
		}
		
		// Update is called once per frame
		public void  OnTriggerEnter(Collider colli) 
		{
			if(colli.tag == "Player" && m_TestItem != null)
			{
				DungonManager.Inst.Player.PackageManager.AddItem(m_TestItem);
				gameObject.SetActive(false);
				m_IsPicked = true;

				// 既然当前的ITEM已经传入了背包中，则当前的Item必须重新产生，
				// 否则下一次对该Item的修改,将会影响背包中对应的Item
				m_TestItem = null;
				m_TestItem = new ItemTest();
			}
		}

		/// <summary>
		/// 设置该3D物品的参数
		/// </summary>
		public void SetItem3D(Item item, Vector3 pos)
		{
			transform.position = pos;

			m_TempColor.a = 1.0f;
			m_TestItem = item;
			m_Renderer.material.mainTexture = item.ItemTex;
			m_Renderer.material.SetColor("_Color", m_TempColor);
			m_IsPicked = false;
			gameObject.SetActive(true);
		}
	}
}