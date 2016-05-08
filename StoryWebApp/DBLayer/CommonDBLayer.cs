using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StoryWebApp.DBLayer
{
    public class CommonDbLayer
    {
        private readonly string _asyncConnectionString;

        /// <summary>
        /// Connection strings used to create this object should be retrieved 
        /// from web.config in the live project or app.config in the unit test
        /// project and *NOT* passed in manually.
        /// </summary>
        /// 
        /// <param name="connectionString">A fully valid connection 
        /// string</param>
        public CommonDbLayer(string connectionString)
        {
            _asyncConnectionString = new SqlConnectionStringBuilder(connectionString)
            {
                AsynchronousProcessing = true
            }.ToString();
        }

        ///  <summary>
        ///  This is a simple database query function which will return one 
        ///  value per row into a single list.
        ///  
        ///  If mapper returns null for a row then that row is excluded.
        ///  </summary>
        /// 
        ///  <typeparam name="T">The type of the return parameter</typeparam>
        /// <param name="procedureName">The fully qualified (i.e. schema
        ///     included) stored procedure to call</param>
        /// <param name="queryParameters">A list of parameters to pass to this
        ///     stored procedure.</param>
        /// <param name="mapper">A function which takes a single row from
        ///     SqlDataReader and maps it to a single instance of T.
        /// 
        ///     It can safely assume that the data reader is on a valid
        ///     row</param>
        /// <returns>An enumerable containing 0 or more instances of T.
        ///  The number of items in this is equal to the number of rows in the
        ///  result set.</returns>
        internal async Task<List<T>> AsyncDbQuerySimple<T>(string procedureName, IList<SqlParameter> queryParameters, Func<IDataReader, T> mapper)
        {
            return await AsyncDbQuery(
                procedureName,
                queryParameters,
                reader =>
                {
                    var results = new List<T>();

                    while (reader.Read())
                    {
                        var m = mapper(reader);
                        if (m != null)
                        {
                            results.Add(m);
                        }
                    }

                    return results;
                });
        }

        ///  <summary>
        ///  This is a slightly more generic version of AsyncDBQuerySimple which
        ///  is used when we want to do more than return a list of something 
        ///  that is constructed on a once per row basis.
        ///  
        ///  For example, this could be used to create a HashSet of the elements
        ///  of the query.
        ///  
        ///  The only valid parameters for this query are those which can be 
        ///  automatically inferred. If you need to use a table valued 
        ///  parameter then you should use the overloaded version of this which
        ///  takes an enumerable of SqlParameter objects.
        ///  </summary>
        ///  
        ///  <typeparam name="T">The type of the return parameter</typeparam>
        /// <param name="procedureName">The fully qualified (i.e. schema
        ///     included) stored procedure to call</param>
        /// <param name="queryParameters">A dictionary containing any parameters
        ///     to put onto the procedure call. The keys should be prefixed with
        ///     @.</param>
        /// <param name="mapper">A function which takes the entire 
        ///     SqlDataReader and returns an object of type T. 
        ///  
        ///     The reader doesn't need closing but it does need each row to be 
        ///     read separately.</param>
        /// <returns>An enumerable containing 0 or more instances of T.
        ///  The number of items in this is equal to the number of rows in the
        ///  result set.</returns>
        internal async Task<T> AsyncDbQuery<T>(string procedureName, IList<SqlParameter> queryParameters, Func<IDataReader, T> mapper)
        {
            using (var conn = new SqlConnection(_asyncConnectionString))
            using (var comm = new SqlCommand(procedureName, conn))
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddRange(queryParameters.ToArray());

                await conn.OpenAsync();

                using (var reader = await comm.ExecuteReaderAsync())
                {
                    return mapper(reader);
                }
            }
        }

        /// <summary>
        /// Generic query used to run a stored procedure with a set of 
        /// parameters which has a single return code as an integer.
        /// 
        /// This version of the function allows for more complex parameters 
        /// where the data type can't be inferred (e.g. table valued 
        /// parameters).
        /// </summary>
        /// <param name="procedureName">The full procedure name including the 
        ///     schema</param>
        /// <param name="queryParameters">A dictionary containing any 
        ///     parameters to pass to the procedure.</param>
        /// <param name="timeOutSeconds">Change default time out (30) in seconds</param>
        /// <returns>A task that resolves to the return code of the stored 
        /// procedure.</returns>
        internal async Task<int> AsyncDbQueryNoResultSet(string procedureName, IList<SqlParameter> queryParameters, int timeOutSeconds = 30)
        {
            using (var conn = new SqlConnection(_asyncConnectionString))
            using (var comm = new SqlCommand(procedureName, conn))
            {
                comm.CommandTimeout = timeOutSeconds;
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddRange(queryParameters.ToArray());
                comm.Parameters.Add("@rc", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                await conn.OpenAsync();

                await comm.ExecuteNonQueryAsync();

                return (int) comm.Parameters["@rc"].Value;
            }
        }
    }
}