using CRM.DataBase;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace CRM.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private CRMEntities db = new CRMEntities();
        Database dbase = new Database();
        //
        // GET: /Admin/Admin/
        #region 视图部分
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Depart()
        {
            return View();
        }
        public ActionResult Position()
        {
            List<tb_Departs> tb_departs = db.tb_Departs.OrderBy(d => d.Sort).ToList();
            SelectList DepartList = new SelectList(tb_departs,"DepartID","Name");
            ViewData["DepartList"] = DepartList;
            return View();
        }
        public ActionResult Users()
        {
            return View();
        }
        public ActionResult Permission()
        {
            return View();
        }
        /// <summary>
        /// 修改用户信息页面action
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UserInfo(string id)
        {
            tb_Users user = new tb_Users();
            if (!string.IsNullOrEmpty(id)) 
            {
                long id_long=long.Parse(id);
                user = db.tb_Users.Where(u => u.UserID == id_long).FirstOrDefault();
            }
            SelectList departs=new SelectList(db.tb_Departs.ToList(),"DepartID","Name");
            SelectList duties = new SelectList(db.tb_Duties.ToList(), "DutyID", "Name");
            SelectList usertype = new SelectList(db.tb_UserType.ToList(), "UserTypeID", "UserType");
            ViewData["Departs"] = departs;
            ViewData["Duties"] = duties;
            ViewData["UserType"] = usertype;
            //ViewData["user"] = user;
            return View(user);
        }
        #endregion
        #region 操作部分
        /// <summary>
        /// 根据父节点获取部门列表信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult Gettb_Departs(string id)
        {
            long id_long = long.Parse(id);
            List<tb_Departs> tb_departs = db.tb_Departs.Where(d => d.ParentNode==id_long).OrderBy(d=>d.Sort).ToList();
            List<Depart> treelist = new List<Depart>();
            if (tb_departs == null)
            {
                throw new Exception("未找到请求的数据！");
            }
            else 
            {
                foreach (tb_Departs td in tb_departs)
                {
                    bool isParent = false;//判定是否有子节点
                    tb_Departs tbd = db.tb_Departs.Where(d => d.ParentNode == td.DepartID).FirstOrDefault();
                    if(tbd!=null)
                    {
                        isParent = true;
                    }
                    treelist.Add(new Depart((long)td.DepartID, td.Name, td.Remark, (long)td.ParentNode, td.Authority, 
                        td.Sort==null?0:(int)td.Sort, isParent));
                }
            }
            JsonResult jr = Json(treelist, JsonRequestBehavior.AllowGet);
            return jr;
        }
        /// <summary>
        /// treeNode拖拽
        /// </summary>
        /// <param name="id"></param>
        /// <param name="target"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string DropDepartTree(string id,string target,string type)
        {
            long id_long1 = long.Parse(id);
            long id_long2 = long.Parse(target);
            tb_Departs first = db.tb_Departs.Where(d => d.DepartID == id_long1).FirstOrDefault();
            tb_Departs second = db.tb_Departs.Where(d => d.DepartID == id_long2).FirstOrDefault();
            if (first == null || second == null)
            {
               // throw new Exception("不存在的节点！");
                return "不存在的节点！";
            }
            if (type == "inner")//成为子节点
            {
                first.ParentNode = second.DepartID;
                List<tb_Departs> next = db.tb_Departs.Where(d => d.ParentNode == second.DepartID)
                    .OrderBy(d => d.Sort).ToList();
                int nextSort=0;
                if(next.Count>0)
                {
                    nextSort = (int)next[next.Count-1].Sort + 1;
                }
                first.Sort = nextSort;
                db.tb_Departs.Attach(first);
                DbEntityEntry<tb_Departs> entry = db.Entry(first);
                entry.State = EntityState.Modified;
                db.SaveChanges();
                return "拖拽成功！";
            }
            else if (type == "prev")//成为同级前一个节点
            {
                List<tb_Departs> td = db.tb_Departs.Where(d => d.ParentNode == second.ParentNode)
                    .OrderBy(d=>d.Sort).ToList();
                bool isChange = false;
                foreach(tb_Departs tbd in td)
                {
                    if (first != tbd)
                    {
                        if (tbd == second)
                        {
                            first.ParentNode = second.ParentNode;
                            first.Sort = second.Sort;
                            db.tb_Departs.Attach(first);
                            DbEntityEntry<tb_Departs> entry = db.Entry(first);
                            entry.State = EntityState.Modified;
                            db.SaveChanges();
                            isChange = true;
                            second.Sort = second.Sort + 1;
                            db.tb_Departs.Attach(second);
                            DbEntityEntry<tb_Departs> entry1 = db.Entry(second);
                            entry1.State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        else
                        {
                            if (isChange == true)
                            {
                                tbd.Sort +=  1;
                                db.tb_Departs.Attach(tbd);
                                DbEntityEntry<tb_Departs> entry2 = db.Entry(tbd);
                                entry2.State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }
                }
                return "拖拽成功！";
            }
            else if (type == "next")//成为同级后节点
            {
                List<tb_Departs> td = db.tb_Departs.Where(d => d.ParentNode == second.ParentNode)
                    .OrderBy(d => d.Sort).ToList();
                bool isChange = false;
                foreach (tb_Departs tbd in td)
                {
                    if (first != tbd)
                    {
                        if (tbd == second)
                        {
                            first.ParentNode = second.ParentNode;
                            first.Sort = second.Sort + 1;
                            db.tb_Departs.Attach(first);
                            DbEntityEntry<tb_Departs> entry = db.Entry(first);
                            entry.State = EntityState.Modified;
                            db.SaveChanges();
                            isChange = true;
                        }
                        else
                        {
                            if (isChange == true)
                            {
                                tbd.Sort+=1;
                                db.tb_Departs.Attach(tbd);
                                DbEntityEntry<tb_Departs> entry2 = db.Entry(tbd);
                                entry2.State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }
                }
                return "拖拽成功！";
            }
            return "拖拽失败！";
        }
        /// <summary>
        /// 根据条件获取职位信息
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public JsonResult GetAlltb_Duties(string id) 
        {
            //需要使用实体来获取外键表的信息
            //db.Configuration.LazyLoadingEnabled = false;
            //db.Configuration.ProxyCreationEnabled = false;
            long id_long = 0;
            if (!string.IsNullOrEmpty(id))
            {
                id_long = long.Parse(id);
            }
            List<tb_Duties> td = db.tb_Duties.Where(d => d.DepartID == id_long).ToList();
            List<Duties> dutyList = new List<Duties>();
            //td[0].tb_Departs.DepartID=100002;
            foreach(tb_Duties t in td)
            {
                dutyList.Add(new Duties(t.DutyID,t.Name,t.Remark,t.DepartID,t.tb_Departs.Name));            
            }
            return Json(dutyList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 根据职位id获取职位信息，实现修改操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public JsonResult Gettb_Duties(string id)
        {
            long id_long = 0;
            if (!string.IsNullOrEmpty(id))
            {
                id_long = long.Parse(id);
            }
            tb_Duties td = db.tb_Duties.Where(d => d.DutyID == id_long).FirstOrDefault();
            Duties duty = new Duties(td.DutyID, td.Name, td.Remark, td.DepartID, td.tb_Departs.Name);
            return Json(duty, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取所有用户列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetAlltb_Users(string id)
        {
            long id_long = 0;
            if (!string.IsNullOrEmpty(id))
            {
                id_long = long.Parse(id);
            }
            List<tb_Users> tt = db.tb_Users.Where(d => d.DepartID == id_long).ToList();
            List<Users> List = new List<Users>();
            //td[0].tb_Departs.DepartID=100002;
            foreach (tb_Users t in tt)
            {
                List.Add(new Users(t.UserID, t.Name, t.RealName, t.WorkNum, t.DepartID, t.tb_Departs.Name, t.DutyID, t.tb_Duties.Name, t.Telephone, t.Sex, (long)t.UserTypeID, t.tb_UserType.UserType));
            }
            return Json(List, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 提交添加用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string PutUserInfo(tb_Users model)
        {
            if (dbase.PutUserInfo(model) > 0)
            {
                return "修改成功！";
            }
            else
            {
                return "修改失败！";
            }
        }
        /// <summary>
        /// 提交用户修改信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string PostUserInfo(tb_Users model)
        {
            if (dbase.PostUserInfo(model) > 0)
            {
                return "添加成功！";
            }
            else
            {
                return "添加失败！";
            }
        }
        #endregion
    }
}
