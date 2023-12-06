using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
//using RuntimeInspectorNamespace;
using UnityEditor;

namespace UnifySDK.Editor
{
    public class UnifyLuaAutoGenEditor
    {
        private const string EventDataPath = "./Assets/LuaProject/sdk/unifysdk/remark_unifysdk_eventdata.lua";
        private const string InterfacePath = "./Assets/LuaProject/sdk/unifysdk/remark_unifysdk_interface.lua";

        [MenuItem("Tools/UnifySDK/自动生成lua代码")]
        private static void UnifyLuaAutoGen()
        {
            UnifyLuaAutoGen_EventData();
            UnifyLuaAutoGen_Interface();
        }

        private static void UnifyLuaAutoGen_Interface()
        {
            if (!File.Exists(InterfacePath))
                return;
            var allInterFace = GetAttr<UnifySDKInterfaceAttribute>();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            foreach (var interFace in allInterFace)
            {
                sb.AppendLine($"---@class {interFace.Name} : CS.{interFace.FullName}");
                var methodInfos = interFace.GetMethods();
                var properties = interFace.GetProperties();
                foreach (var methodInfo in methodInfos)
                {
                    if (methodInfo.IsSpecialName)
                        continue;
                    var returnType = GetTypeName(methodInfo.ReturnType);
                    var paramsStr = GetParams(methodInfo);
                    if (string.IsNullOrEmpty(paramsStr))
                        sb.AppendLine($"---@field {methodInfo.Name} fun(param:self):{returnType}");
                    else
                        sb.AppendLine($"---@field {methodInfo.Name} fun(param:self,{paramsStr}):{returnType}");
                }

                foreach (var property in properties)
                {
                    var propertyType = GetTypeName(property.PropertyType);
                    sb.AppendLine($"---@field {property.Name} {propertyType}");
                }

                sb.AppendLine();
            }

            sb.AppendLine("EnvironmentVariableSettings = CS.UnifySDK.EnvironmentVariableSettings.Instance");
            File.WriteAllText(InterfacePath, sb.ToString());
        }

        private static void UnifyLuaAutoGen_EventData()
        {
            if (!File.Exists(EventDataPath))
                return;
            var allEventData = GetAttr<UnifySDKEventDataAttribute>();
            Dictionary<string, System.Collections.Generic.List<Type>> dicType =
                new Dictionary<string, System.Collections.Generic.List<Type>>();

            foreach (var t in allEventData)
            {
                if (dicType.ContainsKey(t.Namespace))
                {
                    dicType[t.Namespace].Add(t);
                }
                else
                    dicType.Add(t.Namespace, new System.Collections.Generic.List<Type> { t });
            }

            StringBuilder sb = new StringBuilder();
            StringBuilder sbExtend = new StringBuilder();
            sb.AppendLine("---@class AEvent");
            sb.AppendLine();
            sb.AppendLine("---@class UnifySDKType");
            Type unifySDKType= typeof(UnifySDKType);
            var enumFields = unifySDKType.GetFields();
            foreach (var enumFieldInfo in enumFields)
            {
                if (enumFieldInfo.Name == "value__")
                    continue;
                sb.AppendLine($"---@field {enumFieldInfo.Name} number");
            }
            sb.AppendLine($"{unifySDKType.Name} = CS.{unifySDKType.FullName}");
            sb.AppendLine();
            var nameSpace = string.Empty;
            foreach (var kv in dicType)
            {
                if (nameSpace != kv.Key)
                {
                    if (!string.IsNullOrEmpty(nameSpace))
                    {
                        sb.AppendLine(
                            $"------------------------------------------------------{nameSpace}------------------------------------------------------");
                        sb.AppendLine();
                    }

                    nameSpace = kv.Key;
                    sb.AppendLine(
                        $"------------------------------------------------------{nameSpace}------------------------------------------------------");
                    var des = "";
                    foreach (var t in kv.Value)
                    {
                        var attrs = t.GetCustomAttributes(typeof(UnifySDKEventDataAttribute), true);
                        foreach (var attr in attrs)
                        {
                            if (attr is UnifySDKEventDataAttribute)
                            {
                                des = (attr as UnifySDKEventDataAttribute).GetDes();
                                break;
                            }
                        }

                        sb.AppendLine($"---@class {t.Name} {des}");

                        var fields = t.GetFields();
                        foreach (var fieldInfo in fields)
                        {
                            if (fieldInfo.IsPublic)
                            {
                                var typeName = GetTypeName(fieldInfo, sbExtend, t);
                                sb.AppendLine($"---@field {fieldInfo.Name}  {typeName}");
                            }
                        }
                        sb.AppendLine($"{t.Name} = CS.{t.FullName}");
                        if (sb.Length > 0)
                        {
                            sb.Append(sbExtend);
                            sbExtend.Clear();
                        }

                        sb.AppendLine();
                    }
                }
            }

            sb.AppendLine(
                $"------------------------------------------------------{nameSpace}------------------------------------------------------");
            File.WriteAllText(EventDataPath, sb.ToString());
        }

