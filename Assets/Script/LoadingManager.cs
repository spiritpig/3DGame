/// <summary>
/// 类名: LoadingControl
/// 描述: Loading界面，控制对下一个场景的加载
/// </summary>

using UnityEngine;
using System.Collections;

namespace ActionGame
{
	public class LoadingManager : MonoBehaviour {
		AsyncOperation m_Async;
		ValBarControl m_BarControl;

		// Use this for initialization
		void Start () 
		{
			m_BarControl = gameObject.GetComponent<ValBarControl>();
			StartCoroutine("LoadScene");
		}
		
		// Update is called once per frame
		void Update () 
		{
		}

		// 滚动流畅的版本，但是，可能会拖慢加载时间
		IEnumerator LoadScene()
		{
			float curProcess = 0.0f;

			m_Async = Application.LoadLevelAsync(Global.g_MainScene);
			m_Async.allowSceneActivation = false;

			while(m_Async.progress < 0.9f &&
			      curProcess <m_Async.progress)
			{
				curProcess += 0.01f;
				// 显示进度小于当前进度时，不断地累加
				m_BarControl.OnValueChange(curProcess, "");

				yield return new WaitForEndOfFrame();
			}

			while(curProcess < 1.0f)
			{
				curProcess += 0.01f;
				m_BarControl.OnValueChange(curProcess, "");

				yield return new WaitForEndOfFrame();
			}
			m_Async.allowSceneActivation = true;
			
		}

		/*
		 * 更科学的进度条滚动版本，不拖慢加载时间
		IEnumerator LoadScene()
		{
			m_Async = Application.LoadLevelAsync(Global.g_MainScene);
			m_Async.allowSceneActivation = false;
			
			while(m_Async.progress < 0.9f)
			{
				// 显示进度小于当前进度时，不断地累加
				m_BarControl.OnValueChange(m_Async.progress, "");
				
				yield return new WaitForEndOfFrame();
			}
			
            m_Async.allowSceneActivation = true;			
            m_BarControl.OnValueChange(1.0f, "");
            yield return new WaitForEndOfFrame();
		}
		*/
	}
}