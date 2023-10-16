using AutomaticEntrySystem.Library;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntrySystemTest
{
    public class DatabaseTest
    {
        public virtual int ExecuteNonQueryWithParameters(string sql, SqlParameter[] sqlParameters)
        {
            return Database.ExecuteNonQueryWithParameters(sql, sqlParameters);
        }
    }
}
