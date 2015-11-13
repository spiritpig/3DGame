/// <summary>
/// 类名: ItemTest
/// 描述: 测试物品类，用来把流程走通
/// </summary>
	
using UnityEngine;
using System.Collections;

namespace ActionGame
{
	public class ItemTest : Item {

		/// <summary>
		/// 处理被拾取的情况
		/// </summary>
		public override void OnPick()
		{
		}
		
		/// <summary>
		/// 处理装备掉落
		/// </summary>
		public override void OnGroundDrop()
		{
		}
	}
}