using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextDamage : MonoBehaviour
{
   public TextMeshProUGUI textDamage;

   private void Hide()
   {
      gameObject.SetActive(false);
   }
}
