using UnityEngine;
using System.Collections;

public class Interactive : FourthDimension {

    public GameObject TargetObj;

    public virtual void DoAction() {
        var wLock = TargetObj.GetComponent<FourthDimensionLock>();

        if (wLock != null) {
            if (wLock.IsLocked)
                wLock.OpenLock();
            else
                wLock.CloseLock();
        }
    }
}
