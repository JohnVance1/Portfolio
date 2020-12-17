using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//Author: Ben Stern
/// <summary>
/// A component that allows objects to load other scenes
/// </summary>
// Since you cant call static methods in the inspector this object cant be a singleton
public class SceneLoader : MonoBehaviour
{
    private GameObject foodTransferScript;
    /// <summary>
    /// A simple function to load a scene
    /// </summary>
    /// <param name="s">the name of the scene</param>
    public void LoadScene(string s)
	{
		SceneManager.LoadScene(s);
        
        
    }

	/// <summary>
	/// A simple function to load a scene
	/// </summary>
	/// <param name="i">The index of the scene</param>
	public void LoadScene(int i)
	{
		SceneManager.LoadScene(i);
	}
}
