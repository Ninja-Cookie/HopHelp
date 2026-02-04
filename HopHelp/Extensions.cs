using UnityEngine;
using System.Reflection;
using System.Linq;
using System;

namespace HopHelp
{
    internal static class Extensions
    {
        public static BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static;

        public static T GetValue<T>(this object input, string name, params object[] methodParams)
        {
            if (GetMember(input, name, out MemberInfo member))
            {
                switch (member.MemberType)
                {
                    case MemberTypes.Field:     return (T)(member as FieldInfo)     .GetValue(input);
                    case MemberTypes.Property:  return (T)(member as PropertyInfo)  .GetValue(input, null);
                    case MemberTypes.Method:    return (T)(member as MethodInfo)    .Invoke(input, methodParams);
                }
            }
            return default;
        }

        public static void InvokeMethod(this object input, string name, params object[] methodParams)
        {
            if (GetMember(input, name, out MemberInfo member, methodParams))
                (member as MethodInfo).Invoke(input, methodParams);
        }

        public static void SetValue<T>(this object input, string name, T value)
        {
            if (GetMember(input, name, out MemberInfo member))
            {
                switch (member.MemberType)
                {
                    case MemberTypes.Field:     (member as FieldInfo)   .SetValue(input, value); break;
                    case MemberTypes.Property:  (member as PropertyInfo).SetValue(input, value); break;
                }
            }
        }

        public static T GetMember<T>(this object input, string name, params object[] methodParams) where T : MemberInfo
        {
            if (GetMember(input, name, out MemberInfo member, methodParams))
                return (T)member;

            return default;
        }

        private static bool GetMember(this object input, string name, out MemberInfo member, params object[] methodParams)
        {
            MemberInfo[] members = input.GetType().GetMember(name, flags);
            if (members == null || members.Length == 0)
            {
                member = default(MemberInfo);
                return false;
            }

            if (members.Length == 1)
            {
                member = members.First();
                return true;
            }

            foreach (var m in members)
            {
                if (m.MemberType != MemberTypes.Method)
                    continue;

                MethodInfo      method  = m as MethodInfo;
                ParameterInfo[] parms   = method.GetParameters();

                bool isMatch = true;
                for (int i = 0; i < parms.Length; i++)
                {
                    if (parms[i].HasDefaultValue || (!parms[i].HasDefaultValue && methodParams.Length < i))
                        break;

                    if (parms[i].ParameterType != methodParams[i]?.GetType())
                    {
                        isMatch = false;
                        break;
                    }
                }

                if (isMatch)
                {
                    member = m;
                    return true;
                }
            }

            member = default(MemberInfo);
            return false;
        }

        public static float Width(this RectTransform rect, float? newX = null)
        {
            return RectSize(rect, newX, null).x;
        }

        public static float Height(this RectTransform rect, float? newY = null)
        {
            return RectSize(rect, null, newY).y;
        }

        public static Vector2 Size(this RectTransform rect, float? newX = null, float? newY = null)
        {
            return RectSize(rect, newX, newY);
        }

        private static Vector2 RectSize(this RectTransform rect, float? x, float? y)
        {
            Vector2 sizeDelta = rect.sizeDelta;

            float realX = x ?? sizeDelta.x;
            float realY = y ?? sizeDelta.y;

            if (x != null)
                rect.sizeDelta = new Vector2(realX, sizeDelta.y);

            if (y != null)
                rect.sizeDelta = new Vector2(sizeDelta.x, realY);

            return rect.sizeDelta;
        }

        public static T AddComponentIfMissing<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject.TryGetComponent(out T comp))
                return comp;
            return gameObject.AddComponent<T>();
        }

        public static void AddComponentsIfMissing(this GameObject gameObject, params Type[] components)
        {
            foreach (var component in components)
                gameObject.AddComponentIfMissing(component);
        }

        public static Component AddComponentIfMissing(this GameObject gameObject, Type component)
        {
            if (typeof(Component).IsAssignableFrom(component))
            {
                if (gameObject.TryGetComponent(component, out Component comp))
                    return comp;
                return gameObject.AddComponent(component);
            }
            else
            {
                Debug.LogError($"Type {component} is not a Component.");
            }
            return null;
        }
    }
}
