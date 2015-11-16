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
		public const float g_PlayerAtkAnimatTime = 1.0f;
		public const float g_PlayerMagicOneAnimatTime = 1.0f;

		/// <summary>
		/// NPC
		/// </summary>
		public const float g_NpcInteractRange = 5.0f;

		/// <summary>
		/// Scene
		/// </summary>
		public const string g_LoadingScene = "loading";
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
			public int gainExp;			// 死亡后，能给予的EXP
		}

		// 方向
		public enum DIR
		{
			D_NONE,
			D_UP,
			D_DOWN,
			D_LEFT,
			D_RIGHT,
			
			D_LEFTUP,
			D_LEFTDOWN,
			D_RIGHTUP,
			D_RIGHTDOWN
		}

		public class SkillObjData
		{
			public float time;
			public float timeMax;
			public float moveSpeed;
			public float atk;
			public Transform transObj;
			public SkillObj skillObj;

			public SkillObjData()
			{
				time = 0.0f;
				moveSpeed = 10.0f;
				atk = 40.0f;
				transObj = null;
				skillObj = null;
			}
		}

		public static void SwapInt(ref int i, ref int j)
		{
			int temp = i;
			i = j;
			j = temp;
		}
	}
}