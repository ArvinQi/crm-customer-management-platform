using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models;

namespace CRM.Controllers
{
    public class HomeController : Controller
    {
        CRMEntities db = new CRMEntities();
        DataBase.Database dba = new DataBase.Database();
        //
        // GET: /Home/

        public ActionResult Index()
        {
            Session["currentUser"] = null;
            return View();
        }

        public ActionResult Logon()
        {
            return View();
        }

        public ContentResult Login(string Username,string Password)
        {
            ContentResult cr = new ContentResult();
            try
            {
                tb_Users user=db.tb_Users.FirstOrDefault(u=>u.Name==Username);
                if (user != null)
                {
                    if (user.Password == Password)
                    {
                        Session["currentUser"] = user;
                        Session["UserType"] = user.tb_UserType.UserType;
                        dba.LogRecord("用户登录", DateTime.Now, user.UserID);
                        cr.Content = "登录成功！";
                    }
                    else
                    {
                        cr.Content = "密码不正确！";
                    }
                    return cr;
                }
                else
                {
                    cr.Content = "用户不存在！";                
                    return cr;
                }
            }
            catch(Exception)
            {
                cr.Content = "登陆失败！";
                return cr;
            }
        }
    }
}
