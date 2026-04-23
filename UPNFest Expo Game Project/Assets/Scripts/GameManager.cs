using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Property

    // SerializeField
    [SerializeField] private bool m_isGameRunning;

    // Field

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_isGameRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
