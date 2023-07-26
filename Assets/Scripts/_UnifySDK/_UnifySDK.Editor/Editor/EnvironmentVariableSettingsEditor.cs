using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnifySDK.Editor
{
    [CustomEditor(typeof(EnvironmentVariableSettings))]
    public class EnvironmentVariableSettingsEditor :UnityEditor.Editor
    {
        private EnvironmentVariableSettings settings;
        private string[] eventNameArr;
        private int selectIndex;
        private string[] canAddEventNameArr;
        public void OnEnable()
        {
            settings = target as EnvironmentVariableSettings;
            UpdateCanAddEventNameArr();
        }

        private void UpdateCanAddEventNameArr()
        {
            var unifySDKAssetsSettings = UnifySDKAssetsSetting.GetUnifySDKAssetsSettings();
            eventNameArr = new String [unifySDKAssetsSettings.Length];
            List<string> canAddList = new List<string>();
            for (int i = 0; i < unifySDKAssetsSettings.Length; i++)
            {
                eventNameArr[i] = unifySDKAssetsSettings[i].name;
                if (!settings.Keys.Contains(eventNameArr[i]))
                {
                    canAddList.Add(eventNameArr[i]);
                }
            }
            canAddEventNameArr = canAddList.ToArray();
        }

        private string sdkConfigName;
        private bool _showDictionary = true;
        private string _statusStr = "要打进包的SDK列表";    
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Space(10);
            GUILayout.BeginVertical();
            _showDictionary = EditorGUILayout.BeginFoldoutHeaderGroup(_showDictionary, _statusStr);
            if (_showDictionary)
            { 
                GUILayout.BeginHorizontal();
                GUILayout.Label("SDK名",GUILayout.Width(150.0f));
                GUILayout.Label("对应的配置表",GUILayout.ExpandWidth(false)); 
                GUILayout.EndHorizontal();
                for (int i = 0; i < settings.Keys.Count; i++)
                {
                    GUILayout.BeginHorizontal();
         
                    GUILayout.TextField(settings.Keys[i],GUILayout.Width(150.0f));
              
                    var v= GUILayout.TextField(settings.Values[i]);
                    if (v!=settings.Values[i])
                    {
                        settings.SetSDKValue(settings.Keys[i],v); 
                    }
                    if (GUILayout.Button("DeleteSDK", GUILayout.MinWidth(60.0f), GUILayout.MaxWidth(100.0f)))
                    {
                        settings.DeleteSDKValue(i);
                        UpdateCanAddEventNameArr();
                    }
                    GUILayout.EndHorizontal();
                } 
            }
            GUILayout.EndVertical();
            GUILayout.Space(10);
            if (canAddEventNameArr.Length==0)
                return;
            GUILayout.BeginHorizontal();
            UpdateCanAddEventNameArr();
            //GUILayout.Width(.width), GUILayout.Height(70);
            if (selectIndex>canAddEventNameArr.Length-1)
                selectIndex = 0;
            int index = selectIndex;
            GUILayout.Label("可添加的SDK：",GUILayout.ExpandWidth(false));
            //首先获取枚举中的所有名字数组，通过 Popup将数组显示为下拉框，返回选中的项
            selectIndex = EditorGUILayout.Popup(index, canAddEventNameArr);
            sdkConfigName =  GUILayout.TextField(sdkConfigName,GUILayout.MinWidth( 150.0f));
            if (GUILayout.Button("AddSDK",GUILayout.MinWidth(60.0f),GUILayout.MaxWidth(100.0f)))
            {
                if (string.IsNullOrEmpty(sdkConfigName))
                {
                    EditorUtility.DisplayDialog("AddSDK提示","请填写SDK拥有的配置表名", "确定");
                }
                else
                {
                    string strTip = $"是否添加该SDK{canAddEventNameArr[selectIndex]},请确保该SDK的拥有该{sdkConfigName}配置表";
                    if (EditorUtility.DisplayDialog("AddSDK提示", strTip, "确定", "取消"))
                    {
                        settings.SetSDKValue(canAddEventNameArr[selectIndex],sdkConfigName);
                        selectIndex = selectIndex>0? selectIndex--: selectIndex;
                        sdkConfigName = "";
                    }
                }
            }
            GUILayout.EndVertical();
        }
    }
}

