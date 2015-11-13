/// <summary>
/// 类名: ItemUIControl
/// 描述: 控制背包中物品的拖拽
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace ActionGame
{
	[RequireComponent(typeof(RawImage))]
	public class ItemUIControl : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,
								IDropHandler{
		RawImage m_SelfIcon;
		Texture m_TempTexture = null;
		Vector3 m_TempPos;
		Color m_TempColor;
		RectTransform m_ParentTransform;
		int m_ItemIndexInPackge = -1;			// 物品在背包中的下标
		public RawImage m_DragIcon = null;

		public int ItemIndexInPackge {
			get { return m_ItemIndexInPackge; }
			set { m_ItemIndexInPackge = value; }
		}
		public Texture ItemTexture {
			get { return m_SelfIcon.texture; }
		}

		void Awake ()
		{
			m_SelfIcon = GetComponent<RawImage>();
			m_TempPos = new Vector3();
			m_TempColor = Color.white;
			m_ParentTransform = (transform.parent.parent as RectTransform);
		}

		public void SetItem(Item item, int index)
		{
			m_SelfIcon.texture = item.ItemTex;
			m_ItemIndexInPackge = index;
		}
		
		public void OnBeginDrag(PointerEventData eventData)
		{
			if(m_SelfIcon.texture != null)
			{
				m_TempColor.a = 0.5f;
				m_SelfIcon.color = m_TempColor;

				m_DragIcon.color = Color.white;
				m_DragIcon.texture = m_SelfIcon.texture;

				m_DragIcon.rectTransform.SetAsLastSibling();
			}
		}

		public void OnDrag(PointerEventData eventData)
		{
			if(m_DragIcon.texture != null)
			{
				_SetDragIconPos(eventData);
			}
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			if(m_DragIcon.texture != null)
			{
				// 若结束拖拽时，拖拽图标不在有效范围内，则暂时视为无效拖拽，物品返回原来的位置
				// 实际上，此时应当认为把物品扔到了地上
				if(!RectTransformUtility.RectangleContainsScreenPoint(m_ParentTransform, eventData.position))
				{
					m_DragIcon.texture = null;

					m_TempColor.a = 0.0f;
					m_DragIcon.color = m_TempColor;
					m_SelfIcon.color = Color.white;
				}
			}
		}

		public void OnDrop(PointerEventData eventData)
		{
			if(m_DragIcon.texture != null)
			{
				GameObject dragGo = eventData.pointerDrag;
				_SwapImageIcon(m_SelfIcon, dragGo.GetComponent<RawImage>());

				// 同时将背包中两物品的位置也一并交换
				ItemUIControl itemUI = dragGo.GetComponent<ItemUIControl>();
				DungonManager.Inst.Player.PackageManager.SwapItem(
					m_ItemIndexInPackge, itemUI.ItemIndexInPackge );

				// 拖拽图标恢复原状
				m_DragIcon.texture = null;
				m_TempColor = Color.white;
				m_TempColor.a = 0.0f;
				m_DragIcon.color = m_TempColor;
			}
        }

		public void SetAlpha(float alpha)
		{
			m_TempColor = m_SelfIcon.color;
			m_TempColor.a = alpha;
			m_SelfIcon.color = m_TempColor;
		}
        
		/// <summary>
		/// 辅助函数，设置拖拽图标的位置
		/// </summary>
        void _SetDragIconPos(PointerEventData data)
		{
			m_TempPos.x = data.position.x;
			m_TempPos.y = data.position.y;
			m_TempPos.z = 0;
			m_DragIcon.transform.position = m_TempPos;
		}

		/// <summary>
		/// 辅助函数，交换两个Item的图标
		/// </summary>
		void _SwapImageIcon(RawImage dest, RawImage src)
		{
			m_TempTexture = dest.texture;
			dest.texture = src.texture;
			src.texture = m_TempTexture;

			dest.color = Color.white;
			// 原物品为空，则应该设为透明
			if(src.texture == null)
			{
				m_TempColor.a = 0.0f;
				src.color = m_TempColor;
			}
			// 不为空，则设置为不透明的白色
			else
			{
				src.color = Color.white;
			}
		}

	}
}