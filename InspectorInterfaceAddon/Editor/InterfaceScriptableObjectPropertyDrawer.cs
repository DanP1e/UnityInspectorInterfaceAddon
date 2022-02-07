using System;
using UnityEditor;
using UnityEngine;

namespace InspectorAddons
{
    [CustomPropertyDrawer(typeof(InterfaceScrptableObject<>))]
    public class InterfaceScriptableObjectPropertyDrawer : InterfacePropertyDrawer<ScriptableObject>
    {
        protected override ScriptableObject GetComponentWithType(ScriptableObject component, Type type)
        {
            if (type.IsAssignableFrom(component.GetType()))
                return component;

            return null;
        }
    }
}
