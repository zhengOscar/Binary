/*
 * Copyright (c) 2020
 * All rights reserved.
 *
 * 文件名称：BinaryConfigManager.cs
 * 摘    要：
 *
 * 当前版本：1.0
 * 作    者：Oscar
 * 创建日期：2021/01/27 10:05:00
 */

//#define BinaryConfigManager_Test

using System.Collections.Generic;
using System;
using LitJson;
using UnityEngine;
using System.IO;

namespace OEngine.Binary
{
    /// <summary> 
    /// 
    /// </summary>
    public class BinaryConfigManager
    {

        public void Test()
        {
            JsonData data = GetJsonData("welfare_test");
            IDictionary<string, JsonData> dic = data.ValueAsObject();
            
            IBinaryWriter writer = new BinaryStreamLittleWriter();
            writer.WriteByte((byte)BinaryType.Object);
            //writer.WriteByte((byte)BinaryType.Int);
            writer.WriteShort((short)dic.Count);

            foreach (var item in dic)
            {
                short len = 0;
                writer.WriteUTF8(item.Key);

                writer.WriteByte((byte)BinaryType.Array);
                writer.WriteShort(5);

                writer.WriteByte((byte)BinaryType.Int);
                writer.WriteInt(item.Value["nth"].ValueAsInt());
                writer.WriteByte((byte)BinaryType.Int);
                writer.WriteInt(item.Value.ContainsKey("moduleId")? item.Value["moduleId"].ValueAsInt():0);
                writer.WriteByte((byte)BinaryType.String);
                writer.WriteUTF8(item.Value["title"].ValueAsString());

                writer.WriteByte((byte)BinaryType.Array);
                //writer.WriteByte((byte)BinaryType.Int);
                IList<JsonData> list = item.Value.ContainsKey("awardNameAssetItems") ? item.Value["awardNameAssetItems"].ValueAsArray():null;
                len = list != null ? (short)list.Count : (short)0;
                writer.WriteShort(len);
                for(int i = 0; i < len; i++)
                {
                    writer.WriteByte((byte)BinaryType.Array);
                    writer.WriteShort(4);

                    writer.WriteByte((byte)BinaryType.Int);
                    writer.WriteInt(list[i]["assetType"].ValueAsInt());
                    writer.WriteByte((byte)BinaryType.Int);
                    writer.WriteInt(list[i]["awardID"].ValueAsInt());
                    writer.WriteByte((byte)BinaryType.Int);
                    writer.WriteInt(list[i]["awardQuota"].ValueAsInt());
                    writer.WriteByte((byte)BinaryType.Int);
                    writer.WriteInt(list[i]["rate"].ValueAsInt());
                }

                writer.WriteByte((byte)BinaryType.Array);
                //writer.WriteByte((byte)BinaryType.Int);
                list = item.Value["condDefines"].ValueAsArray();
                len = list != null ? (short)list.Count : (short)0;
                writer.WriteShort(len);
                for (int i = 0; i < len; i++)
                {
                    writer.WriteByte((byte)BinaryType.Array);
                    writer.WriteShort(4);

                    writer.WriteByte((byte)BinaryType.Int);
                    writer.WriteInt(list[i]["condId"].ValueAsInt());
                    writer.WriteByte((byte)BinaryType.Int);
                    writer.WriteInt(list[i]["type"].ValueAsInt());
                    writer.WriteByte((byte)BinaryType.Int);
                    writer.WriteInt(list[i].ContainsKey("quota") ? list[i]["quota"].ValueAsInt():0);
                    writer.WriteByte((byte)BinaryType.String);
                    writer.WriteUTF8(list[i]["desc"].ValueAsString());
                }
            }

            File.WriteAllBytes(Application.dataPath + "/Art/Config/welfare_test.bin", writer.GetBuffer());
            writer = null;
        }

        static JsonData GetJsonData(string name)
        {
            string path = Application.dataPath + "/Art/Config/" + name + ".json";
            return GetJsonDataByPath(path);
        }
        static JsonData GetJsonDataByPath(string path)
        {
            string data = File.ReadAllText(path);
            return JsonMapper.ToObject(data);
        }

        public void Release()
        {

        }

        public static BinaryConfigManager GetInstance()
        {
            return Nested.Instance;
        }

        private BinaryConfigManager()
        {

        }

        /// <summary>
        /// 线程安全，延迟初始化
        /// </summary>
        private class Nested
        {
            static Nested()
            {
            }
            internal static readonly BinaryConfigManager Instance = new BinaryConfigManager();
        }
    }

}