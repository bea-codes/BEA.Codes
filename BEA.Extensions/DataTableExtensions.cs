using System.ComponentModel;
using System.Data;

namespace BEA.Extensions;

public static class DataTableExtensions
{
    public static DataTable ToDataTable<T>(this IList<T> data)
    {
        var props = TypeDescriptor.GetProperties(typeof(T));
        var table = new DataTable();

        for (int i = 0, loopTo = props.Count - 1; i <= loopTo; i++)
        {
            var prop = props[i];
            table.Columns.Add(prop.Name, prop.PropertyType);
        }

        object[] values = new object[props.Count];

        foreach (T item in data)
        {

            for (int i = 0, loopTo1 = values.Length - 1; i <= loopTo1; i++)
                values[i] = props[i].GetValue(item);

            table.Rows.Add(values);
        }

        return table;
    }

    public static T Then<T>(this bool value, T result)
    {
        return value ? result : default(T);
    }
}
