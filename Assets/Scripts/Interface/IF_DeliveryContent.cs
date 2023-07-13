using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IF_DeliveryContent
{
    public void SetReceivePoint(Transform _receivePoint, string _pointName);

    public string GetReceivePointName();

    public Transform GetReceivePoint();

    public void ClearReceivePoint();

    public void TransformPositionToNextPoint(Transform _nextPoint);
    public void TransformPositionToNextPosition(Vector3 _nextPosition);

    public void ActiveSpecialEffect();


    public void Show();
    public void Hide();

}

