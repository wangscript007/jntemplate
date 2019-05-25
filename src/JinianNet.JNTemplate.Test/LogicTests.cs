﻿using JinianNet.JNTemplate;
using System;
using System.Collections.Generic;
using Xunit;

namespace JinianNet.JNTemplate.Test
{
    /// <summary>
    /// 逻辑测试
    /// </summary>
    public partial class TagsTests : TagsTestBase
    {
        /// <summary>
        /// 测试For标签里面的逻辑运算
        /// </summary>
        [Fact]
        public void TestForAndTag()
        {
            var templateContent = @"${foreach(entity in list)}${ if (entity.Name.Length > 4 && entity.Name.Substring(0, 4) == ""File"")}yes${else}no${end}${end}";
            var template = Engine.CreateTemplate(templateContent);
            var arr = new List<object>();
            arr.Add(new
            {
                Name = "File20190128"
            });
            arr.Add(new
            {
                Name = "19"
            });
            template.Context.TempData["list"] = (arr);
            var render = Excute(template);
            Assert.Equal("yesno", render);
        }

        /// <summary>
        /// 测试AND 在条件1不满足的条件下不应该执行条件2
        /// </summary>
        [Fact]
        public void TestAndTag()
        {
            var templateContent = @"${ if (entity.Name.Length > 4 && entity.Name.Substring(0, 4) == ""File"" &&  entity.Name.EndsWith(""19""))}yes${else}no${end}";
            var template = Engine.CreateTemplate(templateContent);
            template.Context.TempData["entity"] = (new
            {
                Name = "File19"
            });
            var render = Excute(template);
            Assert.Equal("yes", render);
        }


        /// <summary>
        /// 测试逻辑优先级处理
        /// </summary>
        [Fact]
        public void TestLogicPriority()
        {
            var templateContent = @"${if(3+5>20 && 1-2<7 && 1<2) }yes${else}no${end}";
            var template = Engine.CreateTemplate(templateContent);
            var render = this.Excute(template);
            Assert.Equal("no", render);
        }


        /// <summary>
        /// 测试逻辑表达式(逻辑运算符包括 ==,!=,<,>,>=,<=,||,&&)
        /// </summary>
        [Fact]
        public void TestLogicExpression()
        {
            var templateContent = "${4<=5}";
            var template = Engine.CreateTemplate(templateContent);
            var render = Excute(template);

            Assert.Equal("True", render);
        }

        /// <summary>
        /// 测试逻辑或表达式 
        /// </summary>
        [Fact]
        public void TestLogicOrExpression()
        {
            var templateContent = "${4==5||5==5}";//如果是复杂的表达式，应优先考虑使用括号来表达优先级，比如 ${(4==5)||(5==5)} 比简单使用  ${4==5||5==5} 更好
            var template = Engine.CreateTemplate(templateContent);
            var render = Excute(template);

            Assert.Equal("True", render);
        }

        /// <summary>
        /// 测试逻辑与表达式
        /// </summary>
        [Fact]
        public void TestLogicAndExpression()
        {
            var templateContent = "${4==5&&5==5}";
            var template = Engine.CreateTemplate(templateContent);
            var render = Excute(template);

            Assert.Equal("False", render);
        }

        /// <summary>
        /// 测试IF
        /// </summary>
        [Fact]
        public void TestIf()
        {
            var templateContent = "${if(3==8)}3=8 success${elseif(3>8)}3>8 success${elseif(2<5)}2<5 success${else}null${end}";
            var template = Engine.CreateTemplate(templateContent);

            var render = Excute(template);

            Assert.Equal("2<5 success", render);
        }

        /// <summary>
        /// 对象方法与逻辑运算混合判断
        /// </summary>
        [Fact]
        public void TestAdmixtureIf()
        {
            var templateContent = "$if(CreteDate >= date.AddDays(-3))yes$end"; //数组取值用get即可取到 List<Int32>用get_Item  见.NET的索引实现原理
            var template = Engine.CreateTemplate(templateContent);
            template.Context.TempData["CreteDate"] = (DateTime.Now);
            template.Context.TempData["date"] = (DateTime.Now);
            var render = Excute(template);
            Assert.Equal("yes", render);
        }

        /// <summary>
        /// 简单对象NULL判断
        /// </summary>
        [Fact]
        public void TestObjectIsNull()
        {
            var templateContent = "${if(dd)}yes${else}no$end";
            var template = Engine.CreateTemplate(templateContent);
            template.Context.TempData["dd"] = (new object());
            var render = Excute(template);
            Assert.Equal("yes", render);
        }

        /// <summary>
        /// 算术与算术混合判断
        /// </summary>
        [Fact]
        public void TestArithmeticAndArithmetic()
        {
            var templateContent = "$if(3>2 && 5<2)yes${else}no${end}";
            var template = Engine.CreateTemplate(templateContent); ;
            var render = Excute(template);
            Assert.Equal("no", render);
        }

        /// <summary>
        /// 对象与算术混合判断
        /// </summary>
        [Fact]
        public void TestObjectAndArithmetic()
        {
            //v1 为空 false,5<2为false，整体结果 false || false 为false
            var templateContent = "$if(v1 || 5<2)yes${else}no${end}";
            var template = Engine.CreateTemplate(templateContent); ;
            var render = Excute(template);
            Assert.Equal("no", render);
        }

        /// <summary>
        /// 对象判断测试
        /// </summary>
        [Fact]
        public void TestIfObject()
        {
            //单个对象的判断原则类同于JS
            //即数字0为FALSE，否则为TRUE
            //字符串空或者NULL为FALSE，否则为TRUE
            //对象在为NULL时为FALSE，否则为TRUE
            //v1 为空 false,v2等于9，数字不等于0即为true,整体结果 false || true 为true
            var templateContent = "$if(v1 || v2)yes${else}no${end}";
            var template = Engine.CreateTemplate(templateContent);
            template.Context.TempData["v2"] = (9);
            var render = Excute(template);
            Assert.Equal("yes", render);
        }

        /// <summary>
        /// 测试elif
        /// </summary>
        [Fact]
        public void TestElif()
        {
            var templateContent = "${if(3>5)}3>5${elif(2==2)}2=2${else}not${end}";
            var template = Engine.CreateTemplate(templateContent);
            template.Context.TempData["list"] = (new int[] { 7, 0, 2, 0, 6 });
            var render = Excute(template);
            Assert.Equal("2=2", render);
        }
    }
}
