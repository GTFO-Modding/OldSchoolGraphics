using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OldSchoolGraphics.Utils;
internal static class ComponentExtension
{
    public static T GetOrAddComponent<T>(this Component self) where T : Component
    {
        var comp = self.gameObject.GetComponent<T>();
        if (comp != null)   return comp;
        else                return self.gameObject.AddComponent<T>();
    }

    public static bool TryGetOrAddComponent<T>(this Component self, out T comp) where T : Component
    {
        comp = GetOrAddComponent<T>(self);
        return comp != null;
    }
}
