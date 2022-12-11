using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterPositionWindows : EditorWindow
{
    private GameObject[] _selectedObjects;
    private Vector3 _position;
    private string _selectedObjectsNames;

    [MenuItem("Window/Character Position")]
    public static void ShowWindow()
    {
        GetWindow<CharacterPositionWindows>("Character Position");
    }

    private void OnGUI()
    {
        _selectedObjects = Selection.gameObjects;
        _selectedObjectsNames =
            _selectedObjects.Aggregate("", (current, gameObject) => current + (gameObject.name + ", "));
        _selectedObjectsNames = _selectedObjectsNames.TrimEnd(',', ' ');

        GUILayout.Label("Selected objects:");
        GUILayout.Label(_selectedObjectsNames);

        GUILayout.Space(5);

        GUILayout.Label("Predefined positions:");
        if (GUILayout.Button("Game start"))
        {
            _position = new Vector3(-1.37f, -28.8f, 480.4f);
            SetPositions();
        }

        if (GUILayout.Button("Stomach"))
        {
            _position = new Vector3(70.9f, -17.8f, -55.4f);
            SetPositions();
        }

        if (GUILayout.Button("Game end"))
        {
            _position = new Vector3(140.7f, -23.3f, -156.8f);
            SetPositions();
        }

        GUILayout.Space(5);

        _position = EditorGUILayout.Vector3Field("Custom Position", _position);
        if (GUILayout.Button("Set Position"))
        {
            SetPositions();
        }
    }

    private void SetPositions()
    {
        foreach (var obj in _selectedObjects)
        {
            obj.transform.position = _position;
        }
    }
}