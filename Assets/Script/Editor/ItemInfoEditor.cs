/// <summary>
/// 类名: ItemInfoEditorWindow
/// 描述: 物品数值编辑器
/// </summary>

using UnityEngine;
using UnityEditor;
using System.Collections;

public class ItemInfoEditorWindow : EditorWindow 
{
	string id, name, price, type;

	[MenuItem ("Window/Item Info Eidtor")]
	public static void ShowWindow () 
	{
		EditorWindow.GetWindow(typeof(ItemInfoEditorWindow));
	}

	void OnGUI()
	{
		// 数据项
		id = EditorGUILayout.TextField("ID", id);
		name = EditorGUILayout.TextField("名字", name);
		price = EditorGUILayout.TextField("价格", price);
		type = EditorGUILayout.TextField("类型", type);
	}
}
