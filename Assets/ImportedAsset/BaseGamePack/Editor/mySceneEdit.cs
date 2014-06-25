using UnityEngine;
using UnityEditor;

public class mySceneEdit : EditorWindow
{

	string SceneName;
	//string PrefabsPath;
	//string GameStatePath;
	//string ScriptPath;

	[MenuItem("Custom/Add Scene")]
	public static void Create ()
	{

		mySceneEdit window = (mySceneEdit)EditorWindow.GetWindow (typeof(mySceneEdit));
		window.position = new Rect(500,250,250,100);
		window.ShowUtility();

	}

	void OnGUI ()
	{
		EditorGUILayout.HelpBox( "新しいシーンのディレクトリの追加をします。\nシーンの名前は保存時に決めてください。", MessageType.Info, true );

		SceneName = EditorGUILayout.TextField("ディレクトリの名前",SceneName);

		if (GUILayout.Button("Create!") && SceneName != null) {

			EditorApplication.NewScene();
			CreateFolder();
			//CreatePrefabs();

			EditorApplication.SaveScene();
			this.Close();
		}

	}

	/*
	void CreatePrefabs(){

		GameObject SoundManager = Instantiate(Resources.Load("SoundManager"))as GameObject;
		SoundManager.name = SceneName + "_SoundManager";
		
		GameObject StateManager = Instantiate(Resources.Load("StateManager"))as GameObject;
		StateManager.name = SceneName + "_StateManager";

		StateManager scripts = StateManager.GetComponent<StateManager>();

		GenerateCode(SceneName + "_Initailizer");
		GenerateCode(SceneName + "_Play");
		GenerateCode(SceneName + "_End");

		GameObject obj = new GameObject(SceneName + "_Initailizer");
		GameObject obj2 = new GameObject(SceneName + "_Play");
		GameObject obj3 = new GameObject(SceneName + "_End");

		scripts.State_Initalizer = PrefabUtility.CreatePrefab (GameStatePath + "/" + SceneName + "_Initailizer" + ".prefab", obj);
		scripts.State_Play = PrefabUtility.CreatePrefab (GameStatePath + "/" + SceneName + "_Play" +".prefab", obj2);
		scripts.State_End = PrefabUtility.CreatePrefab (GameStatePath + "/" + SceneName + "_End" + ".prefab", obj3);

		DestroyImmediate(obj);
		DestroyImmediate(obj2);
		DestroyImmediate(obj3);

		PrefabUtility.CreatePrefab(PrefabsPath + "/" + SceneName + "_SoundManager.prefab",SoundManager);
		PrefabUtility.CreatePrefab(PrefabsPath + "/" + SceneName + "_StateManager.prefab",StateManager);
		AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
	}*/

	void CreateFolder(){

		string id = AssetDatabase.CreateFolder ("Assets",SceneName);
		string newFolderPath = AssetDatabase.GUIDToAssetPath(id);

		AssetDatabase.CreateFolder(newFolderPath,"Animation");
		AssetDatabase.CreateFolder(newFolderPath + "/Animation","AnimationController");
		AssetDatabase.CreateFolder(newFolderPath + "/Animation","clip");
		
		AssetDatabase.CreateFolder(newFolderPath,"/Audio");
		AssetDatabase.CreateFolder(newFolderPath + "/Audio","BGM");
		AssetDatabase.CreateFolder(newFolderPath + "/Audio","SE");

		AssetDatabase.CreateFolder(newFolderPath,"Image");

		AssetDatabase.GUIDToAssetPath(AssetDatabase.CreateFolder(newFolderPath,"Prefabs"));
		
		AssetDatabase.CreateFolder(newFolderPath,"Scripts");

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

	}

	/*
	void GenerateCode (string name)
	{
		
		System.Text.StringBuilder builder = new System.Text.StringBuilder ();
		builder.AppendLine ("using UnityEngine;");
		builder.AppendLine ("using System.Collections;");
		builder.AppendLine ("public class " + name + " : State {");
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
		
		System.IO.File.WriteAllText (ScriptPath + "/" + name + ".cs", builder.ToString (), System.Text.Encoding.UTF8);
		AssetDatabase.Refresh (ImportAssetOptions.ImportRecursive);
		
	}*/

}
