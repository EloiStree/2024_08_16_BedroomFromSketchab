using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceObjectInsideOfSquareRoomMono : MonoBehaviour
{

    public Transform [] m_objectToForceInsideOfRoom;

    public Transform m_roomCenter;
    public Transform m_roomCornerTopFrontRight;
    public Transform m_teleportObjectOutOfBox;

    public bool m_useResetRotation = true;
    void Update()
    {

        
        for (int i = 0; i < m_objectToForceInsideOfRoom.Length; i++)
        {
            Transform t = m_objectToForceInsideOfRoom[i];
            if(IsOutOfZone(t))
            ForceObjectInsideOfRoom(m_objectToForceInsideOfRoom[i]);
        }        
    }

    private bool IsOutOfZone(Transform t)
    {
        Vector3 direction = m_roomCornerTopFrontRight.position - m_roomCenter.position;

        GetWorldToLocal_Point(t.position, m_roomCenter, out Vector3 localPosition);
        if(localPosition.x >  direction.x)
            return true;
        if(localPosition.x < -direction.x)
            return true;
        if(localPosition.z >  direction.z)
            return true;
        if(localPosition.z < -direction.z)
            return true;
        if(localPosition.y >  direction.y)
            return true;
        if(localPosition.y < -direction.y)
            return true;
        return false;

    }

    private void ForceObjectInsideOfRoom(Transform transform)
    {
        transform.position = m_teleportObjectOutOfBox.position;
        if(m_useResetRotation)
           transform.rotation = m_teleportObjectOutOfBox.rotation;

    }






    #region RELOCATE POINTS
    public static void GetWorldToLocal_Point(in Vector3 worldPosition, in Transform rootReference, out Vector3 localPosition)
    {
        Vector3 positionReference = rootReference.position;
        Quaternion rotationReference = rootReference.rotation;
        GetWorldToLocal_Point(in worldPosition, in positionReference, in rotationReference, out localPosition);
    }

    public static void GetLocalToWorld_Point(in Vector3 localPosition, in Transform rootReference, out Vector3 worldPosition)
    {
        Vector3 positionReference = rootReference.position;
        Quaternion rotationReference = rootReference.rotation;
        GetLocalToWorld_Point(in localPosition, in positionReference, in rotationReference, out worldPosition);
    }

    public static void GetWorldToLocal_Point(in Vector3 worldPosition, in Vector3 positionReference, in Quaternion rotationReference, out Vector3 localPosition)
    {
        localPosition = Quaternion.Inverse(rotationReference) * (worldPosition - positionReference);
    }

    public static void GetLocalToWorld_Point(in Vector3 localPosition, in Vector3 positionReference, in Quaternion rotationReference, out Vector3 worldPosition)
    {
        worldPosition = rotationReference * localPosition + positionReference;
    }

    public static void GetWorldToLocal_DirectionalPoint(in Vector3 worldPosition, in Quaternion worldRotation, in Transform rootReference, out Vector3 localPosition, out Quaternion localRotation)
    {
        Vector3 positionReference = rootReference.position;
        Quaternion rotationReference = rootReference.rotation;
        GetWorldToLocal_DirectionalPoint(in worldPosition, in worldRotation, in positionReference, in rotationReference, out localPosition, out localRotation);
    }

    public static void GetLocalToWorld_DirectionalPoint(in Vector3 localPosition, in Quaternion localRotation, in Transform rootReference, out Vector3 worldPosition, out Quaternion worldRotation)
    {
        Vector3 positionReference = rootReference.position;
        Quaternion rotationReference = rootReference.rotation;
        GetLocalToWorld_DirectionalPoint(in localPosition, in localRotation, in positionReference, in rotationReference, out worldPosition, out worldRotation);
    }

    public static void GetWorldToLocal_DirectionalPoint(in Vector3 worldPosition, in Quaternion worldRotation, in Vector3 positionReference, in Quaternion rotationReference, out Vector3 localPosition, out Quaternion localRotation)
    {
        localRotation = Quaternion.Inverse(rotationReference) * worldRotation;
        localPosition = Quaternion.Inverse(rotationReference) * (worldPosition - positionReference);
    }

    public static void GetLocalToWorld_DirectionalPoint(in Vector3 localPosition, in Quaternion localRotation, in Vector3 positionReference, in Quaternion rotationReference, out Vector3 worldPosition, out Quaternion worldRotation)
    {
        worldRotation = localRotation * rotationReference;
        worldPosition = rotationReference * localPosition + positionReference;
    }

    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        return RotatePointAroundPivot(point, pivot, Quaternion.Euler(angles));
    }

    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
    {
        return rotation * (point - pivot) + pivot;
    }
    #endregion
}
