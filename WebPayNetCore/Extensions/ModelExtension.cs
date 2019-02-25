using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using WebPayNetCore.Attributes;
using WebPayNetCore.Models;

namespace WebPayNetCore.Extensions
{
    internal static class ModelExtension
    {
        internal static string ToQueryString(this Dictionary<string, string> dictionary)
        {
            var sb = new StringBuilder();
            foreach (var (key, value) in dictionary)
            {
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }

                if (sb.Length > 0)
                {
                    sb.Append("&");
                }
                sb.Append(HttpUtility.UrlEncode(key));
                sb.Append("=");
                sb.Append(HttpUtility.UrlEncode(value));
            }

            return sb.ToString();
        }

        internal static string CalculateMd5(this string encodedParams, string signPassword)
        {
            var text = encodedParams + signPassword;
            var md5 = MD5.Create();
            var textAsBytes = Encoding.UTF8.GetBytes(text);
            var hash = md5.ComputeHash(textAsBytes);
            var sb = new StringBuilder();

            foreach (var t in hash)
                sb.Append(t.ToString("x2"));

            return sb.ToString();
        }

        internal static string ToBase64String(this MacroRequestParams param)
        {
            var query = GenerateQueryString(param);
            var textAsBytes = Encoding.UTF8.GetBytes(query);
            var encodedText = Convert.ToBase64String(textAsBytes);
            encodedText = encodedText.Replace('+', '-');
            encodedText = encodedText.Replace('/', '_');
            return encodedText;
        }

        internal static T ToModel<T>(this Dictionary<string, string> dictionary) where T : new()
        {
            var newModel = Activator.CreateInstance<T>();
            var properties = newModel.GetType().GetProperties();

            foreach (var (key, value) in dictionary)
            {
                var property = properties.FirstOrDefault(p =>
                    p.GetCustomAttribute<ParameterNameAttribute>()?.ParameterName == key);

                if (property == null) continue;

                if (property.PropertyType == typeof(string))
                    property.SetValue(newModel, value);
                else if (property.PropertyType == typeof(int))
                    property.SetValue(newModel, int.Parse(value));
                else if (property.PropertyType == typeof(bool))
                    property.SetValue(newModel, value == "1");
                else if (property.PropertyType == typeof(double))
                    property.SetValue(newModel, double.Parse(value));
                else
                    property.SetValue(newModel, value);
            }

            return newModel;
        }

        internal static byte[] DecodeBase64(this string encodedData)
        {
            encodedData = encodedData.Replace('-', '+');
            encodedData = encodedData.Replace('_', '/');
            var data = Convert.FromBase64String(encodedData);
            return data;
        }

        internal static Dictionary<string, string> ToDictionary(this NameValueCollection collection)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            foreach (string key in collection.AllKeys)
            {
                queryParams[key] = collection[key];
            }
            return queryParams;
        }

        private static string GenerateQueryString(object obj)
        {
            var sb = new StringBuilder();
            var properties = obj.GetType().GetProperties();

            foreach (var propertyInfo in properties)
            {
                var paramAttribute = propertyInfo.GetCustomAttribute<ParameterNameAttribute>();
                if (paramAttribute == null) continue;

                var valueObj = propertyInfo.GetValue(obj);
                if (valueObj == null) continue;

                if (sb.Length > 0)
                    sb.Append("&");

                string value;

                if (valueObj is bool boolObj)
                    value = boolObj ? "1" : "0";
                else
                    value = valueObj.ToString();

                sb.Append(HttpUtility.UrlEncode(paramAttribute.ParameterName));
                sb.Append("=");
                sb.Append(HttpUtility.UrlEncode(value));
            }

            return sb.ToString();
        }
    }
}