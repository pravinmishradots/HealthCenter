using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Security.Cryptography;

namespace HC.Core
{
    public static class Extensions
    {
        #region "Dictionary"

        public static void AddOrReplace(this IDictionary<string, object> DICT, string key, object value)
        {
            if (DICT.ContainsKey(key))
                DICT[key] = value;
            else
                DICT.Add(key, value);
        }

        public static dynamic GetObjectOrDefault(this IDictionary<string, object> DICT, string key)
        {
            if (DICT.ContainsKey(key))

                return DICT[key];
            else
                return null;
        }

        public static T GetObjectOrDefault<T>(this IDictionary<string, object> DICT, string key)
        {
            if (DICT.ContainsKey(key))
                return (T)Convert.ChangeType(DICT[key], typeof(T));
            else
                return default(T);
        }

        #endregion "Dictionary"

        #region "String"

        public static string ToSelfURL(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            string outputStr = text.Trim().Replace(":", "").Replace("&", "").Replace(" ", "-").Replace("'", "").Replace(",", "").Replace("(", "").Replace(")", "").Replace("--", "").Replace(".", "");
            return Regex.Replace(outputStr.Trim().ToLower().Replace("--", ""), "[^a-zA-Z0-9_-]+", "", RegexOptions.Compiled);
        }

