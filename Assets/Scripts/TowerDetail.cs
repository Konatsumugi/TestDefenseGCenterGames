using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TowerDetail : MonoBehaviour
{
    [SerializeField] protected float damage = 40;
    public Type type;
    public int levelMax;
    public int levelCurrent;
    public int attackRange;
    public int buildPrice;
    public int cellPrice;
    public int upgradePrice;
    public int attackSpeed;
    private int previousAttackSpeed;  
    public Transform attackPoint;
    public Transform endPoint;
    public GameObject range;
    public SpriteRenderer spriteRenderer;
    public GameObject prefab;
    public List<Enemy> enemies;
    private Enemy CurrentEnemyTarget;
    public float ShootTime;
    public float ShootTimeMax;

    // Theem quái vật vào tầm đánh của Tower
    public void OnChildTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemies.Add(enemy);
        }
    }
    private void Awake()
    {
        ShootTimeMax = 1f;
    }

    public void OnChildTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemies.Contains(enemy))
            {
                enemies.Remove(enemy);
            }
        }
    }

    private void Update()
    {
        SpeedShoot();
        UpdateShootTimeMax();
    }

    private void SpeedShoot()
    {
        if (attackSpeed <= 0) return;
        ShootTime -= Time.deltaTime;
        if (ShootTime <= 0f)
        {
            ShootTime = ShootTimeMax;
            GetCurrentEnemy();
        }
    }
    
    private void GetCurrentEnemy()
    {
        if (enemies.Count <= 0)
        {
            CurrentEnemyTarget = null;
            return;
        }

        CurrentEnemyTarget = enemies[0];
        Bullet.Create(attackPoint.position, CurrentEnemyTarget.gameObject.transform.position);
    }
    private void UpdateShootTimeMax()
    {
        // Tính toán mức thay đổi của attackSpeed
        int speedDifference = attackSpeed - previousAttackSpeed;

        // Nếu attackSpeed tăng
        if (speedDifference > 0)
        {
            // Giảm ShootTimeMax theo mức tăng của attackSpeed (0.05 cho mỗi đơn vị tăng)
            ShootTimeMax = Mathf.Max(0.1f, ShootTimeMax - (0.02f * speedDifference));
        }
        // Nếu attackSpeed giảm
        else if (speedDifference < 0)
        {
            // Tăng ShootTimeMax theo mức giảm của attackSpeed (0.05 cho mỗi đơn vị giảm)
            ShootTimeMax += 0.02f * Mathf.Abs(speedDifference);
        }

        // Cập nhật previousAttackSpeed sau khi điều chỉnh ShootTimeMax
        previousAttackSpeed = attackSpeed;
    }
}