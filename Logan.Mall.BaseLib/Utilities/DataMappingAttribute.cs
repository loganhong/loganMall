using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logan.Mall.BaseLib.Utilities
{
    public class DataMappingAttribute : Attribute
    {
        public string FieldName { get; set; }

        public Type PropertyType { get; set; }

        public DataMappingAttribute()
        { }

        public DataMappingAttribute(string _fieldName)
        {
            FieldName = _fieldName;
        }

        public DataMappingAttribute(string _fieldName, Type _propertyType)
        {
            FieldName = _fieldName;
            PropertyType = _propertyType;
        }
    }
}

