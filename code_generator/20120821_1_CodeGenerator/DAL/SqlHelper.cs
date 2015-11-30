using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using _20120821_1_CodeGenerator.BLL;

namespace _20120821_1_CodeGenerator.DAL
{
    class SqlHelper
    {
        public static DataTable ExecuteDataTable(string sqlQuery, params SqlParameter[] parameters) {
            SqlConnection connection = ConnectDatabase();
            SqlCommand cmd = InitCommand(sqlQuery, parameters, connection);
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            try
            {
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public static SqlDataReader ExecuteDataReader(string sqlQuery, params SqlParameter[] parameters) {
            SqlConnection connection = ConnectDatabase();
            connection.Open();
            SqlCommand cmd = InitCommand(sqlQuery, parameters, connection);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                connection.Close();
            }
        }

        public static int ExecuteNonQuery(string sqlQuery, params SqlParameter[] parameters) {
            SqlConnection connection = ConnectDatabase();
            connection.Open();
            SqlCommand cmd = InitCommand(sqlQuery, parameters, connection);
            try
            {
                int res = cmd.ExecuteNonQuery();
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally {
                connection.Close();
            }
        }

        public static object ExecuteScalar(string sqlQuery, params SqlParameter[] parameters) {
            SqlConnection connection = ConnectDatabase();
            connection.Open();
            SqlCommand cmd = InitCommand(sqlQuery, parameters, connection);
            try
            {
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                connection.Close();
            }
        }

        private static SqlCommand InitCommand(string sqlQuery, SqlParameter[] parameters, SqlConnection connection)
        {
            SqlCommand cmd = new SqlCommand(sqlQuery, connection);
            cmd.Parameters.AddRange(parameters);
            return cmd;
        }

        private static SqlConnection ConnectDatabase()
        {
            SqlConnection connection = new SqlConnection(SqlHelperController.DatabaseSetting);
            return connection;
        }
    }
}
