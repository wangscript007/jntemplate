﻿/********************************************************************************
 Copyright (c) jiniannet (http://www.jiniannet.com). All rights reserved.
 Licensed under the MIT license. See licence.txt file in the project root for full license information.
 ********************************************************************************/
using System;
using System.Collections.Generic;

namespace JinianNet.JNTemplate.Nodes
{
    /// <summary>
    /// 简单JSON标签
    /// 注意：该标签仅只支持简单的JSON解析
    /// </summary>
    public class JsonTag : TagBase
    {
        /// <summary>
        /// 集合
        /// </summary>
        public Dictionary<Tag, Tag> Dict { get; private set; } = new Dictionary<Tag, Tag>();

        /// <summary>
        ///  解析JSON
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override object Parse(TemplateContext context)
        {
            var result = new Dictionary<object, object>();
            foreach (var kv in Dict)
            {
                var key = kv.Key == null ? null : kv.Key.Parse(context);
                var value = kv.Value == null ? null : kv.Value.Parse(context);
                result.Add(key, value);
            }
            return result;
        }
    }
}