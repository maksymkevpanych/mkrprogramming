using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Max\\Desktop\\ConsoleApp1\\ConsoleApp1\\Database1.mdf;Integrated Security=True;Connect Timeout=30"; // Потрібно замінити на власний рядок підключення до бази даних

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                
                string createTableQuery = @"CREATE TABLE Kadry (
                                        Код INT PRIMARY KEY,
                                        Прізвище VARCHAR(50),
                                        Цех VARCHAR(50),
                                        Посада VARCHAR(50),
                                        Стать VARCHAR(10),
                                        Стаж INT,
                                        Заробітна_плата DECIMAL(10, 2)
                                    )";

                using (SqlCommand command = new SqlCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                
                string insertDataQuery = @"INSERT INTO Kadry VALUES 
                                       (1, 'Іванов', 'Цех1', 'менеджер', 'Чоловік', 5, 50000.00),
                                       (2, 'Іванова', 'Цех1', 'прибиральниця', 'Жінка', 6, 6000.00),
                                        (5, 'Петрова', 'Цех1', 'прибиральниця', 'Жінка', 7, 7000.00),
                                       (3, 'Петров', 'Цех2', 'бухгалтер', 'Чоловік', 3, 4500.00),
                                       (4, 'Пупкіна', 'Цех2', 'секретар', 'Жінка', 6, 5500.00)";

                using (SqlCommand command = new SqlCommand(insertDataQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                
                string selectAllQuery = "SELECT * FROM Kadry";

                using (SqlCommand command = new SqlCommand(selectAllQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Код: {reader["Код"]}, Прізвище: {reader["Прізвище"]}, Цех: {reader["Цех"]}, Посада: {reader["Посада"]}, Стать: {reader["Стать"]}, Стаж: {reader["Стаж"]}, Заробітна плата: {reader["Заробітна_плата"]}");
                        }
                    }
                }

                
                int employeeCode = 2;
                string selectEmployeeQuery = $"SELECT Прізвище, Посада, Стаж, Заробітна_плата FROM Kadry WHERE Код = {employeeCode}";

                using (SqlCommand command = new SqlCommand(selectEmployeeQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Прізвище: {reader["Прізвище"]}, Посада: {reader["Посада"]}, Стаж: {reader["Стаж"]}, Заробітна плата: {reader["Заробітна_плата"]}");
                        }
                    }
                }

                string averageSalaryQuery = "SELECT AVG(Заробітна_плата) AS Середня_зарплата FROM Kadry WHERE Стать = 'Жінка' AND Цех = 'Цех1'";

                using (SqlCommand command = new SqlCommand(averageSalaryQuery, connection))
                {
                    object result = command.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        Console.WriteLine($"Середня зарплата працівників-жінок цеху Х: {result}");
                    }
                    else
                    {
                        Console.WriteLine("Немає даних для обчислення середньої зарплати.");
                    }
                }
            }
        }
    }
}
