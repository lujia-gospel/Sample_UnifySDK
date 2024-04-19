using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEditor;

namespace UnifySDK.Editor
{
    [CustomPropertyDrawer(typeof(CustomHeaderAttribute))]
    public class CustomHeaderPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            CustomHeaderAttribute customHeader = (CustomHeaderAttribute)attribute;
            EditorGUI.BeginProperty(position, null, property);
            Rect tipRect = new Rect(position);
            var tipWidth = Encoding.Default.GetByteCount(label.text)*6+40;
            switch (property.propertyType)      
            {
                case SerializedPropertyType.Boolean:
                    if (customHeader.Headlines.Length > 1)
                        label.text = property.boolValue? customHeader.Headlines[0]: customHeader.Headlines[1];
                    else if (customHeader.Headlines.Length > 0)
                        label.text = customHeader.Headlines[0];
                    tipRect.width = tipWidth;
                    break;
                case SerializedPropertyType.Enum:
                    if (property.enumValueIndex < 0)
                        label.text = "error:None";
                    else if (property.enumValueIndex < customHeader.Headlines.Length )
                        label.text = customHeader.Headlines[property.enumValueIndex];
                    else if (customHeader.Headlines.Length > 0)
                        label.text = $"error:枚举提示数量不足";
                    break;
                default:
                    if (customHeader.Headlines.Length > 0)
                        label.text = customHeader.Headlines[0];
                    break;
            }
        
            EditorGUI.LabelField(tipRect,label);
            EditorGUI.PropertyField(position, property, true);
            EditorGUI.EndProperty();
        }
    }
}