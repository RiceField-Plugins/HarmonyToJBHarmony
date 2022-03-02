using System;
using System.Reflection;
using SDG.Framework.Modules;
using UnityEngine;

namespace HarmonyToJBHarmony
{
	public class UnturnedModule : MonoBehaviour, IModuleNexus
	{
		public void initialize()
		{
			ModuleHook.onModulesInitialized += OnModulesInitialized;
		}

		public void shutdown()
		{
			ModuleHook.onModulesInitialized -= OnModulesInitialized;
		}

		private static void OnModulesInitialized()
		{
			AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
		}

		private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			try
			{
				var assemblyName = new AssemblyName(args.Name);
				if (assemblyName.Name == "0Harmony")
				{
					Console.WriteLine("[INFO] Redirecting 0Harmony to JBHarmony...");
					return Type.GetType("HarmonyLib.Harmony, JBHarmony, Version=2.2.1.0, Culture=neutral, PublicKeyToken=null")?.Assembly;
				}
			
				return null;
			}
			catch (Exception e)
			{
				Console.WriteLine("[INFO] Failed to redirect 0Harmony to JBHarmony!");
				Console.WriteLine(e);
				throw;
			}
		}
	}
}
