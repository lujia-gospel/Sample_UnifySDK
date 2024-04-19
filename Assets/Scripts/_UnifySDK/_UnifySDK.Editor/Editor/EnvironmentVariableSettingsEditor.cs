using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace UnifySDK.Editor
{
    [CustomEditor(typeof(EnvironmentVariableSettings))]
    public class EnvironmentVariableSettingsEditor :UnityEditor.Editor
    {
        public const string LocalUnifySDKRecord="LocalUnifySDKRecord";
        
        private EnvironmentVariableSettings settings; 
        private Dictionary<string,string []> configInfoDict; 
        private string[] eventNameArr;
        private int selectIndex;
        private string[] canAddEventNameArr;
        public void OnEnable()
        {
            settings = target as EnvironmentVariableSettings;
            configInfoDict = GetUnifySDKConfigInfoDict();
            UpdateCanAddEventNameArr();
            templateName = string.Empty;
            AnalysisTemplate();
        }

        public void OnDisable()
        {
            if (PlayerPrefs.GetString(LocalUnifySDKRecord,"None")!=templateArr[templateIndex])
                SaveTemplate(templateArr[templateIndex]);
            AssetDatabase.SaveAssetIfDirty(target);
        }

        private void UpdateCanAddEventNameArr()
        {
            var unifySDKTypes = Enum.GetValues(typeof(UnifySDKType)).Cast<UnifySDKType>().Select(e => e.ToString());
            var unifyCustomPlugins = Enum.GetValues(typeof(UnifyCustomPlugin)).Cast<UnifyCustomPlugin>().Select(e => e.ToString());

            var allTypes = unifySDKTypes.Concat(unifyCustomPlugins).Distinct().Where(type => type != "All").ToArray();
            
    
            var canAddList = new List<string>();
            foreach (var eventName in allTypes)
            {
                if (!configInfoDict.ContainsKey(eventName))
                {
                    configInfoDict[eventName] = new[] { "Unique" };
                }
                if (!settings.Keys.Contains(eventName))
                {
                    canAddList.Add(eventName);
                }
            }
            canAddEventNameArr = canAddList.ToArray();
        }

        private string sdkConfigName;
        private bool _showDictionary = true;
        private string _statusStr = "要打进包的SDK列表";    
        bool isChange ;
        public override void OnInspectorGUI()
        {
            isChange = false;
            base.OnInspectorGUI();
            GUILayout.Space(10);
            GUILayout.BeginVertical();
            TemplateOnGUI();
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

                    if (configInfoDict.ContainsKey(settings.Keys[i]))
                    {
                        var configValues = configInfoDict[settings.Keys[i]];
                        int tempIndex  = Array.IndexOf(configValues,settings.Values[i]);
                        var tempIndex2 = EditorGUILayout.Popup(tempIndex, configValues);
                        if (tempIndex==-1)
                            GUILayout.TextField($"error:{settings.Values[i]}");
                        if (tempIndex2!=tempIndex)
                        {
                            isChange = true;
                            settings.SetSDKValue(settings.Keys[i],configValues[tempIndex2]);
                            EditorUtility.SetDirty(target);
                        }
                    }
                    else
                    {
                        GUILayout.TextField($"error:{settings.Values[i]}");
                    }

                    if (GUILayout.Button("DeleteSDK", GUILayout.MinWidth(60.0f), GUILayout.MaxWidth(100.0f)))
                    {
                        isChange = true;
                        settings.DeleteSDKValue(i);
                        UpdateCanAddEventNameArr();
                        EditorUtility.SetDirty(target);
                    }
                    GUILayout.EndHorizontal();
                } 
                
                GUILayout.Space(10);
                if (canAddEventNameArr.Length == 0)
                {
                    if (isChange)
                    {
                        SaveTemplate(templateArr[templateIndex]);
                        AnalysisTemplate();
                    }
                    AssetDatabase.SaveAssetIfDirty(target);
                    return;
                }
                GUILayout.BeginHorizontal();
                UpdateCanAddEventNameArr();
                //GUILayout.Width(.width), GUILayout.Height(70);
                if (selectIndex>canAddEventNameArr.Length-1)
                    selectIndex = 0;
                int index = selectIndex;
                GUILayout.Label("可添加的SDK：",GUILayout.ExpandWidth(false));
                //首先获取枚举中的所有名字数组，通过 Popup将数组显示为下拉框，返回选中的项
                selectIndex = EditorGUILayout.Popup(index, canAddEventNameArr);
                //sdkConfigName =  GUILayout.TextField(sdkConfigName,GUILayout.MinWidth( 150.0f));
               
                if (GUILayout.Button("AddSDK",GUILayout.MinWidth(60.0f),GUILayout.MaxWidth(100.0f)))
                {
                    // if (string.IsNullOrEmpty(sdkConfigName))
                    // {
                    //     EditorUtility.DisplayDialog("AddSDK提示","请填写SDK拥有的配置表名", "确定"); 
                    // }
                    // else
                    {
                        // string strTip = $"是否添加该SDK{canAddEventNameArr[selectIndex]},请确保该SDK的拥有该{sdkConfigName}配置表";
                        // if (EditorUtility.DisplayDialog("AddSDK提示", strTip, "确定", "取消"))
                        {
                            
                            settings.SetSDKValue(canAddEventNameArr[selectIndex],configInfoDict[canAddEventNameArr[selectIndex]][0]);
                            selectIndex = selectIndex>0? selectIndex--: selectIndex;
                            sdkConfigName = "";
                            isChange = true;
                            EditorUtility.SetDirty(target);
                        }
                    }
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
            
            if (isChange)
            {
                SaveTemplate(templateArr[templateIndex]);
                AnalysisTemplate();
            }
            
            //AssetDatabase.SaveAssetIfDirty(target);
         
        }
        
        private int templateIndex;
        private string[] templateArr;
        private string templateName;
        private Dictionary<string, Dictionary<string, string>> templateDic;
        private void AnalysisTemplate()
        {
            templateDic=new Dictionary<string, Dictionary<string, string>>();
            //var settingsDic = settings.GetDic();
            // templateDic["None"] = new  Dictionary<string, string>();  
            // foreach (var kv in settingsDic)
            // {
            //     templateDic["None"].Add(kv.Key,kv.Value);
            // }
            string sdkModel = PlayerPrefs.GetString(LocalUnifySDKRecord,"None");
           
            var temp = Tools.ReadFileString($"{EnvironmentVariableSettings.SavePath}/SDKListTemplate.txt");
            try
            {
                var cache= LitJson.JsonMapper.ToObject<Dictionary<string, Dictionary<string, string>>>(temp);
                foreach (var kv in cache)
                {
                    templateDic.Add(kv.Key,kv.Value);
                }
            }
            catch (Exception e)
            {
                UDebug.Sys.LogError(e);
                throw;
            }
            finally
            {
                templateArr = templateDic.Keys.ToArray();
                for (int i = 0; i < templateArr.Length; i++)
                {
                    if (sdkModel==templateArr[i])
                    {
                        templateIndex = i;
                        break;
                    }
                }
                settings.UpdateDic(templateDic[templateArr[templateIndex]]);
            }
        }

        private void SaveTemplate(string name)
        {
            templateDic[name] = settings.GetDic();
            if (!templateArr.Contains(name))
            {
                templateArr = templateArr.Concat(new []{name}).ToArray();
                templateIndex = templateArr.Length-1;
            }
            PlayerPrefs.SetString(LocalUnifySDKRecord,name);
            var saveStr = LitJson.JsonMapper.ToJson(templateDic);
            Tools.WriteFileString($"{EnvironmentVariableSettings.SavePath}/SDKListTemplate.txt",saveStr);
            AssetDatabase.Refresh();
        }

        private void TemplateOnGUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("SDK列表模板：",GUILayout.ExpandWidth(false));
          
            int index = templateIndex;
            //首先获取枚举中的所有名字数组，通过 Popup将数组显示为下拉框，返回选中的项
            templateIndex = EditorGUILayout.Popup(index, templateArr);
            if (templateIndex!=index)
            {
                settings.UpdateDic(templateDic[templateArr[templateIndex]]);
                EditorUtility.SetDirty(target);
            }
            if (templateArr[templateIndex] == "None")
            {
                templateName = GUILayout.TextField(templateName,GUILayout.MinWidth( 150.0f));
            }

            if (templateArr[templateIndex] == "None")
            {
                if (GUILayout.Button("添加SDK模板", GUILayout.MinWidth(80.0f), GUILayout.MaxWidth(100.0f)))
                {
                    if (string.IsNullOrEmpty(templateName) || templateName == "None")
                        EditorUtility.DisplayDialog("添加SDK模板Tip", "添加的模板名称不能为空或者为None", "确定");
                    else if (templateDic.ContainsKey(templateName))
                    {
                        if (EditorUtility.DisplayDialog("添加SDK模板Tip", $"是否要覆盖{templateName}该SDK模板", "确定"))
                        {
                            SaveTemplate(templateName);
                        }
                    }
                    else
                    {
                        SaveTemplate(templateName);
                    }
                    AnalysisTemplate();
                }
            }
           

            if ( templateArr[templateIndex]!="None" )
            {
                if (GUILayout.Button("删除SDK模板", GUILayout.MinWidth(80.0f), GUILayout.MaxWidth(100.0f)))
                {
                    templateDic.Remove(templateArr[templateIndex]);
                    var saveStr = LitJson.JsonMapper.ToJson(templateDic);
                    Tools.WriteFileString($"{EnvironmentVariableSettings.SavePath}/SDKListTemplate.txt",saveStr);
                    AnalysisTemplate();
                    //AssetDatabase.Refresh();
                }
            }
            GUILayout.EndHorizontal();
        }
        
        public static Dictionary<string,string[]> GetUnifySDKConfigInfoDict()
        {
            Dictionary<string,string[]> dict=new ();
            var list= Tools.GetTypesByBaseClass<BaseUnifySDKConfig>();
            foreach (var kv in list)
            {
                var sdk = kv.GetType().Name.Replace("_UnifySDKConfig",string.Empty);
                dict[sdk] = kv.GetTargetPlatforms();
            }
            return dict;
        }
    }
}

