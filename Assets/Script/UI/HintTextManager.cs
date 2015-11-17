/// <summary>
/// 类名: HintTextManager
/// 描述: 提示信息管理类
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ActionGame
{
	public class HintTextManager: MonoBehaviour {

		Text m_HintText;
		int m_TweenId;
		Color m_AlphaWhite;
		static HintTextManager _Inst;

		void Awake()
		{
			_Inst = this;
			m_HintText = GetComponentInChildren<Text>();
			m_HintText.gameObject.SetActive(false);
			m_TweenId = -1;
			m_AlphaWhite = Color.white;
			m_AlphaWhite.a = 0.0f;
		}

		public static HintTextManager Inst{
			get{ return _Inst; }
		}

		public void ShowHintText(string content, float time)
		{
			if(m_HintText != null)
			{
				// 若正在播放，则先暂停掉该动画
				if(m_TweenId != -1)
				{
					LeanTween.cancel(m_TweenId);
				}

				// 重置属性，防止上一次动画未播完的情况，属性值不是初始值。
				m_HintText.gameObject.SetActive(true);
				m_HintText.text = content;
				m_HintText.color = m_AlphaWhite;

				// 文字从全透明，变为不透明，再到全透明
				LTDescr desc = LeanTween.textAlpha( m_HintText.rectTransform, 1.0f, time/2).setEase(LeanTweenType.easeOutCubic)
				.setOnComplete(() => {
					LeanTween.textAlpha(m_HintText.rectTransform, 0.0f, time/2).setEase(LeanTweenType.easeInCubic)
					.setOnComplete(() => { 
							m_HintText.gameObject.SetActive(false);
					});
				});

				m_TweenId = desc.id;
			}
		}
	}
}