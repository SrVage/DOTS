using System.Linq;
using Code.Configs;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(GameParametersInstaller))]
    public class GameParametersInstallerEditor:UnityEditor.Editor
    {
        private string[] _strings;
        private string _viewName;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (!EditorGUILayout.DropdownButton(new GUIContent(_viewName), FocusType.Passive))
                return;
            void handleItemClicked(object parameter)
            {
                _viewName = parameter.ToString();
                var field = serializedObject.FindProperty("_pathName");
                field.stringValue = _viewName;
                serializedObject.ApplyModifiedProperties();
            }
            _strings = AssetDatabase.FindAssets("t:GameCfg", new[] { "Assets/" });
            GenericMenu menu = new GenericMenu();
            foreach (var name in _strings)
            {
                string path = AssetDatabase.GUIDToAssetPath(name);
                var fileName = path.Split("/");
                menu.AddItem(new GUIContent(fileName.Last()), false, handleItemClicked, path);
            }
            menu.DropDown(new Rect());
        }
    }
}