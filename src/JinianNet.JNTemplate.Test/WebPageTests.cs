﻿//#define EQVER
//#define TESTNV
#if WIN
using System.IO;
using System.Reflection;
using System;
using JinianNet.JNTemplate.Test.Model;
using System.Diagnostics;


namespace JinianNet.JNTemplate.Test
{
    /// <summary>
    /// 实际WEB页面模板测试
    /// </summary>
    [TestClass]
    public class WebPageTests
    {
        [TestMethod]
        public void TestILVsReflectionPage()
        {

            JinianNet.JNTemplate.TemplateContext ctx = new JinianNet.JNTemplate.TemplateContext();
            ctx.TempData.Push("func", new TemplateMethod());
            SiteInfo site = new SiteInfo();
            site.Copyright = "&copy;2014 - 2015";
            site.Description = "";
            site.Host = "localhost";
            site.KeyWords = "";
            site.Logo = "";
            site.Name = "xxx";
            site.SiteDirectory = "";
            site.Theme = "Blue";
            site.ThemeDirectory = "theme";
            site.Title = "jntemplate测试页";
            site.Url = string.Concat("http://localhost");

            if (!string.IsNullOrEmpty(site.SiteDirectory) && site.SiteDirectory != "/")
            {
                site.Url += "/" + site.SiteDirectory;
            }
            site.ThemeUrl = string.Concat(site.Url, "/", site.ThemeDirectory, "/", site.Theme);
            //ctx.TempData.Push("Model", );
            ctx.TempData.Push("Site", site);

            string basePath = new System.IO.DirectoryInfo(System.Environment.CurrentDirectory).Parent.Parent.FullName;
            string path = basePath + "\\templets\\default";
           // JinianNet.JNTemplate.Dynamic.IDynamicHelpers h;
            Configuration.EngineConfig conf;
 
            string text1 = null, text2 = null;
            string result="";
            Stopwatch s = new Stopwatch();
            s.Start();
            s.Stop();
            ////////////////////////////////////////////////////////////////////////////////////
            //h = new JinianNet.JNTemplate.Dynamic.ILHelpers();
            conf = Configuration.EngineConfig.CreateDefault();
            //conf.CachingProvider = "JinianNet.JNTemplate.Test.UserCache,JinianNet.JNTemplate.Test";
            conf.CachingProvider = "JinianNet.JNTemplate.Caching.MemoryCache";
            Engine.Configure(conf);
            s.Restart();

            for (var i = 0; i < 1000; i++)
            {
                JinianNet.JNTemplate.Template t = new JinianNet.JNTemplate.Template(ctx, System.IO.File.ReadAllText(path + "\\questionlist.html"));

                t.Context.CurrentPath = path;
                text1 = t.Render();

            }
            s.Stop();
            result += "IL　１０００次运行 耗时　：" + s.ElapsedMilliseconds + "毫秒";
            ////////////////////////////////////////////////////////////////////////////////////

            GC.Collect();
 

            ////////////////////////////////////////////////////////////////////////////////////
            //h = new JinianNet.JNTemplate.Dynamic.ReflectionHelpers();
            conf = Configuration.EngineConfig.CreateDefault();
            conf.CachingProvider = null;
            Engine.Configure(conf);
            s.Restart();
            for (var i = 0; i < 1000; i++)
            {
                JinianNet.JNTemplate.Template t = new JinianNet.JNTemplate.Template(ctx, System.IO.File.ReadAllText(path + "\\questionlist.html"));
       
                t.Context.CurrentPath = path;
                text2 = t.Render();
                //h.ExcuteMethod(DateTime.Now, "AddDays", new object[] { 30 });
            }
            s.Stop();
            result += ": Reflection　１０００次运行 耗时　：" + s.ElapsedMilliseconds + "毫秒";
            ////////////////////////////////////////////////////////////////////////////////////


            System.IO.File.WriteAllText(basePath + "\\html\\ILVsReflection.txt", result);
            System.IO.File.WriteAllText(basePath + "\\html\\ILVsReflection1.txt", text1);
            System.IO.File.WriteAllText(basePath + "\\html\\ILVsReflection2.txt", text2);
            Assert.AreEqual(text1, text2);

        }


