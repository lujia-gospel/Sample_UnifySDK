using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEditor.Build.Reporting;

namespace UnifySDK.Editor {

    public class UnifySDKAssetsEditor : EditorWindow
    {
        private const string ExternalRootPath = "./Tools/SdkFrame";
        [MenuItem("Tools/UnifySDK/资源配置")]
        static void ShowWindow()
        {
            GetWindow<UnifySDKAssetsEditor>();
        }
        private UnifySDKAssetsSetting _unifySDKAssetsSetting;
        private UnifySDKAssetsSetting[] _unifySDKAssetsSettings;
        private Vector2 m_ScrollViewVector1;
        private Vector2 m_ScrollViewVector2;
        private ReorderableList m_ReorderableList2;
        private bool m_IsForceText;
        private int selectedOption = 0;
        private string sdkFieldName="Your SDK Name";
        private void Awake()
        {
            titleContent.text = "UnifySDK资源配置";
        }

        private void OnGUI()
        {
            Init();
            
            var style = UnifySDKAssetStyle.Get();

            if (!m_IsForceText)
            {
                EditorGUILayout.LabelField(style.forceText);
                return;
            }
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            
            GUILayout.FlexibleSpace();
            
            EditorGUILayout.EndHorizontal();
            
            AssetsSettingsOnGUI();
            
            AssetsSettingsToolOnGUI();
            
            CurrentAssetsSettingOnGUI();
        }
        private void Init() 
        {
            if (_unifySDKAssetsSetting == null)
            {
                m_IsForceText = EditorSettings.serializationMode == SerializationMode.ForceText;
                if (!m_IsForceText)
                { 
                    return;
                }
                _unifySDKAssetsSettings = UnifySDKAssetsSetting.GetUnifySDKAssetsSettings();
            }
        }

        private void AssetsSettingsOnGUI()
        {
            if (_unifySDKAssetsSettings.Length>0)
            {
                m_ScrollViewVector1 = GUILayout.BeginScrollView(m_ScrollViewVector1,GUILayout.Width(position.width), GUILayout.Height(70));
               
                GUILayout.BeginHorizontal();
                // 在这里添加需要的水平列表项
                for (int i = 0; i < _unifySDKAssetsSettings.Length; i++)
                {
                    Color bc = GUI.backgroundColor;
                    if (i==selectedOption)
                        GUI.backgroundColor = Color.grey;
                    float width = Encoding.Default.GetByteCount(_unifySDKAssetsSettings[i].name);
                    if (GUILayout.Button(_unifySDKAssetsSettings[i].name, GUILayout.Width(width*10+2), GUILayout.Height(30)))
                    {
                        selectedOption = i;
                    }
                    GUI.backgroundColor = bc;
                }

                GUILayout.EndHorizontal();
                if (_unifySDKAssetsSetting!=_unifySDKAssetsSettings[selectedOption])
                {
                    _unifySDKAssetsSetting = _unifySDKAssetsSettings[selectedOption];
                    m_ReorderableList2 = new ReorderableList(_unifySDKAssetsSetting.UnifySDKResPathInfos, null, true, true, true, true);
                    m_ReorderableList2.drawHeaderCallback = OnDrawHeaderCallback;
                    m_ReorderableList2.drawElementCallback = OnDrawElementCallback;
                    m_ReorderableList2.elementHeight += 8;
                }
                EditorGUILayout.LabelField("Selected Setting: " + _unifySDKAssetsSetting.name);
                GUILayout.EndScrollView();
            }
        }

        private void AssetsSettingsToolOnGUI()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("根据配置移动到外部", GUILayout.Width(130), GUILayout.Height(20)))
            {
                MoveToProject(_unifySDKAssetsSetting,true,true);
            }
            
            if (GUILayout.Button("根据配置移动到工程内", GUILayout.Width(140), GUILayout.Height(20)))
            {
                MoveToProject(_unifySDKAssetsSetting);
            }

