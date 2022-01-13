using System;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace InspectorAddons
{
    [CustomPropertyDrawer(typeof(InterfaceComponent<>))]
    public class InterfaceComponentPropertyDrawer : PropertyDrawer
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
            object fI = fieldInfo.GetValue(property.serializedObject.targetObject);
            Type fIType = fI.GetType();          
            
            Type element = fIType;
            if (fIType.IsArray)
            {
                if (fIType.GetArrayRank() == 0)
                    return;
                element = fIType.GetElementType();
            }

            Type interfaceType = element.GetGenericArguments()[0];

            DrawProperty(position, property, label, interfaceType);
        }

        private void DrawProperty(
            Rect position, 
            SerializedProperty property, 
            GUIContent label, 
            Type interfaceType)
        {
            EditorGUI.BeginProperty(position, label, property);

            var objectWithInterface = property.FindPropertyRelative("_componentWithInterface");

            var indent = EditorGUI.indentLevel;
            
            EditorGUI.indentLevel = 0;
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
