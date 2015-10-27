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


		static KeyPadControl g_KeyPad = null;
		/// <summary>
		/// 获取虚拟键盘，不保证返回值的正确性。
		/// 请确保虚拟键盘的返回值不为NULL
		/// </summary>
		public static KeyPadControl GetKeyPad()
		{
			if(g_KeyPad == null)
			{
				g_KeyPad = GameObject.FindGameObjectWithTag( "KeyPad" ).GetComponent<KeyPadControl>();
			}

			return g_KeyPad;
		}
	}
}