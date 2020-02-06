using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Indigo.Models
{
    public class Persons
    {
        public List<People> Person { get; set; }
        public class People
        {
            public string FName { get; set; }
            public string MName { get; set; }
            public string LName { get; set; }
            public string Email { get; set; }
            public int TypeId { get; set; }
            public int RankId { get; set; }
        }

        public List<PersonTypes> PersonType { get; set; }
        public class PersonTypes
        {
            public int id { get; set; }
            public string typeName { get; set; }
        }

        public Persons()
        {
            Person = new List<People>();
            PersonType = new List<PersonTypes>();
            var p = GetInfo();
            Person.AddRange(p);
            var pt = GetInfoTypes();
            PersonType.AddRange(pt);
        }

        public static List<People> GetInfo()
        {
            var info = new List<People>();
            //info.Add(new People { FName = "John", MName= "", LName = "Doe", Email = "aemail@gmail.com", TypeId = 1});
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Indigo"].ConnectionString))
            using (var cmd = con.CreateCommand())
            {
                try
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GetPeople";
                    cmd.ExecuteNonQuery();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var dataRow = new People
                            {
                                FName = reader.GetString(reader.GetOrdinal("FName")),
                                MName = reader.GetString(reader.GetOrdinal("MName")),
                                LName = reader.GetString(reader.GetOrdinal("LName")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                TypeId = reader.GetInt16(reader.GetOrdinal("TypeId")),
                                RankId = reader.GetInt32(reader.GetOrdinal("RankId"))
                            };
                            info.Add(dataRow);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.Write("Sql Error");
                }
            }
            return info;
        }

        public static List<PersonTypes> GetInfoTypes()
        {
            var pt = new List<PersonTypes>();
            //pt.Add(new PersonTypes { id = '1', typeName = "Employee" });
            //pt.Add(new PersonTypes { id = '1', typeName = "Customer" });
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Indigo"].ConnectionString))
            using (var cmd = con.CreateCommand())
            {
                try
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GetPersonTypes";
                    cmd.ExecuteNonQuery();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var dataRow = new PersonTypes
                            {
                                id = reader.GetInt16(reader.GetOrdinal("TypeId")),
                                typeName = reader.GetString(reader.GetOrdinal("Type")),
                            };
                            pt.Add(dataRow);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.Write("Sql Error");
                }
            }
            return pt;
        }

        public static bool InsertPerson(string f, string m, string l, string email, int type)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Indigo"].ConnectionString))
            using (var cmd = con.CreateCommand())
            {
                try
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "AddPerson";
                    cmd.Parameters.AddWithValue("@FName", f);
                    cmd.Parameters.AddWithValue("@MName", m);
                    cmd.Parameters.AddWithValue("@LName", l);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.Write("Sql Error");
                    return false;
                }
            }
            return true;
        }

        public static bool DeletePerson(int rank)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Indigo"].ConnectionString))
            using (var cmd = con.CreateCommand())
            {
                try
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "DeletePerson";
                    cmd.Parameters.AddWithValue("@rank", rank);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.Write("Sql Error");
                    return false;
                }
            }
            return true;
        }
    }
}