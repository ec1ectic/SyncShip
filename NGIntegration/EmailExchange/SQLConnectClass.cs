using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;
using Microsoft.Win32;
using System.Data;
using NextGen.Shared.Crypto;
using System.Windows.Forms;
using NextGen.Core.Cryptography;
using System.Security.Principal;
using System.IO;


namespace EmailExchange
{
    public class SQLConnectClass
    {
        static string userName = WindowsIdentity.GetCurrent().Name.Split('\\')[1];

        static string homePath = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
        static string[] homePathArray = homePath.Split('\\');


        public static string userPath = string.Empty;
        public bool checkPathBool;
        public bool checkPath1Bool;
        public bool checkPath2Bool;
        public bool checkPath3Bool;
        string decryptPwd;
        string decryptUid;
        string decrypt56Pwd;
        string decrypt56Uid;
        public static System.Data.SqlClient.SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection();
        public int bitVersion;

        public string initCatalog;

        const string _sAES128 = "AES128-";
        const int _nSecondsSalt = 3;

        //*****************************************************
        //*Read NGConfig.ini File to Determine Decryption Route
        //*****************************************************
        public void DetermineRoute()
        {



            //MessageBox.Show("Currect user: " + userName);
            //MessageBox.Show("Array item 1: " + homePathArray[1]);

            //if (!string.IsNullOrEmpty(homePathArray[1]))
            //{
            //    if (homePathArray[1] == "Users" || homePathArray[1] == "Documents and Settings")
            //    {
            //        //MessageBox.Show("Passed users and DS Settings");
            //        if (userName != homePathArray[2])
            //        {
            //            userName = homePathArray[2];
            //            //MessageBox.Show("Home Path as UserName.");
            //        }
            //    }
            //}

            string checkPath = "C:\\users\\" + userName + "\\Windows\\NGConfig.ini";
            string checkPath1 = "C:\\documents and settings\\" + userName + "\\Windows\\NGConfig.ini";
            string checkPath2 = Application.StartupPath.ToString() + "\\NGClaimsDb.ini";
            string dbUid = string.Empty;
            string dbDsn = string.Empty;

            checkPathBool = DoesFileExist(checkPath);
            checkPath1Bool = DoesFileExist(checkPath1);
            checkPath2Bool = DoesFileExist(checkPath2);

            //MessageBox.Show("Path: " + checkPath1);
            if (checkPathBool == false && checkPath1Bool == false && checkPath2Bool == false)
            {
                userPath = "C:\\Windows\\NGConfig.ini";
                //MessageBox.Show(userPath);
            }
            else if (checkPath2Bool == true)
            {
                userPath = checkPath2;
            }
            else if (checkPathBool == true)
            {
                userPath = checkPath;
                //MessageBox.Show(userPath);
            }
            else if (checkPath1Bool == true)
            {
                userPath = checkPath1;
                //MessageBox.Show(userPath);
            }
            //else if (checkPath2Bool == true)
            //{
            //    userPath = checkPath2;
            //}
            //else if (checkPath3Bool == true)
            //{
            //    userPath = checkPath3;
            //}

            //MessageBox.Show(userPath);
            IniFile ngIni = new IniFile(Application.StartupPath.ToString() + @"\NGClaimsDB.ini");
            IniFile objIniFile = new IniFile(userPath);

            if (checkPath2Bool == true)
            {
                if (!String.IsNullOrEmpty(ngIni.GetString("Data Sources", "Source1", "").ToString()))
                {
                    dbDsn = ngIni.GetString("Data Sources", "Source1", "");
                    dbUid = objIniFile.GetString(dbDsn, "UID", "");
                }
            }
            if (checkPath2Bool == false)
            {
                dbDsn = objIniFile.GetString("Data Sources", "Source1", "");
                dbUid = objIniFile.GetString(dbDsn, "UID", "");
            }
            bitVersion = IntPtr.Size;

            if (!dbUid.StartsWith("~") && !dbUid.StartsWith("#"))
            {
                Connect();
            }

            if (dbUid.StartsWith("~"))
            {
                Connect();
            }

            if (dbUid.StartsWith("#"))
            {
                Connect56();
            }

           // MessageBox.Show(dbDsn);

        }

