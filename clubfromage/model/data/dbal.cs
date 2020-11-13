using System;
using MySql;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Collections;
using model.business;
using model.data;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace model.data
{
    class dbal
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public dbal()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "localhost";
            database = "PF";
            uid = "root";
            password = "5MichelAnnecy";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        //Insert statement
        public void Insert(string query)
        {
            query = "INSERT INTO " + query;
            
            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Update statement
        public void Update(string query)
        {
            query = "UPDATE " + query;

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }


       
        //Execquery statement
        public void Execquery(string query)
        {

            OpenConnection();
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Execute commande
            cmd.ExecuteNonQuery();
            CloseConnection();
        }

        
        //Delete statement
        public void Delete(string query)
        {
            query = "DELETE " + query;

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }


        public DataSet RQuery (string query)
        {
            DataSet dataset = new DataSet();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = new MySqlCommand(query, connection);
            adapter.Fill(dataset);
            return dataset;
        }
        public DataTable SelectAll (string table)
        {
            return this.RQuery("SELECT * FROM " + table).Tables[0];
        }
        public  DataTable SelectByField(string table, string fielTestCondition)
        {
            return this.RQuery("SELECT * FROM " + table + " where " + fielTestCondition).Tables[0];
        }
        public DataRow SelectById (string table, int id)
        {
            return this.RQuery("SELECT * FROM " + table + " where id = " + id).Tables[0].Rows[0];
        }
    }
}
