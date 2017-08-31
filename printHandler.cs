using UnityEngine;
using System.Collections;

public class printHandler : MonoBehaviour {

	public GameObject readButton;
	// Use this for initialization
	public void PrintLibrary(){
		libraryHandler print = readButton.GetComponent<libraryHandler>();
		print.PrintLibrary();
	}
}
