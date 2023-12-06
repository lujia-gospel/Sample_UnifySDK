using UnityEngine;
using System.Collections;

namespace Horizon
{
    public class ShaderReset : MonoBehaviour 
    {
    	// Use this for initialization
    	void Start () 
    	{
    		MeshRenderer thisRender = this.GetComponent<MeshRenderer> ();
    		if(thisRender != null)
    		{
    			string shaderName = thisRender.material.shader.name;
    			Shader newShader =	ShaderCache.GetShader(shaderName);
    			thisRender.material.shader = newShader;
    		}
    	}
    }
}
