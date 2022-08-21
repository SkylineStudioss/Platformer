using UnityEngine;
using UnityEditor;

public class _SceneTool : EditorWindow
{
    void OnGUI()
    {

    }

    [MenuItem("Tools/Scene Tool")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<_SceneTool>("Scene Tool");
    }
}
