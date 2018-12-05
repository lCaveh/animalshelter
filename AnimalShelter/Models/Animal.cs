using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace AnimalShelter.Models
{
    public class Animal
    {
        private string _name;
        private string _sex;
        private DateTime _dateAdmitted = new DateTime();
        private string _breed;
        private int _id;

        public Animal(string name, string sex, DateTime dateAdmitted, string breed, int id = 0)
        {
            _name = name;
            _sex = sex;
            _dateAdmitted = dateAdmitted;
            _breed = breed;
            _id = id;
        }
        public string GetName()
        {
            return _name;
        }
        public string GetSex()
        {
            return _sex;
        }
        public DateTime GetDateAdmitted()
        {
            return _dateAdmitted;
        }
        public string GetBreed()
        {
            return _breed;
        }
        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO animals (name, sex, dateAdmitted, breed) VALUES (@petName, @petSex, @petDateAdmitted, @petBreed);";
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@petName";
            name.Value = this._name;
            MySqlParameter sex = new MySqlParameter();
            sex.ParameterName = "@petSex";
            sex.Value = this._sex;
            MySqlParameter dateAdmitted = new MySqlParameter();
            dateAdmitted.ParameterName = "@petDateAdmitted";
            dateAdmitted.Value = this._dateAdmitted;
            MySqlParameter breed = new MySqlParameter();
            breed.ParameterName = "@petBreed";
            breed.Value = this._breed;
            cmd.Parameters.Add(name);
            cmd.Parameters.Add(sex);
            cmd.Parameters.Add(dateAdmitted);
            cmd.Parameters.Add(breed);
            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static Animal Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `animals` WHERE id = @thisId;";
            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            string name = "";
            string sex = "";
            DateTime tableDate = new DateTime();
            string breed = "";
            int tableId = 0;

            while (rdr.Read())
            {
                name = rdr.GetString(0);
                sex = rdr.GetString(1);
                tableDate = rdr.GetDateTime(2);
                breed = rdr.GetString(3);
                tableId = rdr.GetInt16(4);
            }
            Animal foundanimal = new Animal(name, sex, tableDate, breed, tableId);  // This line is new!
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundanimal;
        }
        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM animals;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public int GetId()
        {
            return _id;
        }
    }
}
