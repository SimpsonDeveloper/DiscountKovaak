using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TFTarget : MonoBehaviour
{
    void TakeDamage()
    {
        TFManager.Instance.TargetClicked(this.gameObject);
    }
}
