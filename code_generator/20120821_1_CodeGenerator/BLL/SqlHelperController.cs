using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _20120821_1_CodeGenerator.DAL;
using System.Data;
using _20120821_1_CodeGenerator.library;
using System.Data.SqlClient;
using System.IO;

namespace _20120821_1_CodeGenerator.BLL
{
    class SqlHelperController
    {
        public static string DatabaseSetting
        {
            get{
                return "server=.;database=" + GlobalParameters.DatabaseName + ";integrated security=true";
            }
        }
        /// <summary>
        /// Get a table information and show int the check list box
        /// </summary>
        /// <returns></returns>
        public static DataTable ShowTable(){
            string sqlQuery = "select table_name from INFORMATION_SCHEMA.TABLES";
            DataTable dt = SqlHelper.ExecuteDataTable(sqlQuery);
            return dt;
        }

        /// <summary>
        /// Get column information in a table
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns>datatable</returns>
        private static DataTable GetColInfo(string tableName) {
            string sqlQuery = "select COLUMN_NAME, DATA_TYPE from INFORMATION_SCHEMA.COLUMNS where table_name = @tableName";
            DataTable dt = SqlHelper.ExecuteDataTable(sqlQuery, new SqlParameter("@tableName",tableName));
            return dt;
        }

        /// <summary>
        /// Create a class for a table containing the column name and corresponding data type supported in C#
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="nameSpace"></param>
        /// <param name="outputPath"></param>
        public static void CreateModel(string tableName, string nameSpace, string outputPath) {
            DataTable dt = GetColInfo(tableName);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using System.Data.SqlClient;");
            sb.AppendLine("using System.Data;\r\n");
            sb.AppendLine("namespace " + nameSpace +".Model");
            sb.AppendLine("{");
            sb.AppendLine("\tpartial class " + tableName);
            sb.AppendLine("\t{");
            foreach (DataRow dr in dt.Rows) {
                sb.AppendLine("\t\tpublic " + ToNetType(dr["data_type"].ToString()) + " " + dr["column_name"].ToString() + " { set; get; }");
            }
            sb.AppendLine("\t}");
            sb.AppendLine("}");
            string path = Path.Combine(outputPath, tableName +".cs");
            File.WriteAllText(path, sb.ToString());
        }

        /// <summary>
        /// Create a data access layer for a table.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="nameSpace"></param>
        /// <param name="outputPath"></param>
        public static void CreateDAL(string tableName, string nameSpace, string outputPath) {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using System.Data.SqlClient;");
            sb.AppendLine("using System.Data;");
            sb.AppendLine("using " + nameSpace + ".Model;\r\n");
            sb.AppendLine("namespace " + nameSpace + ".DAL");
            sb.AppendLine("{");
            sb.AppendLine("\tpartial class " + tableName + "DAL");
            sb.AppendLine("\t{");
            DataTable dt = SqlHelper.ExecuteDataTable("select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = @tableName", new SqlParameter("@tableName", tableName));
            string[] colNames = new string[dt.Rows.Count-1];
            string[] paraNames = new string[dt.Rows.Count-1];
            //set up sql parameters for querying in sql server.
            for (int i = 1; i < dt.Rows.Count; i++) {
                colNames[i-1] = dt.Rows[i]["column_name"].ToString();
                paraNames[i-1] = "@" + dt.Rows[i]["column_name"].ToString();
            }
            //connect the column names into one string seperated by commas
            string colList = string.Join(",", colNames);
            string paraNameList = string.Join(",", paraNames);

            //AddNew method in DLL
            sb.AppendLine("\t\tpublic int AddNew(" + tableName + " model){");
            sb.AppendLine("\t\t\tstring sqlQuery = \"insert into " + tableName + "(" + colList + ") output inserted.id values(" + paraNameList + ")\";");
            string[] paraList = new string[dt.Rows.Count-1];
            for (int i = 0; i < dt.Rows.Count-1; i++) {
                paraList[i] = "new SqlParameter(\"" + paraNames[i] + "\", model." + colNames[i] + ")";
            }
            string newParaList = string.Join(",", paraList);
            sb.AppendLine("\t\t\tint id = (int)SqlHelper.ExecuteScalar(sqlQuery, " + newParaList + ");");
            sb.AppendLine("\t\treturn id;");
            sb.AppendLine("\t\t}");
            //****//

            //Update method in DLL
            sb.AppendLine("\r\n\t\tpublic bool Update(" + tableName + " model)");
            sb.AppendLine("\t\t{");
            string[] updatePara = new string[colNames.Length];
            for (int i = 0; i < colNames.Length; i++)
            {
                updatePara[i] = colNames[i] + "=" + paraNames[i];
            }
            string updateParaList = string.Join(",", updatePara);
            sb.AppendLine("\t\t\tstring sqlQuery = \"update " + tableName +" set " + updateParaList + " where id=@id\";");
            string[] newPara = new string[dt.Rows.Count];
            newPara[0] = "new SqlParameter(\"@Id\", model.Id)";
            for (int i = 1; i < dt.Rows.Count; i++) {
                newPara[i] = "new SqlParameter(\"" + paraNames[i - 1] + "\", model." + colNames[i - 1] + ")"; 
            }
            string updateNewPara = string.Join(",", newPara);
            sb.AppendLine("\t\t\tint rows = SqlHelper.ExecuteNonQuery(sqlQuery, " + updateNewPara + ");");
            sb.AppendLine("\t\t\treturn rows > 0;");
            sb.AppendLine("\t\t}");
            //****//

            //Delete Method
            sb.AppendLine("\r\n\t\tpublic bool Delete(int id)");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\tint rows = SqlHelper.ExecuteNonQuery(\"delete from " + tableName + " where id = @id\", new SqlParameter(\"@id\", id));");
            sb.AppendLine("\t\t\treturn rows > 0;");
            sb.AppendLine("\t\t}");
            //****//

            //private static ToModel method
            sb.AppendLine("\r\n\t\tprivate static " + tableName + " ToModel(DataRow row)");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\t" + tableName + " model = new " + tableName + "();");
            dt = SqlHelper.ExecuteDataTable("select COLUMN_NAME, DATA_TYPE from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = @tableName", new SqlParameter("@tableName", tableName));
            string[] toModelPara = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++) {
                toModelPara[i] = "model." + dt.Rows[i]["column_name"].ToString() + " = (" + ToNetClassType(dt.Rows[i]["data_type"].ToString()) +")row[\"" + dt.Rows[i]["column_name"].ToString() + "\"];";
                sb.AppendLine("\t\t\t"+toModelPara[i]);
            }
            sb.AppendLine("\t\t\treturn model;");
            sb.AppendLine("\t\t}");
            //****//

