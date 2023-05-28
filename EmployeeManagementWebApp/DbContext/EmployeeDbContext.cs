using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using EmployeeManagementWebApp.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EmployeeManagementWebApp.DbAccess
{
    public class EmployeeDbContext
    {
        string conString = ConfigurationManager.ConnectionStrings["EmployeeDBConnectionString"].ToString();

        // Get all Employees
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection conn = new SqlConnection(conString))
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spGetAllEmployees";
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();

                conn.Open();
                adapter.Fill(dt);
                conn.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    employees.Add(new Employee
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        EmpNo = Convert.ToString(dr["EmpNo"]),
                        FirstName = Convert.ToString(dr["FirstName"]),
                        LastName = Convert.ToString(dr["LastName"]),
                        Birthdate = Convert.ToDateTime(dr["Birthdate"]),
                        ContactNo = Convert.ToInt64(dr["ContactNo"]),
                        EmailAddress = Convert.ToString(dr["EmailAddress"])
                    });
                }
            }

            return employees;
        }

        //Insert Employee
        // Insert Employee
        public int InsertEmployee(Employee employee)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("spInsertEmployee", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@EmpNo", employee.EmpNo);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@Birthdate", employee.Birthdate);
                command.Parameters.AddWithValue("@ContactNo", employee.ContactNo);
                command.Parameters.AddWithValue("@EmailAddress", employee.EmailAddress);

                SqlParameter resultParam = new SqlParameter("@Result", SqlDbType.Int);
                resultParam.Direction = ParameterDirection.Output;
                command.Parameters.Add(resultParam);

                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    result = Convert.ToInt32(command.Parameters["@Result"].Value);
                }
                catch (SqlException ex)
                {
                    // Handle specific SQL exceptions
                    if (ex.Number == 50000)
                    {
                        string errorMessage = ex.Message;

                        if (errorMessage.Contains("EmpNo"))
                        {
                            result = -1;
                        }
                        else if (errorMessage.Contains("combination of FirstName and LastName"))
                        {
                            result = -2;
                        }
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return result;
        }


        // Get Employee by Id
        public Employee GetEmployeeById(int id)
        {
            Employee employee = new Employee();

            using (SqlConnection conn = new SqlConnection(conString))
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spGetEmployeeById";
                command.Parameters.AddWithValue("@Id", id);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();

                conn.Open();
                adapter.Fill(dt);
                conn.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    employee.Id = Convert.ToInt32(dr["Id"]);
                    employee.EmpNo = Convert.ToString(dr["EmpNo"]);
                    employee.FirstName = Convert.ToString(dr["FirstName"]);
                    employee.LastName = Convert.ToString(dr["LastName"]);
                    employee.Birthdate = Convert.ToDateTime(dr["Birthdate"]);
                    employee.ContactNo = Convert.ToInt64(dr["ContactNo"]);
                    employee.EmailAddress = Convert.ToString(dr["EmailAddress"]);
                }
            }

            return employee;
        } 

        // Update Employee
        public bool UpdateEmployee(Employee employee)
        {
            int i = 0;
            using (SqlConnection conn = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("spUpdateEmployee", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", employee.Id);
                command.Parameters.AddWithValue("@ContactNo", employee.ContactNo);
                command.Parameters.AddWithValue("@EmailAddress", employee.EmailAddress);

                conn.Open();
                i = command.ExecuteNonQuery();
                conn.Close();
            }
            
            if(i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Delete Employee
        public string DeleteEmployee(int id)
        {
            string result = "";
            using (SqlConnection conn = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("spDeleteEmployeeById", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.Add("@ReturnMessage", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                conn.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@ReturnMessage"].Value.ToString(); 
                conn.Close();
            }
            return result;
        }

    }
}