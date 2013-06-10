using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace SampleWebService
{
    /// <summary>
    /// Summary description for UserService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class UserService : System.Web.Services.WebService
    {
        public UserService()
        {
            DataCenter.InitUsers();
        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void UpdateUsers(User[] users)
        {
            foreach (var u in users)
            {
                var user = GetUserById(u.Id);
                if (user!=null)
                {
                    user.Age = u.Age;
                    user.Birth = u.Birth;
                    user.Name = u.Name;
                }
            }
        }

        /// <summary>
        /// Add user object to datasource.
        /// 新增User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string AddUser(User user)
        {
            user.Id = Guid.NewGuid().ToString("N");
            DataCenter.AllUsers.Add(user);
            return user.Id;
        }


        /// <summary>
        /// Get all users from datasource.
        /// 取得所有的users
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public User[] GetAllUsers()
        {
            return DataCenter.AllUsers.ToArray();
        }


        /// <summary>
        /// Get one User By User Id from datasource.
        /// 取得使用者者資料by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public User GetUserById(string id)
        {
            return DataCenter.AllUsers.SingleOrDefault(x => x.Id == id);
        }
    }
}
