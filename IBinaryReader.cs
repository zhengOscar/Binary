/*
 * Copyright (c) 2020
 * All rights reserved.
 *
 * 文件名称：IBinaryWriter.cs
 * 摘    要：
 *
 * 当前版本：1.0
 * 作    者：Oscar
 * 创建日期：2021/01/27 10:05:00
 */

namespace OEngine.Binary
{
    public interface IBinaryReader
    {
        byte ReadByte();
        byte[] ReadByteArray(int size);
        short ReadShort();
        int ReadInt();
        long ReadLong();
        double ReadDouble();
        string ReadUTF8();
        bool ReadBoolean();
    }
}
