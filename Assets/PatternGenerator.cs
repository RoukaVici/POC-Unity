﻿using UnityEngine;
using System.Collections;
using System.IO;

public class PatternGenerator : MonoBehaviour {

	private VibrationStyle[] vibrations = new VibrationStyle[2];

	private void getFiles(string folder) {
		DirectoryInfo dir = new DirectoryInfo(folder);
		FileInfo[] info = dir.GetFiles("*.*");

		int i = 0;

		int[] arr = new int[4];
		arr [0] = 1;
		arr [1] = 2;
		arr [2] = 1;
		arr [3] = 0;

		foreach (FileInfo f in info) {
			string data = File.ReadAllText(info[i].FullName);
			this.vibrations[i] = JsonUtility.FromJson<VibrationStyle> (data);
			int k = 2;
			while (k < 11) {
				string str = '{' + data.Split('{')[k];
				this.vibrations[i].addFinger(JsonUtility.FromJson<Fingers>(str.Remove(str.Length - 3)));
				k += 1;
			}
			i += 1;
		}
	}

	// Use this for initialization
	void Start () {
		this.getFiles("Patterns");

		float x_axys = 12.0f;
		float y_axis = 3.0f;
		float z_axis = 0.0f;

		GameObject[] cubes = new GameObject[10];
		int i = 0;
		foreach (VibrationStyle vs in this.vibrations) {
			cubes[i] = new GameObject();
			cubes[i].transform.position = new Vector3(x_axys, y_axis, z_axis);
			cubes[i].AddComponent<BoxCollider>();
			cubes[i].GetComponent<BoxCollider>().size = new Vector3(5.0f, 5.0f, 5.0f);
			TextMesh t = cubes[i].AddComponent<TextMesh>();
			t.text = vs.getName();
			t.fontSize = 10;
			cubes[i].GetComponent<TextMesh>().color = Color.red;
			cubes [i].AddComponent<ActivatePattern>();
			cubes [i].GetComponent<ActivatePattern> ().addPatern (vs.getName(), vs.getFingers());

			i += 1;
			y_axis -= 2.0f;
		}

		//		this.vibrations = JsonUtility.FromJson<VibrationStyle>(data);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}