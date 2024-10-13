using System;
using UnityEngine;
using PathCreation;
using TMPro;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Update = Unity.VisualScripting.Update;


public class Enemy : MonoBehaviour, IDamage
{
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public TypePath path;
    public float speed = 5;
    private int priceDie;
    [SerializeField] private Transform damageSpawnPoint;
    [SerializeField] private Transform cointSpawnPoint;
    [SerializeField] private float health;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private Image _healthBar;
    private float distanceTravelled;
    public bool isPlaying = true;
    private EnemyAnimation enemyAnimation;

    private void Awake()
    {
        health = maxHealth;
        enemyAnimation = gameObject.GetComponent<EnemyAnimation>();
    }

    void Update()
    {
        CompareType();
        _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, health / maxHealth, Time.deltaTime * 10f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (health <= 0) return;
            int damageCount = Random.Range(20, 40);
            ShowDamageText(damageCount);
            TakeDamage(damageCount);
        }
    }

    private void ShowCointText()
    {
        GameObject obCointText = ObjectPooler.Ins.GetPooledObject(4);
        obCointText.transform.position = cointSpawnPoint.position;
        TextCoint damageText = obCointText.GetComponent<TextCoint>();
        priceDie = Random.Range(20, 40);
        AddCoint(priceDie);
        damageText.textDamage.text = "+" + priceDie.ToString();
        obCointText.SetActive(true);
    }

    private void ShowDamageText(int damage)
    {
        GameObject obDamageText = ObjectPooler.Ins.GetPooledObject(3);
        obDamageText.transform.position = damageSpawnPoint.position;
        PlayAnimHurt();
        TextDamage damageText = obDamageText.GetComponent<TextDamage>();
        damageText.textDamage.text = "-" + damage.ToString();
        obDamageText.SetActive(true);
    }

    protected virtual void PlayAnimHurt()
    {
        enemyAnimation.IsHurt();
    }

    protected virtual void PlayAnimDeath()
    {
        enemyAnimation.IsDeath();
    }

    private void AddCoint(int coint)
    {
        GameManager.Ins.coint += coint;
    }

    private void CompareType()
    {
        DeductHeart();
        if (!isPlaying) return;
        for (int i = 0; i < PathManager.Ins.lspath.Count; i++)
        {
            PathDetail pathDetail = PathManager.Ins.lspath[i].gameObject.GetComponent<PathDetail>();

            if (pathDetail == null) return;
            //kiểm tra xem path của enemy trùng với path nào trong list phần tử trong lspath 
            // gắn path tương ứng cho Enemy dó
            if (path == pathDetail.pathType)
            {
                pathCreator = PathManager.Ins.lspath[i];
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            }
        }
    }

// kiểm tra xem enemy có đến điểm cuối chưa
    private void DeductHeart()
    {
        if (pathCreator == null) return;
        if (transform.position == pathCreator.path.GetPoint(pathCreator.path.NumPoints - 1))
        {
            transform.position = pathCreator.path.GetPoint(0);
            isPlaying = false;
            distanceTravelled = 0;
            GameManager.Ins.heart--;
            Hide();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Death();
        }
    }

    private void Death()
    {
        PlayAnimDeath();
        ShowCointText();
    }
}