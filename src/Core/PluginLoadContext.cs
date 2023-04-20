using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Loader;

namespace Core;

internal sealed class PluginLoadContext : AssemblyLoadContext
{
    private readonly AssemblyDependencyResolver _resolver;

    public PluginLoadContext(string pluginPath)
    {
        this._resolver = new AssemblyDependencyResolver(pluginPath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar));
    }

    protected override Assembly Load(AssemblyName assemblyName)
    {
        var assemblyPath = this._resolver.ResolveAssemblyToPath(assemblyName);
        if (assemblyPath != null)
        {
            return this.LoadFromAssemblyPath(assemblyPath);
        }

        assemblyPath = Path.Combine(Environment.CurrentDirectory, "bin/Core", assemblyName.Name + ".dll");
        if (File.Exists(assemblyPath))
        {
            return this.LoadFromAssemblyPath(assemblyPath);
        }

        assemblyPath = Path.Combine(RuntimeEnvironment.GetRuntimeDirectory(), assemblyName.Name + ".dll");
        if (File.Exists(assemblyPath))
        {
            return this.LoadFromAssemblyPath(assemblyPath);
        }
        
        assemblyPath = Path.Combine("/usr/local/share/dotnet/shared/Microsoft.AspNetCore.App/7.0.4", assemblyName.Name + ".dll");
        if (File.Exists(assemblyPath))
        {
            return this.LoadFromAssemblyPath(assemblyPath);
        }


        return null!;
    }

    protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
    {
        var libraryPath = this._resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
        return libraryPath != null
            ? this.LoadUnmanagedDllFromPath(libraryPath)
            : nint.Zero;
    }
}