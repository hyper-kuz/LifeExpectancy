using UnityEngine;
using UnityEditor;
using System.Collections;

public class StageEdit : EditorWindow
{

	bool CreateStageFlag;
	bool CreatePlaneFlag;
	bool CreateRoomFlag;
	bool Objectfoldflag;
	bool ItemfoldFlag;
	bool GimicfoldFlag;
	bool is_Select;
	bool is_Create;
	//bool CreateObject;
	//bool CreateGimic;
	//bool CreateItem;
	int blocks_x = 0, blocks_y = 0;
	int Roomblocks_x = 0, Roomblocks_y = 0;
	ArrayList Gimics;
	ArrayList Items;
	ArrayList Objects;
	int CountYoko;
	GameObject NowSelectObject;
	Vector2 ScrollPosition = Vector2.zero;

	[MenuItem("Window/StageCreate")]
	static void ShowWindow ()
	{

		StageEdit window = (StageEdit)EditorWindow.GetWindow (typeof(StageEdit));
		window.Show ();
	}

	void Update ()
	{

		Object[] Prefabs = Resources.LoadAll ("",typeof(GameObject));
		Gimics = new ArrayList ();
		Items = new ArrayList ();
		Objects = new ArrayList ();
		is_Select = false;

		/*if (GameObject.FindWithTag ("Stage") == null) {
			CreateStageFlag = true;
		} else {
			CreateStageFlag = false;
		}*/

		foreach (GameObject value in Prefabs) {

			/*switch (value.tag) {
			case "Gimic":
				Gimics.Add (value);
				break;
				
			case "Item":
				Items.Add (value);
				break;
				
			case "Object":
				Objects.Add (value);
				break;
				
			default:
				if (value.name == "GameObject") {
					Objects.Insert (0, value);
				}
				break;
			}*/

			Objects.Add(value);

		}

	}

	void OnGUI ()
	{

		if (CreateStageFlag == true) {
			CreateStage ();
			return;
		}

		ScrollPosition = EditorGUILayout.BeginScrollView (ScrollPosition, true, true);

		Update ();

		EditorGUILayout.Space ();

		AddObject ();
		//AddGimic ();
		//AddItem ();
		//CreatePlaen ();
		//CreateRoom ();
		//DeleteSelectObject ();

		if (is_Select == true) {
			GameObject clone = Instantiate (NowSelectObject)as GameObject;
			Undo.RegisterCreatedObjectUndo (clone, clone.name);
			clone.name = NowSelectObject.name;
			Selection.activeGameObject = clone;
			SceneView Now = SceneView.currentDrawingSceneView;
			Selection.activeGameObject.transform.position = Now.pivot;
			Now.Focus ();
			//Now.LookAt (clone.transform.position);
			is_Select = false;

			//Gimic ScoreItem Objectに対応した所の親に所属させる
			/*if (CreateObject == true) {
				GameObject ObjectParent = GameObject.FindGameObjectWithTag ("RootObject");
				if (ObjectParent == null) {
					GameObject RootObject = new GameObject ("RootObject");
					RootObject.transform.position = Vector3.zero;
					clone.transform.parent = RootObject.transform;
				}
				else{
					clone.transform.parent = ObjectParent.transform;
				}
				CreateObject = false;
			}*/

			/*if (CreateGimic == true) {
				GameObject GimicParent = GameObject.FindGameObjectWithTag ("RootGimic");
				if (GimicParent == null) {
					GameObject RootGimic = new GameObject ("RootGimic");
					RootGimic.transform.position = Vector3.zero;
					clone.transform.parent = RootGimic.transform;
				}
				else{
					clone.transform.parent = GimicParent.transform;
				}
				CreateGimic = false;
			}

			if (CreateItem == true) {
				GameObject ItemParent = GameObject.FindGameObjectWithTag ("RootItem");
				if (ItemParent == null) {
					GameObject RootItem = new GameObject ("RootItem");
					RootItem.transform.position = Vector3.zero;
					clone.transform.parent = RootItem.transform;
				}
				else{
					clone.transform.parent = ItemParent.transform;
				}
				CreateItem = false;
			}*/
		}
	
		if (is_Create == true) {
			Selection.activeGameObject = NowSelectObject;
			SceneView Now = SceneView.currentDrawingSceneView;
			Selection.activeGameObject.transform.position = Now.pivot;
			Now.Focus ();
			Now.LookAt (NowSelectObject.transform.position);
			is_Create = false;
		}

		EditorGUILayout.EndScrollView ();
	}

