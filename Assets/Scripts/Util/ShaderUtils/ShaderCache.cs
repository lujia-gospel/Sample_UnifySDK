using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Horizon
{
	public class ShaderCache  
	{
		
		public static Dictionary<string, Shader> ShaderCacheDict = new Dictionary<string, Shader>();
		
		public static Shader GetShader(string shaderName)
		{
			if (!ShaderCacheDict.ContainsKey (shaderName))
			{
				Shader shader = Shader.Find(shaderName);
				//Horizon.HDebug.Log("Find the shader " + shaderName +" = "+shader);
				ShaderCacheDict.Add(shaderName, shader);
			}
			
			return ShaderCacheDict[shaderName];
		}
	}
}

