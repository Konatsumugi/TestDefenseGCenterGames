using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SpawnPoint : MonoBehaviour
{
    public GameObject buildTower;
    public GameObject upgradeTower;
    [SerializeField] private GameObject timeSlide;
    [SerializeField] private GameObject prefabTower;
    [SerializeField] private GameObject max;
    [SerializeField] private GameObject tower;
    [SerializeField] private Button btnMagicTower;
    [SerializeField] private Button btnArcharTower;
    [SerializeField] private Button btnCell;
    [SerializeField] private Button btnUpgrade;
    [SerializeField] private TextMeshProUGUI textMagicTower;
    [SerializeField] private TextMeshProUGUI textArcharTower;
    [SerializeField] private TextMeshProUGUI textCell;
    [SerializeField] private TextMeshProUGUI textUpgrade;
    private TowerDetail towerDetail;
    private TowerType towerArcharDetail;
    private TowerType towerMagicDetail;

    private void Awake()
    {
        towerDetail = prefabTower.GetComponentInChildren<TowerDetail>(true);
        towerArcharDetail = btnArcharTower.gameObject.GetComponent<TowerType>();
        towerMagicDetail = btnMagicTower.gameObject.GetComponent<TowerType>();
    }

    private void Start()
    {
        FillDataMagicUI();
        FillDataAcharUI();
        btnMagicTower.onClick.AddListener(MagicTower);
        btnArcharTower.onClick.AddListener(ArcharTower);
        btnUpgrade.onClick.AddListener(UpgradeTower);
        btnCell.onClick.AddListener(CellTower);
    }

    private void FillDataMagicUI()
    {
        Tower towerData = TowerData.Ins.lsTowers.Find(r => r.towerType == towerMagicDetail.towerType);
        textMagicTower.text = towerData.buildPrice.ToString();
    }

    private void FillDataAcharUI()
    {
        Tower towerData = TowerData.Ins.lsTowers.Find(r => r.towerType == towerArcharDetail.towerType);
        textArcharTower.text = towerData.buildPrice.ToString();
    }

    private void CellTower()
    {
        InputManager.Ins.ReturnPoint();
        GameManager.Ins.coint += towerDetail.cellPrice;
        ResetTower();
    }

    private void ResetTower()
    {
        towerDetail.range.transform.localScale = Vector2.one;
        towerDetail.levelCurrent = 1;
        btnUpgrade.gameObject.SetActive(true);
        max.gameObject.SetActive(false);
        tower.SetActive(false);
    }

    private void MagicTower()
    {
        InputManager.Ins.ReturnPoint();
        FillData(towerMagicDetail);
        if (GameManager.Ins.coint < towerDetail.buildPrice) return;
        DeductCoint(towerDetail.buildPrice);
        timeSlide.SetActive(true);
    }

    private void ArcharTower()
    {
        InputManager.Ins.ReturnPoint();
        FillData(towerArcharDetail);
        if (GameManager.Ins.coint < towerDetail.buildPrice) return;
        DeductCoint(towerDetail.buildPrice);
        timeSlide.SetActive(true);
    }

    private void DeductCoint(int coint)
    {
        GameManager.Ins.coint -= coint;
    }

    private void FillData(TowerType towerType)
    {
        Tower towerData = TowerData.Ins.lsTowers.Find(r => r.towerType == towerType.towerType);
        if (towerData != null)
        {
            towerDetail.type = towerData.towerType;
            towerDetail.levelMax = towerData.levelMax;
            towerDetail.spriteRenderer.sprite = towerData.iconLevel1;
            towerDetail.attackSpeed = towerData.attackSpeed;
            towerDetail.attackRange = towerData.attackRange;
            towerDetail.buildPrice = towerData.buildPrice;
            towerDetail.upgradePrice = towerData.updatePrice;
            towerDetail.cellPrice = towerData.cellPrice;
            textCell.text = towerData.cellPrice.ToString();
            textUpgrade.text = towerData.updatePrice.ToString();
        }
    }

    private void UpgradeTower()
    {
        if (towerDetail.levelCurrent <= towerDetail.levelMax)
        {
            InputManager.Ins.ReturnPoint();
            if (GameManager.Ins.coint < towerDetail.upgradePrice) return;
            towerDetail.levelCurrent++;
            Tower towerData = TowerData.Ins.lsTowers.Find(r => r.towerType == towerDetail.type);
            switch (towerDetail.levelCurrent)
            {
                case 1:
                    towerDetail.spriteRenderer.sprite = towerData.iconLevel1;
                    towerDetail.range.transform.localScale = Vector2.one;
                    DeductCoint(towerDetail.upgradePrice);
                    break;
                case 2:
                    towerDetail.spriteRenderer.sprite = towerData.iconLevel2;
                    towerDetail.range.transform.localScale = Vector2.one * 1.2f;
                    towerDetail.attackSpeed += 10;
                    DeductCoint(towerDetail.upgradePrice);
                    break;
                case 3:
                    towerDetail.spriteRenderer.sprite = towerData.iconLevel3;
                    towerDetail.range.transform.localScale = Vector2.one * 1.4f;
                    towerDetail.attackSpeed += 10;
                    DeductCoint(towerDetail.upgradePrice);
                    btnUpgrade.gameObject.SetActive(false);
                    max.gameObject.SetActive(true);
                    break;
            }
        }
    }
}