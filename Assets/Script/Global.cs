/// <summary>
/// 类名： Global
/// 用途： 管理全局的数据和函数
/// </summary>

using UnityEngine;

namespace ActionGame
{
	public class Global
	{
		/// <summary>
		/// Camera
		/// </summary>
		public const float g_CameraDistance = 10.0f;

		/// <summary>
		/// Player
		/// </summary>
		public const float g_PlayerMoveSpeed = 6.0f;
		// Animation
		public const float g_PlayerIdleAnimatTime = 1.0f;
		public const float g_PlayerRunAnimatTime = 1.0f;

		/// <summary>
		/// Scene
		/// </summary>
		public const string g_MainScene = "playing";

		/// <summary>
		/// 共享数据结构
		/// </summary>
		public struct Attribute
		{
			public string charName;
			public float hp;			// 生命值
			public float maxHp;			// 生命值
			public float eng;			// 能量
			public float maxEng;		// 生命值
			public float atkPhy;		// 物理攻击
			public float atkMag;		// 魔法攻击
			public float defPhy;		// 物理防御
			public float defMag;		// 魔法防御
			public float movSp;			// 移动速度
			public float atkSp;			// 攻击速度
			public float atkRange;		// 攻击范围
		}
	}
}