        [Test]
        public void TestILage()
        {

            JinianNet.JNTemplate.TemplateContext ctx = new JinianNet.JNTemplate.TemplateContext();
            ctx.TempData.Push("func", new TemplateMethod());
            SiteInfo site = new SiteInfo();
            site.Copyright = "&copy;2014 - 2015";
            site.Description = "";
            site.Host = "localhost";
            site.KeyWords = "";
            site.Logo = "";
            site.Name = "xxx";
            site.SiteDirectory = "";
            site.Theme = "Blue";
            site.ThemeDirectory = "theme";
            site.Title = "jntemplate测试页";
            site.Url = string.Concat("http://localhost");

            if (!string.IsNullOrEmpty(site.SiteDirectory) && site.SiteDirectory != "/")
            {
                site.Url += "/" + site.SiteDirectory;
            }
            site.ThemeUrl = string.Concat(site.Url, "/", site.ThemeDirectory, "/", site.Theme);
            //ctx.TempData.Push("Model", );
            ctx.TempData.Push("Site", site);

            string basePath = new System.IO.DirectoryInfo(System.Environment.CurrentDirectory).Parent.Parent.FullName;
            string path = basePath + "\\templets\\default";

            var conf = Configuration.EngineConfig.CreateDefault();
            conf.CachingProvider = "JinianNet.JNTemplate.Caching.MemoryCache";
            Engine.Configure(conf);

            for (var i = 0; i < 20000; i++)
            {
                JinianNet.JNTemplate.Template t = new JinianNet.JNTemplate.Template(ctx, System.IO.File.ReadAllText(path + "\\questionlist.html"));
                t.Context.CurrentPath = path;
                t.Render();

            }

        }
           [TestMethod]
        public void TestReflectionPage()
        {

            JinianNet.JNTemplate.TemplateContext ctx = new JinianNet.JNTemplate.TemplateContext();
            ctx.TempData.Push("func", new TemplateMethod());
            SiteInfo site = new SiteInfo();
            site.Copyright = "&copy;2014 - 2015";
            site.Description = "";
            site.Host = "localhost";
            site.KeyWords = "";
            site.Logo = "";
            site.Name = "xxx";
            site.SiteDirectory = "";
            site.Theme = "Blue";
            site.ThemeDirectory = "theme";
            site.Title = "jntemplate测试页";
            site.Url = string.Concat("http://localhost");

            if (!string.IsNullOrEmpty(site.SiteDirectory) && site.SiteDirectory != "/")
            {
                site.Url += "/" + site.SiteDirectory;
            }
            site.ThemeUrl = string.Concat(site.Url, "/", site.ThemeDirectory, "/", site.Theme);
            //ctx.TempData.Push("Model", );
            ctx.TempData.Push("Site", site);

            string basePath = new System.IO.DirectoryInfo(System.Environment.CurrentDirectory).Parent.Parent.FullName;
            string path = basePath + "\\templets\\default";

            var conf = Configuration.EngineConfig.CreateDefault();
            conf.CachingProvider = null;
            Engine.Configure(conf);

            for (var i = 0; i < 20000; i++)
            {
                JinianNet.JNTemplate.Template t = new JinianNet.JNTemplate.Template(ctx, System.IO.File.ReadAllText(path + "\\questionlist.html"));
                t.Context.CurrentPath = path;
                t.Render();

            }
        }


