using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] public float curentLevel;
    [SerializeField] public int currentWave;
    [SerializeField] public int coint;
    [SerializeField] public int heart;
    [SerializeField] public int enemy;
    public Wave wave;

    private float previousLevel;
    private int previousWave;

    private void Start()
    {
        previousLevel = curentLevel;
        previousWave = currentWave;
        FillData();
    }

    private void Update()
    {
        // Chỉ cập nhật khi curentLevel hoặc currentWave thay đổi
        if (curentLevel != previousLevel || currentWave != previousWave)
        {
            FillData();
            previousLevel = curentLevel;
            previousWave = currentWave;
        }
    }

    public void FillData()
    {
        wave = LevelData.Ins.lsLevel.Find(r => r.level == curentLevel);
        if (wave != null)
        {
            if (currentWave > 0 && currentWave <= wave.lsWave.Count)
            {
                enemy = wave.lsWave[currentWave - 1].count;
            }
        }
    }
}