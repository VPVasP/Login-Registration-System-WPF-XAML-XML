using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace Login_Registration_WPF_XML
{
    public class UserInfoList
    {
        public List<UserInfo> UsersInfoList { get; set; } = new List<UserInfo>();
    }

    public class UserInfo
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    internal class LoginRegisterManager
    {
        private UserInfoList userInfoList;
        private string userSavedFile = "userData.xml";

        public LoginRegisterManager()
        {
            userInfoList = new UserInfoList();
            LoadUserData();
        }

        // register method
        public void Register(string registerUsername, string registerPassword)
        {
            if (DoesUserExistRegister(registerUsername))
            {
                Debug.WriteLine("Username already exists");
            }
            else
            {
                UserInfo newUser = new UserInfo()
                {
                    Username = registerUsername,
                    Password = registerPassword,
                };

                Debug.WriteLine("Registered New User, Also Welcome " + registerUsername);
                userInfoList.UsersInfoList.Add(newUser);
                SaveUserData();
            }
        }

        // login method
        public void Login(string loginUsername, string loginPassword)
        {
            if (DoesUserExistLogin(loginUsername, loginPassword))
            {
                Debug.WriteLine("You are now logged in " + loginUsername);
            }
            else
            {
                Debug.WriteLine("Wrong Username or Password");
            }
        }

        // checking if a username already exists in registration method
        public bool DoesUserExistRegister(string username)
        {
            return userInfoList.UsersInfoList.Exists(user => user.Username == username);
        }

        // checking if a username already exists in login method
        public bool DoesUserExistLogin(string username, string password)
        {
            return userInfoList.UsersInfoList.Exists(user => user.Username == username && user.Password == password);
        }

        // save data to XML
        private void SaveUserData()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(UserInfoList));
            using (TextWriter writer = new StreamWriter(userSavedFile))
            {
                serializer.Serialize(writer, userInfoList);
            }
        }

        // load data from XML
        private void LoadUserData()
        {
            if (File.Exists(userSavedFile))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(UserInfoList));
                using (TextReader reader = new StreamReader(userSavedFile))
                {
                    userInfoList = (UserInfoList)serializer.Deserialize(reader);
                }
            }
        }
    }
}