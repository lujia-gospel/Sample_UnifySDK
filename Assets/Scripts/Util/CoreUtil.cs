using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Horizon
{
	public static class Util
	{
        public static void Fill<T>(T[] arr, T val)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = val;
            }
        }

        //获取从父节点到自己的完整路径
        public static string GetRootPathName(UnityEngine.Transform transform)
        {
            var pathName = "/" + transform.name;
            while (transform.parent != null)
            {
                transform = transform.parent;
                pathName += "/" + transform.name;
            }
            return pathName;
        }

        public static void CopyTransformToTarget(Transform sourceTrans, Transform targetTrans)
        {
            targetTrans.localPosition = sourceTrans.localPosition;
            targetTrans.localRotation = sourceTrans.localRotation;
            targetTrans.localScale = sourceTrans.localScale;
        }


        // 概率，百分比, // 注意，0的时候当是100%
        public static bool Probability(float chancePercent)
        {
            var chance = UnityEngine.Random.Range(0f, 100f);

            if (chance <= chancePercent) // 概率
            {
                return true;
            }

            return false;
        }

        public static bool Probability(byte chancePercent)
        {
            int chance = UnityEngine.Random.Range(1, 101);

            if (chance <= chancePercent) // 概率
            {
                return true;
            }

            return false;
        }

        public static void SetWidth(RectTransform rectTrans, float width)
        {
            var size = rectTrans.sizeDelta;
            size.x = width;
            rectTrans.sizeDelta = size;
        }

        public static void SetHeight(RectTransform rectTrans, float height)
        {
            var size = rectTrans.sizeDelta;
            size.y = height;
            rectTrans.sizeDelta = size;
        }

        public static void SetParent(Transform obj, Transform parent)
        {
            obj.parent = parent;
            obj.localPosition = Vector3.zero;
            obj.localScale = Vector3.one;
            obj.localEulerAngles = Vector3.zero;
        }

        public static void TransformZero(Transform obj)
        {
            obj.localPosition = Vector3.zero;
            obj.localScale = Vector3.one;
            obj.localEulerAngles = Vector3.zero;
        }

        public static void SetPositionX(Transform trans, float x)
        {
            trans.position = new Vector3(x, trans.position.y, trans.position.z);
        }

        public static void SetPositionY(Transform trans, float y)
        {
            trans.position = new Vector3(trans.position.x, y, trans.position.z);
        }

        public static void SetPositionZ(Transform trans, float z)
        {
            trans.position = new Vector3(trans.position.x, trans.position.y, z);
        }

        public static void SetLocalPositionX(Transform trans, float x)
        {
            trans.localPosition = new Vector3(x, trans.localPosition.y, trans.localPosition.z);
        }

        public static void SetLocalPositionY(Transform trans, float y)
        {
            trans.localPosition = new Vector3(trans.localPosition.x, y, trans.localPosition.z);
        }

        public static void SetLocalPositionZ(Transform trans, float z)
        {
            trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y, z);
        }

        public static void SetPositionXYZ(Transform trans, float? X, float? Y, float? Z)
        {
            trans.position = new Vector3(X.HasValue ? X.Value : trans.position.x,
                                         Y.HasValue ? Y.Value : trans.position.y,
                                         Z.HasValue ? Z.Value : trans.position.z);
        }

        public static Vector3 SetX(Vector3 position, float X)
        {
            return new Vector3(X, position.y, position.z);
        }

        public static Vector3 SetY(Vector3 position, float Y)
        {
            return new Vector3(position.x, Y, position.z);
        }

        public static Vector3 SetZ(Vector3 position, float Z)
        {
            return new Vector3(position.x, position.y, Z);
        }

        public static Rect SetX(Rect rect, float X)
        {
            rect.Set(X, rect.y, rect.width, rect.height);
            return rect;
        }

        public static Rect SetY(Rect rect, float Y)
        {
            rect.Set(rect.x, Y, rect.width, rect.height);
            return rect;
        }

        public static Vector2 ToVector2(Vector3 vec)
        {
            return new Vector2(vec.x, vec.y);
        }


        public static string GetHierarchy(GameObject obj)
        {
            if (obj == null) return "";
            string path = obj.name;

            while (obj.transform.parent != null)
            {
                obj = obj.transform.parent.gameObject;
                path = obj.name + "\\" + path;
            }
            return path;
        }

		public static Hashtable FromList( params object[] list )
		{
			if ( (list.Length&1) == 1 ) 
			{
				throw new System.Exception("List must be even length!");
			}
			var ht = new Hashtable();
			var count = list.Length / 2;
			for( var i = 0; i < count; ++i )
			{
				ht.Add( list[i*2], list[i*2+1] );
			}
			return ht;
		}
		
		public static Rect RectFromPointSize( Vector2 pt, Vector2 size ) 
		{
			var half = size*0.5f;
			return new Rect(pt.x-half.x,pt.y-half.y, size.x,size.y);
		}
		


		public static Vector2 ScreenSize 
		{
			get { return new Vector2(Screen.width, Screen.height); }	
		}
		
		public static Vector2 ScreenCenter 
		{
			get { return ScreenSize*0.5f; }	
		}
		
		public static void SetActiveRecursive(GameObject obj, bool active)
		{
			if(obj != null)
			{
				//obj.active = active;
				obj.SetActive(active);
				foreach(Transform t in obj.transform)
				{
					SetActiveRecursive(t.gameObject, active);
				}
			}
		}
		
		public static void SetActiveRecursive(GameObject obj, string name, bool active)
		{
			SetActiveRecursive( GetObject(obj, name, true), active); 
		}
		
		public static void SetLayerRecursive(GameObject obj, string name, int layer)
        {
        	SetLayerRecursive( GetObject(obj,name), layer );    
        }
		
		public static void SetLayerRecursive(GameObject obj, int layer)
        {
            if (obj != null)
            {
                obj.layer = layer;
                foreach (Transform t in obj.transform)
                {
                    SetLayerRecursive(t.gameObject, layer);
                }
            }
        }
		
		public static void SetLayerRecursiveCheckRoot(GameObject obj, string name, int layer)
		{
			if (obj != null && obj.layer != layer)
			{
				SetLayerRecursive(obj, name, layer);
			}
		}
				
		public static void SetLayerRecursiveCheckRoot(GameObject root, int layer)
		{
			if (root != null && root.layer != layer)
			{
				SetLayerRecursive(root, layer);
			}
		}
				
		//todo: see if this should go into a different file
		public static T FindComponent<T>( GameObject obj ) where T : Component
        {
            if ( obj != null )
            {
                var comp = obj.GetComponent<T>();
                if (comp != null)
                {
                    return comp;
                }

                foreach (Transform child in obj.transform)
                {
                    comp = FindComponent<T>(child.gameObject);
                    if (comp != null)
                    {
                        return comp;
                    }
                }

            }
            return null;
        }
		
       

		public static T FindComponentUpwards<T>( GameObject obj ) where T : Component
        {
            if ( obj != null )
            {
                var comp = obj.GetComponent<T>();
                if (comp != null)
                {
                    return comp;
                }

                {
					Transform parent = obj.transform.parent;
					if (parent != null)
					{
	                    comp = FindComponentUpwards<T>(obj.transform.parent.gameObject);
	                    if (comp != null)
	                    {
	                        return comp;
	                    }
					}
                }

            }
            return null;
        }
		
		public static T[] FindAllComponents<T>( GameObject obj ) where T : Component
        {
			List<T> foundElements = new List<T>();
            
			if ( obj != null )
            {
                T[] components = obj.GetComponents<T>();
                if (components != null)
                {
					foreach( var com in components )
					{
						foundElements.Add(com);
					}
                }

                foreach (Transform child in obj.transform)
                {
	                components = FindAllComponents<T>(child.gameObject);
                    if (components != null)
                    {
						foreach( var com in components )
						{
							foundElements.Add(com);
						}
                    }
                }

            }
            
			return foundElements.ToArray();
        }
		
        public static void RemoveComponents<T>(GameObject target) where T: Component
        {
            if (target != null)
            {
                T[] components = target.GetComponents<T>();
                if ((components != null) && (components.Length > 0))
                {
                    for (int i = 0; i < components.Length; i++)
                    {
                        if (!(components[i] is Transform))
                        {
                            GameObject.Destroy(components[i]);
                        }
                    }
                }
            }
        }

        public static GameObject CreateChild(GameObject parent, GameObject childPrefab)
        {
            GameObject child = GameObject.Instantiate(childPrefab) as GameObject;
            SetChildIdentity(parent, child);
            return child;
        }


		public static void Parent(GameObject parent, string name, GameObject child)
        {
        	Parent( GetObjectExactMatch(parent,name), child); 
        }

        public static void Parent(Transform parent, Transform child)
        {
            Vector3 localPosition = child.localPosition;
            Quaternion localRotation = child.localRotation;
            Vector3 localScale = child.localScale;
            child.parent = parent;
            child.localPosition = localPosition;
            child.localRotation = localRotation;
            child.localScale = localScale;
        }

        public static void Parent(GameObject parent, GameObject child)
        {
			if ( parent != null && child != null )
			{
				Parent(parent.transform, child.transform);
			}
        }

        /// <summary>
        /// 是否存在中文字符
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool HasChinese(this string input)
        {
            return Regex.IsMatch(input, @"[\u4e00-\u9fa5]");
        }

		// 娣诲姞瀛╁瓙鑺傜偣, add by guonan
		public static void AddChild( GameObject parent,  GameObject newChild)
		{
			if(parent == null || newChild == null)
			{
                Debug.LogError("The GameObject is null: parent:" + parent + "child:" + newChild);
				return;
			}
			
			Transform t =  newChild.transform;
			t.parent = parent.transform;
			t.localPosition = Vector3.zero;
			t.localScale = Vector3.one;
			t.localRotation =  Quaternion.identity;
		}

		// 娣诲姞瀛╁瓙鑺傜偣, add by guonan
		public static void AddChildWorldPosition( GameObject parent,  GameObject newChild)
		{
			if(parent == null || newChild == null)
			{
                Debug.LogError("The GameObject is null: parent:" + parent + "child:" + newChild);
				return;
			}
			
			Transform t =  newChild.transform;
			t.parent = parent.transform;
//			t.localScale = Vector3.one;
//			t.localRotation =  Quaternion.identity;
		}

		// add a child gameobject with localPos, NGUITools will changeLayer add by guonan
		public static void AddChild( GameObject parent,  GameObject newChild, Vector3 childLocalPos)
		{
			if(parent == null || newChild == null)
			{
                Debug.LogError("The GameObject is null: parent:" + parent + "child:" + newChild);
				return;
			}
			
			Transform t =  newChild.transform;
			t.parent = parent.transform;
			t.localPosition = Vector3.zero;
			t.localScale = Vector3.one;
			t.localRotation =  Quaternion.identity;
			t.localPosition = childLocalPos;
		}

		public static GameObject[] GetObjects(GameObject obj, string name = null)
        {
			List<GameObject> list = new List<GameObject>();
			
            if ( obj == null ) return list.ToArray();
			
            if (null == name || obj.name.Contains(name))
            {
				list.Add(obj);
            }

			foreach (Transform t in obj.transform)
            {
				var tmp = GetObjects(t.gameObject,name);
				foreach( var tt in tmp )
				{
					list.Add(tt);
				}
            }

            return list.ToArray();
        }
		
		public static GameObject[] GetObjectsWithPreName(GameObject obj, string name = null)
		{
			List<GameObject> list = new List<GameObject>();
			
			if ( obj == null ) return list.ToArray();
			
			if (null == name || obj.name.StartsWith(name))
			{
				list.Add(obj);
			}

			foreach (Transform t in obj.transform)
			{
				var tmp = GetObjectsWithPreName(t.gameObject,name);
				foreach( var tt in tmp )
				{
					list.Add(tt);
				}
			}

			return list.ToArray();
		}
		
        public static GameObject GetObject(GameObject obj, string name)
        {
            return GetObject(obj, name, false);
        }

        public static GameObject GetRootObjectExactMath(string name, bool justRoot = false)
        {
            UnityEngine.SceneManagement.Scene scene =    UnityEngine.SceneManagement.SceneManager.GetActiveScene(); 

            string[] tNames = name.Split('/');
            GameObject[] rootObjs = scene.GetRootGameObjects();

            if(tNames.Length > 1)
            {
                for (int j = 0; j < rootObjs.Length; j++)
                {
                    if (rootObjs[j].name == tNames[0])
                    {
                        GameObject tarObj = rootObjs[j];
                        if (!justRoot)
                        {
                            for (int i = 1; i < tNames.Length; i++)
                            {
                                tarObj = GetObject(tarObj, tNames[i], true);
                                if (tarObj == null)
                                    return null;
                            }
                        }
                        return tarObj;
                    }
                } 
            }
            if (tNames.Length == 1)
            {
                for (int j = 0; j < rootObjs.Length; j++)
                {
                    GameObject tarObj = rootObjs[j];
                    if (rootObjs[j].name == tNames[0])
                    {
                        return tarObj;
                    }
                    else
                    {
                        if (!justRoot)
                        {
                            tarObj = GetObject(tarObj, tNames[0], true);
                            if (tarObj != null)
                            {
                                return tarObj;
                            }
                        }
                    }
                }
            }


            return null;
        }

        public static GameObject GetChildExactMatch(GameObject obj, string name)
        {
            string[] tNames = name.Split('/');

            GameObject tarObj = obj;
            for (int i = 0; i < tNames.Length; i++ )
            {
                tarObj = GetDirectChildObject(tarObj, tNames[i], true);
                if (tarObj == null)
                    return null;
            }
            return tarObj;
        }


        public static GameObject GetDirectChildObject(GameObject obj, string name, bool bExactMatch =true )
        {
            if ( obj == null || string.IsNullOrEmpty(name) ) 
            {
                return null;
            }

            if (!bExactMatch)
            {
                if (obj.name.Contains(name))
                {
                    return obj;
                }
            }
            else if (obj.name == name)
            {
                return obj;
            }

            if (obj.transform)
            {
                int childCount = obj.transform.childCount;
                Transform trans = obj.transform;
                for (int i = 0; i < childCount; i++)
                {
                    Transform t = trans.GetChild(i);
                    if (!t) continue;

                    if (!bExactMatch)
                    {
                        if (obj.name.Contains(name))
                        {
                            return obj;
                        }
                    }
                    else if (t.name == name)
                    {
                        return t.gameObject;
                    }
                }

            }
            return null;
        }

        public static GameObject GetObjectExactMatch(GameObject obj, string name)
        {
            string[] tNames = name.Split('/');

            GameObject tarObj = obj;
            for (int i = 0; i < tNames.Length; i++ )
            {
                tarObj = GetObject(tarObj, tNames[i], true);
                if (tarObj == null)
                    return null;
            }
            return tarObj;
        }

        public static GameObject GetObject(GameObject obj, string name, bool bExactMatch )
        {
			if ( obj == null || string.IsNullOrEmpty(name) ) 
			{
				return null;
			}
			
            if (!bExactMatch)
            {
                if (obj.name.Contains(name))
                {
                    return obj;
                }
            }
            else if (obj.name == name)
            {
                return obj;
            }

			if (obj.transform)
			{
                int childCount = obj.transform.childCount;
                Transform trans = obj.transform;
                for (int i = 0; i < childCount; i++)
                {
                    Transform t = trans.GetChild(i);
                    if (!t) continue;

                    GameObject result = GetObject(t.gameObject, name, bExactMatch);
                    if (result != null)
                    {
                        return result;
                    }
                }

			}
            return null;
        }
		
		public static Material[] GatherMaterials( GameObject obj, string name)
        {
            List<Material> materials = new List<Material>();
			
			if ( obj != null )
			{
				if (obj.GetComponent<Renderer>() != null)
	            {
	                if (string.IsNullOrEmpty(name))
	                {
						foreach( var mat in obj.GetComponent<Renderer>().materials)
						{
							materials.Add(mat);
						}
	                }
	                else
	                {
	                    foreach (Material material in obj.GetComponent<Renderer>().materials)
	                    {
	                        if (material.name.Contains(name))
	                        {
	                            materials.Add(material);
	                        }
	                    }
	                }
	            }
	
	            foreach (Transform child in obj.transform)
	            {
					var tmp = GatherMaterials(child.gameObject, name);
					foreach( var tt in tmp )
					{
						materials.Add(tt);
					}
	            }
			}
            return materials.ToArray();
        }
	
		public static int[] GetIntArray(ArrayList data)
	    {
	        List<int> list = new List<int>();
	        if ( data != null)
	        {
	            foreach (object d in data)
	            {
	                if ( d != null )
	                {
						int value = 0;
						if ( int.TryParse(d.ToString(), out value) )
						{
							list.Add(value);
						}
	                }
	            }
	        }
	        return list.ToArray();
	    }
		
		public static void BroadcastMessage(string message)
		{
			foreach( GameObject go in GameObject.FindObjectsOfType(typeof(GameObject)) )
			{
				if (go)
				{
					go.SendMessage(message, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		
		public static void BroadcastMessage(string message, object value)
		{
			foreach( GameObject go in GameObject.FindObjectsOfType(typeof(GameObject)) )
			{
				if (go)
				{
					go.SendMessage(message, value,SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		
		public static float[] GetFloatArray(ArrayList data)
	    {
	        List<float> list = new List<float>();
	        if ( data != null)
	        {
	            foreach (object d in data)
	            {
	                if ( d != null )
	                {
						float value = 0;
						if ( float.TryParse(d.ToString(), out value) )
						{
							list.Add(value);
						}
	                }
	            }
	        }
	        return list.ToArray();
	    }
	
		
		public static Transform Ascend(string ancestorName, Transform child)
		{
			if (child.transform.parent != null)
			{
				if (child.transform.parent.name == ancestorName)
				{
					return child.transform.parent;
				}
				else
				{
					return Ascend(ancestorName, child.transform.parent);
				}
			}
			return null;
		}


		public static IEnumerator CoroutineCallback(System.Action callback, float time)
		{
			yield return new WaitForSeconds (time);
			callback ();
		}

        public static void SetChildIdentity(GameObject parent, GameObject child)
        {
            if (child != null)
            {
                if (parent != null)
                {
                    child.transform.parent = parent.transform;
                    child.transform.localPosition = Vector3.zero;
                    child.transform.localRotation = Quaternion.identity;
                    child.transform.localScale = Vector3.one;
                    SetLayerRecursive(child, parent.layer);
                }
                else
                {
                    child.transform.parent = null;
                    child.transform.position = Vector3.zero;
                    child.transform.rotation = Quaternion.identity;
                    child.transform.localScale = Vector3.one;
                }
            }
        }

        public static void ExchangePosition(GameObject a, GameObject b)
        {
            Vector3 position = a.transform.position;
            a.transform.position = b.transform.position;
            b.transform.position = position;
        }


		public static bool isMobilePlatform()
		{
			if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			{
				return true;
			}
			return false;
		}

        public static string GetGameObjectPath(GameObject obj)
        {
            string path = "/" + obj.name;
            while (obj.transform.parent != null)
            {
                obj = obj.transform.parent.gameObject;
                path = "/" + obj.name + path;
            }
            return path;
        }

        public static Transform[] GetTransformsByName(Transform parent, params string[] names)
        {
            if (names.Length == 0)
                return null;

            Transform[] trans = new Transform[names.Length];
        
            for(int i=0; i<names.Length; i++)
            {
                GameObject obj = Util.GetObjectExactMatch(parent.gameObject, names[i]);
                if(obj != null)
                    trans[i] = obj.transform;
            }
            return trans;
        }

        public static Transform[] GetParentTransformsByName(Transform parent, params string[] names)
        {
            if (names.Length == 0)
                return null;

            Transform[] trans = new Transform[names.Length];

            for (int i = 0; i < names.Length; i++)
            {
                GameObject obj = Util.GetObjectExactMatch(parent.gameObject, names[i]);
                trans[i] = obj.transform.parent;
            }
            return trans;
        }

        public static GameObject InstantiateGameObjectByObject(Object data)
        {
            Object temp = GameObject.Instantiate(data);
            GameObject obj = temp as GameObject;
            return obj;
        }
        public static Material InstantiateMaterialByObject(Object data)
        {
            if (data == null) return null;

            Material mat = (Material)data;
            return mat;
        }
        
        public static Cubemap InstantiateCubemapByObject(Object data)
        {
	        if (data == null) return null;

	        Cubemap cubemap = (Cubemap)data;
	        return cubemap;
        }

        private static GameObject FindLightGroup()
        {
            GameObject result = GameObject.Find("LightGroup");
            if (result == null)
            {
                result = new GameObject("LightGroup");
                GameObject.DontDestroyOnLoad(result);
            }
            return result;
        }
        public static void BuildLightGroup(string resname)
        {
            GameObject result = FindLightGroup();
            if (result == null)
            {
                return;
            }

            bool isexist = false;
            int childcount = result.transform.childCount;
            for (int i = 0; i < childcount; i++)
            {
                Transform tran = result.transform.GetChild(i);
                if (tran != null)
                {
                    tran.gameObject.SetActive(tran.name == resname);
                    isexist = isexist || (tran.name == resname);
                }
            
            }
        }

        public static void DisableLightGroup()
        {
            GameObject result = FindLightGroup();
            if (result == null)
            {
                return;
            }

            int childcount = result.transform.childCount;
            for (int i = 0; i < childcount; i++)
            {
                Transform tran = result.transform.GetChild(i);
                if (tran != null)
                {
                    tran.gameObject.SetActive(false);
                }
            }

        }
        public static string GetMaterialOriginalName(string name)
        {
            if (name.Contains(" (Instance)"))
            {
                name = name.Replace(" (Instance)", string.Empty);
            }
            return name;
        }



    }
}
