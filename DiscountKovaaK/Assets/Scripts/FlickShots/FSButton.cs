using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSButton : MonoBehaviour
{
    void TakeDamage()
    {
        FSManager.Instance.ButtonClicked();
    }
}
