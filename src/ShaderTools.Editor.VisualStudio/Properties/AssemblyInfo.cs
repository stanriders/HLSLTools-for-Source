using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using ShaderTools.VisualStudio.LanguageServices;

[assembly: AssemblyTitle("HLSL Tools for Visual Studio")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Tim Jones & Graham Dianaty")]
[assembly: AssemblyProduct("HLSL Tools for Source Engine")]
[assembly: AssemblyCopyright("Copyright © 2015-2017 Tim Jones")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]   
[assembly: ComVisible(false)]     
[assembly: CLSCompliant(false)]
[assembly: NeutralResourcesLanguage("en-US")]

[assembly: AssemblyVersion(ShaderToolsPackage.Version)]
[assembly: AssemblyFileVersion(ShaderToolsPackage.Version)]

[assembly: InternalsVisibleTo("ShaderTools.Editor.VisualStudio.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]