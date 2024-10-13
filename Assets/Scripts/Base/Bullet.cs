
using UnityEngine;
public class Bullet : MonoBehaviour
{
    public float moveSpeed = 5f;

    public static void Create(Vector3 spawnPosition, Vector3 targetPosition)
    {
        GameObject ob = ObjectPooler.Ins.GetPooledObject(2);
        ob.transform.position = spawnPosition;
        ob.SetActive(true);
        Bullet bullet = ob.transform.GetComponent<Bullet>();
        bullet.Setup(targetPosition);
    }

    private Vector3 targetPosition;

    private void Setup(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    private void Update()
    {
        Vector3 moveDir = (this.targetPosition - this.transform.position).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        float angle = GetAngleFromVector(moveDir);
        transform.eulerAngles = new Vector3(0, 0, angle);
        //gần va chạm với quái vật
        float destroyBullet = .01f;
        if (Vector3.Distance(transform.position, this.targetPosition) < destroyBullet)
        {
            gameObject.SetActive(false);
        }
    }
    private static float GetAngleFromVector(Vector3 dir)
    {
        // chuẩn hóa độ dài dir về 1 vaf vẫn giữ nguyên hướng của vector
        dir = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;
        return angle;
    }
}