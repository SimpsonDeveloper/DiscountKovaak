using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;
using UnityEngine.UI;

public class WallMenu : MonoBehaviour
{
    public string action;
    public GameObject deselected;
    public GameObject selected;

    private RectTransform sTrans;
    private RectTransform dTrans;
    private Vector3 sStartPos;
    private Vector3 dStartPos;
    // Start is called before the first frame update
    void Start()
    {
        sTrans = selected.GetComponent<RectTransform>();
        dTrans = deselected.GetComponent<RectTransform>();
        sStartPos = sTrans.localPosition;
        dStartPos = dTrans.localPosition;
    }
    
    public void DoAction()
    {
        Type thisType = GameManager.Instance.GetType();
        MethodInfo theMethod = thisType.GetMethod(action);
        theMethod.Invoke(GameManager.Instance, null);
    }

    public void EnableSelected()
    {
        sTrans.localPosition = sStartPos + new Vector3(0, 0, -1);
        dTrans.localPosition = dStartPos + new Vector3(0, 0, 1);
    }

    public void DisableSelected()
    {
        sTrans.localPosition = sStartPos;
        dTrans.localPosition = dStartPos;
    }

}
