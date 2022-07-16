using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;

namespace AMoltkoff.UStrings
{
    [CustomPropertyDrawer(typeof(string))]
    public class UStringsPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var target = property.serializedObject.targetObject;
            var targetType = target.GetType();
            var fieldName = property.name;
            var field = targetType.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            var binderAttributes = field.GetCustomAttributes(typeof(UStringsAttribute), true);

            if (binderAttributes.Length > 0)
            {
                var uStringsAttribute = (UStringsAttribute)binderAttributes[0];
                var uStrings = uStringsAttribute.Values;
                var uString = property.stringValue;

                var popupPosition = EditorGUI.PrefixLabel(position, label);
                var uStringIndex = Array.IndexOf(uStrings, uString);
                var newUStringIndex = EditorGUI.Popup(popupPosition, uStringIndex, uStrings);

                if (newUStringIndex != uStringIndex)
                {
                    property.stringValue = uStrings[newUStringIndex];
                    property.serializedObject.ApplyModifiedProperties();
                }
            }
            else
                EditorGUI.PropertyField(position, property, label);
        }
    }
}