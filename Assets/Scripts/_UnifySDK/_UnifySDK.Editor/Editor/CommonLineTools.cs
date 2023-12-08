using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System;
using UnifySDK.Editor;

public static class CommonLineTools 
{
	[MenuItem("Tools/UnifySDK/UnifyExtendGitReset", false, 0)]
	public static void UnifyExtendGitReset()
	{
		// 删除文件夹及其中所有文件
		string folderPath = $"{Application.dataPath}/Plugins";
		if (Directory.Exists(folderPath))
			Directory.Delete(folderPath, true);
		string metaFolderPath =  $"{Application.dataPath}/Plugins.meta";
		// 删除.meta文件夹
		if (File.Exists(metaFolderPath))
			File.Delete(metaFolderPath);
		var resetPath = $"{Application.dataPath}/Scripts/_UnifySDK/_UnifySDK.Extend";
		CommandLineUtils.RunDirectly("git", $"checkout -- {resetPath}", Application.dataPath);
		AssetDatabase.Refresh();
	}
	[MenuItem("Tools/UnifySDK/根据环境变量删除SDK", false, 0)]
	public static void UnifySDKBuild()
	{
		var py = $"{Application.dataPath}/../Tools/UnifySDKTool/UnifySDKTool.py";
		var model = PlayerPrefs.GetString(EnvironmentVariableSettingsEditor.LocalUnifySDKRecord, "None");
		CommandLineUtils.RunDirectly("Python3", $"{py} {model}", $"{Application.dataPath}/..");
		AssetDatabase.Refresh();
	}
}


public static class CommandLineUtils  
{	

	const int kMaxThreads = 4;
	
	public class AsyncTask
	{
		public string command;
		public string args;
		public string dir;
		public string result;
		public bool   done;
	};
	static Queue<AsyncTask> _queue = new Queue<AsyncTask>(1024);
	static System.Threading.Semaphore _semaphore = new System.Threading.Semaphore(0,kMaxThreads);
	static bool _running = false;
	
	
	static void _Thread()
	{
		while (_running)
		{
			while(true)
			{
				AsyncTask task = null;
				
				lock(_queue)
				{
					if ( _queue.Count > 0 )
					{
						task = _queue.Dequeue();
					}
				}
				
				if ( task == null )
				{
					break;
				}
				
				task.result = Run( task.command, task.args, task.dir ); 
				task.done = true;
			}
		
			_semaphore.WaitOne();
		}
	}
	
	static System.Diagnostics.Process _RunAsync(string command, string arguments, string workingDir) 
	{

		//Debug.Log("Runing: " + command + " " + arguments);
		//      System.Diagnostics.Process p = new System.Diagnostics.Process();
		//p.StartInfo.FileName = command;
		//p.StartInfo.Arguments = arguments;
		//p.StartInfo.WorkingDirectory = workingDir;
		//p.StartInfo.UseShellExecute = true;
		//p.StartInfo.CreateNoWindow = false;			

		//p.Start();
		//return p;
		var pStartInfo = new System.Diagnostics.ProcessStartInfo(command);
		pStartInfo.Arguments = arguments;
		pStartInfo.CreateNoWindow = false;
		pStartInfo.UseShellExecute = true;
		pStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
		pStartInfo.RedirectStandardError = false;
		pStartInfo.RedirectStandardInput = false;
		pStartInfo.RedirectStandardOutput = false;
		if (!string.IsNullOrEmpty(workingDir))
			pStartInfo.WorkingDirectory = workingDir;
	   return System.Diagnostics.Process.Start(pStartInfo);
	}
	
	static int _Run(string command, string arguments, string workingDir) 
	{
		var p = _RunAsync(command, arguments, workingDir);
		p.WaitForExit();
		//Debug.Log("Run done " + p.ExitCode);
		var code = p.ExitCode;
		p.Dispose();
		System.GC.Collect();
		return code;
	}
	
	static void _DeleteFile(string file)
	{
		try
		{
			File.Delete(file);
		}
		catch {}
	}
	
	public static AsyncTask RunAsync( string command, string arguments )
	{
		return RunAsync( command, arguments, Directory.GetCurrentDirectory() );	
	}
	
	public static AsyncTask RunAsync( string command, string arguments, string workingDir )
	{
		if ( _running == false )
		{
			_running = true;
			for ( int i = 0; i < kMaxThreads; ++i )
			{
				new System.Threading.Thread(_Thread).Start();
			}
		}
		
		AsyncTask task = new AsyncTask();
		task.command = command;
		task.args = arguments;
		task.dir = workingDir;
		lock(_queue)
		{
			_queue.Enqueue(task);
		}
		
		// wake up threads
		try
		{
			_semaphore.Release();
		}
		catch {}
		
		return task;
	}
	
	public static void WaitForTasks()
	{
		while( true )
		{
			bool done = true;
			lock(_queue)
			{
				done = _queue.Count == 0;
			}
			
			if ( done )
			{
				return;
			}
			
			System.Threading.Thread.Sleep(100);
		}
	}
	
	public static string Run(string command, string arguments)
	{
		return Run(command,arguments,Directory.GetCurrentDirectory());
	}
		
	public static string Run(string command, string arguments, string workingDir) 
	{
		Debug.Log("Running: " + command + " " + arguments);
	
		int hash		= System.Diagnostics.Process.GetCurrentProcess().Id;
		int threadId	= System.Threading.Thread.CurrentThread.ManagedThreadId;
		
		string file 	= string.Format("/tmp/unity_command_{0}_{1}.sh", hash, threadId);
		string outfile 	= string.Format("/tmp/unity_command_{0}_{1}out.txt", hash, threadId);
		string script 	= string.Format("#!/bin/bash\ntouch {2}\n{0} {1} > {2}\n", command, arguments, outfile);
		_DeleteFile(outfile);
		File.WriteAllText(file, script);
		_Run("chmod", "u+x " + file, workingDir);
		_Run(file, string.Empty, workingDir);
		
		string output = string.Empty;
		try
		{
			output = File.ReadAllText(outfile);
            Debug.Log(@" Running log:  {output} ");
		}
		catch {}
		
        if(output.Contains("xcodebuild: error:"))
        {
	        Debug.LogError($" Running log error::  {output} ");
            throw new Exception(output);
        }

        _DeleteFile(script);
		_DeleteFile(outfile);
		
		return output;
	}

    public static void RunDirectly(string command, string arguments, string workingDir)
    {
        _Run(command, arguments, workingDir);
    }

    public static void RunDirectly(string command)
    {
        int code = _Run(command, "", Directory.GetCurrentDirectory());
    }

}
