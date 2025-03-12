using System;
using System.Collections.Concurrent;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AiUoVsix.Common
{

    public static class ReflectionUtil
    {
        internal delegate T ObjectActivator<out T>(params object[] args);

        private static readonly Dictionary<string, string> _jsTypeMapCache = new Dictionary<string, string>
    {
        { "System.Sbyte", "Number" },
        { "System.Byte", "Number" },
        { "System.Int16", "Number" },
        { "System.UInt16", "Number" },
        { "System.Int32", "Number" },
        { "System.UInt32", "Number" },
        { "System.Int64", "Number" },
        { "System.UInt64", "Number" },
        { "System.Single", "Number" },
        { "System.Double", "Number" },
        { "System.Decimal", "Number" },
        { "System.Boolean", "Boolean" },
        { "System.Char", "String" },
        { "System.String", "String" },
        { "System.DateTime", "String" },
        { "System.TimeSpan", "String" },
        { "System.Enum", "String" },
        { "System.Guid", "String" },
        { "System.Object", "Object" }
    };

        private static readonly ConcurrentDictionary<string, Delegate> ObjectActivators = new ConcurrentDictionary<string, Delegate>();

        private static readonly ConcurrentDictionary<PropertyInfo, MethodInfo> _propertyGetterCache = new ConcurrentDictionary<PropertyInfo, MethodInfo>();

        private static readonly ConcurrentDictionary<string, MethodInfo> _propertyNameGetterCache = new ConcurrentDictionary<string, MethodInfo>();

        private static readonly ConcurrentDictionary<PropertyInfo, MethodInfo> _propertySetterCache = new ConcurrentDictionary<PropertyInfo, MethodInfo>();

        private static readonly ConcurrentDictionary<string, MethodInfo> _propertyNameSetterCache = new ConcurrentDictionary<string, MethodInfo>();

        private static readonly Dictionary<Type, Dictionary<string, PropertyInfo>> _propertyCache = new Dictionary<Type, Dictionary<string, PropertyInfo>>();

        public static AssemblyProductAttribute GetAssemblyProduct(Assembly asm)
        {
            return asm.GetCustomAttribute<AssemblyProductAttribute>();
        }

        public static Version GetAssemblyVersion(Assembly asm)
        {
            return asm.GetName().Version;
        }

        public static string GetAssemblyGuidString(Assembly asm)
        {
            string result = null;
            GuidAttribute[] array = (GuidAttribute[])asm.GetCustomAttributes(typeof(GuidAttribute), inherit: false);
            if (array != null && array.Length != 0)
            {
                result = array[0].Value;
            }

            return result;
        }



        public static string MapToJsType(Type type)
        {
            string result = null;
            string fullName = type.FullName;
            if (_jsTypeMapCache.ContainsKey(fullName))
            {
                return _jsTypeMapCache[fullName];
            }

            if (typeof(ICollection).IsAssignableFrom(type) || typeof(IEnumerable<>).IsAssignableFrom(type))
            {
                return "Array";
            }

            return result;
        }

        public static Delegate GetActivator<T>(ConstructorInfo ctor)
        {
            Type declaringType = ctor.DeclaringType;
            ParameterInfo[] parameters = ctor.GetParameters();
            ParameterExpression parameterExpression = Expression.Parameter(typeof(object[]), "args");
            Expression[] array = new Expression[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                Expression index = Expression.Constant(i);
                Type parameterType = parameters[i].ParameterType;
                Expression expression = Expression.ArrayIndex(parameterExpression, index);
                Expression expression2 = Expression.Convert(expression, parameterType);
                array[i] = expression2;
            }

            NewExpression body = Expression.New(ctor, array);
            LambdaExpression lambdaExpression = Expression.Lambda(typeof(ObjectActivator<T>), body, parameterExpression);
            return lambdaExpression.Compile();
        }

        public static object CreateInstance(Type type, params object[] args)
        {
            if (!ObjectActivators.TryGetValue(type.FullName, out var value))
            {
                ConstructorInfo[] constructors = type.GetConstructors();
                if (constructors.Count() == 1)
                {
                    value = GetActivator<object>(constructors.First());
                }

                ObjectActivators.AddOrUpdate(type.FullName, value, (string k, Delegate v) => v);
            }

            return ((object)value == null) ? Activator.CreateInstance(type, args) : ((ObjectActivator<object>)value)(args);
        }

        public static T CreateInstance<T>(params object[] args)
        {
            return (T)CreateInstance(typeof(T), args);
        }

        public static object CreateInstance(string typeName, params object[] args)
        {
            return CreateInstance(Type.GetType(typeName), args);
        }

        public static object InvokeMethod(string typeName, string methodName, params object[] args)
        {
            Type type = Type.GetType(typeName);
            MethodInfo method = type.GetMethod(methodName);
            object obj = Activator.CreateInstance(type);
            return method.Invoke(obj, args);
        }

        public static object InvokeStaticMethod(string typeName, string methodName, params object[] args)
        {
            Type type = Type.GetType(typeName);
            MethodInfo method = type.GetMethod(methodName);
            return method.Invoke(null, args);
        }

        public static object GetPropertyValue(this object obj, PropertyInfo property)
        {
            if (!_propertyGetterCache.TryGetValue(property, out var value))
            {
                value = property.GetGetMethod();
                _propertyGetterCache.TryAdd(property, value);
            }

            return value.Invoke(obj, null);
        }

        public static object GetPropertyValue(this object obj, string propertyName)
        {
            string key = obj.GetType().FullName + ":" + propertyName;
            if (!_propertyNameGetterCache.TryGetValue(key, out var value))
            {
                PropertyInfo property = obj.GetType().GetProperty(propertyName);
                value = property.GetGetMethod();
                _propertyNameGetterCache.TryAdd(key, value);
            }

            return value.Invoke(obj, null);
        }


        public static void SetPropertyValue(this object obj, PropertyInfo property, object value)
        {
            if (!_propertySetterCache.TryGetValue(property, out var value2))
            {
                value2 = property.GetSetMethod();
                _propertySetterCache.TryAdd(property, value2);
            }

            value2.Invoke(obj, new object[1] { value });
        }

        public static void SetPropertyValue(this object obj, string propertyName, object value)
        {
            string key = obj.GetType().FullName + ":" + propertyName;
            if (!_propertyNameSetterCache.TryGetValue(key, out var value2))
            {
                PropertyInfo property = obj.GetType().GetProperty(propertyName);
                value2 = property.GetSetMethod();
                _propertyNameSetterCache.TryAdd(key, value2);
            }

            value2.Invoke(obj, new object[1] { value });
        }

        public static Dictionary<string, PropertyInfo> GetPropertyDic<T>() where T : new()
        {
            Type typeFromHandle = typeof(T);
            if (!_propertyCache.TryGetValue(typeFromHandle, out var value))
            {
                value = new Dictionary<string, PropertyInfo>();
                PropertyInfo[] properties = typeFromHandle.GetProperties();
                foreach (PropertyInfo propertyInfo in properties)
                {
                    value.Add(propertyInfo.Name, propertyInfo);
                }

                _propertyCache.Add(typeFromHandle, value);
            }

            return value;
        }

        public static string GetManifestResourceString(Assembly assembly, string name, Encoding encoding = null)
        {
            Stream manifestResourceStream = assembly.GetManifestResourceStream(name);
            using (StreamReader streamReader = new StreamReader(manifestResourceStream, encoding ?? Encoding.UTF8))
            {
                return streamReader.ReadToEnd();
            }
        }
 
         

        public static bool IsSubclassOfGeneric(this Type type, Type generic)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (generic == null)
            {
                throw new ArgumentNullException("generic");
            }

            if (type.GetInterfaces().Any(IsTheRawGenericType))
            {
                return true;
            }

            while (type != null && type != typeof(object))
            {
                if (IsTheRawGenericType(type))
                {
                    return true;
                }

                type = type.BaseType;
            }

            return false;
            bool IsTheRawGenericType(Type test)
            {
                return generic == (test.IsGenericType ? test.GetGenericTypeDefinition() : test);
            }
        }

        public static bool IsBulitinType(Type type)
        {
            return type.FullName.StartsWith("System.");
        }
    }
}
