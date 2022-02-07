using System;
using UnityEditor;
using UnityEngine;

namespace InspectorAddons
{
    [CustomPropertyDrawer(typeof(InterfaceComponent<>))]
    public class InterfaceComponentPropertyDrawer : InterfacePropertyDrawer<Component>
    {
        protected override Component GetComponentWithType(Component component, Type type)
        {
            if (type.IsAssignableFrom(component.GetType()))
                return component;

            foreach (var item in component.GetComponents(typeof(Component)))
                if (type.IsAssignableFrom(item.GetType()))
                    return item;
            return null;
        }
    }
}
