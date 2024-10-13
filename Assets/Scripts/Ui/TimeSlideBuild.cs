using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TimeSlideBuild : MonoBehaviour
{
    [SerializeField] private Image hold;
    [SerializeField] private float duration = 3f;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform patten;
    private float timer = 0f;
    private bool isFilling = false;

    private void OnEnable()
    {
        ResetTimer();
    }
    private void Start()
    {
        ResetTimer();
    }
    public void ResetTimer()
    {
        isFilling = true;
        hold.fillAmount = 0;
        timer = 0f;
    }

    void Update()
    {
        if (isFilling)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;
            hold.fillAmount = Mathf.Lerp(0f, 1f, progress);

            if (timer >= duration)
            {
                prefab.SetActive(true);
                gameObject.SetActive(false);
                hold.fillAmount = 0f;
                isFilling = false;
            }
        }
    }
}