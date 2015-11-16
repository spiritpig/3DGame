/// <summary>
/// 类名: ItemListManager
/// 描述: 物品列表管理类,相当于一个物品的数据仓库。
/// 储存游戏内，所有可能出现的物品 
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ActionGame
{
	public class ItemListManager {
		int m_CurId = -1;
		bool m_IsConstructDone = false;
		public bool IsConstructDone {
			get { return m_IsConstructDone; }
		}

		Dictionary<int, Item> m_ItemDict;
		public Dictionary<int, Item> ItemDict {
			get {
				return m_ItemDict;
			}
		}

		public void Init()
		{
			m_ItemDict = new Dictionary<int, Item>();
		}

		public void AddItem(Item item)
		{
			m_ItemDict.Add(item.Id, item);
		}

		public Item GetItem(int id)
		{
			return m_ItemDict[id];
		}

		void _ReadItemInfoInner(byte[] result)
		{
			// 读取item条目数
			int itemNum = System.BitConverter.ToInt32(result, 0);
			int offset = sizeof(int);

			byte[] tempBytes;
			int tempInt = 0;
			string tempStr;
			// 将EXCEL表中的数据，存储到二进制文件
			for(int i = 0;  i< itemNum; i++)
			{
				Item item = new ItemTest();
				
				// 读取ID
				m_CurId++;
				item.Id = m_CurId;
				
				// 读取名字
				tempInt = System.BitConverter.ToInt32(result, offset);
				offset += sizeof(int);
				tempStr = System.Text.Encoding.Default.GetString(result, offset, tempInt);
				offset += tempInt;
				item.Name = tempStr;


				// 读取作用值
				tempInt = System.BitConverter.ToInt32(result, offset);
				item.Val = tempInt;
				offset += sizeof(int);

				// 读取价格
				tempInt = System.BitConverter.ToInt32(result, offset);
				item.Price = tempInt;
				offset += sizeof(int);

				// 读取等级
				tempInt = System.BitConverter.ToInt32(result, offset);
				item.Level = tempInt;
				offset += sizeof(int);

				// 读取类型
				tempInt = System.BitConverter.ToInt32(result, offset);
				item.Type = (Item.ITEM_TYPE)tempInt;
				offset += sizeof(int);

				// 读取装备类型
				tempInt = System.BitConverter.ToInt32(result, offset);
				item.EquipType = (Item.EQUIP_TYPE)tempInt;
				offset += sizeof(int);

				// 读取贴图名
				tempInt = System.BitConverter.ToInt32(result, offset);
				offset += sizeof(int);
				tempStr = System.Text.Encoding.Default.GetString(result, offset, tempInt);
				offset += tempInt;
				item.ItemTex = Resources.Load("Texture/" + tempStr) as Texture;

				// 读取品质
				tempInt = System.BitConverter.ToInt32(result, offset);
				item.Quality = (Item.QUALITY)tempInt;
				offset += sizeof(int);

				// 写入描述文字
				tempInt = System.BitConverter.ToInt32(result, offset);
				offset += sizeof(int);
				tempStr = System.Text.Encoding.Default.GetString(result, offset, tempInt);
				offset += tempInt;
				item.Info = tempStr;

				Debug.Log(item.Id + " " + item.Name + " " + item.Val + " " + item.Price + " " + 
				          item.Level + " " + item.Type + " " + item.EquipType + " " +
				          item.ItemTex.name + " " + item.Quality + " " + item.Info);

				AddItem(item);
	        }	
		}

		public IEnumerator ReadItemInfo()
		{
			string path = System.IO.Path.Combine(Application.streamingAssetsPath, "ItemInfo.dat");
			byte[] result;
			if(path.Contains("://"))
			{
				WWW www = new WWW(path);
				yield return www;
				result = www.bytes;
			}
			else
			{
				result = System.IO.File.ReadAllBytes(path);
			}

			_ReadItemInfoInner(result);

			m_IsConstructDone = true;
		}

	}
}