using System;
using UnifySDK.Event.Crash;

namespace UnifySDK
{
    [UnifySDKInterface]
    public interface IUnifySDK_Crash
    {
        /// <summary>
        /// 发送脚本异常
        /// </summary>
        /// <param name="ex">Excepiton Info</param>
        void SendScriptException(Exception ex);

        /// <summary>
        /// 上报脚本异常
        /// </summary>
        void SendScriptException(ScriptExceptionConfigData data);

        /// <summary>
        /// 设置是否只在wifi下上报报告文件
        /// </summary>
        /// <param name="enabled"></param>
        void SetFlushOnlyOverWiFi(bool enabled);

        /// <summary>
        /// 设置该版本是否为测试版本
        /// </summary>
        /// <param name="isBeta">是否为测试版本</param>
        void SetIsBetaVersion(bool isBeta);

        /// <summary>
        /// 如果启动了C#堆栈回溯可能会导致某些机型出现宕机
        /// </summary>
        void EnableCSharpStackTrace();

        /// <summary>
        /// 指定获取应用程序log日志的行数
        /// </summary>
        /// <param name="lines">需要获取log行数</param>
        void SetLogging(int lines);

        /// <summary>
        /// 获取应用程序log日志关键字过滤
        /// </summary>
        /// <param name="filter">需要过滤关键字</param>
        void SetLogging(string filter);


        /// <summary>
        /// 获取应用程序log日志（过滤条件：关键字过滤+行数）
        /// </summary>
        void SetLogging(LoggingData data);

        /// <summary>
        /// SetURL
        /// </summary>
        /// <param name="url"></param>
        void SetURL(string url);
    }
}