        private static Type[] GetAttr<T>()
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetAssembly(typeof(T));
            System.Type[] types = asm.GetExportedTypes();

            Func<Attribute[], bool> IsMyAttribute = o =>
            {
                foreach (Attribute a in o)
                {
                    if (a is T)
                        return true;
                }

                return false;
            };

            System.Type[] allType = types.Where(o =>
                {
                    return IsMyAttribute(System.Attribute.GetCustomAttributes(o, true));
                }
            ).ToArray();

            return allType;
        }

        private static string GetTypeName(FieldInfo fieldInfo, StringBuilder sbExtend, Type baseType)
        {
            var typeName = fieldInfo.FieldType.ToString();
            if (fieldInfo.FieldType == typeof(String) || fieldInfo.FieldType == typeof(string))
            {
                typeName = "string";
            }
            else if (fieldInfo.FieldType == typeof(bool) || fieldInfo.FieldType == typeof(Boolean))
            {
                typeName = "boolean";
            }
            else if (fieldInfo.FieldType == typeof(byte) || fieldInfo.FieldType == typeof(sbyte) ||
                     fieldInfo.FieldType == typeof(short) ||
                     fieldInfo.FieldType == typeof(ushort) || fieldInfo.FieldType == typeof(int) ||
                     fieldInfo.FieldType == typeof(uint) || fieldInfo.FieldType == typeof(long) ||
                     fieldInfo.FieldType == typeof(ulong) || fieldInfo.FieldType == typeof(float) ||
                     fieldInfo.FieldType == typeof(double))
            {
                typeName = "number";
            }
            else if (fieldInfo.FieldType.BaseType == typeof(Enum))
            {
                typeName = $"{baseType.Name}.{fieldInfo.FieldType.Name}";
                var enumFields = fieldInfo.FieldType.GetFields();
                sbExtend.AppendLine();
                sbExtend.AppendLine($"---@class {typeName}");
                foreach (var enumFieldInfo in enumFields)
                {
                    if (enumFieldInfo.Name == "value__")
                        continue;
                    sbExtend.AppendLine($"---@field {enumFieldInfo.Name} number");
                }

                sbExtend.AppendLine(
                    $"{baseType.Name}_{fieldInfo.FieldType.Name} = CS.{baseType.FullName}.{fieldInfo.FieldType.Name}");
            }
            else if (fieldInfo.FieldType.Name == "Action`1")
            {
                typeName = GetTypeName(fieldInfo.FieldType);
            }

            return typeName;
        }

        // 解析方法的参数
        private static string GetParams(MethodInfo method)
        {
            ParameterInfo[] parameters = method.GetParameters();
            if (parameters.Length == 0)
                return string.Empty;

            int length = parameters.Length - 1;
            StringBuilder str = new StringBuilder();
            for (int i = 0; i <= length; i++)
            {
                str.Append(parameters[i].Name + ":");
                // 这里不对复杂类型等做处理
                str.Append(GetTypeName(parameters[i].ParameterType));

                if (i < length)
                    str.Append(",");
            }

            return str.ToString();
        }

        private static string GetTypeName(Type type)
        {
            var typeName = type.FullName.Contains("UnifySDK") ? type.Name : $"CS.{type.FullName}";
            if (type == typeof(void))
                typeName = "void";
            else if (type == typeof(String) || type == typeof(string))
            {
                typeName = "string";
            }
            else if (type == typeof(bool) || type == typeof(Boolean))
            {
                typeName = "boolean";
            }
            else if (type == typeof(byte) || type == typeof(sbyte) || type == typeof(short) ||
                     type == typeof(ushort) || type == typeof(int) ||
                     type == typeof(uint) || type == typeof(long) ||
                     type == typeof(ulong) || type == typeof(float) ||
                     type == typeof(double))
            {
                typeName = "number";
            }
            else if (type.BaseType == typeof(Enum))
            {
                typeName = type.FullName == "UnifySDK.UnifySDKType" ? type.Name : $"CS.{type.FullName}";
            }
            else if (type.Name == "Action`1")
            {
                if (type.GenericTypeArguments.Length > 0)
                {
                    typeName = "fun(";
                    for (int i = 0; i < type.GenericTypeArguments.Length; i++)
                    {
                        if (i > 1)
                            typeName += $",paras{i + 1}:{GetTypeName(type.GenericTypeArguments[i])}";
                        else
                            typeName += $"param{i + 1}:{GetTypeName(type.GenericTypeArguments[i])}";
                    }

                    typeName += ")";
                }
                else
                    typeName = $"fun()";
            }
            else if (type.Name == "AEvent`1")
            {
                if (type.GenericTypeArguments.Length > 0)
                {
                    typeName = $"AEvent<{type.GenericTypeArguments[0].Name}>";
                }
            }

            return typeName;
        }
    }
}