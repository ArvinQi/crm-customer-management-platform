using CRM.DataBase;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.Portal.Controllers
{
    public class ClientController : BaseController
    {
        //
        // GET: /Portal/Client/
        Database db = new Database();
        CRMEntities crm = new CRMEntities();
        #region 视图部分
        public ActionResult Index()
        {
            return View();            
        }
        /// <summary>
        /// 获取selectList内容
        /// </summary>
        /// <param name="id"></param>
        public void GetList(string id)
        {
            List<tb_Users> tb_users = crm.tb_Users.Where(u=>u.tb_UserType.UserType!="admin").OrderBy(d => d.UserID).ToList();
            SelectList UserSelect = new SelectList(tb_users, "UserID", "Name");
            ViewData["UserSelect"] = UserSelect;
            long id_long=long.Parse(id);
            List<tb_Clients> tb_clients = crm.tb_Clients.Where(u => u.UserID == id_long).OrderBy(d => d.ClientID).ToList();
            SelectList ClientIDSelect = new SelectList(tb_clients, "ClientID", "Name");
            ViewData["ClientIDSelect"] = ClientIDSelect;
            List<tb_Products> tb_products=crm.tb_Products.OrderBy(u=>u.ProductID).ToList();
            SelectList ProductIDSelect=new SelectList(tb_products,"ProductID","Name");
            ViewData["ProductIDSelect"] = ProductIDSelect;
            List<tb_Suppliers> tb_suppliers = crm.tb_Suppliers.OrderBy(u => u.SupplierID).ToList();
            SelectList SupplierIDSelect = new SelectList(tb_suppliers, "SupplierID", "Name");
            ViewData["SupplierIDSelect"] = SupplierIDSelect;
        }
        /// <summary>
        /// 客户信息
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public ActionResult ClientInfo(string id)
        {
            //IfLog(id, "ClientInfo");
            ViewData["id"] = id;
            ViewData["currentTag"] = "Clients";
            ViewData["diaform_id"] = "ClientInfo";
            ViewData["datagrid_url"] = "/CRM/Portal/Client/GetClients/"+id;
            ViewData["edit_url"] = "/CRM/api/ApiClients/";
            ViewData["col"] = "ClientID_Name_UserName_Sharing_SourceType_Importance_Phone";
            GetList(id);
            return View("Index");
        }
        /// <summary>
        /// 客户联系、交流
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public ActionResult ClientContact(string id)
        {
            ViewData["id"] = id;
            ViewData["currentTag"] = "Clients";
            ViewData["diaform_id"] = "ClientContact";
            ViewData["datagrid_url"] = "/CRM/Portal/Client/GetClientContactRecord/" + id;
            ViewData["edit_url"] = "/CRM/api/ApiClientContact/";
            ViewData["col"] = "ID_ClientID_UserName_BeginTimeDate_EndTimeDate_Content_Remarks";
            GetList(id);
            return View("Index");
        }
        /// <summary>
        /// 客户关怀
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public ActionResult ClientCare(string id)
        {
            ViewData["id"] = id;
            ViewData["currentTag"] = "Clients";
            ViewData["diaform_id"] = "ClientCare";
            ViewData["datagrid_url"] = "/CRM/Portal/Client/GetClientCare/" + id;
            ViewData["edit_url"] = "/CRM/api/ApiClientCare/";
            ViewData["col"] = "ID_ClientName_Care_CareFeedback_CareRemarks";
            GetList(id);
            return View("Index");
        }
        /// <summary>
        /// 客户反馈
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public ActionResult ClientCallback(string id)
        {
            ViewData["id"] = id;
            ViewData["currentTag"] = "Clients";
            ViewData["diaform_id"] = "ClientCallback";
            ViewData["datagrid_url"] = "/CRM/Portal/Client/GetClientCallback/" + id;
            ViewData["edit_url"] = "/CRM/api/ApiClientCallback/";
            ViewData["col"] = "ID_ClientName_ProductName_Callback_CallbackFeedback_CallbackRemarks";
            GetList(id);
            return View("Index");
        }
        /// <summary>
        /// 客户投诉
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public ActionResult ClientComplaint(string id)
        {
            ViewData["id"] = id;
            ViewData["currentTag"] = "Clients";
            ViewData["diaform_id"] = "ClientComplaint";
            ViewData["datagrid_url"] = "/CRM/Portal/Client/GetClientComplaint/" + id;
            ViewData["edit_url"] = "/CRM/api/ApiClientComplaint/";
            ViewData["col"] = "ID_ClientName_ProductName_Complaint_ComplaintFeedback_ComplaintRemarks";
            GetList(id);
            return View("Index");
        }
        /// <summary>
        /// 客户调查
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public ActionResult ClientSurvey(string id)
        {
            ViewData["id"] = id;
            ViewData["currentTag"] = "Clients";
            ViewData["diaform_id"] = "ClientSurvey";
            ViewData["datagrid_url"] = "/CRM/Portal/Client/GetClientSurvey/" + id;
            ViewData["edit_url"] = "/CRM/api/ApiClientSurvey/";
            ViewData["col"] = "ID_ClientName_ProductName_Survey_SurveyFeedback_SurveyRemarks";
            GetList(id);
            return View("Index");
        }
        /// <summary>
        /// 客户服务记录
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public ActionResult ServiceRecord(string id)
        {
            ViewData["id"] = id;
            ViewData["currentTag"] = "Clients";
            ViewData["diaform_id"] = "ServiceRecord";
            ViewData["datagrid_url"] = "/CRM/Portal/Client/GetServiceRecord/" + id;
            ViewData["edit_url"] = "/CRM/api/ApiServiceRecord/";
            ViewData["col"] = "ID_ClientName_UserName_Mode_CostEstimate_BeginTimeDate_EndTimeDate_Content_Satisfaction_Feedback_ServiceRemarks";
            GetList(id);
            return View("Index");
        }
        /// <summary>
        /// 销售机会
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public ActionResult SaleChance(string id)
        {
            //IfLog(id, "ClientInfo");
            ViewData["id"] = id;
            ViewData["currentTag"] = "Sales";
            ViewData["diaform_id"] = "SaleChance";
            ViewData["datagrid_url"] = "/CRM/Portal/Client/GetSaleChance/" + id;
            ViewData["edit_url"] = "/CRM/api/ApiSalesChance/";
            ViewData["col"] = "ID_ClientName_UserName_ProductName_Title_Total_Content_PreviewTimeDate_Remarks";
            GetList(id);
            return View("Index");
        }
        /// <summary>
        /// 销售报价
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public ActionResult SalePrice(string id)
        {
            //IfLog(id, "ClientInfo");
            ViewData["id"] = id;
            ViewData["currentTag"] = "Sales";
            ViewData["diaform_id"] = "SalePrice";
            ViewData["datagrid_url"] = "/CRM/Portal/Client/GetSalePrice/" + id;
            ViewData["edit_url"] = "/CRM/api/ApiSalesPrice/";
            ViewData["col"] = "ID_ClientName_UserName_ProductName_SalePrice_SaleMode_SaleOfferTitle_ProductClass";
            GetList(id);
            return View("Index");
        }
        /// <summary>
        /// 销售合同
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public ActionResult SaleAgreement(string id)
        {
            //IfLog(id, "ClientInfo");
            ViewData["id"] = id;
            ViewData["currentTag"] = "Sales";
            ViewData["diaform_id"] = "SaleAgreement";
            ViewData["datagrid_url"] = "/CRM/Portal/Client/GetSaleAgreement/" + id;
            ViewData["edit_url"] = "/CRM/api/ApiSalesAgreement/";
            ViewData["col"] = "ID_ClientName_UserName_ProductName_Name_Num_Class_Total_BeginTimeDate_EndTimeDate_FirstParty_SecondParty_CreateTimeDate_Describe_Content_Attachment_Remarks";
            GetList(id);
            return View("Index");
        }
        /// <summary>
        /// 销售记录
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public ActionResult SaleRecord(string id)
        {
            //IfLog(id, "ClientInfo");
            ViewData["id"] = id;
            ViewData["currentTag"] = "Sales";
            ViewData["diaform_id"] = "SaleRecord";
            ViewData["datagrid_url"] = "/CRM/Portal/Client/GetSaleRecord/" + id;
            ViewData["edit_url"] = "/CRM/api/ApiSalesRecord/";
            ViewData["col"] = "ID_ClientName_UserName_ProductName_Mode_Num_Price_Amount_Total_TimeDate_Content_Remarks";
            GetList(id);
            return View("Index");
        }
        /// <summary>
        ///产品信息
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public ActionResult ProductInfo(string id)
        {
            //IfLog(id, "ClientInfo");
            ViewData["id"] = id;
            ViewData["currentTag"] = "Products";
            ViewData["diaform_id"] = "ProductInfo";
            ViewData["datagrid_url"] = "/CRM/Portal/Client/GetProductInfo/" + id;
            ViewData["edit_url"] = "/CRM/api/ApiProductInfo/";
            ViewData["col"] = "ProductID_SupplierName_Name_Class_Spec_ConstPrice_SalePrice_Describe_Remarks";
            GetList(id);
            return View("Index");
        }
        /// <summary>
        /// 产品需求
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public ActionResult ProductDemand(string id)
        {
            //IfLog(id, "ClientInfo");
            ViewData["id"] = id;
            ViewData["currentTag"] = "Products";
            ViewData["diaform_id"] = "ProductDemand";
            ViewData["datagrid_url"] = "/CRM/Portal/Client/GetProductDemand/" + id;
            ViewData["edit_url"] = "/CRM/api/ApiProductDemand/";
            ViewData["col"] = "DemandID_UserName_ProductName_SubmitTimeDate_Problem_Class_Importance_Content_Remarks";
            GetList(id);
            return View("Index");
        }
        /// <summary>
        /// 供货商信息
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public ActionResult SuppliersInfo(string id)
        {
            //IfLog(id, "ClientInfo");
            ViewData["id"] = id;
            ViewData["currentTag"] = "Suppliers";
            ViewData["diaform_id"] = "SuppliersInfo";
            ViewData["datagrid_url"] = "/CRM/Portal/Client/GetSuppliersInfo/" + id;
            ViewData["edit_url"] = "/CRM/api/ApiSuppliersInfo/";
            ViewData["col"] = "SupplierID_Name_Contact_SPhone_Fax_Area_Website_Address_Email_PostalCode_AccountBank_BankAccount_Remarks";
            GetList(id);
            return View("Index");
        }        
        #endregion
        #region 操作部分
        /// <summary>
        /// 获取该用户的客户信息
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns>json对象</returns>
        public JsonResult GetClients(string id)
        {
            long id_long = 0;
            if (!string.IsNullOrEmpty(id))
            {
                id_long = long.Parse(id);
            }
            return Json(db.GetClients(id_long),JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取该用户的客户信息
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public JsonResult GetClientCare(string id)
        {
            long id_long = 0;
            if (!string.IsNullOrEmpty(id))
            {
                id_long = long.Parse(id);
            }
            return Json(db.GetClientCare(id_long), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取该用户的客户联系记录信息
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public JsonResult GetClientContactRecord(string id)
        {
            long id_long = 0;
            if (!string.IsNullOrEmpty(id))
            {
                id_long = long.Parse(id);
            }
            return Json(db.GetClientContactRecord(id_long), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取该用户的客户反馈
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public JsonResult GetClientCallback(string id)
        {
            long id_long = 0;
            if (!string.IsNullOrEmpty(id))
            {
                id_long = long.Parse(id);
            }
            return Json(db.GetClientCallback(id_long), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取该用户的客户投诉
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public JsonResult GetClientComplaint(string id)
        {
            long id_long = 0;
            if (!string.IsNullOrEmpty(id))
            {
                id_long = long.Parse(id);
            }
            return Json(db.GetClientComplaint(id_long), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取该用户的客户调查信息
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public JsonResult GetClientSurvey(string id)
        {
            long id_long = 0;
            if (!string.IsNullOrEmpty(id))
            {
                id_long = long.Parse(id);
            }
            return Json(db.GetClientSurvey(id_long), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取该用户的客户服务记录信息
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public JsonResult GetServiceRecord(string id)
        {
            long id_long = 0;
            if (!string.IsNullOrEmpty(id))
            {
                id_long = long.Parse(id);
            }
            return Json(db.GetServiceRecord(id_long), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取该用户对客户销售机会分析记录
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public JsonResult GetSaleChance(string id)
        {
            long id_long = 0;
            if (!string.IsNullOrEmpty(id))
            {
                id_long = long.Parse(id);
            }
            return Json(db.GetSaleChance(id_long), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取该用户的销售报价信息
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public JsonResult GetSalePrice(string id)
        {
            long id_long = 0;
            if (!string.IsNullOrEmpty(id))
            {
                id_long = long.Parse(id);
            }
            return Json(db.GetSalePrice(id_long), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取该用户的销售合同信息
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public JsonResult GetSaleAgreement(string id)
        {
            long id_long = 0;
            if (!string.IsNullOrEmpty(id))
            {
                id_long = long.Parse(id);
            }
            return Json(db.GetSaleAgreement(id_long), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取该用户的销售记录信息
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public JsonResult GetSaleRecord(string id)
        {
            long id_long = 0;
            if (!string.IsNullOrEmpty(id))
            {
                id_long = long.Parse(id);
            }
            return Json(db.GetSaleRecord(id_long), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取产品信息
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public JsonResult GetProductInfo(string id)
        {
            long id_long = 0;
            if (!string.IsNullOrEmpty(id))
            {
                id_long = long.Parse(id);
            }
            return Json(db.GetProductInfo(id_long), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取产品续期信息
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public JsonResult GetProductDemand(string id)
        {
            long id_long = 0;
            if (!string.IsNullOrEmpty(id))
            {
                id_long = long.Parse(id);
            }
            return Json(db.GetProductDemand(id_long), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        ///获取供货商信息
        /// </summary>
        /// <param name="id">当前用户ID</param>
        /// <returns></returns>
        public JsonResult GetSuppliersInfo(string id)
        {
            long id_long = 0;
            if (!string.IsNullOrEmpty(id))
            {
                id_long = long.Parse(id);
            }
            return Json(db.GetSuppliersInfo(id_long), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
