/********************************************************************************
 Copyright (c) jiniannet (http://www.jiniannet.com). All rights reserved.
 Licensed under the MIT license. See licence.txt file in the project root for full license information.
 ********************************************************************************/
using System;
using System.Collections.Generic;

namespace JinianNet.JNTemplate
{
    /// <summary>
    /// 变量域
    /// </summary>
    public class VariableScope
    {
        private VariableScope parent;
        private IDictionary<string, object> dic;


        /// <summary>
        /// 无参构造函数
        /// </summary>
        public VariableScope()
            : this(null, null)
        {

        }

        /// <summary>
        /// 以字典来初始化对象
        /// </summary>
        /// <param name="dictionary">初始化字典</param>
        public VariableScope(IDictionary<string, object> dictionary)
            : this(null, dictionary)
        {

        }

        /// <summary>
        /// 以父VariableScope与字典来初始化对象
        /// </summary>
        /// <param name="parent">父VariableScope</param>
        /// <param name="dictionary">初始化字典</param>
        public VariableScope(VariableScope parent, IDictionary<string, object> dictionary)
        {
            this.parent = parent;
            if (dictionary == null)
            {
                dictionary = new Dictionary<string, object>(Engine.Runtime.ComparerIgnoreCase);
            }
            this.dic = dictionary;
        }

        /// <summary>
        /// 以父VariableScope来初始化对象
        /// </summary>
        /// <param name="parent">父VariableScope</param>
        public VariableScope(VariableScope parent) :
            this(parent, null)
        {

        }

        /// <summary>
        /// 清空数据
        /// </summary>
        /// <param name="all">是否清空父数据</param>
        public void Clear(bool all)
        {
            this.dic.Clear();
            if (all
                && this.parent != null)
            {
                this.parent.Clear(all);
            }
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        public void Clear()
        {
            Clear(false);
        }

        /// <summary>
        /// 父对象
        /// </summary>
        public VariableScope Parent
        {
            get { return this.parent; }
        }

        /// <summary>
        /// 获取索引值
        /// </summary>
        /// <param name="name">索引名称</param>
        /// <returns>object</returns>
        public object this[string name]
        {
            get
            {
                object val;
                if (this.dic.TryGetValue(name, out val))
                {
                    return val;
                }
                if (this.parent != null)
                {
                    return this.parent[name];
                }
                return null;
            }
            set
            {
                this.dic[name] = value;
            }
        }

        /// <summary>
        /// 为已有键设置新的值(本方法供set标签做特殊处理使用)
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        internal bool SetValue(string key, object value)
        {

            if (this.dic.ContainsKey(key))
            {
                this[key] = value;
                return true;
            }
            if (this.parent != null)
            {
                return this.parent.SetValue(key, value);
            }
            return false;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void Push(string key, object value)
        {
            this.dic.Add(key, value);
        }

        /// <summary>
        /// 是否包含指定键
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>bool</returns>
        public bool ContainsKey(string key)
        {
            if (this.dic.ContainsKey(key))
            {
                return true;
            }
            if (this.parent != null)
            {
                return this.parent.ContainsKey(key);
            }

            return false;
        }

        /// <summary>
        /// 移除指定对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns>是否移除成功</returns>
        public bool Remove(string key)
        {
            return this.dic.Remove(key);
        }

    }
}