        //***********************************************
        //*Version 5.5 and earlier Connection method.
        //***********************************************
        private void Connect()
        {

            IniFile objIniFile = new IniFile(userPath);
            //Call Decryption Method
            Decrypt();

            //Check to see if SQL connection is already open
            if (sqlConn.State == ConnectionState.Open)
            {
                sqlConn.Close();
            }
            //32-bit code to check registry ODBC Settings
            try
            {
                if (bitVersion == 4)
                {
                    //Read INI and Registry Values for Connection String
                    string dbDsn = objIniFile.GetString("Data Sources", "Source1", "");
                    RegistryKey servRegKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\ODBC\\ODBC.INI\\" + dbDsn);
                    RegistryKey dbRegKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\ODBC\\ODBC.INI\\" + dbDsn);
                    object servVal = servRegKey.GetValue("Server");
                    object dbVal = dbRegKey.GetValue("Database");
                    string dataSource = string.Empty;
                    initCatalog = string.Empty;
                    string dbUid = decryptUid;
                    string dbPwd = decryptPwd;

                    dataSource = servVal.ToString();
                    initCatalog = dbVal.ToString();

                    sqlConn.ConnectionString = ("Data Source=" + dataSource + ";" + "Initial Catalog=" + initCatalog + ";" + "User ID=" + dbUid + ";" + "Password=" + dbPwd + ";").ToString();
                }
                //64-bit code to check registry ODBC Settings
                if (bitVersion == 8)
                {
                    //Read INI and Registry Values for Connection String
                    string dbDsn = objIniFile.GetString("Data Sources", "Source1", "");
                    RegistryKey servRegKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\ODBC\\ODBC.INI\\" + dbDsn);
                    RegistryKey dbRegKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\ODBC\\ODBC.INI\\" + dbDsn);
                    object servVal = servRegKey.GetValue("Server");
                    object dbVal = dbRegKey.GetValue("Database");
                    string dataSource = string.Empty;
                    initCatalog = string.Empty;
                    string dbUid = decryptUid;
                    string dbPwd = decryptPwd;

                    dataSource = servVal.ToString();
                    initCatalog = dbVal.ToString();

                    

                   sqlConn.ConnectionString = ("Data Source=" + dataSource + ";" + "Initial Catalog=" + initCatalog + ";" + "User ID=" + dbUid + ";" + "Password=" + dbPwd + ";");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                System.Environment.Exit(0);
            }


        }

        //********************************************
        //*5.5 and earlier Decryption method
        //********************************************
        private void Decrypt()
        {
            IniFile objIniFile = new IniFile(userPath);
            string dbDsn = objIniFile.GetString("Data Sources", "Source1", "");
            string dbUid = objIniFile.GetString(dbDsn, "UID", "");
            string dbPwd = objIniFile.GetString(dbDsn, "PWD", "");
            string UK = "AUUMLSUMSUAUA";
            string PK = "UFUTUSCVUUKUGA";

            if (!dbUid.StartsWith("~") && !dbUid.StartsWith("#"))
            {
                decryptUid = dbUid;
                decryptPwd = dbPwd;
            }
            else
            {
                decryptUid = dbUid.Substring(1);
                decryptPwd = dbPwd.Substring(1);

                NextGen.Shared.Crypto.Blowfish bFish = new NextGen.Shared.Crypto.Blowfish();

                decryptUid = bFish.DecryptString(ref decryptUid, ref UK);
                decryptPwd = bFish.DecryptString(ref decryptPwd, ref PK);
            }
        }

        //***********************************
        //*Close SQL Connection
        //***********************************
        public void Disconnect()
        {
            sqlConn.Close();
        }


        //***********************************
        //*5.6 Connection Method
        //***********************************
        private void Connect56()
        {
            IniFile objIniFile = new IniFile(userPath);
            //Call UserID and Password Decryption
            Decrypt56Uid();
            Decrypt56Pwd();

            //Close SQL Connection if it's currently open
           if (sqlConn.State == ConnectionState.Open)
            {
                sqlConn.Close();
            }

            if (bitVersion == 4)
            {
                string dbDsn = objIniFile.GetString("Data Sources", "Source1", "");
                RegistryKey servRegKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\ODBC\\ODBC.INI\\" + dbDsn);
                RegistryKey dbRegKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\ODBC\\ODBC.INI\\" + dbDsn);
                object servVal = servRegKey.GetValue("Server");
                object dbVal = dbRegKey.GetValue("Database");
                string dataSource = string.Empty;
                initCatalog = string.Empty;
                string dbUid = decrypt56Uid;
                string dbPwd = decrypt56Pwd;

                dataSource = servVal.ToString();
                initCatalog = dbVal.ToString();

                //SQL Connection String
                sqlConn.ConnectionString = ("Data Source=" + dataSource + ";" + "Initial Catalog=" + initCatalog + ";" + "User ID=" + dbUid + ";" + "Password=" + dbPwd + ";");
            }

            if (bitVersion == 8)
            {
                //Read INI and Registry Values for Connection String
                string dbDsn = objIniFile.GetString("Data Sources", "Source1", "");
                RegistryKey servRegKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\ODBC\\ODBC.INI\\" + dbDsn);
                RegistryKey dbRegKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\ODBC\\ODBC.INI\\" + dbDsn);
                object servVal = servRegKey.GetValue("Server");
                object dbVal = dbRegKey.GetValue("Database");
                string dataSource = string.Empty;
                initCatalog = string.Empty;
                string dbUid = decrypt56Uid;
                string dbPwd = decrypt56Pwd;

                dataSource = servVal.ToString();
                initCatalog = dbVal.ToString();

                sqlConn.ConnectionString = ("Data Source=" + dataSource + ";" + "Initial Catalog=" + initCatalog + ";" + "User ID=" + dbUid + ";" + "Password=" + dbPwd + ";");
            }
            try
            {
                sqlConn.Open();
            }
            catch (Exception ex)
           {
                MessageBox.Show(ex.ToString());
                System.Environment.Exit(0);
            }
        }

        //**********************************************
        //*Decrypt 5.6 User ID
        //**********************************************
        private void Decrypt56Uid()
        {
            IniFile objIniFile = new IniFile(userPath);
            string dbDsn = objIniFile.GetString("Data Sources", "Source1", "");
            string dbUid = objIniFile.GetString(dbDsn, "UID", "");


            NGBlowFish bfish = new NGBlowFish();

            //Remove the # and the last character of the encrypted User ID
            decrypt56Uid = dbUid.Substring(1, dbUid.Length - 1);

            //Pass the User ID and User Key to the DecryptAES Method
            decrypt56Uid = NGEncrypt.DecryptAES(decrypt56Uid, NGEncrypt.sNG_USER_KEY);
            decrypt56Uid = RemoveAESSalt(decrypt56Uid);

        }

        //******************************
        //*Decrypt the 5.6 Password
        //******************************
        private void Decrypt56Pwd()
        {
            IniFile objIniFile = new IniFile(userPath);
            string dbDsn = objIniFile.GetString("Data Sources", "Source1", "");
            string dbPwd = objIniFile.GetString(dbDsn, "PWD", "");

            NGBlowFish bfish = new NGBlowFish();

            //Remove the # and the last character of the encrypted Password
            decrypt56Pwd = dbPwd.Substring(1, dbPwd.Length - 1);

            //Pass the Password and Password Ket to the DecryptAES Method
            decrypt56Pwd = NGEncrypt.DecryptAES(decrypt56Pwd, NGEncrypt.sNG_PASSWORD_KEY);
            decrypt56Pwd = RemoveAESSalt(decrypt56Pwd);


        }



        //**********************************************************************
        //* Remove EAS salt and return result
        //**********************************************************************
        private string RemoveAESSalt(string sWithSalt)
        {
            // this if stmt is added incase we change strength of encryption
            if (sWithSalt.StartsWith(_sAES128) == true)
            {
                return sWithSalt.Substring(_sAES128.Length + _nSecondsSalt);
            }
            return ("");
        }

        private bool DoesFileExist(string filePath)
        {
            if (File.Exists(filePath) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
