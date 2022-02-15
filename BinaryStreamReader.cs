/*
 * Copyright (c) 2020
 * All rights reserved.
 *
 * 文件名称：BinaryStreamReader.cs
 * 摘    要：
 *
 * 当前版本：1.0
 * 作    者：Oscar
 * 创建日期：2021/01/27 10:05:00
 */
namespace OEngine.Binary
{
    public class BinaryStreamReader : BinaryStream
    {
        System.IO.BinaryReader m_ReadBuffer;

        public BinaryStreamReader(byte[] value) : base(value)
        {
            m_ReadBuffer = new System.IO.BinaryReader(m_Stream);
        }

        ~BinaryStreamReader()
        {
            Release();
        }

        public byte ReadByte()
        {
            return m_ReadBuffer.ReadByte();
        }

        public byte[] ReadByteArray(int size)
        {
            return size == 0 ? null : m_ReadBuffer.ReadBytes(size);
        }

        public short ReadShort()
        {
            return m_ReadBuffer.ReadInt16();
        }

        public int ReadInt()
        {
            return m_ReadBuffer.ReadInt32();
        }

        public long ReadLong()
        {
            return m_ReadBuffer.ReadInt64();
        }

        public double ReadDouble()
        {
            long value = m_ReadBuffer.ReadInt64();
            return System.BitConverter.Int64BitsToDouble(value);
        }

        public string ReadUTF8()
        {
            short size = this.ReadShort();
            return size == 0 ? "" : System.Text.Encoding.UTF8.GetString(this.ReadByteArray(size));
        }

        public bool ReadBoolean()
        {
            return m_ReadBuffer.ReadBoolean();
        }
        public float ReadFloat()
        {
            return (float)m_ReadBuffer.ReadDouble();
        }

        public override void Release()
        {
            if(null != m_ReadBuffer) {
                m_ReadBuffer.Close();
                m_ReadBuffer.Dispose();
            }
            base.Release();
        }
    }
}
