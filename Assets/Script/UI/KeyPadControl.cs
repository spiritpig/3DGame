/// <summary>
/// 类名： KeyPadControl
/// 用途： 虚拟键盘
///	保证： 附着的游戏对象具有RectTransform组件和Image组件(UGUI),并且使用UGUI
/// 
/// 补充，此处并没有完美的处理WIndows上和手机上的按键处理，侧重处理手机上的按键。WIndows仅供测试
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace ActionGame
{
	public class KeyPadControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
		public static KeyPadControl Inst = null;
		RectTransform m_Transform;
		Image m_Image;

		// XY坐标轴范围均为 [-0.5F, 0.5F]
		Vector2 m_HalfSize;
		Vector2 m_CurAxis;
		public Vector2 Axis{
			get { return m_CurAxis; }
		}

		Vector2 m_RelatPos, m_TempVec2;
		bool m_IsTouch = false;

		// 键位贴图
		[SerializeField]
		public Sprite m_Up, m_Down, m_Left, m_Right, 
						m_LeftUp, m_LeftDown, m_RightUp, m_RightDown;

		public enum DIR
		{
			D_NONE,
			D_UP,
			D_DOWN,
			D_LEFT,
			D_RIGHT,

			D_LEFTUP,
			D_LEFTDOWN,
			D_RIGHTUP,
			D_RIGHTDOWN
		}
		DIR m_CurDir = DIR.D_NONE;
		public DIR Dir {
			get { return m_CurDir; }
		}

		Sprite m_Normal;

		// Use this for initialization
		void Start () 
		{
			m_RelatPos = new Vector2();
			m_TempVec2 = new Vector2();
			m_Transform = (RectTransform)gameObject.transform;
			m_Image = gameObject.GetComponent<Image>();
			m_Normal = m_Image.sprite;

			// 正确的计算，虚拟键盘所占的大小
			m_HalfSize = m_Transform.rect.size/2;
			m_TempVec2 = gameObject.GetComponentInParent<Transform>().localScale;
			m_HalfSize.x *= m_TempVec2.x;
			m_HalfSize.y *= m_TempVec2.y;

			Inst = this;
		}
		
		// Update is called once per frame
		void Update () 
		{
			// 将轴距清空
			m_CurAxis.Set( 0.0f, 0.0f );

#if UNITY_EDITOR
			// 编辑器内直接使用键盘按键进行移动
			// 求出轴距
			m_CurAxis.x = Input.GetAxis( "Horizontal" ) / 2;
			m_CurAxis.y = Input.GetAxis( "Vertical" ) / 2;
				
			// 根据方向适配精灵
			SetSpriteAndDirByAxis();
#elif UNITY_ANDROID || UNITY_IOS
			// 持续按下
			if(m_IsTouch && Input.touchCount > 0)
			{
				// 求出触屏的相对位置
				m_RelatPos.Set(m_Transform.position.x, m_Transform.position.y);
				m_RelatPos = Input.touches[0].position - m_RelatPos;
				
				// 求出轴距
				m_CurAxis.Set(m_RelatPos.x/m_HalfSize.x, m_RelatPos.y/m_HalfSize.y);

				// 根据方向适配精灵
				SetSpriteAndDirByAxis();
			}
#endif
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			m_IsTouch = true;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			m_Image.sprite = m_Normal;
			m_IsTouch = false;
			m_CurDir = DIR.D_NONE;
		}

		/// <summary>
		/// Sets the sprite by axis.
		/// </summary>
		void SetSpriteAndDirByAxis()
		{
			// 初始状态
			if( m_CurAxis == Vector2.zero )
			{
				m_Image.sprite = m_Normal;
				m_CurDir = DIR.D_NONE;
			}
			// 上下
			else
			if(m_CurAxis.x <= 0.25f && m_CurAxis.x >= -0.25f)
			{
				if( m_CurAxis.y > 0)
				{
					m_Image.sprite = m_Up;
					m_CurDir = DIR.D_UP;
				}
				else
				{
					m_Image.sprite = m_Down;
					m_CurDir = DIR.D_DOWN;
				}
			}
			// 左右
			else
			if(m_CurAxis.y <= 0.25 && m_CurAxis.y >= -0.25f)
			{
				if( m_CurAxis.x > 0 )
				{
					m_Image.sprite = m_Right;
					m_CurDir = DIR.D_RIGHT;
				}
				else
				{
					m_Image.sprite = m_Left;
					m_CurDir = DIR.D_LEFT;
				}
			}
			// 上左右
			else
			if(m_CurAxis.y > 0.25f && m_CurAxis.y <= 0.5f)
			{
				if(m_CurAxis.x>= -0.5f && m_CurAxis.x < -0.25f)
				{
					m_Image.sprite = m_LeftUp;
					m_CurDir = DIR.D_LEFTUP;
				}
				// 上下左右的情况已经被判断完了，此处无需再判断
				else
				{
					m_Image.sprite = m_RightUp;
					m_CurDir = DIR.D_RIGHTUP;
				}
			}
			else
			{
				if(m_CurAxis.x>= -0.5f && m_CurAxis.x < -0.25f)
				{
					m_Image.sprite = m_LeftDown;
					m_CurDir = DIR.D_LEFTDOWN;
				}
				// 上下左右的情况已经被判断完了，此处无需再判断
				else
				{
					m_Image.sprite = m_RightDown;
					m_CurDir = DIR.D_RIGHTDOWN;
				}
			}

		}
	}
}