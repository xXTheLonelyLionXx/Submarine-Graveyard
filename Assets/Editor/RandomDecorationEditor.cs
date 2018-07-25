using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(RandomDecoration))]
[CanEditMultipleObjects]
public class RandomDecorationEditor : Editor {

    private RandomDecoration _rd;

    void OnEnable()
    {
        _rd = target as RandomDecoration;
    }

	public override void OnInspectorGUI()
    {
        EditorGUILayout.MinMaxSlider(ref _rd.min, ref _rd.max, 0.1f, 10f);
        if (GUILayout.Button("Randomize!"))
        {
            _rd.Randomi();
        }
    }
}
