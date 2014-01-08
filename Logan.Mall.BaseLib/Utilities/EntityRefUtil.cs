using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Logan.Mall.BaseLib.Utilities
{
    public class EntityRefUtil<T> where T : class
    {
        private delegate void SetValue<TP>(TP value);

        public static List<T> ToEntityList(DataTable dt)
        {
            try
            {
                List<T> list = new List<T>();


                if (dt != null && dt.Rows.Count > 0)
                {
                    Dictionary<string, DataMappingAttribute> _propertyInfoList = GetPropertyList();
                    var pi = typeof(T).GetProperties();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        T obj = Activator.CreateInstance<T>();

                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            foreach (var property in _propertyInfoList)
                            {
                                if (dt.Columns[j].ColumnName.ToUpper() != property.Key.ToUpper())
                                {
                                    continue;
                                }
                                PropertyInfo pro = typeof(T).GetProperty(property.Key);
                                if (pro.PropertyType.BaseType.Equals(typeof(DBNull)))
                                {
                                    continue;
                                }


                                pro.SetValue(obj, dt.Rows[i][j], null);

                            }
                        }
                        list.Add(obj);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private static Dictionary<string, DataMappingAttribute> GetPropertyList()
        {
            Dictionary<string, DataMappingAttribute> _propertyInfoList = new Dictionary<string, DataMappingAttribute>();
            Type type = typeof(T);
            var properties = type.GetProperties();
            foreach (var item in properties)
            {
                var attrs = item.GetCustomAttributes(false);
                foreach (var attr in attrs)
                {
                    if (attr is DataMappingAttribute)
                    {
                        DataMappingAttribute dataMappingAttribute = attr as DataMappingAttribute;
                        if (string.IsNullOrEmpty(dataMappingAttribute.FieldName))
                        {
                            dataMappingAttribute.FieldName = item.Name;
                        }
                        dataMappingAttribute.PropertyType = item.PropertyType;
                        _propertyInfoList.Add(dataMappingAttribute.FieldName, dataMappingAttribute);
                    }
                }
            }
            return _propertyInfoList;
        }

        //private static object ChangeType(object value, Type conversionType)
        //{
        //    if (conversionType.IsGenericType &&
        //        conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
        //    {

        //        if (value == null)
        //            return null;

        //        System.ComponentModel.NullableConverter nullableConverter
        //            = new System.ComponentModel.NullableConverter(conversionType);

        //        conversionType = nullableConverter.UnderlyingType;
        //    }

        //    return Convert.ChangeType(value, conversionType);
        //}

        //private static Delegate CreateSetValueDelegate(T model, string propertyName, Type proType)
        //{
        //    try
        //    {
        //        MethodInfo mi = model.GetType().GetProperty(propertyName).GetSetMethod();
        //        //这里构造泛型委托类型
        //        //Type defType = typeof(SetValue<>).GetField(propertyName).ReflectedType;
        //        //Type defType = typeof(SetValue<>).(GetPropertyType(propertyName));
        //        Type defType = typeof(SetValue<>).MakeGenericType(GetPropertyType(propertyName));

        //        return Delegate.CreateDelegate(defType, model, mi);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }

        //}

        //private static Type GetPropertyType(string propertyName)
        //{
        //    return typeof(T).GetProperty(propertyName).PropertyType;
        //}



        //private class DataEntitySource : IEnumerable<string>, IDisposable
        //{
        //    private IDataReader dr;

        //    public DataEntitySource(IDataReader _dr)
        //    {
        //        dr = _dr;
        //    }

        //    public object this[string columnName]
        //    {
        //        get { return dr[columnName]; }
        //    }

        //    public object this[int index]
        //    {
        //        get { return dr[index]; }
        //    }

        //    public IEnumerator<string> GetEnumerator()
        //    {
        //        return new ColumnEnumerator(dr);
        //    }

        //    IEnumerator IEnumerable.GetEnumerator()
        //    {
        //        return new ColumnEnumerator(dr);
        //    }

        //    public void Dispose()
        //    {
        //        this.Dispose();
        //    }

        //    private class ColumnEnumerator : IEnumerator<string>
        //    {
        //        private IEnumerator enumerator;

        //        public ColumnEnumerator(IDataReader _dr)
        //        {
        //            enumerator = _dr.GetSchemaTable().Rows.GetEnumerator();
        //        }

        //        public string Current
        //        {
        //            get { DataRow row = enumerator.Current as DataRow; return row["ColumnName"] as string; }
        //        }

        //        public void Dispose()
        //        {
        //            this.Dispose();
        //        }

        //        object IEnumerator.Current
        //        {
        //            get { return enumerator.Current; }
        //        }

        //        public bool MoveNext()
        //        {
        //            return enumerator.MoveNext();
        //        }

        //        public void Reset()
        //        {
        //            enumerator.Reset();
        //        }
        //    }
        //}
    }
}
