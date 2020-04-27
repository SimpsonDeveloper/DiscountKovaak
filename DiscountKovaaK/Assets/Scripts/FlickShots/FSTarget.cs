using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSTarget : MonoBehaviour
{
    void TakeDamage()
    {
        FSManager.Instance.TargetClicked(this.gameObject);
    }
}
