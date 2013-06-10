using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleWebService
{

    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Birth { get; set; }
    }

    public class DataCenter
    {
        public static List<User> AllUsers = new List<User>();

        public static void InitUsers()
        {
            if (AllUsers.Count <= 0)
            {
                for (int i = 1; i <= 20; i++)
                {
                    AllUsers.Add(
                        new User
                            {
                                Id = i + "",
                                Name = "User" + i,
                                Age = i,
                                Birth = new DateTime(2013, 1, 1).AddDays(i)
                            }
                        );
                }
            }
        }
    }
}