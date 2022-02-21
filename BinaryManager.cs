/*
 * Copyright (c) 2020
 * All rights reserved.
 *
 * 文件名称：BinaryManager.cs
 * 摘    要：
 *
 * 当前版本：1.0
 * 作    者：Oscar
 * 创建日期：2021/01/27 10:05:00
 */

//#define BinaryManager_Test

using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

namespace OEngine.Binary { 
    /// <summary> 
    /// 
    /// </summary>
    public class BinaryManager
    {

        protected BinaryData Content;
        public void Test()
        {
            byte[] bytes = File.ReadAllBytes(Application.dataPath + "/Art/Config/welfare_test.bin");
            Content = ToObject(bytes);
        }

        public BinaryData ToObject(byte[] bytes)
        {
            BinaryStreamReader reader = new BinaryStreamReader(bytes);
            BinaryData data = Read(reader);
            reader.Release();
            reader = null;
            return data;
        }

        public BinaryData Read(BinaryStreamReader reader)
        {
            BinaryType dataType = (BinaryType)reader.ReadByte();
            int count = 0;

            if(dataType == BinaryType.Null) {
                return null;
            }
            if(dataType == BinaryType.Object || dataType == BinaryType.Array) {
                count = reader.ReadShort();
                if(count ==0) { return null;  }
            }

            BinaryData data = new BinaryData();
            
            switch (dataType)
            {
                case BinaryType.Object:
                    data.DataType = dataType;
                    data.Count = count;
                    for (int i = 0; i < count; i++)
                    {
                        data.Add(reader.ReadUTF8(), Read(reader));
                    }
                    break;
                case BinaryType.Array:
                    data.DataType = dataType;
                    data.Count = count;
                    for (int i = 0; i < count; i++) {
                        data.Add(i, Read(reader));
                    }
                    break;
                case BinaryType.String:
                    data.SetString(reader.ReadUTF8());
                    break;
                case BinaryType.Int:
                    data.SetInt(reader.ReadInt());
                    break;
                case BinaryType.Double:
                    data.SetDouble(reader.ReadDouble());
                    break;
                case BinaryType.Boolean:
                    data.SetBoolean(reader.ReadBoolean());
                    break;
                default:
                    break;
            }
            return data;
        }

        public void Release()
        {

        }

        public static BinaryManager GetInstance()
        {
            return Nested.Instance;
        }

        private BinaryManager()
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
            internal static readonly BinaryManager Instance = new BinaryManager();
        }
    }
}