        public static string ToRemoveSpecialCharacter(this string str, params char[] specialChars)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            var sb = new StringBuilder();
            foreach (char c in str)
            {
                if (!specialChars.Contains(c))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static string RemoveSpecialCharacters(this string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static string ToSafeFileName(this string s)
        {
            return s
                .Replace("\\", "")
                //.Replace("/", "")
                .Replace("\"", "")
                .Replace("*", "")
                .Replace(":", "")
                .Replace("?", "")
                .Replace("<", "")
                .Replace(">", "")
                .Replace("|", "");
        }

        public static string ToRemoveSpecialChars(this string str, params char[] allowSpecialChars)
        {
            if (str == null) return str;
            var sb = new StringBuilder();
            foreach (char c in str)
            {
                if (char.IsLetterOrDigit(c) || (allowSpecialChars.Length > 0 && allowSpecialChars.Contains(c)))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static string ISNULL(this string str, params string[] alternatives)
        {
            if (!string.IsNullOrEmpty(str) && !string.IsNullOrWhiteSpace(str)) return str.Trim();
            else if (alternatives.FirstOrDefault(s => !string.IsNullOrEmpty(s) && !string.IsNullOrWhiteSpace(s)) != null)
                return alternatives.FirstOrDefault(s => !string.IsNullOrEmpty(s) && !string.IsNullOrWhiteSpace(s)).Trim();
            else
                return string.Empty;
        }

        public static bool IsNotNullAndNotEmpty(this string str)
        {
            if (!string.IsNullOrEmpty(str) && !string.IsNullOrWhiteSpace(str)) return true;
            else
                return false;
        }

        public static string AddBracket(this string str)
        {
            if (!string.IsNullOrWhiteSpace(str)) return "[" + str + "]";
            else
                return str;
        }

        public static string AddBranchPrefix(this string str, string branchReference, string charityPrefix)
        {
            if (!string.IsNullOrWhiteSpace(branchReference)) { return $"[{branchReference}] {str}"; }
            else if (!string.IsNullOrWhiteSpace(charityPrefix)) { return $"[{charityPrefix}] {str}"; }
            else { return str; }
        }

        public static string AddCharityPrefix(this string str, string charityPrefix)
        {
            if (!string.IsNullOrWhiteSpace(charityPrefix)) { return $"[{charityPrefix}] {str}"; }
            else { return str; }
        }

        public static string TrimLength(this string input, int length, bool Incomplete = true)
        {
            if (String.IsNullOrEmpty(input)) { return String.Empty; }
            return input.Length > length ? String.Concat(input.Substring(0, length), Incomplete ? "..." : "") : input;
        }

        public static string ToTitle(this string input)
        {
            //return String.IsNullOrEmpty(input) ? String.Empty : (input.All(c => char.IsLetter(c) ? char.IsUpper(c) : true) ? input : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower()));
            if (String.IsNullOrWhiteSpace(input))
                return string.Empty;

            string[] words = input.Split(' ');
            string finalString = string.Empty;
            foreach (string word in words)
            {
                finalString += String.IsNullOrEmpty(word) ? String.Empty : ((char.IsLetter(word.First()) ? char.IsUpper(word.First()) : true) ? word : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word.ToLower()));
                finalString += " ";
            }
            return finalString.Trim();
        }

        public static string ToCamelCase(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                return Char.ToLowerInvariant(str[0]) + str.Substring(1);
            }
            return str;
        }

        public static bool ContainsAny(this string input, params string[] values)
        {
            return String.IsNullOrEmpty(input) ? false : values.Any(S => input.Contains(S));
        }

        public static string ReplaceEscape(this string input)
        {
            return String.IsNullOrEmpty(input) ? String.Empty : input.Replace("'", "''");
        }

        public static string ReplaceMultiSpaces(this string input)
        {
            return String.IsNullOrWhiteSpace(input) ? String.Empty : Regex.Replace(input, @"\s+", " ", RegexOptions.Multiline);
        }

        public static string ReplaceSymbol(this string input)
        {
            return String.IsNullOrEmpty(input) ? String.Empty : new string(input.Where(e => !char.IsSymbol(e)).ToArray()).Trim();
        }

        public static string ReplaceAt(this string str, int index, int length, string replace)
        {
            return str.Remove(index, Math.Min(length, str.Length - index))
                    .Insert(index, replace);
        }

        public static DateTime ToDateTime(this string str, bool isWithTime = false)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(str))
                    return DateTime.Now;

                if (isWithTime)
                {
                    if (DateTime.TryParseExact(str, "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                    {
                        return result;
                    }
                    else if (DateTime.TryParseExact(str, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                    {
                        return result;
                    }
                }
            }
            catch
            {
                return DateTime.ParseExact(str, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
            }
            return DateTime.ParseExact(str, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        public static DateTime ToDateTimeByFormat(this string str, string format)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(str))
                    return DateTime.Now;
            }
            catch
            {
                return DateTime.ParseExact(str, format, CultureInfo.InvariantCulture, DateTimeStyles.None);
            }
            return DateTime.ParseExact(str, format, CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        public static DateTime AddEndTime(this DateTime date)
        {
            try
            {
                return date.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            catch
            {
                return date;
            }
        }

        public static DateTime? UnixTimestampToDateTime(this long? unixTime)
        {
            if (unixTime.HasValue)
            {
                DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                long unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
                return new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc);
            }
            else
            {
                return null;
            }
        }

        public static DateTime? ToDateTimeNullable(this string str, bool isWithTime = false)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(str))
                    return null;

                if (isWithTime)
                {
                    return DateTime.ParseExact(str, "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None);
                }
            }
            catch
            {
                return DateTime.ParseExact(str, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
            }
            return DateTime.ParseExact(str, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        public static string ToGenerateHash(this string input)
        {
            string hash = string.Empty;
            try
            {
                System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
                byte[] claimUnicodeStringAsBytes = UTF8.GetBytes(input);

                using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
                {
                    hash = Convert.ToBase64String(sha1.ComputeHash(claimUnicodeStringAsBytes));
                }
            }
            catch { }
            return hash;
        }

        public static string PercentEncode(this string s)
        {
            const string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
            var encoded = new StringBuilder();
            foreach (var c in s)
            {
                if (unreservedChars.Contains(c))
                {
                    encoded.Append(c);
                }
                else
                {
                    encoded.Append('%');
                    encoded.Append(String.Format("{0:X2}", (int)c));
                }
            }
            return encoded.ToString();
        }

        public static string ToUnderscoreCase(this string s)
        {
            return Regex.Replace(s, @"(\p{Ll})(\p{Lu})", "$1_$2").ToLower();
        }

        public static int ToInt(this string value, int defaultValue = 0)
        {
            int result;
            return int.TryParse(value, out result) ? result : defaultValue;
        }

        public static bool IsInteger(this string value)
        {
            int result;
            return int.TryParse(value, out result);
        }

        public static bool IsDecimal(this string value)
        {
            decimal result;
            return decimal.TryParse(value, out result);
        }

        public static bool IsDateTime(this string value)
        {
            DateTime result;
            return DateTime.TryParse(value, out result);
        }

        public static Tuple<bool, DateTime?> IsDateTimeFormat(this string value, string format)
        {
            DateTime result;
            if (DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return new Tuple<bool, DateTime?>(true, result);
            }
            return new Tuple<bool, DateTime?>(false, null);
        }

        public static T ToObject<T>(this string xml)
        {
            var stringReader = new System.IO.StringReader(xml);
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(stringReader);
        }

        public static bool IsAnyNullOrEmpty(params string[] str)
        {
            return str.Any(s => string.IsNullOrWhiteSpace(s));
        }

        public static string ToTrim(this string input)
        {
            if (!string.IsNullOrEmpty(input) && !string.IsNullOrWhiteSpace(input))
                return input.Trim();
            else
                return null;
        }


        #endregion "String"

        #region "DateTime"

        public static string ToDayAndDateFormatString(this DateTime date)
        {
            return date.ToString("ddd, dd MMM, yyyy");
        }
        public static string ToDayAndDateFormatString(this DateTime? date)
        {
            if (!date.HasValue)
                return string.Empty;

            return date.Value.ToString("ddd, dd MMM, yyyy");
        }

        public static string ToFormatString(this DateTime? date)
        {
            if (!date.HasValue)
                return string.Empty;

            return date.Value.ToString("dd MMM, yyyy");
        }

        public static string ToFormatString(this DateTime date)
        {
            return date.ToString("dd MMM, yyyy");
        }

        public static string ToFormatCustomString(this DateTime date)
        {
            //return date.ToString("dd/MM/yyyy");
            return date.ToString("dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo);
        }

        public static string ToFormatCustomStringWithTime(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy hh:mm:ss", DateTimeFormatInfo.InvariantInfo);
        }

        public static string ToFormatCustomString(this DateTime date, string dateformat)
        {
            return date.ToString(dateformat);
        }

        public static string ToFormatCustomString(this DateTime? date)
        {
            if (!date.HasValue)
                return string.Empty;

            return date.Value.ToString("dd/MM/yyyy");
        }

        public static DateTime? ToDBFDateTime(this DateTime date)
        {
            if (date.ToString("dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo) == "30/12/1899")
                return null;

            return date;
        }

        public static string ToDateTimeString(this DateTime? date)
        {
            if (!date.HasValue)
                return string.Empty;

            return date.Value.ToString("dd/MM/yyyy HH:mm");
        }

        public static string ToDateTimeString(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy HH:mm");
        }

        public static string IsoFormatTime(this DateTimeOffset time)
        {
            return time.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }

        public static string ToFinancialYear(this DateTime dateTime)
        {
            return "31/12/" + dateTime.Year;
        }



        public static string TwoDigits(this int digit)
        {
            return (digit > 9 ? digit.ToString() : ("0" + digit));
        }

        public static string GetTaxYears(this DateTime dateTime)
        {
            DateTime finYearEndDate = ("05/04/2000").ToDateTime();
            DateTime currDate = ($"{dateTime.Day.TwoDigits()}/{dateTime.Month.TwoDigits()}/2000").ToDateTime();

            if (currDate <= finYearEndDate)
                return string.Format("{0}-{1}", dateTime.Year - 1, dateTime.Year);
            else
                return string.Format("{0}-{1}", dateTime.Year, dateTime.Year + 1);
        }

        public static Tuple<int, int> GetFinancialYears(this DateTime dateTime)
        {
            DateTime finYearEndDate = ("05/04/2000").ToDateTime();
            DateTime currDate = ($"{dateTime.Day.TwoDigits()}/{dateTime.Month.TwoDigits()}/2000").ToDateTime();

            if (currDate <= finYearEndDate)
                return new Tuple<int, int>(dateTime.Year - 1, dateTime.Year);
            else
                return new Tuple<int, int>(dateTime.Year, dateTime.Year + 1);
        }

        public static DateTime GetFinancialStartDate(this DateTime financialEndDate)
        {
            //int finsEndYear = GetFinancialYears(DateTime.Now).Item2;
            DateTime finsEndYear = new DateTime(DateTime.Now.Year, financialEndDate.Month, financialEndDate.Day);
            var finStDate = finsEndYear >= DateTime.Now.Date ? finsEndYear : finsEndYear.AddYears(1);
            return finStDate.AddMonths(-12).AddDays(1);
        }

        #endregion "DateTime"

        #region "Collection"

        public static void ForEachIgnore<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source != null && source.Count() >= 0)
            {
                foreach (T element in source)
                {
                    action(element);
                }
            }
        }

        public static bool IsNotNullAndNotEmpty<T>(this ICollection<T> source)
        {
            return source != null && source.Count() > 0;
        }

        public static bool IsNotNullAndNotEmpty<T>(this IEnumerable<T> source)
        {
            return source != null && source.Count() > 0;
        }

        public static List<int> ToNumeric(this ICollection<string> source)
        {
            List<int> values = new List<int>();
            int outputvalue;
            foreach (var item in source)
            {
                if (int.TryParse(item, out outputvalue))
                {
                    values.Add(outputvalue);
                }
            }
            return values;
        }



        public static Func<T, object> CustomOrderBy<T>(this string orderByStringCol)
        {
            var propertyInfo = typeof(T).GetProperty(orderByStringCol);
            return x => propertyInfo.GetValue(x, null);
        }

        public static IQueryable<T> CustomOrderBy<T>(this IQueryable<T> source, string orderByStringCol, bool isOrderByDesc = false)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(orderByStringCol))
                {
                    var propertyInfo = typeof(T).GetProperty(orderByStringCol);
                    var isDecimal = source.Any(x => Convert.ToString(propertyInfo.GetValue(x, null)).IsDecimal());
                    var isDateTime = false;

                    if (!isDecimal)
                    {
                        isDateTime = source.Any(x => Convert.ToString(propertyInfo.GetValue(x, null)).IsDateTime());
                    }

                    Func<T, object> order;

                    if (isDateTime)
                    {
                        order = x => (Convert.ToString(propertyInfo.GetValue(x, null)).IsDateTime() ? Convert.ToDateTime(propertyInfo.GetValue(x, null)) : (DateTime?)null);
                    }
                    else
                    {
                        order = x => propertyInfo.GetValue(x, null);
                    }

                    if (isOrderByDesc)
                    {
                        return source.OrderByDescending(order).AsQueryable();
                    }
                    else
                    {
                        return source.OrderBy(order).AsQueryable();
                    }
                }
            }
            catch
            {
            }
            return source;
        }

        public static string ToQueryString(this List<KeyValuePair<string, object>> hash)
        {
            var s = new StringBuilder();
            foreach (string key in hash.Select(x => x.Key).Distinct().OrderBy(x => x))
            {
                string key1 = key;
                foreach (var value in hash.Where(x => x.Key == key1).Select(x => x.Value).OrderBy(x => x.ToString()))
                {
                    s.Append("&");
                    s.Append(key.ToUrlString());
                    s.Append("=");
                    s.Append(value.ToUrlString());
                }
            }

            return s.ToString().Substring(1);
        }

        #endregion "Collection"

        #region "Enums"

        public static IDictionary<string, int> EnumToDictionary(this Type t)
        {
            if (t == null) throw new NullReferenceException();
            if (!t.IsEnum) throw new InvalidCastException("object is not an Enumeration");

            string[] names = Enum.GetNames(t);
            Array values = Enum.GetValues(t);

            return (from i in Enumerable.Range(0, names.Length)
                    select new { Key = names[i], Value = (int)values.GetValue(i) })
                        .ToDictionary(k => k.Key, k => k.Value);
        }

        public static IDictionary<string, int> EnumToDictionaryWithDescription(this Type t)
        {
            if (t == null) throw new NullReferenceException();
            if (!t.IsEnum) throw new InvalidCastException("object is not an Enumeration");

            string[] names = Enum.GetNames(t);
            Array values = Enum.GetValues(t);

            return Enumerable.Range(0, names.Length)
                .Select(i => new { Key = ((Enum)values.GetValue(i)).GetDescription(), Value = (int)values.GetValue(i) })
                .ToDictionary(k => k.Key, k => k.Value);
        }

        public static IDictionary<string, string> EnumToDictionaryKeyWithDescription(this Type t)
        {
            if (t == null) throw new NullReferenceException();
            if (!t.IsEnum) throw new InvalidCastException("object is not an Enumeration");

            string[] names = Enum.GetNames(t);
            Array values = Enum.GetValues(t);

            return Enumerable.Range(0, names.Length)
                .Select(i => new { Key = ((Enum)values.GetValue(i)).GetDescription(), Value = values.GetValue(i).ToString() })
                .ToDictionary(k => k.Key, k => k.Value.ToString());
        }

        public static DataTable DictionaryToDataTable(this List<Dictionary<string, object>> list)
        {
            DataTable result = new DataTable();
            if (list.Count == 0)
                return result;

            result.Columns.AddRange(
                list.First().Select(r => new DataColumn(r.Key)).ToArray()
            );

            list.ForEach(r => result.Rows.Add(r.Select(c => c.Value).ToArray()));

            return result;
        }

        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DisplayAttribute[] attributes = (DisplayAttribute[])fi.GetCustomAttributes(typeof(DisplayAttribute), false);
            DescriptionAttribute[] descriptionAttribute = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Name;
            else if (descriptionAttribute != null && descriptionAttribute.Length > 0)
                return descriptionAttribute[0].Description;
            else
                return value.ToString();
        }

        public static string GetDescription<T>(this string value)
        {
            MemberInfo property = typeof(T).GetProperty(value);

            if (property != null)
            {
                DisplayNameAttribute[] attributes = (DisplayNameAttribute[])property.GetCustomAttributes(typeof(DisplayNameAttribute), false);

                if (attributes != null && attributes.Length > 0)
                    return attributes[0].DisplayName;
            }
            return value.ToString();
        }


        public static List<string> GetPropertiesNameOfClass<T>()
        {
            List<string> propertyList = new List<string>();
            foreach (var prop in typeof(T).GetProperties())
            {
                propertyList.Add(prop.Name);
            }
            return propertyList;
        }


        public static DataTable ToDataTable<T>(this IList<T> list)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in list)
            {
                for (int i = 0; i < values.Length; i++)
                    values[i] = props[i].GetValue(item) ?? DBNull.Value;
                table.Rows.Add(values);
            }
            return table;
        }

