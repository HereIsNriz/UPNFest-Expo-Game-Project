using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    // Property

    // SerializeField
    [SerializeField] private bool m_isBadBullet;
    [SerializeField] private float m_speed;

    // Field
    private GameManager m_gameManager;
    private Rigidbody2D m_bulletRb;
    private float m_minMaxYAxis = 6f;

    void Start()
    {
        m_bulletRb = GetComponent<Rigidbody2D>();
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        ReturnGoodProjectile();
        ReturnBadProjectile();
    }
    private void FixedUpdate()
    {
        MoveProjectileUp();
        MoveProjectileDown();
    }
    private void MoveProjectileUp()
    {
        if (!m_isBadBullet)
        {
            m_bulletRb.velocity = Vector2.up * m_speed * Time.deltaTime;
        }
    }
    private void ReturnGoodProjectile()
    {
        if (!m_isBadBullet && (transform.position.y >= m_minMaxYAxis))
        {
            m_gameManager.ReturnGoodBulletIntoPool(this.gameObject);
        }
    }
    private void MoveProjectileDown()
    {
        if (m_isBadBullet)
        {
            m_bulletRb.velocity = Vector2.down * m_speed * Time.deltaTime;
        }
    }
    private void ReturnBadProjectile()
    {
        if (m_isBadBullet && (transform.position.y <= -m_minMaxYAxis))
        {
            m_gameManager.ReturnBadBulletIntoPool(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_isBadBullet)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                m_gameManager.ReturnBadBulletIntoPool(this.gameObject);
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                m_gameManager.ReturnGoodBulletIntoPool(this.gameObject);
            }
        }
    }
}
