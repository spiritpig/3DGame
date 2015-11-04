/// <summary>
/// 类名： ShakeCamera
/// 用途： 管理玩家的行为和状态
/// </summary>

using UnityEngine;
using System.Collections;

namespace ActionGame
{
	public class ShakeCamera : MonoBehaviour {

		private float shakeTime = 0.0f;
		private float fps= 60.0f;
		private float frameTime =0.0f;
		private float shakeDelta =0.005f;
		public static bool m_IsShakeCamera =false;
		// Use this for initialization
		void Start ()
		{
			shakeTime = 1.0f;
			fps= 60.0f;
			frameTime =0.03f;
			shakeDelta =0.005f;
			
		}
		
		// Update is called once per frame
		void Update ()
		{
			if (m_IsShakeCamera)
			{
				if(shakeTime > 0)
				{
					shakeTime -= Time.deltaTime;
					if(shakeTime <= 0)
					{
						Camera.main.rect = new Rect(0.0f,0.0f,1.0f,1.0f);
						m_IsShakeCamera =false;
						shakeTime = 1.0f;
						fps= 60.0f;
						frameTime =0.017f;
						shakeDelta =0.005f;
					}
					else
					{
						frameTime += Time.deltaTime;
						
						if(frameTime > 1.0 / fps)
						{
							frameTime = 0;
							Camera.main.rect = new Rect(
								shakeDelta * ( -1.0f + 2.0f * Random.value),
								shakeDelta * ( -1.0f + 2.0f * Random.value), 
								1.0f, 1.0f);
							
						}
					}
				}
			}
			
		}
		
		public static void shakeCamera()
		{
			m_IsShakeCamera =true;
		}
	}
}