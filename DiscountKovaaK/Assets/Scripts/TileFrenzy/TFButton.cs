using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TFButton : MonoBehaviour
{
    void TakeDamage()
    {
        TFManager.Instance.ButtonClicked();
    }
}
