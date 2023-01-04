using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

public class ModeWindow : EditorWindow
{
    [MenuItem("Window/Mode")]

    static void Init()
    {
        // Get existing open window or if none, make a new one:
        ModeWindow window = (ModeWindow)EditorWindow.GetWindow(typeof(ModeWindow));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Mode:");
        if (GUILayout.Button("Develop"))
        {
            PlayerPrefs.SetInt("LaunchMode", 0);
            PlayerPrefs.Save();
            EditorSceneManager.OpenScene("Assets/Scenes/Level1.unity");
        }
        if (GUILayout.Button("Test"))
        {
            PlayerPrefs.SetInt("LaunchMode", 1);
            PlayerPrefs.Save();
            EditorSceneManager.OpenScene("Assets/Scenes/MenuScene.unity");
        }

    }
}
