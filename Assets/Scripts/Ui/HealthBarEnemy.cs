using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    [SerializeField] private Image fillAmount;
    public Image FillAmount => fillAmount;
}