	void DeleteSelectObject ()
	{

		EditorGUILayout.BeginVertical ("box");
		if (GUILayout.Button ("Delete Object", GUILayout.Width (280.0f)) == true) {
			if (Selection.activeObject != null) {
				Undo.DestroyObjectImmediate (Selection.activeObject);
				DestroyImmediate (Selection.activeObject);
			}

		}
		EditorGUILayout.EndVertical ();

	}

	void CreateStage ()
	{

		EditorGUILayout.BeginVertical ("box");
		if (GUILayout.Button ("Stage Create", GUILayout.Width (280.0f)) == true) {
			GameObject Stage = Resources.Load ("other/Stage")as GameObject;
			GameObject clone = Instantiate (Stage)as GameObject;
			clone.name = "Stage";
			Undo.RegisterCreatedObjectUndo (clone, clone.name);
			CreateStageFlag = false;
		}
		EditorGUILayout.EndVertical ();

	}

	void CreateRoom ()
	{

		CreateRoomFlag = EditorGUILayout.Foldout (CreateRoomFlag, "CreateRoom");
		
		if (CreateRoomFlag == true) {
			EditorGUILayout.BeginVertical ("box");
			EditorGUILayout.LabelField ("Create n*n Room", GUILayout.Width (280.0f));
			
			Roomblocks_x = EditorGUILayout.IntSlider ("X", Roomblocks_x, 1, 10, GUILayout.Width (280.0f));
			Roomblocks_y = EditorGUILayout.IntSlider ("Y", Roomblocks_y, 1, 10, GUILayout.Width (280.0f));
			
			if (GUILayout.Button ("Create!", GUILayout.Width (280.0f))) {
				GameObject Parent = new GameObject (Roomblocks_x.ToString () + " * " + Roomblocks_y.ToString () + "Room");
				Vector3 position = Vector3.zero;
				NowSelectObject = Parent;
				is_Create = true;

				for (int i = 0; i < Roomblocks_x; i++) {
					for (int j = 0; j < Roomblocks_y; j++) {
						position.x = i * 1.5f;
						position.z = j * 1.5f;

						DecideCube (i, j, Roomblocks_x, Roomblocks_y, position, Parent);
					}
				}
			}
			
			EditorGUILayout.EndVertical ();
		}
	}

	void DecideCube (int x, int y, int Xmax, int Ymax, Vector3 position, GameObject Parent)
	{

		GameObject Cube_on_Plane = Resources.Load ("Object/Cube_on_Plane")as GameObject;
		GameObject Wall_on_Cube = Resources.Load ("Object/Wall_on_Cube")as GameObject;
		GameObject DoubleWallCube = Resources.Load ("Object/DoubleWallCube")as GameObject;

		if (x == 0) {
			if (y == 0) {
				CreateCube (DoubleWallCube, position, Parent, -180.0f);
			} else if (y == Ymax - 1) {
				CreateCube (DoubleWallCube, position, Parent, -90.0f);
			} else {
				CreateCube (Wall_on_Cube, position, Parent, 270.0f);
			}
		} else if (x == Xmax - 1) {
			if (y == 0) {
				CreateCube (DoubleWallCube, position, Parent, -270.0f);
			} else if (y == Ymax - 1) {
				CreateCube (DoubleWallCube, position, Parent, 0.0f);
			} else {
				CreateCube (Wall_on_Cube, position, Parent, 90.0f);
			}
		} else {
			if (y == 0) {
				CreateCube (Wall_on_Cube, position, Parent, 180.0f);
			} else if (y == Ymax - 1) {
				CreateCube (Wall_on_Cube, position, Parent, 0.0f);
			} else {
				CreateCube (Cube_on_Plane, position, Parent, 180.0f);
			}

		}


	}

	void CreateCube (GameObject Prefab, Vector3 position, GameObject Parent, float angle)
	{

		Vector3 tempAngles;
		GameObject clone = Instantiate (Prefab, position, Quaternion.identity)as GameObject;
		clone.transform.parent = Parent.transform;
		clone.name = "Cube_on_Plane";
		tempAngles = clone.transform.eulerAngles;
		tempAngles.y = angle;
		clone.transform.eulerAngles = tempAngles;

		Undo.RegisterCreatedObjectUndo (clone, clone.name);
		Undo.RegisterCreatedObjectUndo (Parent, Parent.name);
	}