        [TestMethod]
        public void TestPage()
        {
            var conf = Configuration.EngineConfig.CreateDefault();
            conf.CachingProvider = "JinianNet.JNTemplate.Caching.MemoryCache";
            Engine.Configure(conf);

            JinianNet.JNTemplate.TemplateContext ctx = new JinianNet.JNTemplate.TemplateContext();

            ctx.TempData.Push("func", new TemplateMethod());

            SiteInfo site = new SiteInfo();
            site.Copyright = "&copy;2014 - 2015";
            site.Description = "";
            site.Host = "localhost";
            site.KeyWords = "";
            site.Logo = "";
            site.Name = "xxx";
            site.SiteDirectory = "";
            site.Theme = "Blue";
            site.ThemeDirectory = "theme";
            site.Title = "jntemplate测试页";
            site.Url = string.Concat("http://localhost");

            if (!string.IsNullOrEmpty(site.SiteDirectory) && site.SiteDirectory != "/")
            {
                site.Url += "/" + site.SiteDirectory;
            }
            site.ThemeUrl = string.Concat(site.Url, "/", site.ThemeDirectory, "/", site.Theme);
            //ctx.TempData.Push("Model", );
            ctx.TempData.Push("Site", site);

            string basePath = new System.IO.DirectoryInfo(System.Environment.CurrentDirectory).Parent.Parent.FullName;
            string path = basePath + "\\templets\\default";

            JinianNet.JNTemplate.Template t = new JinianNet.JNTemplate.Template(ctx, System.IO.File.ReadAllText(path + "\\questionlist.html"));
            t.Context.CurrentPath = path;

            string result = t.Render();

            //可直接查看项目录下的html/jnt.html 文件效果
            System.IO.File.WriteAllText(basePath + "\\html\\jnt.html", result);

        }
#if EQVER
        /// <summary>
        /// 多版本比较测试
        /// </summary>
        [TestMethod]
        public void TestEqVersion()
        {
            var tm = new TemplateMethod();
            SiteInfo site = new SiteInfo();
            site.Copyright = "&copy;2014 - 2015";
            site.Description = "";
            site.Host = "localhost";
            site.KeyWords = "";
            site.Logo = "";
            site.Name = "xxx";
            site.SiteDirectory = "";
            site.Theme = "Blue";
            site.ThemeDirectory = "theme";
            site.Title = "jntemplate测试页";
            site.Url = string.Concat("http://localhost");
            if (!string.IsNullOrEmpty(site.SiteDirectory) && site.SiteDirectory != "/")
            {
                site.Url += "/" + site.SiteDirectory;
            }
            site.ThemeUrl = string.Concat(site.Url, "/", site.ThemeDirectory, "/", site.Theme);
            string basePath = new System.IO.DirectoryInfo(System.Environment.CurrentDirectory).Parent.Parent.FullName;
            string path = basePath + "\\templets\\default";

            string content = System.IO.File.ReadAllText(path + "\\questionlist.html");

            FileInfo[] assFlies = new DirectoryInfo(basePath + "\\dll").GetFiles("JinianNet.JNTemplate*.dll");
            string result = DateTime.Now.ToString();
            Stopwatch s = new Stopwatch();
            for (int i = 0; i < assFlies.Length; i++)
            {
                Assembly ass = System.Reflection.Assembly.LoadFile(assFlies[i].FullName);
                object ctx = ass.CreateInstance("JinianNet.JNTemplate.TemplateContext");
                object data = ctx.GetType().GetProperty("TempData").GetValue(ctx, null);
                MethodInfo mi = data.GetType().GetMethod("Push");
                mi.Invoke(data, new object[] { "func", tm });
                mi.Invoke(data, new object[] { "Site", site });
                ctx.GetType().GetProperty("CurrentPath").SetValue(ctx, path, null);

                s.Restart();
                for (int j = 0; j < 100; j++)
                {
                    object t = ass.CreateInstance("JinianNet.JNTemplate.Template"); ;
                    t.GetType().GetProperty("Context").SetValue(t, ctx, null);
                    t.GetType().GetProperty("TemplateContent").SetValue(t, content, null);
                    object r = t.GetType().GetMethod("Render", new Type[0]).Invoke(t, new object[0] { });

                    if (j == 99)
                    {
                        //System.IO.File.WriteAllText(basePath + "\\html\\jnt"+ assFlies[i].Name +".html", r.ToString());
                    }
                }
                s.Stop();
                result += "\r\n:" + assFlies[i].Name + " 版本号：" + ass.GetName().Version + "耗时：" + s.ElapsedMilliseconds.ToString() + "毫秒";
                System.Threading.Thread.Sleep(200);
            }
            if (System.IO.File.Exists(basePath + "\\html\\TestResult.txt"))
            {
                if (System.IO.File.GetLastWriteTime(basePath + "\\html\\TestResult.txt").Date == DateTime.Now.Date)
                {
                    result = System.IO.File.ReadAllText(basePath + "\\html\\TestResult.txt") + "\r\n" + result;
                }
            }

            System.IO.File.WriteAllText(basePath + "\\html\\TestResult.txt", result);
        }
#endif

#if TESTNV
        [TestMethod]
        public void TestJuxtaposePage()
        {
            SiteInfo site = new SiteInfo();
            site.Copyright = "&copy;2014 - 2015";
            site.Description = "";
            site.Host = "localhost";
            site.KeyWords = "";
            site.Logo = "";
            site.Name = "xxx";
            site.SiteDirectory = "";
            site.Theme = "Blue";
            site.ThemeDirectory = "theme";
            site.Title = "jntemplate测试页";
            site.Url = string.Concat("http://localhost");

            if (!string.IsNullOrEmpty(site.SiteDirectory) && site.SiteDirectory != "/")
            {
                site.Url += "/" + site.SiteDirectory;
            }
            site.ThemeUrl = string.Concat(site.Url, "/", site.ThemeDirectory, "/", site.Theme);


            string basePath = new System.IO.DirectoryInfo(System.Environment.CurrentDirectory).Parent.Parent.FullName;
            string path = basePath + "\\templets\\nv";


            NVelocity.Context.IContext ctx = new NVelocity.VelocityContext();
            ctx.Put("func", new TemplateMethod());
            ctx.Put("Site", site);



            NVelocity.App.VelocityEngine velocity = new NVelocity.App.VelocityEngine();
            Commons.Collections.ExtendedProperties props = new Commons.Collections.ExtendedProperties();
            props.AddProperty(NVelocity.Runtime.RuntimeConstants.RESOURCE_LOADER, "file");
            props.AddProperty(NVelocity.Runtime.RuntimeConstants.FILE_RESOURCE_LOADER_PATH, path);
            props.AddProperty(NVelocity.Runtime.RuntimeConstants.INPUT_ENCODING, "utf-8");
            props.AddProperty(NVelocity.Runtime.RuntimeConstants.OUTPUT_ENCODING, "utf-8");
            velocity.Init(props);
            NVelocity.Template t = velocity.GetTemplate("questionlist.html");
            string result;
            using (System.IO.StringWriter write = new StringWriter())
            {
                t.Merge(ctx, write);
                result = write.ToString();
            }

            //可直接查看项目录下的html/nv.html 文件效果
            System.IO.File.WriteAllText(basePath + "\\html\\nv.html", result);
        }
#endif
    }
}
#endif