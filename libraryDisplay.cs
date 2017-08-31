using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class libraryDisplay : MonoBehaviour {

	public GameObject libraryWindow;
	// Use this for initialization
	public void displayLibrary(){
			libraryWindow.SetActive(true);
	}
	public void closeLibrary(){
		libraryWindow.SetActive(false);
	}
}
