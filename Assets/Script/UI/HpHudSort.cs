using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HpHudSort : MonoBehaviour {

	List<Transform> m_ChildList;
	// Use this for initialization
	void Start () 
	{
		m_ChildList = new List<Transform>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_ChildList.Clear();
		for( int i=0; i<transform.childCount; ++i)
		{
			m_ChildList.Add(transform.GetChild(i));
		}

		m_ChildList.Sort((a,b) =>{
			return (int)((b.transform.position.z - a.transform.position.z)*100.0f); 
		});

		for( int i=0; i<m_ChildList.Count; ++i )
		{
			m_ChildList[i].SetSiblingIndex(i);
		}
	}
}
