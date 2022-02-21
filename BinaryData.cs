/*
 * Copyright (c) 2020
 * All rights reserved.
 *
 * 文件名称：BinaryData.cs
 * 摘    要：
 *
 * 当前版本：1.0
 * 作    者：Oscar
 * 创建日期：2021/01/27 10:05:00
 */

using System.Collections.Generic;

namespace OEngine.Binary
{
    public class BinaryData: IBinaryWrapper
    {
        protected BinaryType m_DataType;
        public BinaryType DataType
        {
            get { return m_DataType; }
            set { m_DataType = value; }
        }

        protected int m_IntVal;
        protected float m_FloatVal;
        protected double m_DoubleVal;
        protected bool m_BooleanVal;
        protected string m_StringVal;

        protected IDictionary<string, BinaryData> m_ObjectVal;
        protected BinaryData[] m_ArrayVal;

        public void SetInt(int val)
        {
            DataType = BinaryType.Int;
            m_IntVal = val;
        }
        public void SetFloat(float val)
        {
            DataType = BinaryType.Float;
            m_FloatVal = val;
        }
        public void SetDouble(double val)
        {
            DataType = BinaryType.Double;
            m_DoubleVal = val;
        }
        public void SetBoolean(bool val)
        {
            DataType = BinaryType.Boolean;
            m_BooleanVal = val;
        }
        public void SetString(string val)
        {
            DataType = BinaryType.String;
            m_StringVal = val;
        }

        public int GetInt()
        {
            return m_IntVal;
        }
        public float GetFloat()
        {
            return m_FloatVal;
        }
        public double GetDouble()
        {
            return m_DoubleVal;
        }
        public bool GetBoolean()
        {
            return m_BooleanVal;
        }
        public string GetString()
        {
            return m_StringVal;
        }

        public override string ToString()
        {
            switch (DataType)
            {
                case BinaryType.Int:
                    return m_IntVal.ToString();
                case BinaryType.Float:
                    return m_FloatVal.ToString();
                case BinaryType.Double:
                    return m_DoubleVal.ToString();
                case BinaryType.Boolean:
                    return m_BooleanVal.ToString();
                case BinaryType.String:
                    return m_StringVal.ToString();
                case BinaryType.Array:
                    return m_ArrayVal.ToString();
                case BinaryType.Object:
                    return m_ObjectVal.ToString();
            }
            return string.Empty;
        }

        #region 字典/列表

        public void Add(int index,BinaryData data)
        {
            EnsureArray();
            this[index] = data;
        }

        public void Add(string key, BinaryData data)
        {
            EnsureObject();
            if (null != m_ObjectVal)  {
                m_ObjectVal.Add(key, data);
            }
        }

        public bool Contains(string key)
        {
            if(DataType == BinaryType.Object && m_ObjectVal != null)  {
                return m_ObjectVal.ContainsKey(key);
            }
            return false;
        }

        public BinaryData this[int index]
        {
            get
            {
                return m_ArrayVal[index];
            }
            set
            {
                m_ArrayVal[index] = value;
            }
        }
        public BinaryData this[string key]
        {
            get
            {
                return null != m_ObjectVal ? m_ObjectVal[key] :null;
            }
            set
            {
                if(null != m_ObjectVal)
                {
                    m_ObjectVal[key] = value;
                }
            }
        }

        protected void EnsureArray()
        {
            if(DataType== BinaryType.Array && null == m_ArrayVal && Count>0) {
                m_ArrayVal = new BinaryData[Count];
            }
        }

        protected void EnsureObject()
        {
            if (DataType == BinaryType.Object && null == m_ObjectVal && Count>0)
            {
                m_ObjectVal = new Dictionary<string, BinaryData>();
            }
        }

        public int Count;
        #endregion
    }
}
