using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace AddressRequest.Extensions
{
    public static class Conversions
    {

        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }


        public static T To<T>(this object value)
        {
            Type conversionType = typeof(T);
            return (T)To(value, conversionType);
        }

        public static byte[] StringToByte(this string strByte)
        {
            var stringByte = strByte.Split(';');

            if (stringByte.Count() == 0)
                return null;

            var arquivo = new byte[stringByte.Count() - 1];
            for (int i = 0; i < stringByte.Count() - 1; i++)
            {
                arquivo[i] = stringByte[i].To<byte>();
            }

            return arquivo;
        }

        public static DateTime StringToDate(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return DateTime.MinValue;

            if (value.Replace("00:00:00", "").Trim().Replace("/", "").Length != 8)
                throw new Exception(string.Format("Formato incorreto de data [{0}]", value));

            if (value.Substring(2, 1).Equals("/") & value.Substring(5, 1).Equals("/"))
                return value.To<DateTime>();

            var day = value.Substring(0, 2).To<int>();
            var month = value.Substring(2, 2).To<int>();
            var year = value.Substring(4, 4).To<int>();

            return new DateTime(year, month, day);
        }

        public static object To(this object value, Type conversionType)
        {
            if (conversionType == null)
                throw new ArgumentNullException("conversionType");

            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                    return null;
                NullableConverter nullableConverter = new NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            else if (conversionType == typeof(Guid))
            {
                return new Guid(value.ToString());

            }
            else if (conversionType == typeof(Int64) && value.GetType() == typeof(int))
            {
                throw new InvalidOperationException("Can't convert an Int64 (long) to Int32(int).");
            }

            if ((value is string || value == null || value is DBNull) &&
                (conversionType == typeof(short) ||
                conversionType == typeof(int) ||
                conversionType == typeof(long) ||
                conversionType == typeof(double) ||
                conversionType == typeof(decimal) ||
                conversionType == typeof(float)))
            {
                decimal number;
                if (!decimal.TryParse(value as string, out number))
                    value = "0";
            }

            return Convert.ChangeType(value, conversionType);
        }

        /// <summary>
        /// Remover acentos da strins
        /// </summary>
        /// <param name="pValue">String com Acentos</param>
        /// <returns>Uma string sem accentos</returns>
        public static string NoAccents(this string pValue)
        {
            if (String.IsNullOrEmpty(pValue))
                return pValue;

            string normalizedString = pValue.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(normalizedString[i]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(normalizedString[i]);
                }
            }
            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }



        /// <summary>
        /// Converter caracteres de simbols (&) para (&amp) < > " ' para códigos HMTLs
        /// </summary>
        /// <param name="pValue">String com Acentos</param>
        /// <returns>Uma string sem accentos</returns>
        public static string ToParserSimbol(this string pValue)
        {
            if (String.IsNullOrEmpty(pValue))
                return pValue;

            var dic = new Dictionary<char, string>(){ 
                { '<' , "&alt;" },
                { '>' , "&gt;" },
                { '&' , "&amp;" },
                { '"' , "&quot;" },
                {  "'"[0] , "&#39" }
            };

            foreach (var item in dic)
                pValue = pValue.Replace(item.Key.ToString(), item.Value);

            return pValue;
        }
    }
}
