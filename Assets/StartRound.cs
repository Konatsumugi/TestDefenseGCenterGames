using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartRound : MonoBehaviour
{
    [SerializeField] private Image fillAmount;
    public Image FillAmount => fillAmount;
    [SerializeField] private int timeStart;
    private Button btnStart;
    private float timer = 0f;
    private bool isFilling = false;
    
    private void OnEnable()
    {
        ResetTimer();
    }

    private void Start()
    {
        ResetTimer();
        btnStart.onClick.AddListener(SpawnMonster);
    }

    private void Awake()
    {
        btnStart = gameObject.GetComponent<Button>();
    }

    public void ResetTimer()
    {
        isFilling = true;
        FillAmount.fillAmount = 0;
        timer = 0f;
    }

    public void SpawnMonster()
    {
        GameObject obj = ObjectPooler.Ins.GetPooledObject(1);
        Enemy pathFollower = obj.GetComponent<Enemy>();
        pathFollower.path = TypePath.Type1;
        obj.SetActive(true);
    }

    void Update()
    {
        if (isFilling)
        {
            timer += Time.deltaTime;
            float progress = timer / timeStart;
            FillAmount.fillAmount = Mathf.Lerp(1f, 0f, progress);

            if (timer >= timeStart)
            {
                SpawnMonster();
                FillAmount.fillAmount = 0f;
                isFilling = false;
                ResetTimer();
            }
        }
    }
}