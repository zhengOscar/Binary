/*
 * Copyright (c) 2020
 * All rights reserved.
 *
 * 文件名称：BinaryStream.cs
 * 摘    要：
 *
 * 当前版本：1.0
 * 作    者：Oscar
 * 创建日期：2021/01/27 10:05:00
 */

namespace OEngine.Binary
{
    public class BinaryStream
    {
        protected System.IO.MemoryStream m_Stream;

        public BinaryStream()
        {
            m_Stream = new System.IO.MemoryStream();
        }

        public BinaryStream(byte[] value)
        {
            m_Stream = new System.IO.MemoryStream(value, false);
        }


        ~BinaryStream()
        {
            Release();
        }


        public long GetFilePointer()
        {
            return m_Stream.Position;
        }

        public long GetFileLength()
        {
            return m_Stream.Length;
        }

        public bool IsEnd()
        {
            return m_Stream.Position == m_Stream.Length;
        }

        public virtual void Release()
        {
            if (null != m_Stream)
            {
                m_Stream.Close();
                m_Stream.Dispose();
            }
        }
    }
}
