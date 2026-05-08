using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Property
    public bool IsGameRunning;

    // SerializeField
    [SerializeField] private GameObject m_goodBulletPrefab;
    [SerializeField] private GameObject m_badBulletPrefab;
    [SerializeField] private GameObject m_gameWinPanel;
    [SerializeField] private GameObject m_gameOverPanel;
    [SerializeField] private TextMeshProUGUI m_numOfPlayerSurvivedText;
    [SerializeField] private Slider m_bossLivesSlider;

    // Field
    private Queue<GameObject> m_goodBulletPool = new Queue<GameObject>();
    private Queue<GameObject> m_badBulletPool = new Queue<GameObject>();
    private GameObject[] m_players;
    private int m_poolSize = 10;
    private int m_numOfPlayerSurvived;

    private void Awake()
    {
        for (int i = 0; i < m_poolSize; i++)
        {
            StoreGoodBulletIntoPool();
            StoreBadBulletIntoPool();
        }
    }
    void Start()
    {
        IsGameRunning = true;
    }
    void Update()
    {
        m_players = GameObject.FindGameObjectsWithTag("Player");
        DetectNumberOfPlayer();
    }
    public void GameWin()
    {
        if (IsGameRunning)
        {
            m_bossLivesSlider.gameObject.SetActive(false);
            m_gameWinPanel.gameObject.SetActive(true);
            m_numOfPlayerSurvivedText.text = $"Player Survived: {m_numOfPlayerSurvived}";
            IsGameRunning = false;
        }
    }
    private void GameOver()
    {
        if (IsGameRunning)
        {
            m_bossLivesSlider.gameObject.SetActive(false);
            m_gameOverPanel.gameObject.SetActive(true);
            IsGameRunning = false;
        }
    }
    private void DetectNumberOfPlayer()
    {
        m_numOfPlayerSurvived = m_players.Length;
        if (m_numOfPlayerSurvived <= 0)
        {
            GameOver();
        }
    }
    public void PressBackButton()
    {
        SceneManager.LoadScene(1);
    }
    // Good Bullet Pool
    private GameObject StoreGoodBulletIntoPool()
    {
        GameObject goodBullet = Instantiate(m_goodBulletPrefab);
        goodBullet.gameObject.SetActive(false);
        m_goodBulletPool.Enqueue(goodBullet);
        return goodBullet;
    }
    public GameObject ShootGoodBullet(Vector2 position, Quaternion rotation)
    {
        GameObject goodBullet = m_goodBulletPool.Count > 0 ? m_goodBulletPool.Dequeue() : StoreGoodBulletIntoPool();
        goodBullet.gameObject.transform.SetPositionAndRotation(position, rotation);
        goodBullet.gameObject.SetActive(true);
        return goodBullet;
    }
    public void ReturnGoodBulletIntoPool(GameObject goodBullet)
    {
        goodBullet.gameObject.SetActive(false);
        m_goodBulletPool.Enqueue(goodBullet);
    }
    // Bad Bullet Pool
    private GameObject StoreBadBulletIntoPool()
    {
        GameObject badBullet = Instantiate(m_badBulletPrefab);
        badBullet.gameObject.SetActive(false);
        m_badBulletPool.Enqueue(badBullet);
        return badBullet;
    }
    public GameObject ShootBadBullet(Vector2 position, Quaternion rotation)
    {
        GameObject badBullet = m_badBulletPool.Count > 0 ? m_badBulletPool.Dequeue() : StoreBadBulletIntoPool();
        badBullet.gameObject.transform.SetPositionAndRotation(position, rotation);
        badBullet.gameObject.SetActive(true);
        return badBullet;
    }
    public void ReturnBadBulletIntoPool(GameObject badBullet)
    {
        badBullet.gameObject.SetActive(false);
        m_badBulletPool.Enqueue(badBullet);
    }
}
