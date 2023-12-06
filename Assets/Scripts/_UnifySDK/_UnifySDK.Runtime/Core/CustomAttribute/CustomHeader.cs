using UnityEngine;

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



