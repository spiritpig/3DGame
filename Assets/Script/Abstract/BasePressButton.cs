/// <summary>
/// 类名: BasePressButton
/// 描述: 带有按下事件的按钮基类
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(Button))]
public abstract class BasePressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    bool m_IsPress = false;
    
    void Update()
    {
		if(m_IsPress)
        {
            // 按下时做些什么
            OnPress();
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        m_IsPress = true;
		OnDown();
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        m_IsPress = false;
		OnUp();
    }

    public abstract void OnPress();
	public virtual void OnDown()
	{
	}
	public virtual void OnUp()
	{
	}
}
