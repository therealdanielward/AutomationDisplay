using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestersErrorLog.Helpers
{
    public class TableNames
    {
        public string Name { get; set; }
    }
    public static class SelectListHelper
    {
       
        public static List<SelectListItem> CreateSelectListItems<T>(IList<T> entities, Func<T, object> funcToGetValue,
            Func<T, object> funcToGetText)
        {
            var items = entities
                .Select(x => new SelectListItem
                {
                    Value = funcToGetValue(x).ToString(),
                    Text = funcToGetText(x).ToString()
                }).ToList();

            return items;
        }

        public static SelectList CreateSelectList<T>(IList<T> entities, Func<T, object> funcToGetValue,
            Func<T, object> funcToGetText)
        {
            var items = CreateSelectListItems(entities, funcToGetValue, funcToGetText);

            return new SelectList(items, "Value", "Text");
        }
        
        public static SelectList GetEmpty()
        {
            return new SelectList(new List<SelectListItem>());
        }
        
        public static SelectList GetTableNames()
        {
            var tableNames = new List<TableNames>();

            DataTable dt = new DataTable();
            string query = @"Use Testers
                            SELECT o.NAME,
                            i.rowcnt 
                            FROM sysindexes AS i
                            INNER JOIN sysobjects AS o ON i.id = o.id 
                            WHERE i.indid < 2  AND OBJECTPROPERTY(o.id, 'IsMSShipped') = 0 AND i.rowcnt > 0
                            ORDER BY o.NAME";

            SqlDataAdapter adapter = new SqlDataAdapter(query, System.Configuration.ConfigurationManager.ConnectionStrings["Testers"].ConnectionString);
            adapter.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                tableNames.Add(new TableNames() { Name = row["name"].ToString().Replace("tbl_ErrorLog_", "") + "  ("+row["rowcnt"].ToString()+")"});
            }

            return CreateSelectList(tableNames, x => x.Name, x => x.Name);
        }
        
        
    }
}

