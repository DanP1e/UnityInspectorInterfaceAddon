using System;
using UnityEngine;

namespace InspectorAddons
{
    [Serializable]
    public class InterfaceComponent<InterfaceT> : InterfaceObject<InterfaceT, Component>
        where InterfaceT: class
    {

    }
}
