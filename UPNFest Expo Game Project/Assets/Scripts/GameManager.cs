using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Property
    public bool IsGameRunning;

    // SerializeField
    [SerializeField] private GameObject m_goodBulletPrefab;

    // Field
    private Queue<GameObject> m_goodBulletPool = new Queue<GameObject>();
    private int m_poolSize = 10;

    private void Awake()
    {
        for (int i = 0; i < m_poolSize; i++)
        {
            StoreGoodBulletIntoPool();
        }
    }
    void Start()
    {
        IsGameRunning = true;
    }
    void Update()
    {
        
    }
    public void GameWin()
    {
        IsGameRunning = false;
    }
    public void GameOver()
    {
        IsGameRunning = false;
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
}