            var btnStr = $"删除当前{_unifySDKAssetsSetting.name}资源配置";
            var btnWidth = Encoding.Default.GetByteCount(_unifySDKAssetsSetting.name)*10+100;
            if (GUILayout.Button(btnStr, GUILayout.Width(btnWidth), GUILayout.Height(20)))
            {
                string strTip = $"是否删除该{_unifySDKAssetsSetting.name}资源配置文件";
                if (EditorUtility.DisplayDialog("提示",strTip , "确定","取消"))
                {
                    AssetDatabase.DeleteAsset($"{UnifySDKAssetsSetting.SavePath}/{_unifySDKAssetsSetting.name}.asset");
                    _unifySDKAssetsSetting = null;
                    selectedOption--;
                    AssetDatabase.Refresh();
                }
            }
            bool isAdd= GUILayout.Button("添加资源配置", GUILayout.Width(100), GUILayout.Height(20));
            sdkFieldName =  GUILayout.TextField(sdkFieldName,GUILayout.Width(position.width-(btnWidth+390)),GUILayout.Height(20));
            if (isAdd)
            {
                bool isNeedTip = string.IsNullOrEmpty(sdkFieldName)|| sdkFieldName=="Your SDK Name";
                string strTip = "你的SDK名字不能为空或者是\"Your SDK Name\"";
                for (int i = 0; i < _unifySDKAssetsSettings.Length; i++)
                {
                    if (_unifySDKAssetsSettings[i].name == sdkFieldName)
                    {
                        isNeedTip = true;
                        strTip = $"该 {sdkFieldName} 已经存在";
                    }
                }
                if (isNeedTip)
                    EditorUtility.DisplayDialog("提示", strTip, "确定");
                else
                {
                    if (CreateUnifySDKAsset(sdkFieldName))
                    {
                        _unifySDKAssetsSetting = null;
                    }
                }
            }
            GUILayout.EndHorizontal();
        }

        private void CurrentAssetsSettingOnGUI()
        {
            if (m_ReorderableList2 != null)
            {
                m_ScrollViewVector2 = GUILayout.BeginScrollView(m_ScrollViewVector2);
                m_ReorderableList2.DoLayoutList();
                GUILayout.EndScrollView();
            }
        }

        private void OnDrawHeaderCallback(Rect rect)
        {
            EditorGUI.LabelField(rect, UnifySDKAssetStyle.Get().unifySDKTitle);
        }

        private void OnDrawElementCallback(Rect rect, int index, bool isactive, bool isfocused)
        {
            if (_unifySDKAssetsSetting == null || _unifySDKAssetsSetting.UnifySDKResPathInfos.Count < index)
            {
                return;
            }

            var style = UnifySDKAssetStyle.Get();
            var info = _unifySDKAssetsSetting.UnifySDKResPathInfos[index];
            rect.height = EditorGUIUtility.singleLineHeight;
            rect.y += 2;
 
            EditorGUI.BeginChangeCheck();
            Rect rect2 = new Rect(rect) { width = 100f };
            Rect rect3 = new Rect(rect) { x = rect2.x + rect2.width, width = rect.width - rect2.width };
            bool valueChanged = EditorGUI.EndChangeCheck();

            EditorGUI.LabelField(rect2, style.assetPathTitle);
            EditorGUI.BeginChangeCheck();
            
            info.assetFolder = EditorGUI.TextField(rect3, info.assetFolder);
            var pathStrs= OnDrawElementAcceptDrop(rect3, info.assetFolder);
            info.assetFolder =pathStrs.Length>0 ? pathStrs[0]:"";
            for (int i = 1; i < pathStrs.Length; i++)
            {
                _unifySDKAssetsSetting.UnifySDKResPathInfos.Add(new UnifySDKAssetsSetting.UnifySDKAssetInfo(pathStrs[i]));
            }
            valueChanged |= EditorGUI.EndChangeCheck();

            if (valueChanged)
            {
                EditorUtility.SetDirty(_unifySDKAssetsSetting);
                AssetDatabase.SaveAssets();
                Repaint();
            }
        }

        private string[] OnDrawElementAcceptDrop(Rect rect, string label)
        {
         
            if (rect.Contains(UnityEngine.Event.current.mousePosition))
            {
                if (DragAndDrop.paths != null && DragAndDrop.paths.Length > 0 && !string.IsNullOrEmpty(DragAndDrop.paths[0]))
                {
                    if (UnityEngine.Event.current.type == EventType.DragUpdated ||    UnityEngine.Event.current.type == EventType.DragPerform)
                    {
                        DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                    }

                    if (UnityEngine.Event.current.type == EventType.DragPerform)
                    {
                        DragAndDrop.AcceptDrag();
                        GUI.changed = true;

                        return PathArrayToStr(DragAndDrop.paths);
                    }
                }
            }

            return new string[] {label};
        }

        public static string[] PathArrayToStr(string[] paths)
        {
            var pathStrs = new string[paths.Length];
            for (int i = 0; i < paths.Length; i++)
            {
                pathStrs[i] =  paths[i].Replace("Assets/",string.Empty);
            }
           //string.Join("|", paths) ;
            return pathStrs;
        }

        static bool CreateUnifySDKAsset(string name)
        {
            try
            {
                //将对象实例化  
                ScriptableObject so = CreateInstance<UnifySDKAssetsSetting>();  
        
                if (so == null)  
                {  
                    UDebug.Sys.LogError("该对象无效，无法将对象实例化");  
                    return false;  
                } 
            
                var assetPath= $"{UnifySDKAssetsSetting.SavePath}/{name}.asset";
                //按指定路径生成配置文件  
                AssetDatabase.CreateAsset(so,assetPath);
                AssetDatabase.SaveAssets();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }
        
        public static void MoveToProject(UnifySDKAssetsSetting setting,bool isReverse=false,bool isDelSource=false) 
        { 
            if (setting == null)
            {
                UDebug.Sys.LogError("UnifySDKAssetsSetting 配置文件为空 ");
                return;
            }
            string sourcePath ,targetPath = "";
            string externalPath = $"{ExternalRootPath}/{setting.name}";
           
            foreach (var info in setting.UnifySDKResPathInfos)
            {
                sourcePath = isReverse? $"{Application.dataPath}/{info.assetFolder}" : $"{externalPath}/{info.assetFolder}";
                targetPath = isReverse? $"{externalPath}/{info.assetFolder}" : $"{Application.dataPath}/{info.assetFolder}";
                DirectoryInfo sourceDireInfo = new DirectoryInfo(sourcePath);
                DirectoryInfo destDire = new DirectoryInfo(targetPath);
                CopyDireToDire(sourceDireInfo.FullName, destDire.FullName,isDelSource);
            }
            AssetDatabase.Refresh();
        }

        public static void DeleteExternalSDKAssets(UnifySDKAssetsSetting setting,bool clearConfigSetting=false)
        {
            string externalPath = $"{ExternalRootPath}/{setting.name}";
            if (Directory.Exists(externalPath))
                Directory.Delete(externalPath);
            if (clearConfigSetting)
                setting.UnifySDKResPathInfos.Clear();
        }

        [MenuItem("Tools/Unify/TestBuildApk")]
        public static void TestBuildApk()
        {
            BuildBeforeSetSDK();
            var apkPath = "./APK/TestBuildApk.apk";
            BuildReport buildReport = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, apkPath,
                BuildTarget.Android,
                BuildOptions.CompressWithLz4HC);
            AssetDatabase.Refresh();
        }

        public static void BuildBeforeSetSDK()
        {
            try
            {
                BuildBeforeClearSDKAssets();
                
                for (int i = 0; i < Math.Min(EnvironmentVariableSettings.Instance.Keys.Count, EnvironmentVariableSettings.Instance.Values.Count); i++)
                {
                    var assetsSetting =   UnifySDKAssetsSetting.GetUnifySDKAssetsSetting(EnvironmentVariableSettings.Instance.Keys[i]);
                    MoveToProject(assetsSetting);
                }
            }
            catch (Exception e)
            {
                UDebug.Sys.LogError(e);
                throw;
            }
        }

        public static void BuildBeforeClearSDKAssets()
        {
            var list = UnifySDKAssetsSetting.GetUnifySDKAssetsSettings();
            for (int i = 0; i < list.Length; i++)
            {
                DeleteInternalSDKAssets(list[i]);
            }
        }

        public static void DeleteInternalSDKAssets(UnifySDKAssetsSetting setting,bool clearConfigSetting=false)
        {
            foreach (var info in setting.UnifySDKResPathInfos)
            {
                string sourcePath = $"{Application.dataPath}/{info.assetFolder}";
                DirectoryInfo sourceDireInfo = new DirectoryInfo(sourcePath);
                DeleteDire(sourceDireInfo.FullName);
            }
            if (clearConfigSetting)
                setting.UnifySDKResPathInfos.Clear();
        }

        /// <summary>
        /// 将一个文件夹下的所有东西复制到另一个文件夹或者文件  (可备份文件夹)
        /// </summary>
        /// <param name="sourceDire">源文件夹全名</param>
        /// <param name="destDire">目标文件夹全名</param>
        /// <param name="backupsDire">备份文件夹全名</param>
        public static void CopyDireToDire(string sourceDire, string destDire,bool isDelSource=false, string backupsDire = null)
        {
            if (File.Exists(sourceDire))
            {
                var path = Path.GetDirectoryName(destDire);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                File.Copy(sourceDire, destDire, true);
                var sourceDireMeta = sourceDire + ".meta";
                if (File.Exists(sourceDireMeta))
                    File.Copy(sourceDireMeta, destDire+".meta", true);
                if (isDelSource)
                {
                    File.Delete(sourceDire); 
                    if (File.Exists(sourceDireMeta))
                        File.Delete(sourceDireMeta);
                }
            }
            else if (!Directory.Exists(destDire))
                Directory.CreateDirectory(destDire);

            if (Directory.Exists(sourceDire))
            {
                DirectoryInfo sourceDireInfo = new DirectoryInfo(sourceDire);
                FileInfo[] fileInfos = sourceDireInfo.GetFiles();
                foreach (FileInfo fInfo in fileInfos)
                {
                    string sourceFile = fInfo.FullName;
                    string destFile = sourceFile.Replace(sourceDireInfo.FullName, destDire);
                    if (backupsDire != null && File.Exists(destFile))
                    {
                        Directory.CreateDirectory(backupsDire);
                        string backFile = destFile.Replace(sourceDireInfo.FullName, backupsDire);
                        File.Copy(destFile, backFile, true); 
                    }
                    File.Copy(sourceFile, destFile, true);
                    if (isDelSource)
                        File.Delete(sourceFile);
                }
                DirectoryInfo[] direInfos = sourceDireInfo.GetDirectories();
                foreach (DirectoryInfo dInfo in direInfos)
                {
                    string sourceDire2 = dInfo.FullName;
                    string destDire2 = sourceDire2.Replace(sourceDireInfo.FullName, destDire);
                    string backupsDire2 = null;
                    if (backupsDire != null)
                    {
                        backupsDire2 = sourceDire2.Replace(sourceDireInfo.FullName, backupsDire);
                    }
                    Directory.CreateDirectory(destDire2);
                    CopyDireToDire(sourceDire2, destDire2,isDelSource,backupsDire2);
                }
                var sourceDireMeta = sourceDire + ".meta";
                if (File.Exists(sourceDireMeta))
                    File.Copy(sourceDireMeta, destDire+".meta", true);
                if (isDelSource)
                {
                    Directory.Delete(sourceDire);
                    if (File.Exists(sourceDireMeta))
                        File.Delete(sourceDireMeta);
                }
            }
        }
         /// <summary>
        /// 删除一个文件夹或者文件 
        /// </summary>
        /// <param name="deleteDire">删除的文件夹全名</param>
        public static void DeleteDire(string deleteDire)
        {
            if (File.Exists(deleteDire))
            {        
                File.Delete(deleteDire); 
                var deleteDireMeta = deleteDire + ".meta";
                if (File.Exists(deleteDireMeta))
                    File.Delete(deleteDireMeta);
            }
            if (Directory.Exists(deleteDire))
            {
                Directory.Delete(deleteDire,true);
                var deleteDireMeta = deleteDire + ".meta";
                if (File.Exists(deleteDireMeta))
                    File.Delete(deleteDireMeta);
            }
        }
    }


    public class UnifySDKAssetStyle
    {
        public class Style
        {
            public GUIContent unifySDKTitle = new GUIContent("unifySDK资源分类列表");
            public GUIContent assetPathTitle = new GUIContent("资源目录");
            public GUIContent forceText = new GUIContent("Asset Serialization must be ForceText");

            public GUIStyle labelStyle;

            public void InitGUI()
            {
                if (labelStyle == null)
                {
                    labelStyle = new GUIStyle(EditorStyles.label);
                    labelStyle.alignment = TextAnchor.MiddleRight;
                }
            }
        }

        private static Style sStyle;

        public static Style Get()
        {
            if (sStyle == null)
            {
                sStyle = new Style();
            }

            return sStyle;
        }
    }
}
