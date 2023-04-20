// See https://aka.ms/new-console-template for more information

using Core;

var fullPluginDllPath = Path.Combine(Environment.CurrentDirectory, "bin/plugins/Plugin1/Plugin1.dll");

var pluginLoadContext = new PluginLoadContext(fullPluginDllPath);

var asm = pluginLoadContext.LoadFromAssemblyPath(fullPluginDllPath);

var testClass1Type = asm.GetTypes().First(t => t.FullName == "Plugin1.TestClass1");

var getTestObjectMethodInfo = testClass1Type.GetMethods().First(i => i.Name == "GetTestObject");

var testObject = getTestObjectMethodInfo.Invoke(null, null);

Console.WriteLine("Hello, World!");