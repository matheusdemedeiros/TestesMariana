using System;
using System.Data.Common;
using System.Reflection;
using TestesMariana.Dominio.Compartilhado;

namespace TesteMariana.infra.DataBase.Compartilhado
{
    public static class MapeamentoExtensions
    {
        public static T MapearObjeto<T>(this DbDataReader dataReader)
        {
            T obj = (T)Activator.CreateInstance(typeof(T));

            PropertyInfo[] props = obj.GetType().GetFilteredProperties();

            foreach (PropertyInfo prop in props)
            {
                string nomeUppercase = prop.Name.ToUpper();

                int indexColuna = dataReader.GetOrdinal(nomeUppercase);

                Type tipoPoropriedade = dataReader.GetFieldType(indexColuna);

                if (prop.PropertyType == tipoPoropriedade)
                    prop.SetValue(obj, dataReader.GetFieldValue<dynamic>(indexColuna));
            }
            return obj;
        }
    }
}
