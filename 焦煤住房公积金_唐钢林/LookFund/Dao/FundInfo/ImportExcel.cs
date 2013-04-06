using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;

namespace LookFund.Dao.FundInfo
{
    public class ImportExcel
    {
        public DataTable ReadFile(string source, string query)
        {
            string ConnStr = string.Empty;
            ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + source + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
          
            OleDbCommand oleCommand = new OleDbCommand(query, new OleDbConnection(ConnStr));
            OleDbDataAdapter oleAdapter = new OleDbDataAdapter(oleCommand);
            DataTable myDataSet = new DataTable();
            // 将Excel的表内容填充到DataSet对象 
            oleAdapter.Fill(myDataSet);
            return myDataSet;
        }
    }
}