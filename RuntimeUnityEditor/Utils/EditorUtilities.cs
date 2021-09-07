using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RuntimeUnityEditor.Core.Inspector.Entries;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RuntimeUnityEditor.Core.Utils
{
    public static class EditorUtilities
    {
        public static IEnumerable<ReadonlyCacheEntry> GetTransformScanner()
        {
            RuntimeUnityEditorCore.Logger.Log(LogLevel.Debug, "Looking for Transforms...");

            var trt = typeof(Transform);
            var types = GetAllComponentTypes().Where(x => trt.IsAssignableFrom(x));

            return ScanComponentTypes(types, false);
        }

        public static IEnumerable<ReadonlyCacheEntry> GetRootGoScanner()
        {
            RuntimeUnityEditorCore.Logger.Log(LogLevel.Debug, "Looking for Root Game Objects...");

            List<ReadonlyCacheEntry> list = new List<ReadonlyCacheEntry>();

            foreach (GameObject go in Resources.FindObjectsOfTypeAll<GameObject>().Where(x => x.transform == x.transform.root))
                list.Add(new ReadonlyCacheEntry($"GameObject({go.name})", go));

            return list;
        }

        public static IEnumerable<ReadonlyCacheEntry> GetMonoBehaviourScanner()
        {
            RuntimeUnityEditorCore.Logger.Log(LogLevel.Debug, "Looking for MonoBehaviours...");

            var mbt = typeof(MonoBehaviour);
            var types = GetAllComponentTypes().Where(x => mbt.IsAssignableFrom(x));

            return ScanComponentTypes(types, true);
        }

        public static IEnumerable<ReadonlyCacheEntry> GetComponentScanner()
        {
            RuntimeUnityEditorCore.Logger.Log(LogLevel.Debug, "Looking for Components...");

            var mbt = typeof(MonoBehaviour);
            var trt = typeof(Transform);
            var allComps = GetAllComponentTypes().ToList();
            var types = allComps.Where(x => !mbt.IsAssignableFrom(x) && !trt.IsAssignableFrom(x));

            return ScanComponentTypes(types, true);
        }

        private static IEnumerable<ReadonlyCacheEntry> ScanComponentTypes(IEnumerable<Type> types, bool noTransfroms)
        {
            var allObjects = from type in types
                             let components = Object.FindObjectsOfType(type).OfType<Component>()
                             from component in components
                             where !(noTransfroms && component is Transform)
                             select component;

            string GetTransformPath(Transform tr)
            {
                if (tr.parent != null)
                    return GetTransformPath(tr.parent) + "/" + tr.name;
                return tr.name;
            }

            List<ReadonlyCacheEntry> list = new List<ReadonlyCacheEntry>();

            foreach (var obj in allObjects.Distinct())
                list.Add(new ReadonlyCacheEntry($"{GetTransformPath(obj.transform)} ({obj.GetType().Name})", obj));

            return list;
        }

        private static IEnumerable<Type> GetAllComponentTypes()
        {
            var compType = typeof(Component);
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(Extensions.GetTypesSafe)
                .Where(t => t.IsClass && !t.IsAbstract && !t.ContainsGenericParameters)
                .Where(compType.IsAssignableFrom);
        }

        public static IEnumerable<ReadonlyCacheEntry> GetInstanceClassScanner()
        {
            RuntimeUnityEditorCore.Logger.Log(LogLevel.Debug, "Looking for class instances...");

            var query = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(Extensions.GetTypesSafe)
                .Where(t => t.IsClass && !t.IsAbstract && !t.ContainsGenericParameters);

            List<ReadonlyCacheEntry> list = new List<ReadonlyCacheEntry>();

            foreach (var type in query)
            {
                object obj = null;
                try
                {
                    obj = type.GetProperty("Instance", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)?.GetValue(null, null) ??
                          type.GetField("_instance", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)?.GetValue(null);
                }
                catch (Exception ex)
                {
                    RuntimeUnityEditorCore.Logger.Log(LogLevel.Debug, ex.ToString());
                }
                if (obj != null)
                    list.Add(new ReadonlyCacheEntry(type.Name + ".Instance", obj));
            }

            return list;
        }
    }
}