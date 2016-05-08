using System.Data;

namespace StoryWebApp.DBLayer
{
    public static class DataReaderExtensionMethods
    {
        /// <summary>
        /// A utility method to retrieve a value from a data reader giving a 
        /// default value if the column was DBNull.
        /// </summary>
        /// <typeparam name="T">The type of the column</typeparam>
        /// <param name="reader">An open reader which is on a valid row.</param>
        /// <param name="fieldName">The field name as seen by the reader.</param>
        /// <param name="defaultValue">An optional default value for the column 
        /// to take if it is DbNull.</param>
        /// 
        /// <returns>An object of type T which contains the value of the 
        /// column as understood by fieldName</returns>
        public static T GetValueOrDefault<T>(this IDataReader reader, string fieldName, T defaultValue = default(T))
        {
            var ordinal = reader.GetOrdinal(fieldName);
            return GetValueOrDefault(reader, ordinal, defaultValue);
        }

        /// <summary>
        /// A utility method to retrieve a value from a data reader giving a 
        /// default value if the column was DBNull.
        /// </summary>
        /// <typeparam name="T">The type of the column</typeparam>
        /// <param name="reader">An open reader which is on a valid row.</param>
        /// <param name="fieldIndex">The index of the column.</param>
        /// <param name="defaultValue">An optional default value for the column 
        /// to take if it is DbNull.</param>
        /// 
        /// <returns>An object of type T which contains the value of the 
        /// column at index fieldIndex</returns>
        private static T GetValueOrDefault<T>(this IDataRecord reader, int fieldIndex, T defaultValue = default(T))
        {
            return (T) (reader.IsDBNull(fieldIndex) ? defaultValue : reader.GetValue(fieldIndex));
        }
    }
}
