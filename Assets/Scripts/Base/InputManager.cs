using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : Singleton<InputManager>
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            GameObject obj = ObjectPooler.Ins.GetPooledObject(1);
            Enemy pathFollower = obj.GetComponent<Enemy>();
            pathFollower.isPlaying = true;
            pathFollower.path = TypePath.Type1;
            obj.SetActive(true);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity);
            // Kiểm tra nếu raycast trúng đối tượng có BoxCollider2D
            ReturnPoint();
            if (hit.collider == null) return;
            if (hit.collider.gameObject.CompareTag("Tower"))
            {
                Debug.Log(hit.collider.gameObject.name);
                SpawnPoint sp = hit.collider.gameObject.GetComponentInParent<SpawnPoint>();
                sp.upgradeTower.gameObject.SetActive(true);
            }

            if (hit.collider.gameObject.CompareTag("SpawnPoint"))
            {
                Debug.Log(hit.collider.gameObject.name);
                SpawnPoint sp = hit.collider.gameObject.GetComponentInParent<SpawnPoint>();
                sp.buildTower.gameObject.SetActive(true);
            }
        }
    }

    public void ReturnPoint()
    {
        for (int i = 0; i < PointManager.Ins.lsPoint.Count; i++)
        {
            PointManager.Ins.lsPoint[i].buildTower.gameObject.SetActive(false);
            PointManager.Ins.lsPoint[i].upgradeTower.gameObject.SetActive(false);
        }
    }
}