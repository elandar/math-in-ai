using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISeeable {

    float ViewDistance { get; }
    float ViewAngle { get; }
    LayerMask LayerMask { get; }

    bool CanSeeObject();
}
