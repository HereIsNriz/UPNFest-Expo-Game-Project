using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuManager : MonoBehaviour
{
    // Property
    // SerializeField
    [SerializeField] private AudioSource m_buttonsSound;

    // Field
    private float m_buttonSoundDelay = 0.03f;

    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private IEnumerator PlayButtonSound()
    {
        yield return new WaitForSeconds(m_buttonSoundDelay);
        SceneManager.LoadScene(0);
    }
    private IEnumerator ExitButtonSound()
    {
        yield return new WaitForSeconds(m_buttonSoundDelay);
        EditorApplication.ExitPlaymode();
        //Application.Quit();
    }
    public void PressPlayButton()
    {
        m_buttonsSound.PlayOneShot(m_buttonsSound.clip, 1f);
        StartCoroutine(PlayButtonSound());
    }
    public void PressExitButton()
    {
        m_buttonsSound.PlayOneShot(m_buttonsSound.clip, 1f);
        StartCoroutine(ExitButtonSound());
    }
}
