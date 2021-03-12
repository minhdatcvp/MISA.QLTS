using Dapper;
using MISA.QLTS.BACKEND.DATALAYER.Interface;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MISA.QLTS.BACKEND.DATALAYER.DBConnection
{
    /// <summary>
    /// Tầng kết nối với Database
    /// Khởi tạo 2 method query và excute sử dụng dapper để truy vấn database
    /// </summary>
    /// <typeparam name="T">Entity : Asset or Decrement</typeparam>
    public class DataConnection<T> : IDataConnection<T>
    {
        #region DECLARE
        //Khai báo chuỗi kết nối tới db
        string _connectionString = "User Id=dev;Host=47.241.69.179;Character Set=utf8;Port=3306;Password=12345678;Database=MF700_NMDAT_QLTS";

        // Khai báo kết nối
        private readonly IDbConnection _dbConnection;
        #endregion

        #region CONTRUCTOR
        //Khởi tạo kết nối
        public DataConnection()
        {
            _dbConnection = new MySqlConnection(_connectionString);
        }
        #endregion
        #region METHOD

        /// <summary>
        /// Thực thi câu lệnh Query
        /// </summary>
        /// <typeparam name="T">Đối tượng cần thực hiện (Entity : Asset or Decrement) </typeparam>
        /// <param name="sqlCommand">Câu lệnh sql hoặc tên procedure</param>
        /// <param name="parameters">Tham số truyền vào (mặc định ban đầu là null)</param>
        /// <param name="commandType">Loại command (mặc định ban đầu là text)</param>
        /// <returns>Danh sách dữ liệu trả về</returns>
        public IEnumerable<T> Query(string sqlCommand, DynamicParameters parameters = null, CommandType commandType = CommandType.Text)
        {
            // Thực thi truy vấn 
            var result = _dbConnection.Query<T>(sqlCommand, parameters, commandType: commandType);

            // Trả về 
            return result;
        }
        /// <summary>
        /// Thực thi các câu lệnh Excute dùng để thêm, sửa, xóa
        /// </summary>
        /// <param name="sqlCommand">Câu lệnh sql hoặc tên procedure (Entity : Asset or Decrement) </param>
        /// <param name="parameters">Tham số truyền vào (mặc định ban đầu là null)</param>
        /// <param name="commandType">Loại command (mặc định ban đầu là text)</param>
        /// <returns>Số dòng thực hiện được</returns>
        public int Excute(string sqlCommand, DynamicParameters parameters = null, CommandType commandType = CommandType.Text)
        {
            // Thực thi truy vấn bằng proceduce
            var result = _dbConnection.Execute(sqlCommand, parameters, commandType: commandType);
            // Kết quả trả về
            return result;
        }

        /// <summary>
        /// Lấy ra giá trị đầu tiên trong danh sách trả về
        /// </summary>
        /// <param name="sqlCommand">Câu lệnh sql hoặc tên procedure</param>
        /// <param name="parameters">Tham số truyền vào (mặc định là null)</param>
        /// <param name="commandType">Loại command (mặc định là text)</param>
        /// <returns>Đối tượng đầu tiên trong danh sách</returns>
        public T QueryFirst(string sqlCommand, DynamicParameters parameters = null, CommandType commandType = CommandType.Text)
        {
            // Thực thi truy vấn bằng proceduce
            var entity = _dbConnection.Query<T>(sqlCommand, parameters, commandType: commandType).FirstOrDefault();

            // Trả về 
            return entity;
        }
        #endregion

    }
}
