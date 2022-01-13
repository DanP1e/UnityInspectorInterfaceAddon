using System;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace InspectorAddons
{
    [CustomPropertyDrawer(typeof(InterfaceField<>))]
    public class InterfaceFieldPropertyDrawer : PropertyDrawer
    {
        private Component GetComponentWithType(Component component, Type type) 
        {
            if (type.IsAssignableFrom(component.GetType()))
                return component;

            foreach (var item in component.GetComponents(typeof(Component)))
                if (type.IsAssignableFrom(item.GetType()))
                    return item;
            return null;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var indent = EditorGUI.indentLevel;

            EditorGUI.indentLevel = 0;

            var objectWithInterface = property.FindPropertyRelative("_componentWithInterface");
            var fI = fieldInfo.GetValue(property.serializedObject.targetObject);
            Type interfaceType = fI.GetType().GetGenericArguments()[0];

            EditorGUI.BeginChangeCheck();
            Component inputVal = EditorGUI.ObjectField(
                    position, 
                    label, 
                    objectWithInterface.objectReferenceValue,
                    typeof(Component), 
                    true) as Component;

            if (inputVal != null && objectWithInterface.objectReferenceValue != inputVal)
            {
                Component component = GetComponentWithType(inputVal, interfaceType);
                if (EditorGUI.EndChangeCheck())
                {
                    objectWithInterface.objectReferenceValue = component;
                }
            }
            else
            {
                if (EditorGUI.EndChangeCheck())
                {
                    objectWithInterface.objectReferenceValue = inputVal;
                }
            }
            EditorGUI.indentLevel = indent;
            
            EditorGUI.EndProperty();
        }           
    }
}
