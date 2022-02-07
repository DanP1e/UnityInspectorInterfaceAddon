using System;
using UnityEngine;
using UnityEditor;
using InspectorAddons.Utilities;

namespace InspectorAddons
{
    /// <typeparam name="T">The type of the object passed in the field.</typeparam>
    public abstract class InterfacePropertyDrawer<T> : PropertyDrawer
        where T: UnityEngine.Object
    {
        protected abstract T GetComponentWithType(T component, Type type);      

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {            
            Type targetType = TypeDetector.GetTargetObjectOfProperty(property).GetType();        

            Type interfaceType = targetType.GetGenericArguments()[0];

            DrawProperty(position, property, label, interfaceType);
        }

        private void DrawProperty(
            Rect position, 
            SerializedProperty property, 
            GUIContent label, 
            Type interfaceType)
        {
            EditorGUI.BeginProperty(position, label, property);

            var objectWithInterface = property.FindPropertyRelative("_objectWithInterface");

            var indent = EditorGUI.indentLevel;
            
            EditorGUI.indentLevel = 0;
            EditorGUI.BeginChangeCheck();
            T inputVal = EditorGUI.ObjectField(
                    position,
                    label,
                    objectWithInterface.objectReferenceValue,
                    typeof(T),
                    true) as T;

            if (inputVal != null && objectWithInterface.objectReferenceValue != inputVal)
            {
                T component = GetComponentWithType(inputVal, interfaceType);
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