            //Get method
            sb.AppendLine("\r\n\t\tpublic " + tableName + " Get(int id)");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\tDataTable dt = SqlHelper.ExecuteDataTable(\"select * from " + tableName + " where id = @id\", new SqlParameter(\"@id\", id));");
            sb.AppendLine("\t\t\tif(dt.Rows.Count > 1)");
            sb.AppendLine("\t\t\t{");
            sb.AppendLine("\t\t\t\tthrow new Exception(\"more than 1 row was found\");");
            sb.AppendLine("\t\t\t}");
            sb.AppendLine("\t\t\tif(dt.Rows.Count <= 0) { return null;}");
            sb.AppendLine("\t\t\tDataRow row = dt.Rows[0];");
            sb.AppendLine("\t\t\tT_Seats model = ToModel(row);");
            sb.AppendLine("\t\t\treturn model;");
            sb.AppendLine("\t\t}");
            //****//

            //ListAll method
            sb.AppendLine("\r\n\t\tpublic IEnumerable<" + tableName + "> ListAll()");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\tList<" + tableName + "> list = new List<" + tableName + ">();");
            sb.AppendLine("\t\t\tDataTable dt = SqlHelper.ExecuteDataTable(\"select * from " + tableName + "\");");
            sb.AppendLine("\t\t\tforeach(DataRow row in dt.Rows)");
            sb.AppendLine("\t\t\t{");
            sb.AppendLine("\t\t\t\tlist.Add(ToModel(row));");
            sb.AppendLine("\t\t\t}");
            sb.AppendLine("\t\t\treturn list;");
            sb.AppendLine("\t\t}");
            //****//

            sb.AppendLine("\t}");
            sb.AppendLine("}");
            string path = Path.Combine(outputPath, tableName +"DAL.cs");
            File.WriteAllText(path, sb.ToString());
        }

        /// <summary>
        /// create a business logical layer for a table
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="nameSpace"></param>
        /// <param name="outputPath"></param>
        public static void CreateBLL(string tableName, string nameSpace, string outputPath) {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Text;");
            sb.AppendLine("using " + nameSpace + ".Model;");
            sb.AppendLine("using " + nameSpace + ".DAL;");
            sb.AppendLine("\r\nnamespace " + nameSpace + ".BLL");
            sb.AppendLine("{");
            sb.AppendLine("\tpartial class " + tableName + "BLL{");

            sb.AppendLine("\t\tpublic int AddNew(" + tableName + " model){");
            sb.AppendLine("\t\t\treturn new " + tableName + "DAL().AddNew(model);");
            sb.AppendLine("\t\t}");

            sb.AppendLine("\r\n\t\tpublic bool Delete(int id){");
            sb.AppendLine("\t\t\treturn new " + tableName + "DAL().Delete(id);");
            sb.AppendLine("\t\t}");

            sb.AppendLine("\r\n\t\tpublic bool Update(" + tableName + " model){");
            sb.AppendLine("\t\t\treturn new " + tableName + "DAL().Update(model);");
            sb.AppendLine("\t\t}");

            sb.AppendLine("\t\tpublic " + tableName + " Get(int id){");
            sb.AppendLine("\t\t\treturn new " + tableName + "DAL().Get(id);");
            sb.AppendLine("\t\t}");

            sb.AppendLine("\r\n\t\tpublic IEnumerable<" + tableName + "> ListAll(){");
            sb.AppendLine("\t\t\treturn new " + tableName + "DAL().ListAll();");
            sb.AppendLine("\t\t}");
            sb.AppendLine("\t}");
            sb.AppendLine("}");

            string path = Path.Combine(outputPath, tableName +"BLL.cs");
            File.WriteAllText(path, sb.ToString());
        }

        /// <summary>
        /// Convert a sql server data type into a C# primitive data type
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        private static string ToNetType(string dataType) {
            switch (dataType) { 
                case "int":
                    return "int";
                case "nvarchar":
                case "varchar":
                case "char":
                case "nchar":
                    return "string";
                case "bit":
                    return "bool";
                case "datetime":
                    return "DateTime";
                default:
                    return "object";
            }
        }

        /// <summary>
        /// Convert a sql server data type into a C# system class
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        private static string ToNetClassType(string dataType) {
            switch (dataType)
            { 
                case"int":
                    return "System.Int32";
                case"nvarchar":
                case"varchar":
                case "char":
                case"nchar":
                    return "System.String";
                case "bit":
                    return "System.Boolean";
                case "datetime":
                    return "System.DateTime";
                default:
                    return "System.Object";
            }
        }
    }
}
