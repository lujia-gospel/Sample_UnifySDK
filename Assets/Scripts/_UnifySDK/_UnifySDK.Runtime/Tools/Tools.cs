using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace UnifySDK{
    
    public class Tools
    {
        public static List<T> GetTypesByBaseClass<T>()
        {
            var list = new List<T>();
            var types =typeof(BaseUnifySDKFactory).Assembly.GetTypes();
            var aType = typeof(T);
            foreach (var type in types)
            {
                var baseType = type.BaseType;  //获取基类
                while (baseType != null)  //获取所有基类
                { 
                    if (baseType.Name == aType.Name)
                    {
                        Type objtype = Type.GetType(type.FullName, true);
                        object obj = Activator.CreateInstance(objtype);
                        if (obj != null)
                        { 
                            list.Add( (T)obj );
                        }
                        break;
                    }
                    else
                    {
                        baseType = baseType.BaseType;
                    }
                }
            }
            return list;
        }
#if UNITY_EDITOR
        public static void GetScriptPath(string scriptName ,ref string result)
        {
            if (!string.IsNullOrEmpty(scriptName))
            {
                string[] paths = AssetDatabase.FindAssets($"t:script {scriptName}", new[] {"Assets"});
                if(paths.Length>1)
                    Debug.LogError($"有同名脚本文件 {scriptName} 获取路径失败");
                else if (paths.Length > 0)
                    result = AssetDatabase.GUIDToAssetPath(paths[0]).Replace($"{scriptName}.cs","Resources");
                else
                    Debug.LogError($" {scriptName}.cs 没有该脚本");
            }
        }
#endif
    }
}


