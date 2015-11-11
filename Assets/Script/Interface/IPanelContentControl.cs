/// <su0mmary>
/// 类名： IPanelContentControl
/// 用途： 面板内容接口
/// </summary>

using UnityEngine;
using System.Collections;

namespace ActionGame
{
	public interface IPanelContentControl {
		
		void Init();
		void RefreshData();
	}
}