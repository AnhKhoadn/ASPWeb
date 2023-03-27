using _19T1021105.DataLayers;
using _19T1021105.DataLayers.SQLServer;
using _19T1021105.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021105.DataLayers.SQLServer
{
    public class ProductDAL : _BaseDAL, IProductDAL
    {
        /// <summary>
        /// Cài đặt chức năng xử lý dữ liệu liên quan đến mặt hàng
        /// </summary>
        public ProductDAL(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// Thêm mới một mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Product data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Products(ProductName, SupplierID, CategoryID ,Unit ,Price ,Photo) 
                                    VALUES (@ProductName, @SupplierID , @CategoryID , @Unit , @Price , @Photo) ; 
                                    SELECT SCOPE_IDENTITY()";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                cmd.Parameters.AddWithValue("@SupplierID", data.SuplierID);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@Unit", data.Unit);
                cmd.Parameters.AddWithValue("@Price", data.Price);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);

                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }

            return result;
        }

        /// <summary>
        /// Thêm 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>

        public long AddAttribute(ProductAttribute data)
        {
            long result = 0;

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO ProductAttributes( AttributeName, AttributeValue, DisplayOrder, ProductID) 
                                    VALUES (@AttributeName, @AttributeValue, @DisplayOrder, @ProductID);
                                    SELECT SCOPE_IDENTITY();";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@AttributeValue", data.AttributeValue);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);

                result = Convert.ToInt64(cmd.ExecuteScalar());

                cn.Close();

            }

            return result;
        }

        public long AddPhoto(ProductPhoto data)
        {
            long result = 0;

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO ProductPhotos(ProductID, Photo , Description, DisplayOrder, IsHidden) 
                                    VALUES(@ProductID, @Photo , @Description, @DisplayOrder, @IsHidden);
                                    SELECT SCOPE_IDENTITY();";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@IsHidden", data.IsHidden);

                result = Convert.ToInt64(cmd.ExecuteScalar());

                cn.Close();

            }

            return result;
        }

        /// <summary>
        /// Đếm số lượng mặt hàng
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="categoryID"></param>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public int Count(string searchValue = "", int categoryID = 0, int supplierID = 0)
        {
            int count = 0;

            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT  COUNT(*)
                                    FROM    Products as p
                                    WHERE   ((p.ProductName like @SearchValue) or (@SearchValue = N''))
					                                    and 
					                                    ((p.CategoryID = @CategoryID) or (@CategoryID = 0))
					                                    and
					                                    ((p.SupplierID = @SupplierID) or (@SupplierID = 0))";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@SearchValue", searchValue);
                cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                cmd.Parameters.AddWithValue("@SupplierID", supplierID);

                count = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }

            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public bool Delete(int productID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM ProductAttributes WHERE ProductID = @ProductID 
                                    DELETE FROM ProductPhotos WHERE ProductID = @ProductID
                                    DELETE FROM Products WHERE ProductID = @ProductID
                                    AND NOT EXISTS(SELECT * FROM OrderDetails Where ProductID = @ProductID)";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ProductID", productID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        public bool DeleteAttribute(long attributeID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE from ProductAttributes where AttributeID = @AttributeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@AttributeID", attributeID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public bool DeletePhoto(long photoID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM ProductPhotos WHERE PhotoID = @PhotoID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@PhotoID", photoID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }

            return result;
        }

        /// <summary>
        /// Lấy ra thông tin của một mặt hàng
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public Product Get(int productID)
        {
            Product data = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Products WHERE ProductID = @ProductID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@ProductID", productID);
                var dbRender = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbRender.Read())
                {
                    data = new Product()
                    {
                        ProductID = Convert.ToInt32(dbRender["ProductID"]),
                        ProductName = Convert.ToString(dbRender["ProductName"]),
                        SuplierID = Convert.ToInt32(dbRender["SupplierID"]),
                        CategoryID = Convert.ToInt32(dbRender["CategoryID"]),
                        Unit = Convert.ToString(dbRender["Unit"]),
                        Price = Convert.ToDecimal(dbRender["Price"]),
                        Photo = Convert.ToString(dbRender["Photo"])
                    };
                }
                cn.Close();

            }

            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        public ProductAttribute GetAttribute(long attributeID)
        {
            ProductAttribute data = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM ProductAttributes WHERE AttributeID = @AttributeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@AttributeID", attributeID);
                var dbRender = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbRender.Read())
                {
                    data = new ProductAttribute()
                    {
                        AttributeID = Convert.ToInt32(dbRender["AttributeID"]),
                        ProductID = Convert.ToInt32(dbRender["ProductID"]),
                        AttributeName = Convert.ToString(dbRender["AttributeName"]),
                        AttributeValue = Convert.ToString(dbRender["AttributeValue"]),
                        DisplayOrder = Convert.ToInt32(dbRender["DisplayOrder"])
                    };
                }
                cn.Close();

            }
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public ProductPhoto GetPhoto(long photoID)
        {
            ProductPhoto data = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM ProductPhotos WHERE PhotoID= @PhotoID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@PhotoID", photoID);
                var dbRender = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbRender.Read())
                {
                    data = new ProductPhoto()
                    {
                        PhotoID = Convert.ToInt32(dbRender["PhotoID"]),
                        ProductID = Convert.ToInt32(dbRender["ProductID"]),
                        Photo = Convert.ToString(dbRender["Photo"]),
                        Description = Convert.ToString(dbRender["Description"]),
                        DisplayOrder = Convert.ToInt32(dbRender["DisplayOrder"]),
                        IsHidden = Convert.ToBoolean(dbRender["IsHidden"])
                    };
                }
                cn.Close();

            }

            return data;
        }

        /// <summary>
        /// Kiểm tra xem mặt hàng còn tồn tại hay không ?
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public bool InUsed(int productID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT CASE WHEN EXISTS(SELECT * FROM OrderDetails WHERE ProductID = @ProductID) 
                                    OR EXISTS(SELECT * FROM ProductAttributes WHERE ProductID = @ProductID) 
                                    OR EXISTS(SELECT * FROM ProductPhotos WHERE ProductID = @ProductID) 
                                    THEN 1 ELSE 0 END";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@ProductID", productID);

                result = Convert.ToBoolean(cmd.ExecuteScalar());

                cn.Close();
            }

            return result;
        }

        /// <summary>
        /// Lấy ra danh sách mặt hàng
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="categoryID"></param>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public IList<Product> List(int page = 1, int pageSize = 0, string searchValue = "", int categoryID = 0, int supplierID = 0)
        {
            List<Product> data = new List<Product>();

            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT *
                                    FROM 
                                    (
	                                    SELECT	*, ROW_NUMBER() OVER (ORDER BY ProductName) AS RowNumber
	                                    FROM	Products AS p
	                                    WHERE	((p.ProductName like @SearchValue) or (@SearchValue = N''))
					                                    and 
					                                    ((p.CategoryID = @CategoryID) or (@CategoryID = 0))
					                                    and
					                                    ((p.SupplierID = @SupplierID) or (@SupplierID = 0))
                                    ) AS t JOIN Categories AS c ON c.CategoryID = t.CategoryID
									JOIN Suppliers AS s  ON s.SupplierID = t.SupplierID
                                    WHERE (@PageSize = 0) OR (t.RowNumber BETWEEN (@Page - 1) * @PageSize + 1 AND @Page * @PageSize)";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@Page", page);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);
                cmd.Parameters.AddWithValue("@SearchValue", searchValue);
                cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                cmd.Parameters.AddWithValue("@SupplierID", supplierID);

                var dbRender = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dbRender.Read())
                {
                    data.Add(new Product()
                    {
                        ProductID = Convert.ToInt32(dbRender["ProductID"]),
                        ProductName = Convert.ToString(dbRender["ProductName"]),
                        SuplierID = Convert.ToInt32(dbRender["SupplierID"]),
                        CategoryID = Convert.ToInt32(dbRender["CategoryID"]),
                        Unit = Convert.ToString(dbRender["Unit"]),
                        Price = Convert.ToDecimal(dbRender["Price"]),
                        Photo = Convert.ToString(dbRender["Photo"])
                    });
                }
                dbRender.Close();
                cn.Close();

            }

            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public IList<ProductAttribute> ListAttributes(int productID)
        {
            List<ProductAttribute> data = new List<ProductAttribute>();
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT    *, ROW_NUMBER() OVER (ORDER BY DisplayOrder) AS RowNumber
                                        FROM    ProductAttributes
                                        WHERE   ProductID = @ProductID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@ProductID", productID);

                var dbRender = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dbRender.Read())
                {
                    data.Add(new ProductAttribute()
                    {
                        AttributeID = Convert.ToInt32(dbRender["AttributeID"]),
                        ProductID = Convert.ToInt32(dbRender["ProductID"]),
                        AttributeName = Convert.ToString(dbRender["AttributeName"]),
                        AttributeValue = Convert.ToString(dbRender["AttributeValue"]),
                        DisplayOrder = Convert.ToInt32(dbRender["DisplayOrder"])
                    });
                }
                dbRender.Close();
                cn.Close();
            }

            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public IList<ProductPhoto> ListPhotos(int productID)
        {
            List<ProductPhoto> data = new List<ProductPhoto>();
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT    *, ROW_NUMBER() OVER (ORDER BY DisplayOrder) AS RowNumber
                                        FROM    ProductPhotos
                                        WHERE   ProductID = @ProductID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@ProductID", productID);

                var dbRender = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dbRender.Read())
                {
                    data.Add(new ProductPhoto()
                    {
                        PhotoID = Convert.ToInt32(dbRender["PhotoID"]),
                        ProductID = Convert.ToInt32(dbRender["ProductID"]),
                        Photo = Convert.ToString(dbRender["Photo"]),
                        Description = Convert.ToString(dbRender["Description"]),
                        DisplayOrder = Convert.ToInt32(dbRender["DisplayOrder"]),
                        IsHidden = Convert.ToBoolean(dbRender["IsHidden"])
                    });
                }
                dbRender.Close();
                cn.Close();
            }

            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Product data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Products
                                    SET ProductName = @ProductName, Unit = @Unit, Price = @Price, Photo = @photo, SupplierID = @SupplierID, CategoryID = @CategoryID
                                    WHERE ProductID = @ProductID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                cmd.Parameters.AddWithValue("@SupplierID", data.SuplierID);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@Unit", data.Unit);
                cmd.Parameters.AddWithValue("@Price", data.Price);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateAttribute(ProductAttribute data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE ProductAttributes
                                    SET AttributeName = @AttributeName, AttributeValue = @AttributeValue, DisplayOrder = @DisplayOrder
                                    WHERE AttributeID = @AttributeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@AttributeID", data.AttributeID);
                cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@AttributeValue", data.AttributeValue);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdatePhoto(ProductPhoto data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE ProductPhotos
                                    SET IsHidden = @IsHidden, DisplayOrder = @DisplayOrder, Description = @Description, Photo = @photo
                                    WHERE PhotoID = @PhotoID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@PhotoID", data.PhotoID);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@IsHidden", data.IsHidden);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }

            return result;
        }
    }
}

