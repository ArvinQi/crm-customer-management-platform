using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class Depart
    {
        public Depart(long DepartID,string Name,string Remark,long ParentNode,string Authority ,int Sort,bool isParent) {
            this.DepartID = DepartID;
            this.Name = Name;
            this.Remark = Remark;
            this.ParentNode = ParentNode;
            this.Authority = Authority;
            this.Sort = Sort;
            this.isParent = isParent;
            //return this;
        }
        public long DepartID { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public Nullable<long> ParentNode { get; set; }
        public string Authority { get; set; }
        public Nullable<int> Sort { get; set; }
        public bool isParent { get; set; }
    }
    public class Duties
    {
        public Duties(long DutyID, string Name, string Remark, long DepartID, string DepartName)
        {
            this.DutyID = DutyID;
            this.Name = Name;
            this.Remark = Remark;
            this.DepartID = DepartID;
            this.DepartName = DepartName;
            //return this;
        }
        public long DutyID { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public long DepartID { get; set; }
        public string DepartName { get; set; }
        
    }
    public class Users
    {
        public Users(long UserID,string Name,string RealName,string WorkNum,long DepartID,string DepartName,long DutyID,string DutyName
            , string Telephone, string Sex, long UserTypeID, string UserType)
        {
            this.UserID = UserID;
            this.Name = Name;
            this.RealName = RealName;
            this.WorkNum = WorkNum;
            this.DepartID = DepartID;
            this.DepartName = DepartName;
            this.DutyID = DutyID;
            this.DutyName = DutyName;
            this.Telephone = Telephone;
            this.Sex = Sex;
            this.UserTypeID = UserTypeID;
            this.UserType = UserType;
            //this.Companytime = Companytime;
        }
        public long UserID { get; set; }
        public string Name { get; set; }
        public string RealName { get; set; }        
        public string WorkNum { get; set; }
        public long DepartID { get; set; }
        public string DepartName { get; set; }
        public long DutyID { get; set; }
        public string DutyName { get; set; }
        public string Telephone { get; set; }
        public string Sex { get; set; }
        public long UserTypeID { get; set; }
        public string UserType { get; set; }
        public System.DateTime Companytime { get; set; }
    }
}