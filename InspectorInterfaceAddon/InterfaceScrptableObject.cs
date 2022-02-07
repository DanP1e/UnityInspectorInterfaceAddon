using System;
using UnityEngine;

namespace InspectorAddons
{
    [Serializable]
    public class InterfaceScrptableObject<InterfaceT> : InterfaceObject<InterfaceT, ScriptableObject>
        where InterfaceT : class
    {

    }
}