	void CreatePlaen ()
	{

		CreatePlaneFlag = EditorGUILayout.Foldout (CreatePlaneFlag, "CreatePlane");

		if (CreatePlaneFlag == true) {
			EditorGUILayout.BeginVertical ("box");
			EditorGUILayout.LabelField ("Create n*n Blocks", GUILayout.Width (280.0f));

			blocks_x = EditorGUILayout.IntSlider ("X", blocks_x, 1, 10, GUILayout.Width (280.0f));
			blocks_y = EditorGUILayout.IntSlider ("Y", blocks_y, 1, 10, GUILayout.Width (280.0f));

			if (GUILayout.Button ("Create!", GUILayout.Width (280.0f))) {
				GameObject Parent = new GameObject (blocks_x.ToString () + " * " + blocks_y.ToString () + "Blocks");
				GameObject Cube_on_Plane = Resources.Load ("Object/Cube_on_Plane")as GameObject;
				Vector3 position = Vector3.zero;
				NowSelectObject = Parent;
				is_Create = true;

				for (int i = 0; i < blocks_x; i++) {
					for (int j = 0; j < blocks_y; j++) {
						position.x = i * 1.5f;
						position.z = j * 1.5f;

						GameObject clone = Instantiate (Cube_on_Plane, position, Quaternion.identity)as GameObject;
						clone.transform.parent = Parent.transform;
						clone.name = "Cube_on_Plane";
						Undo.RegisterCreatedObjectUndo (clone, clone.name);
						Undo.RegisterCreatedObjectUndo (Parent, Parent.name);
					}
				}
			}

			EditorGUILayout.EndVertical ();
		}

	}

	void AddGimic ()
	{
		
		GimicfoldFlag = EditorGUILayout.Foldout (GimicfoldFlag, "Add Gimic");

		//Objects
		if (GimicfoldFlag == true) {

			bool EndFlag = false;
			for (int i = 0; i < Gimics.Count/5 + 1; i++) { 
				GUILayout.BeginHorizontal ("box");
				for (int j = 0; j < 5; j++) {
					EndFlag = GimicButton (i, j);
					if (EndFlag == true)
						break;
				}
				GUILayout.EndHorizontal ();
				if (EndFlag == true)
					break;
			}

		}

	}

	bool GimicButton (int num, int j)
	{

		int i = num * 5 + j;

		if (Gimics.Count <= i)
			return true;

		if (Gimics [i] == null) {
			Debug.LogError ("Resources is none");
			return true;
		}
		
		Texture2D tex = AssetPreview.GetAssetPreview (Gimics [i]as GameObject);
		if (GUILayout.Button (tex, GUILayout.Width (50.0f), GUILayout.Height (50.0f))) {
			NowSelectObject = Gimics [i]as GameObject;
			is_Select = true;
			//CreateGimic = true;
		}


		return false;
	}
	
	void AddItem ()
	{
		ItemfoldFlag = EditorGUILayout.Foldout (ItemfoldFlag, "Add Item");

		//Objects
		if (ItemfoldFlag == true) {
			
			bool EndFlag = false;
			for (int i = 0; i < Items.Count/5 + 1; i++) { 
				GUILayout.BeginHorizontal ("box");
				for (int j = 0; j < 5; j++) {
					EndFlag = ItemButton (i, j);
					if (EndFlag == true)
						break;
				}
				GUILayout.EndHorizontal ();
				if (EndFlag == true)
					break;
			}
			
		}
	}

	bool ItemButton (int num, int j)
	{
		
		int i = num * 5 + j;
		
		if (Items.Count <= i)
			return true;
		
		if (Items [i] == null) {
			Debug.LogError ("Resources is none");
			return true;
		}
		
		Texture2D tex = AssetPreview.GetAssetPreview (Items [i]as GameObject);
		if (GUILayout.Button (tex, GUILayout.Width (50.0f), GUILayout.Height (50.0f))) {
			NowSelectObject = Items [i]as GameObject;
			is_Select = true;
			//CreateItem = true;
		}
		
		
		return false;
	}
	
	void AddObject ()
	{
		
		Objectfoldflag = EditorGUILayout.Foldout (Objectfoldflag, "Add Object");

		//Objects
		if (Objectfoldflag == true) {
			
			bool EndFlag = false;
			for (int i = 0; i < Objects.Count/5 + 1; i++) { 
				GUILayout.BeginHorizontal ("box");
				for (int j = 0; j < 5; j++) {
					EndFlag = ObjectButton (i, j);
					if (EndFlag == true)
						break;
				}
				GUILayout.EndHorizontal ();
				if (EndFlag == true)
					break;
			}
			
		}

	}

	bool ObjectButton (int num, int j)
	{
		
		int i = num * 5 + j;
		
		if (Objects.Count <= i)
			return true;
		
		if (Objects [i] == null) {
			Debug.LogError ("Resources is none");
			return true;
		}
		
		Texture2D tex = AssetPreview.GetAssetPreview (Objects [i]as GameObject);
		if (GUILayout.Button (tex, GUILayout.Width (50.0f), GUILayout.Height (50.0f))) {
			NowSelectObject = Objects [i]as GameObject;
			is_Select = true;
			//CreateObject = true;
		}
		
		
		return false;
	}


}
