using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuManager : MonoBehaviour
{
    // Property
    // SerializeField
    // Field
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void PressPlayButton()
    {
        SceneManager.LoadScene(0);
    }
    public void PressExitButton()
    {
        EditorApplication.ExitPlaymode();
    }
}
