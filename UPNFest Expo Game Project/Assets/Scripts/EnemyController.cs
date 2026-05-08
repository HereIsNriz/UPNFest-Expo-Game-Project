using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    // Property

    // SerializeField
    [SerializeField] private int m_lives;
    [SerializeField] private Slider m_bossLivesSlider;

    // Field
    private GameManager m_gameManager;
    private float m_attackSpeed = 2f;
    private float m_minMaxXAxis = 8f;
    private float m_minYAxis = 6.5f;

    // Start is called before the first frame update
    void Start()
    {
        m_bossLivesSlider.maxValue = m_lives;
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(ShootBullet());
    }

    // Update is called once per frame
    void Update()
    {
        m_bossLivesSlider.value = m_lives;
        if (m_lives <= 0)
        {
            m_gameManager.GameWin();
        }
    }
    private IEnumerator ShootBullet()
    {
        while (m_gameManager.IsGameRunning)
        {
            yield return new WaitForSeconds(m_attackSpeed);
            m_gameManager.ShootBadBullet(GenerateRandomPosition(), Quaternion.identity);
        }
    }
    private Vector2 GenerateRandomPosition()
    {
        float randomXAxis = Random.Range(-m_minMaxXAxis, m_minMaxXAxis);
        return new Vector2(randomXAxis, m_minYAxis);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GoodBullet"))
        {
            m_lives--;
        }
    }
}