        public static T ParseEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        #endregion "Enums"

        #region "Object"

        public static XDocument ToXML(this object obj, string defaultNameSpace = "")
        {
            XmlSerializer x = new XmlSerializer(obj.GetType());
            XDocument XDoc = new XDocument();

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            if (defaultNameSpace != "")
            {
                //  Add lib namespace with empty prefix
                ns.Add("", defaultNameSpace);
            }
            using (var writer = XDoc.CreateWriter())
            {
                x.Serialize(writer, obj, ns);
            }

            if (XDoc != null)
                return XDoc;

            return null;
        }

        public static string ToUrlString(this object o)
        {
            if (o is DateTimeOffset)
            {
                return ((DateTimeOffset)o).IsoFormatTime().PercentEncode();
            }
            if (o is decimal)
            {
                return ((decimal)o).ToString("0.00", CultureInfo.InvariantCulture);
            }
            return o.ToString().PercentEncode();
        }

        public static List<KeyValuePair<string, object>> ToKeyValuePairList(this object queryStringable, List<KeyValuePair<string, object>> hash = null, string prefix = null)
        {
            Func<object, bool> isOfSimpleType = o =>
            {
                var type = o.GetType();
                return type.IsPrimitive
                       || type == typeof(string)
                       || type == typeof(decimal)
                       || type == typeof(DateTimeOffset)
                    ;
            };

            PropertyInfo[] propertyInfos = queryStringable.GetType().GetProperties(
                BindingFlags.Public | BindingFlags.Instance);

            hash = hash ?? new List<KeyValuePair<string, object>>();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.GetSetMethod(true) == null)
                {
                    continue;
                }
                var value = propertyInfo.GetValue(queryStringable, null);
                if (value != null)
                {
                    var propertyName = (prefix != null)
                                           ? prefix + "[" + propertyInfo.Name.ToUnderscoreCase() + "]"
                                           : propertyInfo.Name.ToUnderscoreCase();
                    if (value is Array)
                    {
                        foreach (var innerValue in (Array)value)
                        {
                            if (isOfSimpleType(innerValue))
                            {
                                if (innerValue is Boolean)
                                {
                                    hash.Add(new KeyValuePair<string, object>(propertyName + "[]", Convert.ToInt16(innerValue)));
                                }
                                else
                                {
                                    hash.Add(new KeyValuePair<string, object>(propertyName + "[]", innerValue));
                                }
                            }
                            else
                            {
                                innerValue.ToKeyValuePairList(hash, propertyName + "[]");
                            }
                        }
                    }
                    else if (isOfSimpleType(value))
                    {
                        if (value is Boolean)
                        {
                            hash.Add(new KeyValuePair<string, object>(propertyName, Convert.ToInt16(value)));
                        }
                        else
                        {
                            hash.Add(new KeyValuePair<string, object>(propertyName, value));
                        }
                    }
                    else
                    {
                        value.ToKeyValuePairList(hash, propertyName);
                    }
                }
            }
            return hash;
        }

        public static string ToFileNameString(this Guid guid)
        {
            return guid.ToString().ToLower();
        }

        #endregion "Object"

        #region "Decimal To String"

        public static string ToPriceString(this decimal price)
        {
            if (price / 100 < 999)
            {
                return price.ToString("#,#.00#").Replace(".00", "");
            }
            else if (price / 100000 < 99)
            {
                return string.Format("{0:0.00}", price / 100000).Replace(".00", "") + " Lacs";
            }
            else
            {
                return string.Format("{0:0.00}", price / 10000000).Replace(".00", "") + " Crores";
            }
        }

        public static string ToUrlPriceString(this decimal price)
        {
            if (price / 1000 < 99)
            {
                return string.Format("{0:0.00}", price / 1000).Replace(".00", "") + "-thousands";
            }
            else if (price / 100000 < 99)
            {
                return string.Format("{0:0.00}", price / 100000).Replace(".00", "") + "-lacs";
            }
            else
            {
                return string.Format("{0:0.00}", price / 10000000).Replace(".00", "") + "-crores";
            }
        }

        public static decimal ToDecimalUnit(this string value)
        {
            switch (value)
            {
                case "thousands":
                    return 1000M;

                case "lacs":
                    return 100000M;

                case "crores":
                    return 10000000M;

                default:
                    return 1M;
            }
        }

        public static string ToStringFormat(this decimal number)
        {
            //return string.Format("{0:0.00}", number).Replace(".00", "");
            return number.ToString("0.#####");
        }

        public static string ToMoney(this decimal? number)
        {
            if (number.HasValue)
                return new Regex(@"^0+(?=\d)").Replace(string.Format("{0:0,0.00}", number), "");
            else
                return string.Empty;
        }

        public static string ToMoney(this decimal number)
        {
            return string.Format("{0:0.00}", number);
        }

        public static object ToMoney(this object str)
        {
            if (str != null && str.GetType() == typeof(decimal) && str.ToString().IsDecimal())
                return string.Format("{0:0.00}", Convert.ToDecimal(str));
            else
                return str;
        }

        public static dynamic ToDynamic(this object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
                expando.Add(property.Name, property.GetValue(value));

            return (ExpandoObject)expando;
        }

        #endregion "Decimal To String"

        #region "Boolean"

        public static Boolean ToBoolean(this string value)
        {
            if (bool.TryParse(value, out bool boolValue))
            {
                return boolValue;
            }
            int.TryParse(value, out int number);
            return Convert.ToBoolean(number);
        }

        public static bool IsLiveURL(this string url)
        {
            try
            {
                string HTMLSource = string.Empty;
                using (WebClient client = new WebClient())
                {
                    HTMLSource = client.DownloadString(url);
                }
                if (HTMLSource.Length > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        #endregion "Boolean"


        public static object GetPropValue(this object src, string propName)
        {
            src.GetType().GetProperty(propName).SetValue(src, "");
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static void SetPropValue(this object src, string propName, object value)
        {
            src.GetType().GetProperty(propName).SetValue(src, value);
        }

        public static bool HasProperty<T>(T obj, string name)
        {
            Type objType = obj.GetType();

            if (objType == typeof(ExpandoObject))
            {
                return ((IDictionary<string, object>)obj).ContainsKey(name);
            }

            return objType.GetProperty(name) != null;
        }

        public static string GetDonorLinkCode()
        {
            return Guid.NewGuid().ToString();
        }

        public static string RandomCode(int length)
        {
            System.Security.Cryptography.RandomNumberGenerator rng = System.Security.Cryptography.RandomNumberGenerator.Create();

            char[] chars = new char[length];

            //based on your requirment you can take only alphabets or number
            string validChars = "abcdefghijklmnopqrstuvwxyzABCEDFGHIJKLMNOPQRSTUVWXYZ1234567890";

            for (int i = 0; i < length; i++)
            {
                byte[] bytes = new byte[1];
                rng.GetBytes(bytes);

                Random rnd = new Random(bytes[0]);

                chars[i] = validChars[rnd.Next(0, 61)];
            }

            return (new string(chars));
        }

        public static MemoryStream SerializeList(List<object> list)
        {
            XmlSerializer _serializer = new XmlSerializer(typeof(List<object>));
            using (var stream = new MemoryStream())
            {
                _serializer.Serialize(stream, list);
                stream.Position = 0;
                return stream;
            }
        }


        public static T CloneObject<T>(this T objSource, bool mapComplexTypes = false)
        {
            //Get the type of source object and create a new instance of that type
            Type typeSource = objSource.GetType();

            dynamic objTarget = Activator.CreateInstance(typeSource);

            //Get all the properties of source object type
            var propertyInfo = typeSource.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            //Assign all source property to target object 's properties
            foreach (var property in propertyInfo)
            {
                //Check whether property can be written to
                if (property.CanWrite)
                {
                    //check whether property type is value type, enum or string type
                    if (property.PropertyType.IsValueType || property.PropertyType.IsEnum || property.PropertyType.Equals(typeof(string)))
                    {
                        property.SetValue(objTarget, property.GetValue(objSource, null), null);
                    }

                    //else property type is object/complex types, so need to recursively call this method until the end of the tree is reached
                    else if (mapComplexTypes)
                    {
                        var objPropertyValue = property.GetValue(objSource, null);
                        if (objPropertyValue == null)
                        {
                            property.SetValue(objTarget, null, null);
                        }
                        else
                        {
                            property.SetValue(objTarget, objPropertyValue.CloneObject(true), null);
                        }
                    }
                }
            }

            return objTarget;
        }

        public static string ToOccurrenceSuffix(this int integer)
        {
            switch (integer % 100)
            {
                case 11:
                case 12:
                case 13:
                    return "th";
            }
            switch (integer % 10)
            {
                case 1:
                    return "st";
                case 2:
                    return "nd";
                case 3:
                    return "rd";
                default:
                    return "th";
            }
        }

        public static bool IsValidEmail(this string stremail)
        {
            // Return true if stremail is in valid single or multiple e-mail format.
            if (string.IsNullOrWhiteSpace(stremail) || string.IsNullOrEmpty(stremail))
                return false;
            else
                return Regex.IsMatch(stremail, @"^(([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$))*$");
        }

        public static byte[] ObjectToByteArray(this object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static string GetIPAddress()
        {
            try
            {
                return Dns.GetHostEntry(Dns.GetHostName()).AddressList.ToList().Where(p => p.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault().ToString();
                //IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
                //return ipEntry.AddressList[0].ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        //public static Image GetReducedImage(int width, int height, Stream resourceImage)
        //{
        //    try
        //    {
        //        var image = Image.FromStream(resourceImage);
        //        var thumb = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);

        //        return thumb;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        public static decimal ToTimeDecimal(this string value)
        {
            decimal.TryParse(value.Replace(":", "."), out decimal result);
            return result;
        }

        /// <summary>
        /// Append parameter by separator
        /// </summary>
        public static string ToAppend(this string vale, string seprator, params string[] param)
        {
            var str = !string.IsNullOrWhiteSpace(vale) ? vale.Trim() : string.Empty;
            for (int i = 0; i < param.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(str) && !string.IsNullOrWhiteSpace(param[i]))
                {
                    str += seprator;
                }

                if (!string.IsNullOrWhiteSpace(param[i]))
                {
                    str += param[i].Trim();
                }
            }
            return str;
        }


        public static DateTimeOffset? ToDateTimeOffset(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return null;

            return DateTimeOffset.ParseExact(str, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);

        }

        //public static DateTime ToCustomFinancialYear(this DateTime dateTime, FinancialYear financialYear)
        //{
        //    if (financialYear == FinancialYear.CurrentFinancialYearStartDate)
        //        return dateTime.AddYears(-1);
        //    else if (financialYear == FinancialYear.CurrentFinancialYearEndDate)
        //        return dateTime;
        //    else if (financialYear == FinancialYear.PreviousFinancialYearStartDate)
        //        return dateTime.AddYears(-2);
        //    else if (financialYear == FinancialYear.PreviousFinancialYearEndDate)
        //        return dateTime.AddYears(-1);

        //    return dateTime;
        //}
    }
}
