using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New Room Info", menuName = "Data/Room Info", order = 1)]
public class RoomInfo : ScriptableObject
{
    public enum RoomType
    {
        Storage, Workspace, Recreation, Habitation,
        Automation, Generic, Control
    }

    public RoomType type;
    public Vector3 size;

    public RoomInfo(RoomType t, Vector3 s)
    { type = t; size = s; }
}
