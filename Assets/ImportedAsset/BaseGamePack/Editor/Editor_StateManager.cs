using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StateManager))]
public class Editor_StateManager : Editor {

	public StateManager instance;

	public override void OnInspectorGUI ()
	{
		instance = target as StateManager;

		GUIStyle StringStyle = new GUIStyle();
		StringStyle.alignment = TextAnchor.MiddleCenter;
		StringStyle.fontSize = 15;

		//現在の登録さている状態を表示
		EditorGUILayout.LabelField("---Current States---",StringStyle);
		EditorGUILayout.Space();

		base.OnInspectorGUI();

		//状態を追加
		if(GUILayout.Button("Add New State")){
			AddStateWindow window = (AddStateWindow)EditorWindow.GetWindow (typeof(AddStateWindow));
			window.BookMark(this);
			window.position = new Rect(500,250,250,100);
			window.ShowUtility();
		}

		EditorGUILayout.LabelField(name);
	}

}
