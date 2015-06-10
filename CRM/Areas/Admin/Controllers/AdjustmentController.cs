using CRM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.Admin.Controllers
{
    public class AdjustmentController : Controller
    {
        //
        // GET: /Admin/Adjustment/
        CRMEntities crm = new CRMEntities();
        /// <summary>
        /// Transfer页面加载是需要的Select控件参数
        /// </summary>
        /// <returns></returns>
        public ActionResult Transfer()
        {
            List<tb_Users> tb_users = crm.tb_Users.Where(u => u.tb_UserType.UserType != "admin").OrderBy(d => d.UserID).ToList();
            SelectList UserSelect = new SelectList(tb_users, "UserID", "Name");
            ViewData["UserSelect"] = UserSelect;
            return View();
        }
        /// <summary>
        /// Transfer按钮点击后，ajax请求的方法，实现修改客户的业务员绑定
        /// </summary>
        /// <param name="select"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public string PostTransfer(string select, string user_id)
        {
            try
            {
                long user_id_long=long.Parse(user_id);
                var item = select.Split(',');//将参数转换成数组
                for (var i = 0; i < item.Length; i++)//循环遍历该数组
                {
                    long client_id_long=long.Parse(item[i]);
                    tb_Clients client = crm.tb_Clients.Where(c => c.ClientID == client_id_long).FirstOrDefault();//先根据参数查找该客户
                    if (client != null)//判定是否存在该客户
                    {
                        client.UserID = user_id_long;//修改该客户的业务员编号
                        crm.tb_Clients.Attach(client);
                        DbEntityEntry<tb_Clients> entry = crm.Entry(client);
                        entry.State = EntityState.Modified;
                        crm.SaveChanges();//提交事务
                    }
                }
                return "Transferred!";//返回修改成功信息
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            
        }
    }
}
