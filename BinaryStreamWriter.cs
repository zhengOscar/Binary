/*
 * Copyright (c) 2020
 * All rights reserved.
 *
 * 文件名称：BinaryStreamWriter.cs
 * 摘    要：
 *
 * 当前版本：1.0
 * 作    者：Oscar
 * 创建日期：2021/01/27 10:05:00
 */

namespace OEngine.Binary
{
    public class BinaryStreamWriter : BinaryStream
    {
        System.IO.BinaryWriter m_WriteBuffer;

        public BinaryStreamWriter() : base()
        {
            m_WriteBuffer = new System.IO.BinaryWriter(m_Stream, System.Text.Encoding.UTF8);
        }

        ~BinaryStreamWriter()
        {
            Release();
        }

        public void WriteByte(byte value)
        {
            m_WriteBuffer.Write(value);
        }

        public void WriteByteArray(byte[] value)
        {
            m_WriteBuffer.Write(value);
        }

        public void WriteArray<T>(T[] values, System.Action<T> handler)
        {
            short num = (short)(null != values ? values.Length : 0);
            WriteShort(num);
            if (null != handler)
            {
                for (int i = 0; i < num; i++)
                {
                    handler(values[i]);
                }
            }
        }

        public void WriteShort(short value)
        {
            m_WriteBuffer.Write(value);
        }

        public void WriteInt(int value)
        {
            m_WriteBuffer.Write(value);
        }

        public void WriteLong(long value)
        {
            m_WriteBuffer.Write(value);
        }

        public void WriteDouble(double value)
        {
            long netValue = System.BitConverter.DoubleToInt64Bits(value);
            m_WriteBuffer.Write(netValue);
        }

        public void WriteUTF8(string value)
        {
            if (string.IsNullOrEmpty(value)) { value = string.Empty; }
            this.WriteShort((short)System.Text.Encoding.UTF8.GetBytes(value).Length);
            m_WriteBuffer.Write(value.ToCharArray());
        }

        public void WriteFloat(float value)
        {
            m_WriteBuffer.Write((double)value);
        }

        public override void Release()
        {
            if (null != m_WriteBuffer)
            {
                m_WriteBuffer.Close();
                m_WriteBuffer.Dispose();
            }
            base.Release();
        }
    }
}
