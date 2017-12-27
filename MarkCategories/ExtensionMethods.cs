using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Tekla.Structures.Model;

namespace MarkCategories
{
	public static class ExtensionMethods
	{
		public static List<T> ToAList<T>(this IEnumerator enumerator)
		{	
			//if(enumerator is ModelObjectEnumerator)
			//	((ModelObjectEnumerator) enumerator).SelectInstances = false;

			var list = new List<T>();
			while (enumerator.MoveNext())
			{
				var current = enumerator.Current;
				if (!(current is T)) continue;
				list.Add((T)current);
			}
			return list;
		}

		public static string ToJson(this object obj, bool formatting = true)
		{
			return
				formatting
					? JsonConvert.SerializeObject(obj, Formatting.Indented)
					: JsonConvert.SerializeObject(obj, Formatting.None);
		}

		public static T FromJson<T>(this string json)
		{
			return JsonConvert.DeserializeObject<T>(json);
		}
	}
}