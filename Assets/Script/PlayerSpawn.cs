/// <summary>
/// 类名： PlayerSpawn
/// 用途： 负责创建玩家对象
/// </summary>

using UnityEngine;
using System.Collections;

namespace ActionGame
{
	public class PlayerSpawn : MonoBehaviour {
		public GameObject m_Player;

		// Use this for initialization
		void Start () {
			Instantiate( m_Player, transform.position, transform.rotation );
		}
		
	}
}