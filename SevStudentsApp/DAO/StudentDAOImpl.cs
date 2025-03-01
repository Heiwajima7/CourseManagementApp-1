﻿using CourseManagementApp.DAO.DBUtil;
using CourseManagementApp.Models;
using System.Data.SqlClient;

namespace CourseManagementApp.DAO
{
    public class StudentDAOImpl : IStudentDAO
    {
        public Student? Delete(Student? student)
        {
            if (student == null) return null;

            try
            {
                using SqlConnection? conn = DBHelper.GetConnection();

                if (conn is not null) conn.Open();

                string sql = "DELETE FROM STUDENTS WHERE ID = @id";

                using SqlCommand command = new(sql, conn);

                command.Parameters.AddWithValue("@id", student.Id);

                int rowsAffected = command.ExecuteNonQuery();
                return (rowsAffected > 0) ? student : null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        public List<Student> GetAll()
        {
            List<Student> students = new List<Student>();

            try
            {
                using SqlConnection? conn = DBHelper.GetConnection();

                if (conn is not null) conn.Open();

                string sql = "SELECT * FROM STUDENTS";

                using SqlCommand command = new(sql, conn);
                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Student student = new()
                    {
                        Id = reader.GetInt32(0),
                        Firstname = reader.GetString(1),
                        Lastname = reader.GetString(2)
                    };

                    students.Add(student);
                }
                return students;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        public Student? GetStudent(int id)
        {
            Student? student = null;
            try
            {
                using SqlConnection? conn = DBHelper.GetConnection();

                if (conn is not null) conn.Open();

                string sql = "SELECT * FROM STUDENTS WHERE ID = @id";

                using SqlCommand command = new(sql, conn);

                command.Parameters.AddWithValue("@id", id);

                using SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    student = new Student()
                    {
                        Id = reader.GetInt32(0),
                        Firstname = reader.GetString(1),
                        Lastname = reader.GetString(2)
                    };
                }
                return student;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        public void Insert(Student? student)
        {
            if (student == null) return;

            try
            {
                using SqlConnection? conn = DBHelper.GetConnection();

                if (conn is not null) conn.Open();

                string sql = "INSERT INTO STUDENTS " +
                    "(FIRSTNAME, LASTNAME) VALUES" +
                    "(@firstname, @lastname)";

                using SqlCommand command = new SqlCommand(sql, conn);

                command.Parameters.AddWithValue("@firstname", student.Firstname);
                command.Parameters.AddWithValue("@lastname", student.Lastname);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }

        public void Update(Student? student)
        {
            if (student == null) return;

            try
            {
                using SqlConnection? conn = DBHelper.GetConnection();

                if (conn is not null) conn.Open();

                string sql = "UPDATE STUDENTS SET FIRSTNAME = @firstname, " +
                    "LASTNAME = @lastname WHERE ID = @id";

                using SqlCommand command = new SqlCommand(sql, conn);

                command.Parameters.AddWithValue("@firstname", student.Firstname);
                command.Parameters.AddWithValue("@lastname", student.Lastname);
                command.Parameters.AddWithValue("@id", student.Id);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw;
            }
        }
    }
}
