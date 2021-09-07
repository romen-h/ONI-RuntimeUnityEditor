using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;

namespace RuntimeUnityEditor.ONI
{
	[HarmonyPatch(typeof(Assets))]
	[HarmonyPatch("OnSpawn")]
	public static class Patch
	{
		public static void Postfix(Assets __instance)
		{
			Debug.Log("RuntimeUnityInspector: Applying patch");
			__instance.gameObject.AddComponent<RuntimeInspector>();
		}
	}

	[HarmonyPatch(typeof(KSelectable))]
	[HarmonyPatch("Select")]
	public static class KSelectable_Select_Patch
	{
		public static void Postfix(KSelectable __instance)
		{
			RuntimeInspector.Instance.TreeViewer.SelectAndShowObject(__instance.transform);
		}
	}
}
