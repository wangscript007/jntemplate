/********************************************************************************
 Copyright (c) jiniannet (http://www.jiniannet.com). All rights reserved.
 Licensed under the MIT license. See licence.txt file in the project root for full license information.
 ********************************************************************************/
using JinianNet.JNTemplate.Resources;
using System;
using System.Collections.Generic;
using System.IO;
#if !NET20
using System.Threading.Tasks;
#endif

namespace JinianNet.JNTemplate.Nodes
{
    /// <summary>
    /// INCLUDE标签
    /// </summary>
    [Serializable]
    public class IncludeTag : ComplexTag
    {
        private ITag path;
        /// <summary>
        /// 模板路径
        /// </summary>
        public ITag Path
        {
            get { return this.path; }
            set { this.path = value; }
        }


        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="path"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected string LoadResource(object path, TemplateContext context)
        {
            if (path != null)
            {
                var paths =
#if NETCOREAPP || NETSTANDARD
                    context.GetResourceDirectories();
#else
                    TemplateContextExtensions.GetResourceDirectories(context);
#endif
                ResourceInfo info = context.Loader.Load(path.ToString(), context.Charset, paths);
                if (info != null)
                {
                    return info.Content;
                }
            }
            return null;
        }

        /// <summary>
        /// 解析标签
        /// </summary>
        /// <param name="context">上下文</param>
        public override object ParseResult(TemplateContext context)
        {
            object path = this.path.ParseResult(context);
            return LoadResource(path, context);
        }

        /// <summary>
        /// 解析标签
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="write">TextWriter</param>
        public override void Parse(TemplateContext context, TextWriter write)
        {
            write.Write(ParseResult(context));
        }

#if NETCOREAPP || NETSTANDARD


        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="path"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected async Task<string> LoadResourceAsync(object path, TemplateContext context)
        {
            if (path != null)
            {
                var paths = context.GetResourceDirectories();
                ResourceInfo info = await context.Loader.LoadAsync(path.ToString(), context.Charset, paths);
                if (info != null)
                {
                    return info.Content;
                }
            }
            return null;
        }
        /// <summary>
        /// 解析标签
        /// </summary>
        /// <param name="context">上下文</param>
        public override async Task<object> ParseResultAsync(TemplateContext context)
        {
            var path = await this.path.ParseResultAsync(context);
            return await LoadResourceAsync(path, context);
        }

        /// <summary>
        /// 解析标签
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="write">TextWriter</param>
        public override async Task ParseAsync(TemplateContext context, TextWriter write)
        {
            var data = await ParseResultAsync(context);
            await write.WriteAsync(data?.ToString());
        }
#endif
    }
}