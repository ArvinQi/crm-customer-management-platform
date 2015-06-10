using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models;
using CRM.DataBase;

namespace CRM.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        Database db = new Database();
        //
        // GET: /Admin/User/
        /// <summary>
        /// 用户中心action
        /// </summary>
        /// <returns></returns>
        public ActionResult UserCenter()
        {
            ViewData["currentTag"] = "UserCenter";            
            if (Session["currentUser"]==null)//判定用户是否登录否则跳转到登录页
            {
                Response.Redirect("~/Home/Index");
            }
            tb_Users user = db.GetUserInfo((Session["currentUser"] as tb_Users).UserID);
            return View(user);//把当前用户信息同伙user对象传递到UserCenter页面
        }
        /// <summary>
        /// 用户中心更新个人信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult PutUserInfo(tb_Users user)
        {
            tb_Users currentuser = new tb_Users();
            if (Session["currentUser"] != null)//判定用户是否登录否则跳转到登录页
            {
                currentuser=Session["currentUser"] as tb_Users;
            }            
            if (db.PutUserInfo(user) > 0)//更新信息判定，为True则更新成功，否则更新失败，提示错误信息
            {
                if (currentuser.tb_UserType.UserType == "admin")//根据用户类型判断更新成功跳转页面
                {
                    Response.Redirect("~/Admin/User/UserCenter");
                }
                else
                {
                    Response.Redirect("~/Portal/Home/");
                }
            }else
            {
                ViewData["Message"] = Request.QueryString["Message"] ?? "更新失败！";
                ViewData["GoBack"] = "~/Admin/User/UserCenter";
                Response.Redirect("~/Admin/Home/Error");
            }
            
            return View();
        }
        //
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="OldPassword"></param>
        /// <param name="NewPassword"></param>
        /// <returns></returns>
        public ContentResult Password(string id,string OldPassword,string NewPassword)
        {
            ContentResult vResult = new ContentResult();
            vResult.Content = db.ChangePassword(id,OldPassword,NewPassword) ;//更新密码，返回提示信息
            return vResult;        
        }
        //
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        public ContentResult PictureUpload()
        {
            ContentResult rs = new ContentResult();
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            string imgPath = Url.Content("~/Content/PersonPicture");//图片保存路径
            if (hfc.Count > 0)
            {
                imgPath =imgPath+ "/" + (Session["currentUser"] as tb_Users) .Name+ hfc[0].FileName;
                string PhysicalPath = Server.MapPath(imgPath);
                hfc[0].SaveAs(PhysicalPath);//上传图片
            }
            rs.Content = imgPath;//返回保存图片路径
            return rs;
        }

    }
}
