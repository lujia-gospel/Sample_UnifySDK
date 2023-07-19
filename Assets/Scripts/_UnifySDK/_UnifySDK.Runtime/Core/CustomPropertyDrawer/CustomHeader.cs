using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace UnifySDK
{
    public class CustomHeaderAttribute : PropertyAttribute
    {
        public string[] Headlines { get; }

        public CustomHeaderAttribute(params string [] headlines)
        {
            Headlines = headlines;
        }
    }
}



