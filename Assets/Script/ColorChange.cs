using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ColorChange : MonoBehaviour {

	List<Color> m_ColorTable = new List<Color>();
	float m_CurTime, m_TimeDelta = 0.5f;

	void Start()
	{
		m_ColorTable.Add( Color.black );
		m_ColorTable.Add( Color.blue );
		m_ColorTable.Add( Color.cyan );
		m_ColorTable.Add( Color.gray );
		m_ColorTable.Add( Color.green );
		m_ColorTable.Add( Color.grey );
		m_ColorTable.Add( Color.magenta );
		m_ColorTable.Add( Color.red );
		m_ColorTable.Add( Color.yellow );
	}

	void Update () 
	{
		m_CurTime += Time.deltaTime;

		if( m_CurTime >= m_TimeDelta )
		{
			GetComponent<Text>().color = m_ColorTable[Random.Range(0, m_ColorTable.Count-1)];
			m_CurTime = 0.0f;
		}
	}
}
