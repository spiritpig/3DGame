﻿using UnityEngine;
using System.Collections;

namespace ActionGame
{
	public class ThirdPersonCamera : MonoBehaviour {
		Transform m_Player;
		Camera m_Camera;
		Vector3 m_TempVec3, m_BaseRotate = new Vector3( 0.0f, 0.0f, -1.0f );
		public float m_Distance = 3.0f;

		public static bool m_IsShake = false;
		int m_ShakeTimes = 8;
		float m_Seg = 1.0f;
		Vector3 m_ShakeDelta = new Vector3(0.0f, 0.2f, 0.0f);

		// Use this for initialization
		void Awake () 
		{
			m_Player = GameObject.FindGameObjectWithTag( "Player" ).transform;
			m_Camera = Camera.main;
			m_Distance = Global.g_CameraDistance;

			m_TempVec3 = new Vector3( 0.0f, 0.0f, 0.0f );

			// 预计算一遍，摄像机的参数，确定其角度。
			// 由于是固定视角，之后都不会更改
			m_TempVec3 = m_BaseRotate*m_Distance;
			m_TempVec3.y = m_Distance;

			// 摄像机位置
			m_Camera.transform.position = new Vector3(
				m_Player.position.x - m_TempVec3.x,
				m_Player.position.y + m_TempVec3.y,
				m_Player.position.z - m_TempVec3.z
				);
			
			// 摄像机朝向
			m_Camera.transform.LookAt( m_Player.transform.position );
		}
		
		// Update is called once per frame
		void Update () 
		{
			if(m_IsShake)
			{
				m_Camera.transform.position += m_ShakeDelta*m_Seg;
				m_Seg = -m_Seg;

				--m_ShakeTimes;
				if(m_ShakeTimes <= 0)
				{
					m_ShakeTimes = 8;
					m_IsShake = false;
				}
			}
			else
			{
				// 计算当前 朝向向量
				m_TempVec3 = m_BaseRotate*m_Distance;
				m_TempVec3.y = m_Distance;
				
				// 摄像机位置
				m_Camera.transform.position = new Vector3(
					m_Player.position.x - m_TempVec3.x,
                    m_Player.position.y + m_TempVec3.y,
                    m_Player.position.z - m_TempVec3.z
                    );
			}
		}

		public static void OnShakeCamera()
		{
			m_IsShake = true;
		}

	}
}
