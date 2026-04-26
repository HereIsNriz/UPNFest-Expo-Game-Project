using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    // Property

    // SerializeField
    [SerializeField] private float m_speed;

    // Field
    private GameManager m_gameManager;
    private Rigidbody2D m_bulletRb;
    private float m_yBoundary = 6f;

    void Start()
    {
        m_bulletRb = GetComponent<Rigidbody2D>();
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        ReturnProjectile();
    }
    private void FixedUpdate()
    {
        MoveProjectile();
    }
    private void MoveProjectile()
    {
        m_bulletRb.velocity = Vector2.up * m_speed * Time.deltaTime;
    }
    private void ReturnProjectile()
    {
        if (transform.position.y >= m_yBoundary)
        {
            m_gameManager.ReturnGoodBulletIntoPool(this.gameObject);
        }
    }
}
