/// <summary>
/// 类名： DungonItemStarControl
/// 用途： 控制地下城星级选择框的显示
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ActionGame
{
	public class DungonItemStarControl : MonoBehaviour {
		Image m_NormalLevelImg;
		Image m_HardLevelImg;
		Image m_EvilLevelImg;

		void Start () 
		{
			m_NormalLevelImg = transform.FindChild("NormalLevelBtn").GetComponent<Image>();
			m_HardLevelImg = transform.FindChild("HardLevelBtn").GetComponent<Image>();
			m_EvilLevelImg = transform.FindChild("EvilLevelBtn").GetComponent<Image>();

			m_NormalLevelImg.GetComponent<Button>().onClick.AddListener(OnNormalLevelClick);
			m_HardLevelImg.GetComponent<Button>().onClick.AddListener(OnHardLevelClick);
			m_EvilLevelImg.GetComponent<Button>().onClick.AddListener(OnEvilLevelClick);

			Material grayScaleMat = Resources.Load("Material/GrayScaleMat") as Material;
			m_NormalLevelImg.material = Instantiate(grayScaleMat) as Material;
			m_HardLevelImg.material = Instantiate(grayScaleMat) as Material;
			m_EvilLevelImg.material = Instantiate(grayScaleMat) as Material;
		}


		/// <summary>
		/// 三个按钮的点击事件
		/// </summary>
		public void OnNormalLevelClick()
		{
			m_NormalLevelImg.material.SetInt("_IsGrayScale", 0);
			m_HardLevelImg.material.SetInt("_IsGrayScale", 1);
			m_EvilLevelImg.material.SetInt("_IsGrayScale", 1);
		}

		public void OnHardLevelClick()
		{
			m_NormalLevelImg.material.SetInt("_IsGrayScale", 0);
			m_HardLevelImg.material.SetInt("_IsGrayScale", 0);
			m_EvilLevelImg.material.SetInt("_IsGrayScale", 1);
		}

		public void OnEvilLevelClick()
		{
			m_NormalLevelImg.material.SetInt("_IsGrayScale", 0);
			m_HardLevelImg.material.SetInt("_IsGrayScale", 0);
			m_EvilLevelImg.material.SetInt("_IsGrayScale", 0);
		}
	}
}