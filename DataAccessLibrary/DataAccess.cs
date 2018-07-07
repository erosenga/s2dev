using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace DataAccessLibrary
{
    public class Recipe
    {
        public long   Id { get; set; }
        public string NickName { get; set; }
        public string Species { get; set; }
        public string Organ { get; set; }
        public long   GrindTime { get; set; }
        public long   GrindTemp { get; set; }
        public long   RPM { get; set; }
        public long   IncubationTime { get; set; }
        public long   IncubationTemp { get; set; }
        public long   Cycles { get; set; }
        public long   Owner { get; set; }
        public long   Lock { get; set; }
        public string Comment { get; set; }
        public string Tag { get; set; }
        public long   CurrentUser { get; set; }
        public long   Enzyme { get; set; }
        public long   State { get; set; }
        public long   Type { get; set; }
    }
    public class User
    {
        public long   Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string email { get; set; }
        public string Telephone { get; set; }
        public string Password { get; set; }
        public string Comment { get; set; }
        public long   Privilege { get; set; }
        public string Tag { get; set; }
    }
    public class Experiment
    {
        public long   Id { get; set; }
        public long   StartTime { get; set; }
        public long   EndTime { get; set; }
        public long   RecipeId { get; set; }
        public long   UserId { get; set; }
        public long   Barcode { get; set; }
        public string Comment { get; set; }
        public string Tag { get; set; }
    }
    public static class DataAccess
    {
        public static List<Recipe> GetRecipeList()
        {

            List<Recipe> myRecipe = new List<Recipe>();

            using (SqliteConnection db =
                new SqliteConnection("Filename=s2Genomics.db"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand("SELECT * FROM Recipe", db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    myRecipe.Add(new Recipe
                    {//TODO handle null results from query
                        Id = (long)query["Id"],
                        NickName = (string)query["NickName"],
                        Species = (string)query["Species"],
                        Organ = (string)query["Organ"],
                        GrindTime = (long)query["GrindTime"],
                        GrindTemp = (long)query["GrindTemp"],
                        RPM = (long)query["RPM"],
                        IncubationTime = (long)query["IncubationTime"],
                        IncubationTemp = (long)query["IncubationTemp"],
                        Cycles = (long)query["Cycles"],
                        Owner = (long)query["Owner"],
                        Lock = (long)query["Lock"],
                        Comment = (string)query["Comment"],
                        Enzyme= (long)query["Enzyme"],
                        State= (long)query["State"],
                        Type = (long)query["Type"],
                        Tag = null
                    });


                }

                return myRecipe;
            }
        }

  
        public static List<Recipe> GetRecipeList(string name,string atype,string species,string tissue)
        {

            List<Recipe> myRecipe = new List<Recipe>();
            string myquery = "SELECT * FROM Recipe";
            string mywhere = "";
            if (name != "")
                mywhere = mywhere + "NickName=\'" + name + "\'";
            if (atype != "Any")
            {
                if (mywhere != "")
                    mywhere = mywhere + " AND ";
                mywhere = mywhere + "Type=\'" + atype + "\'";
            }
            if (species!="Any")
            {
                if (mywhere != "")
                    mywhere = mywhere + " AND ";
                mywhere = mywhere + "Species=\'"+species+"\'";
            }

            if (tissue != "Any")
            {
                if (mywhere != "")
                    mywhere = mywhere + " AND ";
                mywhere = mywhere + "Organ=\'"+tissue+"\'";
            }
            if (mywhere != "")
                mywhere = " WHERE " + mywhere;
            myquery = myquery + mywhere;

            using (SqliteConnection db =
                new SqliteConnection("Filename=s2Genomics.db"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand(myquery, db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    myRecipe.Add(new Recipe
                    {//TODO handle null results from query
                        Id = (long)query["Id"],
                        NickName = (string)query["NickName"],
                        Species = (string)query["Species"],
                        Organ = (string)query["Organ"],
                        GrindTime = (long)query["GrindTime"],
                        GrindTemp = (long)query["GrindTemp"],
                        RPM = (long)query["RPM"],
                        IncubationTime = (long)query["IncubationTime"],
                        IncubationTemp = (long)query["IncubationTemp"],
                        Cycles = (long)query["Cycles"],
                        Owner = (long)query["Owner"],
                        Lock = (long)query["Lock"],
                        Comment = (string)query["Comment"],
                        Enzyme = (long)query["Enzyme"],
                        State = (long)query["State"],
                        Type = (long)query["Type"],
                        Tag = null
                    });


                }

                return myRecipe;
            }
        }
        public static List<User> GetUserDataList()
        {

            List<User> myUser = new List<User>();

            using (SqliteConnection db =
                new SqliteConnection("Filename=s2Genomics.db"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand("SELECT * FROM User", db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    myUser.Add(new User
                    {
                        Id = (long)query["Id"],
                        LastName = (string)query["LastName"],
                        FirstName = (string)query["FirstName"],
                        email = (string)query["email"],
                        Telephone = (string)query["Telephone"],
                        Password = (string)query["Password"],
                        Comment = (string)query["Comment"],
                        Privilege= (long)query["Privilege"],
                        Tag = null
                    });


                }

                return myUser;
            }
        }

        public static DataTable GetDataTable(string tablename)
        {

            DataTable myDataTable = new DataTable();

            using (SqliteConnection db =
                new SqliteConnection("Filename=s2Genomics.db"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand("SELECT * FROM " + tablename, db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                if (query.HasRows)
                {
                    myDataTable.Clear();
                    myDataTable.BeginLoadData();
                    try
                    {
                        myDataTable.Load(query);
                    }
                    catch
                    {
                        //ignore exceptions
                    }
                    myDataTable.EndLoadData();
                    myDataTable.AcceptChanges();
                    foreach (DataRow row in myDataTable.Rows)
                    {
                        System.Diagnostics.Debug.WriteLine("=============================================================================");
                        for (int x = 0; x < myDataTable.Columns.Count; x++)
                        {
                            System.Diagnostics.Debug.Write(row[x].ToString() + " ");
                        }
                        System.Diagnostics.Debug.WriteLine("=============================================================================\n");
                    }
                }
                db.Close();
                return myDataTable;
            }
        }

        public static String GetPassword(int Id)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=s2Genomics.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Password FROM User WHERE Id=" + Id.ToString(), db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                if (query.Read())
                {
                    String dbpwd = query.GetString(0);
                    db.Close();
                    return (dbpwd);
                }

                db.Close();
                return (null);
            }

        }
        public static long GetUserId(string email)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=s2Genomics.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Id FROM User WHERE email='" + email+"'", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                if (query.Read())
                {
                    long dbId = query.GetInt64(0);
                    db.Close();
                    return (dbId);
                }

                db.Close();
                return (-1);
            }

        }
        public static String GetPassword(string email)
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=s2Genomics.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Password FROM User WHERE email='" + email.ToString()+"'", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                if (query.Read())
                {
                    String dbpwd = query.GetString(0);
                    db.Close();
                    return (dbpwd);
                }

                db.Close();
                return (null);
            }

        }
        public static void InsertNewUserRecord(string FirstName, string LastName, string email, string Telephone, string Password,string Privilege, string Comment)
        {
            using (SqliteConnection db =
            new SqliteConnection("Filename=s2Genomics.db"))
            {
                db.Open();
                List<String> entries = new List<string>();
                SqliteCommand insertCommand = new SqliteCommand
                    ("INSERT INTO User (FirstName,LastName,email,Telephone,Password,Privilege,Comment)" +
                    " VALUES ('" + FirstName + "','" + LastName + "','" + email + "','" + Telephone + "','" + Password + "','" + Privilege + "','" + Comment + "')", db);
                SqliteDataReader query = insertCommand.ExecuteReader();
                db.Close();
                return;
            }
        }
        public static void UpdateUserRecord(int Id, string FirstName, string LastName, string email, string Telephone, string Password, string Privilege, string Comment)
        {
            using (SqliteConnection db =
            new SqliteConnection("Filename=s2Genomics.db"))
            {
                db.Open();
                SqliteCommand updateCommand = new SqliteCommand
                    ("UPDATE  User " +
                     "SET FirstName='" + FirstName + "'," +
                     "LastName='"      + LastName + "'," +
                     "email='"         + email + "'," +
                     "Telephone='"     + Telephone + "'," +
                     "Password='"      + Password + "'," +
                     "Privilege = '"   + Privilege + "', " +
                     "Comment='"       + Comment + "' " +
                     "WHERE Id=" + Id.ToString(), db);
                SqliteDataReader query = updateCommand.ExecuteReader();
                db.Close();
                return;
            }
        }
        public static void DeleteUserRecord(int Id)
        {
            using (SqliteConnection db =
            new SqliteConnection("Filename=s2Genomics.db"))
            {
                db.Open();
                List<String> entries = new List<string>();
                SqliteCommand insertCommand = new SqliteCommand("Delete FROM User WHERE Id=" + Id.ToString(), db);
                SqliteDataReader query = insertCommand.ExecuteReader();
                db.Close();
                return;
            }
        }

        public static IDictionary<string, string> GetUserRecord(int Id)
        {

            using (SqliteConnection db =
                new SqliteConnection("Filename=s2Genomics.db"))
            {
                db.Open();
                List<String> entries = new List<string>();
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * FROM User WHERE Id=" + Id.ToString(), db);

                SqliteDataReader query = selectCommand.ExecuteReader();
                IDictionary<string, string> dict = new Dictionary<string, string>();
                if (query.Read())
                {
                    dict.Add("FirstName", query["FirstName"].ToString());
                    dict.Add("LastName",  query["LastName"].ToString());
                    dict.Add("email",     query["email"].ToString());
                    dict.Add("Telephone", query["Telephone"].ToString());
                    dict.Add("Password",  query["Password"].ToString());
                    dict.Add("Privilege", query["Privilege"].ToString());
                    dict.Add("Comment",   query["Comment"].ToString());

                    db.Close();
                    return (dict);

                }

                db.Close();
                return (null);
            }

        }
        public static User GetUser(string email)
        {

            using (SqliteConnection db =
                new SqliteConnection("Filename=s2Genomics.db"))
            {
                db.Open();
                User localuser = new User();
                List<String> entries = new List<string>();
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT * FROM User WHERE email='" + email +"'", db);

                SqliteDataReader query = selectCommand.ExecuteReader();
                IDictionary<string, string> dict = new Dictionary<string, string>();
                if (query.Read())
                {
                    localuser.FirstName=query["FirstName"].ToString();
                    localuser.LastName= query["LastName"].ToString();
                    localuser.email= query["email"].ToString();
                    localuser.Telephone= query["Telephone"].ToString();
                    localuser.Password= query["Password"].ToString();
                    localuser.Privilege= (long)query["Privilege"];
                    localuser.Comment=query["Comment"].ToString();
                    db.Close();
                    return (localuser);

                }

                db.Close();
                return (null);
            }

        }
        public static List<String> GetUserList()
        {

            using (SqliteConnection db =
                new SqliteConnection("Filename=s2Genomics.db"))
            {
                db.Open();
                List<String> entries = new List<string>();
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT email from User", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    entries.Add(query["email"].ToString());

                }

                db.Close();
                entries.Sort();
                return (entries);
            }

        }
        public static List<String> GetSpeciesList()
        {

            using (SqliteConnection db =
                new SqliteConnection("Filename=s2Genomics.db"))
            {
                db.Open();
                List<String> entries = new List<string>();
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT DISTINCT Species from Recipe", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    entries.Add(query["Species"].ToString());

                }

                db.Close();
                entries.Sort();
                return (entries);
            }

        }

        public static List<String> GetOrganList()
        {

            using (SqliteConnection db =
                new SqliteConnection("Filename=s2Genomics.db"))
            {
                db.Open();
                List<String> entries = new List<string>();
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT DISTINCT Organ from Recipe", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    entries.Add(query["Organ"].ToString());

                }

                db.Close();
                entries.Sort();
                return (entries);
            }

        }

        public static void maintainRecipe(Recipe myrecipe)
        {
            using (SqliteConnection db =
            new SqliteConnection("Filename=s2Genomics.db"))
            {

                if (myrecipe.Tag == "Delete")
                {
                    if (myrecipe.Lock != 0)
                    {
                        //var dialog = new MessageDialog("Recipe is Locked, cannot Delete");
                        //var result1 = await dialog.ShowAsync();
                        return;
                    }
                    db.Open();
                    SqliteCommand insertCommand = new SqliteCommand("Delete FROM Recipe WHERE Id=" + myrecipe.Id.ToString(), db);
                    SqliteDataReader query = insertCommand.ExecuteReader();
                    db.Close();
                    return;
                }
                else if (myrecipe.Tag == "Insert")
                {
                    db.Open();
                    List<String> entries = new List<string>();
                    SqliteCommand insertCommand = new SqliteCommand
                        ("INSERT INTO Recipe (Species,Organ,GrindTime,GrindTemp,RPM,NickName,IncubationTime,IncubationTemp,Cycles,Owner,Lock,State,Enzyme,Type,Comment)" +
                        " VALUES ('" +
                        myrecipe.Species + "','" + 
                        myrecipe.Organ + "','" + 
                        myrecipe.GrindTime + "','" + 
                        myrecipe.GrindTemp + "','" +
                        myrecipe.RPM + "','" + 
                        myrecipe.NickName + "','" + 
                        myrecipe.IncubationTime + "','" + 
                        myrecipe.IncubationTemp + "','" +
                        myrecipe.Cycles + "','" + 
                        myrecipe.Owner + "','" + 
                        myrecipe.Lock + "','" + 
                        myrecipe.State + "','" + 
                        myrecipe.Enzyme+ "','" +
                        myrecipe.Type + "','" +
                        myrecipe.Comment + "')", db);
                    SqliteDataReader query = insertCommand.ExecuteReader();
                    db.Close();
                    return;
                }
                else if (myrecipe.Tag == "Edit")
                {

                    if (myrecipe.Lock != 0)
                        if (myrecipe.Owner != myrecipe.CurrentUser)
                        {
                            //var dialog = new MessageDialog("Recipe is Locked, cannot Edit");
                            //var result1 = await dialog.ShowAsync();
                            return;
                        }
                    db.Open();
                    SqliteCommand updateCommand = new SqliteCommand
                        ("UPDATE  Recipe "  +
                         "SET Species='"    + myrecipe.Species         + "'," +
                         "Organ='"          + myrecipe.Organ           + "'," +
                         "GrindTime='"      + myrecipe.GrindTime       + "'," +
                         "GrindTemp='"      + myrecipe.GrindTemp       + "'," +
                         "RPM='"            + myrecipe.RPM             + "'," +
                         "IncubationTime='" + myrecipe.IncubationTime  + "'," +
                         "NickName='"       + myrecipe.NickName + "'," +
                         "IncubationTemp='" + myrecipe.IncubationTemp  + "'," +
                         "Cycles='"         + myrecipe.Cycles          + "'," +
                         "Owner='"          + myrecipe.Owner           + "'," +
                         "Enzyme='"         + myrecipe.Enzyme          + "'," +
                         "State='"          + myrecipe.State           + "'," +
                         "Lock='"           + myrecipe.Lock            + "'," +
                         "Type='"           + myrecipe.Type + "'," +
                         "Comment='"        + myrecipe.Comment         + "' " +
                         "WHERE Id=" + myrecipe.Id.ToString(), db);
                    SqliteDataReader query = updateCommand.ExecuteReader();
                    db.Close();
                    return;
                }
            }

        }

        public static void InitializeDatabase()
        {
            using (SqliteConnection db = new SqliteConnection("Filename=s2Genomics.db"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS User (Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "LastName NVARCHAR(128) NULL, " +
                    "FirstName NVARCHAR(128) NULL, " +
                    "email NVARCHAR(128) NULL, " +
                    "Telephone NVARCHAR(32) NULL, " +
                    "Password NVARCHAR(128) NULL, " +
                    "Privilege INTEGER NULL, " +
                    "Comment NVARCHAR(2048) NULL " +
                    ")";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();

                string testCommand = "SELECT * FROM User WHERE LastName='Admin'";
                SqliteCommand adminexists = new SqliteCommand(testCommand, db);
                SqliteDataReader query = adminexists.ExecuteReader();
                if (!query.Read())
                {
                    InsertNewUserRecord("Admin", "Admin", "admin@s2genomics.com", "", "Password","0", "");
                }



                tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Recipe (Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "Species NVARCHAR(128) NULL, " +
                    "Organ NVARCHAR(128) NULL, " +
                    "GrindTime INTEGER NULL, " +
                    "GrindTemp INTEGER NULL, " +
                    "RPM INTEGER NULL, " +
                    "NickName NVARCHAR(128) NULL, " +
                    "IncubationTime INTEGER NULL, " +
                    "IncubationTemp INTEGER NULL, " +
                    "Owner INTEGER NULL, " +
                    "Lock INTEGER NULL, " +
                    "Cycles INTEGER NULL, " +
                    "State INTEGER NULL, " +
                    "Enzyme INTEGER NULL, " +
                    "Type INTEGER NULL, " +
                    "Comment NVARCHAR(2048) NULL " +
                    ")";

                createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
                tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Experiment (Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "StartTime INTEGER NULL, " +
                    "EndTime INTEGERNVARCHAR(128) NULL, " +
                    "RecipeId INTEGER NULL, " +
                    "UserId INTEGER NULL, " +
                    "Barcode INTEGER NULL, " +
                    "IncubationTime INTEGER NULL, " +
                    "GrindCycles INTEGER NULL, " +
                    "IncubationTemperature INTEGER NULL, " +
                    "Comment NVARCHAR(2048) NULL " +
                    ")";

                createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
            }
        }
    }
}
