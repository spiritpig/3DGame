/// <summary>
/// 类名： CloneSkillBtnMat
/// 用途： 拷贝当前对象的材质，并付给当前对象。 
/// 		主要用于产生一份新的材质拷贝，这样材质的数据就可以实现独立了
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ActionGame
{
	[RequireComponent(typeof(Image))]
	public class CloneSkillBtnMat : MonoBehaviour {

		void Awake () 
		{
			Image img = gameObject.GetComponent<Image>();
			Material mat = Instantiate(img.material) as Material;
			img.material = mat;
		}
		
	}
}