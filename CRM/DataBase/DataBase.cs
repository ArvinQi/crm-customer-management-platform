using CRM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace CRM.DataBase
{
    public class Database
    {
        protected CRMEntities db = new CRMEntities();        
        //日志记录
        public string LogRecord(string Operation, DateTime LogTimeDate, long UserID)
        {
            tb_Log log = new tb_Log();            
            try
            {
                log.Operation = Operation;
                log.LogTimeDate = LogTimeDate;
                log.UserID = UserID;
                db.tb_Log.Add(log);
                int i=db.SaveChanges();
                return "记录成功";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }
        //
        /// <summary>
        /// 用户中心更新个人信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int PutUserInfo(tb_Users user)
        {
            try
            {
                db.tb_Users.Attach(user);
                DbEntityEntry<tb_Users> entry= db.Entry(user);
                entry.State = EntityState.Modified;                
                db.SaveChanges();
                return 1;
            }
            catch(Exception)
            {                
                return -1;
            }
        
        }
        public int PostUserInfo(tb_Users user)
        {
            try
            {
                user.Password = "123";
                db.tb_Users.Add(user);
                db.SaveChanges();
                return 1;
            }
            catch (Exception )
            {
                return -1;
            }

        }
        //
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public tb_Users GetUserInfo(long UserID)
        {
            return db.tb_Users.FirstOrDefault(u=>u.UserID==UserID);
        }

        //
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="OldPassword"></param>
        /// <param name="NewPassword"></param>
        /// <returns></returns>
        public string ChangePassword(string id,string OldPassword,string NewPassword) 
        {
            try
            {
                long id_long = long.Parse(id);
                tb_Users user = db.tb_Users.FirstOrDefault(u => u.UserID == id_long);
                if (user.Password != OldPassword)
                {
                    throw new Exception("旧密码输入错误！");
                }
                user.Password = NewPassword;
                db.tb_Users.Attach(user);
                DbEntityEntry<tb_Users> entry = db.Entry(user);
                entry.State = EntityState.Modified;
                if (db.SaveChanges() < 0)
                {
                    throw new Exception("保存失败，请检查后重试！");
                }
                return "密码已更改为："+NewPassword;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }        
        }
        /// <summary>
        /// 获取用户客户信息
        /// </summary>
        /// <param name="id">当前用户 ID</param>
        /// <returns></returns>
        public object GetClients(long id)
        {
            if (id == 0)
            {
                return db.tb_Clients.ToList();
                
            }
            else
            {
                return db.tb_Clients.Where(c => c.UserID == id).Select(c => new { c.ClientID, c.Name, c.Phone, c.Importance, c.Email, c.Fax, c.Area, c.Address, c.UserID, UserName = c.tb_Users.Name }).ToList();
                
            }
        }
        /// <summary>
        /// 获取该用户的客户信息
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public object GetClientCare(long id)
        {
            if (id == 0)
            {
                return db.tb_ClientCare.ToList();
            }
            else
            {
                return db.tb_ClientCare.Where(c => c.UserID == id).Select(c => new { c.ID, c.Care, c.ClientID, ClientName = c.tb_Clients.Name, c.UserID, UserName = c.tb_Users.Name, c.CareFeedback, c.CareReamarks }).ToList();
            }
        }
        /// <summary>
        /// 获取该用户的客户联系记录信息
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public object GetClientContactRecord(long id)
        {
            if (id == 0)
            {
                return db.tb_ClientContactRecord.ToList();
            }
            else
            {
                return db.tb_ClientContactRecord.Where(c => c.UserID == id).Select(c => new { c.ID, c.Content, c.ClientID, ClientName = c.tb_Clients.Name, c.UserID, UserName = c.tb_Users.Name, c.BeginTimeDate, c.EndTimeDate, c.Remarks }).ToList();
            }
        }
        /// <summary>
        /// 获取该用户的客户反馈
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public object GetClientCallback(long id)
        {
            if (id == 0)
            {
                return db.tb_ClientCallback.ToList();
            }
            else
            {
                return db.tb_ClientCallback.Where(c => c.UserID == id).Select(c => new { c.ID, c.Callback, c.ClientID,ClientName=c.tb_Clients.Name, c.UserID,UserName=c.tb_Users.Name, c.CallbackFeedback, c.CallbackRemarks,c.ProductID,ProductName=c.tb_Products.Name }).ToList();
            }
        }
        /// <summary>
        /// 获取该用户的客户投诉
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public object GetClientComplaint(long id)
        {
            if (id == 0)
            {
                return db.tb_ClientComplaint.ToList();
            }
            else
            {
                return db.tb_ClientComplaint.Where(c => c.UserID == id).Select(c => new { c.ID, c.Complaint, c.ClientID, ClientName = c.tb_Clients.Name, c.UserID, UserName = c.tb_Users.Name, c.ComplaintFeedback, c.ComplaintRemarks, c.ProductID, ProductName = c.tb_Products.Name }).ToList();
            }
        }
        /// <summary>
        /// 获取该用户的客户调查信息
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public object GetClientSurvey(long id)
        {
            if (id == 0)
            {
                return db.tb_ClientSurvey.ToList();
            }
            else
            {
                return db.tb_ClientSurvey.Where(c => c.UserID == id).Select(c => new { c.ID, c.Survey, c.ClientID, ClientName = c.tb_Clients.Name, c.UserID, UserName = c.tb_Users.Name, c.SurveyFeedback, c.SurveyRemarks, c.ProductID, ProductName = c.tb_Products.Name }).ToList();
            }
        }
        /// <summary>
        /// 获取该用户的客户服务记录信息
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public object GetServiceRecord(long id)
        {
            if (id == 0)
            {
                return db.tb_ServiceRecord.ToList();
            }
            else
            {
                return db.tb_ServiceRecord.Where(c => c.UserID == id).Select(c => new { c.ID, c.Content, c.ClientID, ClientName = c.tb_Clients.Name, c.UserID, UserName = c.tb_Users.Name, c.BeginTimeDate, c.EndTimeDate, c.Mode, c.CostEstimate,c.Feedback,c.Satisfaction,c.ServiceReamarks }).ToList();
            }
        }
        /// <summary>
        /// 获取该用户对客户销售机会分析记录
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public object GetSaleChance(long id)
        {
            if (id == 0)
            {
                return db.tb_SalesChance.ToList();
            }
            else
            {
                return db.tb_SalesChance.Where(c => c.UserID == id).Select(c => new { c.ID, c.ClientID, ClientName = c.tb_Clients.Name, c.UserID, UserName = c.tb_Users.Name, c.ProductID, ProductName = c.tb_Products.Name, c.Title, c.Total, c.Content, c.PreviewTimeDate, c.Remarks }).ToList();
            }
        }
        /// <summary>
        /// 获取该用户的销售报价信息
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public object GetSalePrice(long id)
        {
            if (id == 0)
            {
                return db.tb_SalesPrice.ToList();
            }
            else
            {
                return db.tb_SalesPrice.Where(c => c.UserID == id).Select(c => new { c.ID, c.ClientID, ClientName = c.tb_Clients.Name, c.UserID, UserName = c.tb_Users.Name, c.ProductID, ProductName = c.tb_Products.Name, c.SalePrice, c.SaleMode, c.SaleOfferTitle, c.ProductClass }).ToList();
            }
        }
        /// <summary>
        /// 获取该用户的销售合同信息
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public object GetSaleAgreement(long id)
        {
            if (id == 0)
            {
                return db.tb_SalesAgreement.ToList();
            }
            else
            {
                return db.tb_SalesAgreement.Where(c => c.UserID == id).Select(c => new { c.ID, c.ClientID, ClientName = c.tb_Clients.Name, c.UserID, UserName = c.tb_Users.Name, c.ProductID, ProductName = c.tb_Products.Name, c.Name, c.Num, c.Class, c.Total, c.BeginTimeDate, c.EndTimeDate, c.FirstParty, c.SecondParty, c.CreateTimeDate, c.Describe, c.Content, c.Attachment, c.Remarks }).ToList();
            }
        }
        /// <summary>
        /// 获取该用户的销售记录信息
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public object GetSaleRecord(long id)
        {
            if (id == 0)
            {
                return db.tb_SalesRecord.ToList();
            }
            else
            {
                return db.tb_SalesRecord.Where(c => c.UserID == id).Select(c => new { c.ID, c.ClientID, ClientName = c.tb_Clients.Name, c.UserID, UserName = c.tb_Users.Name, c.ProductID, ProductName = c.tb_Products.Name, c.Mode, c.Num, c.Price, c.Amount, c.Total, c.TimeDate, c.Content, c.Remarks }).ToList();
            }
        }
        /// <summary>
        /// 获取产品信息
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public object GetProductInfo(long id)
        {
            if (id == 0)
            {
                return db.tb_Products.ToList();
            }
            else
            {
                return db.tb_Products.Select(c => new { c.ProductID, SupplierName=c.tb_Suppliers.Name, c.Name, c.Class, c.Spec, c.ConstPrice, c.SalePrice, c.Describe, c.Remarks }).ToList();
            }
        }
        /// <summary>
        /// 获取产品续期信息
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public object GetProductDemand(long id)
        {
            if (id == 0)
            {
                return db.tb_Demands.ToList();
            }
            else
            {
                return db.tb_Demands.Where(c => c.UserID == id).Select(c => new { c.DemandID, c.UserID, UserName = c.tb_Users.Name, c.ProductID, ProductName = c.tb_Products.Name, c.SubmitTimeDate, c.Problem, c.Class, c.Importance, c.Content, c.Remarks }).ToList();
            }
        }
        /// <summary>
        /// 获取供货商信息
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public object GetSuppliersInfo(long id)
        {
            if (id == 0)
            {
                return db.tb_Suppliers.ToList();
            }
            else
            {
                return db.tb_Suppliers.Select(c => new { c.SupplierID, c.Name, c.Contact, c.SPhone, c.Fax, c.Area, c.Website, c.Address, c.Email, c.PostalCode, c.AccountBank, c.BankAccount, c.Remarks }).ToList();
            }
        }
    }
}