using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Property

    // SerializeField
    [SerializeField] private GameObject m_bulletOutPosition;
    [SerializeField] private int m_lives;
    [SerializeField] private float m_speed;
    [SerializeField] private string m_playerCode;

    // Field
    private GameManager m_gameManager;
    private Rigidbody2D m_playerRb;
    private Vector2 m_playerPosition;
    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_xBoundary = 8.35f;
    private float m_yBoundary = 4.5f;

    void Start()
    {
        m_playerRb = GetComponent<Rigidbody2D>();
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        TakePlayerData();
        ClampPlayerPosition();
        ShootBullet();
        if (m_lives <= 0) // Player die
        {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MovePlayer()
    {
        m_playerRb.velocity = m_playerPosition * m_speed * Time.deltaTime;
    }
    private void TakePlayerData()
    {
        if (m_gameManager.IsGameRunning)
        {
            m_horizontalInput = Input.GetAxis("HorizontalP" + m_playerCode);
            m_verticalInput = Input.GetAxis("VerticalP" + m_playerCode);
            m_playerPosition = new Vector2(m_horizontalInput, m_verticalInput).normalized;
        }
    }
    private void ClampPlayerPosition()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -m_xBoundary, m_xBoundary),
            Mathf.Clamp(transform.position.y, -m_yBoundary, m_yBoundary),
            0);
    }
    private void ShootBullet()
    {
        if (m_gameManager.IsGameRunning)
        {
            if (Input.GetButtonDown("FireP" + m_playerCode))
            {
                m_gameManager.ShootGoodBullet(m_bulletOutPosition.transform.position, Quaternion.identity);
            }
        }
    }
}
