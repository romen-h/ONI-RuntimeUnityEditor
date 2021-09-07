using RuntimeUnityEditor.Core;
using UnityEngine;

namespace RuntimeUnityEditor.ONI
{
	public class RuntimeInspector : MonoBehaviour
	{
        public static RuntimeUnityEditorCore Instance { get; set; }

        private void OnGUI()
        {
            Instance.OnGUI();
        }

        private void Start()
        {
            Instance = new RuntimeUnityEditorCore(this, new Logger(), null);
        }

        private void Update()
        {
            Instance.Update();
        }

        private void LateUpdate()
        {
            Instance.LateUpdate();
        }

        private sealed class Logger : ILoggerWrapper
        {
            public void Log(LogLevel logLogLevel, object content)
            {
                Debug.Log("[RuntimeEditor]" + content);
            }
        }
    }
}
