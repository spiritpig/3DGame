﻿/// <summary>
/// 类名： PlayerSpawn
/// 用途： 负责创建玩家对象
/// </summary>

using UnityEngine;
using System.Collections;

namespace ActionGame
{
	public class PlayerSpawn : MonoBehaviour {

		void Awake () {
			GameObject playerPrefab = Resources.Load("Prefab/Player/Player") as GameObject;
			Instantiate( playerPrefab, transform.position, transform.rotation );
		}
		
	}
}