using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Property

    // SerializeField
    [SerializeField] private GameObject m_bulletOutPosition;
    [SerializeField] private GameObject m_teleportPointPosition;
    [SerializeField] private Slider m_playerLivesSlider;
    [SerializeField] private AudioSource m_useAbilitySound;
    [SerializeField] private AudioSource m_hitSound;
    [SerializeField] private AudioSource m_shootSound;
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
        m_playerLivesSlider.maxValue = m_lives;
        m_playerRb = GetComponent<Rigidbody2D>();
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        TakePlayerData();
        ClampPlayerPosition();
        ShootBullet();
        UseAbility();
        if (m_lives <= 0) // Player die
        {
            m_playerLivesSlider.gameObject.SetActive(false);
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
            m_playerLivesSlider.value = m_lives;
        }
        else
        {
            m_playerLivesSlider.gameObject.SetActive(false);
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
                m_shootSound.PlayOneShot(m_shootSound.clip, 1f);
                m_gameManager.ShootGoodBullet(m_bulletOutPosition.transform.position, Quaternion.identity);
            }
        }
    }
    private void UseAbility()
    {
        if (m_gameManager.IsGameRunning)
        {
            if (m_teleportPointPosition.gameObject.activeInHierarchy)
            {
                if (Input.GetButton("AbilityP" + m_playerCode))
                {
                    if (this.m_playerCode == "1")
                    {
                        Vector2 player1Destination = (Vector2)m_teleportPointPosition.transform.position + Vector2.up;
                        this.gameObject.transform.position = Vector2.MoveTowards(m_playerRb.position, player1Destination, m_speed * Time.deltaTime);
                    }
                    if (this.m_playerCode == "2")
                    {
                        Vector2 player2Destination = (Vector2)m_teleportPointPosition.transform.position + Vector2.down;
                        this.gameObject.transform.position = Vector2.MoveTowards(m_playerRb.position, player2Destination, m_speed * Time.deltaTime);
                    }
                }
                if (Input.GetButtonDown("AbilityP" + m_playerCode))
                {
                    m_useAbilitySound.PlayOneShot(m_useAbilitySound.clip, 1f);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BadBullet"))
        {
            m_hitSound.PlayOneShot(m_hitSound.clip, 1f);
            m_lives--;
        }
    }
}
