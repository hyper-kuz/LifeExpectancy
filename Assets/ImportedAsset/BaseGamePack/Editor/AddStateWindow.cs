using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Reflection.Emit;
using System.IO;
using System.Text;

public class AddStateWindow : EditorWindow
{

		GUIStyle StringStyle;
		bool flag = false;

		//State
		string StateName;
		string path;

		public AddStateWindow ()
		{

				StringStyle = new GUIStyle ();
				StringStyle.alignment = TextAnchor.MiddleCenter;
				StringStyle.fontSize = 15;

		}

		public void BookMark (Editor_StateManager parent)
		{
				//this.parent = parent;
		}

		void OnGUI ()
		{

				if (flag == false) {

						EditorGUILayout.LabelField ("Add New State Script:", StringStyle);
						EditorGUILayout.HelpBox ("暫定版なのでエラーが出ますが、支障は無いです。（多分）", MessageType.Info);
						StateName = EditorGUILayout.TextField ("State StateName", StateName);
						EditorGUILayout.Space ();

						if (GUILayout.Button ("Create!")) {
								GenerateCode ();
								flag = true;
						}

				} else {
						GenerateGameObject ();
				}

		}

		void GenerateGameObject ()
		{

				GameObject obj = new GameObject (StateName);

				if (obj.AddComponent (StateName) != null) {

						PrefabUtility.CreatePrefab ("Assets" + "/" + StateName + ".prefab", obj);
						AssetDatabase.Refresh (ImportAssetOptions.ImportRecursive);
						DestroyImmediate (obj);
						this.Close ();
				}

				DestroyImmediate (obj);
		}

		void GenerateCode ()
		{

				System.Text.StringBuilder builder = new System.Text.StringBuilder ();
				builder.AppendLine ("using UnityEngine;");
				builder.AppendLine ("using System.Collections;");
				builder.AppendLine ("public class " + StateName + " : State {");
				builder.AppendLine ("");
				builder.AppendLine ("public override void StateStart ()");
				builder.AppendLine ("{");
				builder.AppendLine ("	Debug.Log (this.ToString() + \" Start!\");");
				builder.AppendLine ("");
				builder.AppendLine ("}");
				builder.AppendLine ("");
				builder.AppendLine ("public override void StateUpdate ()");
				builder.AppendLine ("{");
				builder.AppendLine ("");	
				builder.AppendLine ("	if(Input.GetKeyDown(KeyCode.A)){");
				builder.AppendLine ("		this.EndState();");
				builder.AppendLine ("	}");
				builder.AppendLine ("	");
				builder.AppendLine ("}");
				builder.AppendLine ("");
				builder.AppendLine ("public override void StateDestroy ()");
				builder.AppendLine ("{");
				builder.AppendLine ("	Debug.Log (this.ToString() + \" Destroy!\");");
				builder.AppendLine ("}");
				builder.AppendLine ("");
				builder.AppendLine ("}");

				System.IO.File.WriteAllText ("Assets" + "/" + StateName + ".cs", builder.ToString (), System.Text.Encoding.UTF8);
				AssetDatabase.Refresh (ImportAssetOptions.ImportRecursive);

		}

}
		