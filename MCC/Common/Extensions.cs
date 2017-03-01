using System.Text.RegularExpressions;
using System;
using System.Data;
using System.Collections.Generic;

namespace MCC
{

    public static class Extensions
    {
        public static string SplitCapitalsInPlace(this string str)
        {
            return Regex.Replace(str, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");
        }

        public static string Format(this DataRow r, string format)
        {
            var col = r.Table.Columns;
            var arr = new object[col.Count];
            for (int i = 0; i < col.Count; i++)
            {
                format = format.Replace("{" + col[i].ColumnName + "}", "{" + i + "}");
                arr[i] = r[i];
            }
            var res=string.Format(format, arr);
            return res;
        }

        public static DataTable ToIdTable(this List<Guid> ids)
        {
            var dt = new DataTable();
            dt.Columns.Add("Id", typeof(Guid));     
            foreach (var id in ids)
                dt.Rows.Add(new object[] { id });
            return dt;
        }
    }
}