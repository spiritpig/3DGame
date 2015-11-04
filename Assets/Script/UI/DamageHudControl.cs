/// <summary>
/// 类名： DamageHudControl
/// 用途： 伤害文字显示控制
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DamageHudControl : MonoBehaviour {
	public class DamageHudData
	{
		public Text text;
		public Transform followObjs;
		public float lifeTime;
		public float offsetY;
	}

	Vector3 m_TempVec3;
	DamageHudData m_TempData = null;

	float m_MaxLifeTime = 1.0f;
	Vector3 m_MoveSpeed = new Vector3(0, 22.0f, 0);
	List<DamageHudData> m_DamageTextList;
	int m_CurUseHudCount;					// 记录当前使用的最后一个Hud的下标

	public void Init () 
	{
		m_TempVec3 = new Vector3();
		m_CurUseHudCount = 0;

		m_DamageTextList = new List<DamageHudData>();
		foreach(Text text in gameObject.GetComponentsInChildren(typeof(Text)))
		{
			DamageHudData data = new DamageHudData(); 
			data.text = text;
			data.lifeTime = 0.0f;
			data.followObjs = null;
			data.offsetY = 0.0f;
			m_DamageTextList.Add(data);

			text.gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		for(int i=0; i<m_CurUseHudCount; )
		{
			m_DamageTextList[i].lifeTime += Time.deltaTime;
			// 每帧都需要重新计算文字的位置
			m_TempVec3 = m_DamageTextList[i].followObjs.position;
			m_TempVec3.y += m_DamageTextList[i].offsetY;
			m_TempVec3 = Camera.main.WorldToScreenPoint(m_TempVec3);
			m_DamageTextList[i].text.transform.position = m_TempVec3 + m_MoveSpeed*m_DamageTextList[i].lifeTime;

			// 使用完毕的HUD和最后一个使用中的HUD交换位置
			// 同时，下标不移动。因为，还需要更新交换过来的HUD
			if(m_DamageTextList[i].lifeTime >= m_MaxLifeTime)
			{
				m_DamageTextList[i].text.gameObject.SetActive(false);
				_SwapHud(i, m_CurUseHudCount-1);
				--m_CurUseHudCount;
			}
			// 若是未产生交换则增加下标
			else
			{
				++i;
			}
		}
	}

	/// <summary>
	/// 使用一个伤害数字HUD
	/// </summary>
	public void UseDamageHud(Transform trans, int val)
	{
		if(trans == null)
		{
			return;
		}

		++m_CurUseHudCount;
		if(m_CurUseHudCount > m_DamageTextList.Count)
		{
			Debug.LogError("伤害HUD不够用了！");
			return;
		}

		m_DamageTextList[m_CurUseHudCount-1].text.text = val.ToString();
		m_DamageTextList[m_CurUseHudCount-1].text.gameObject.SetActive(true);
		m_DamageTextList[m_CurUseHudCount-1].followObjs = trans;
		m_DamageTextList[m_CurUseHudCount-1].lifeTime = 0.0f;

		// 保证伤害数字显示在人物的头顶上
		m_TempVec3 = trans.position;
		m_DamageTextList[m_CurUseHudCount-1].offsetY = trans.gameObject.GetComponent<CharacterController>().bounds.size.y;
		// 转换为屏幕坐标
		m_TempVec3.y += m_DamageTextList[m_CurUseHudCount-1].offsetY;
		m_TempVec3 = Camera.main.WorldToScreenPoint(m_TempVec3);
		m_DamageTextList[m_CurUseHudCount-1].text.transform.position = m_TempVec3;
	}

	/// <summary>
	/// 交换I和J处的元素
	/// </summary>
	void _SwapHud(int i, int j)
	{
		m_TempData = m_DamageTextList[i];
		m_DamageTextList[i] = m_DamageTextList[j];
		m_DamageTextList[j] = m_TempData;
	}
}
