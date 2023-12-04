using _Dev.Game.Scripts.Managers;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class GamePanelEditor : EditorWindow
    {
        public bool DebugMode;
        
        private Vector2 _scrollPos;

        [MenuItem("Game/Tools/Game Panel")]
        public static void ShowWindow()
        {
            GetWindow<GamePanelEditor>("Game Panel");
        }
        
        private void OnGUI()
        {
            _scrollPos = GUILayout.BeginScrollView(_scrollPos, false, false);
            DrawDebugPanel();
            GUILayout.EndScrollView();
        }

        private void DrawDebugPanel()
        {
            var labelStyle = CreateLabel(15, FontStyle.Bold);
            
            GUILayout.Space(10);
            GUILayout.Label("Debug Panel", labelStyle);
            
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            
            DrawDebugModeArea();

            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }

        private void DrawDebugModeArea()
        {
            DebugMode = EditorGUILayout.Toggle("Debug Mode", DebugMode);
            GridManager.Instance.IsDebugging = DebugMode;
        }
        
        private static GUIStyle CreateLabel(int fontSize, FontStyle style)
        {
            var labelStyle = new GUIStyle
            {
                alignment = TextAnchor.UpperCenter,
                fontStyle = style,
                fontSize = fontSize,
                normal =
                {
                    textColor = Color.white
                }
            };
            
            return labelStyle;
        }
    }
}
