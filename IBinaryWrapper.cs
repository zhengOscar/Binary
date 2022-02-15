/*
 * Copyright (c) 2020
 * All rights reserved.
 *
 * 文件名称：IBinaryWrapper.cs
 * 摘    要：
 *
 * 当前版本：1.0
 * 作    者：Oscar
 * 创建日期：2021/01/27 10:05:00
 */

namespace OEngine.Binary
{
    public interface IBinaryWrapper
    {
        BinaryType DataType { get; set; }

        void SetInt(int val);
        void SetFloat(float val);
        void SetDouble(double val);
        void SetBoolean(bool val);
        void SetString(string val);
    }
}
