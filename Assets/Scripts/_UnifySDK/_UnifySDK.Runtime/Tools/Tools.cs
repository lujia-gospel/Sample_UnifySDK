using System;
using System.Collections.Generic;
using System.Text;
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif
using UnityEngine;
using XLua;

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
        [BlackList]
        public static void GetScriptPath(string scriptName ,ref string result)
        {
            if (!string.IsNullOrEmpty(scriptName))
            {
                string[] assetPaths = AssetDatabase.FindAssets($"t:script {scriptName}", new[] {"Assets"});

                string scriptPath = string.Empty;
                foreach (var assetPath in assetPaths)
                {
                   var path =AssetDatabase.GUIDToAssetPath(assetPath);
                   if (Path.GetFileName(path) == $"{scriptName}.cs")
                   {
                       scriptPath = path;
                       break;
                   }
                }
                if(string.IsNullOrEmpty(scriptPath))
                    UDebug.Sys.LogError($" {scriptName}.cs 没有该脚本");
                else
                    result = scriptPath.Replace($"{scriptName}.cs","Resources");
            }
        }
#endif
        public static void WriteFileString(string path, string data)
        {
            string p = System.IO.Path.GetDirectoryName(path);
            if (!System.IO.Directory.Exists(p))
            {
                System.IO.Directory.CreateDirectory(p);
            }

            System.IO.File.WriteAllText(path, data, new UTF8Encoding(false));
        }

        public static string ReadFileString(string path)
        {
            if (System.IO.File.Exists(path))
            {
                return System.IO.File.ReadAllText(path);
            }

            return string.Empty;
        }
    }
}


