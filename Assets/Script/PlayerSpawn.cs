/// <summary>
/// 类名： PlayerSpawn
/// 用途： 负责创建玩家对象
/// </summary>

using UnityEngine;
using System.Collections;

namespace ActionGame
{
	public class PlayerSpawn : MonoBehaviour {

		void Start () {
			GameObject playerPrefab = Resources.Load("Prefab/Player") as GameObject;
			Instantiate( playerPrefab, transform.position, transform.rotation );
		}
		
	}
}