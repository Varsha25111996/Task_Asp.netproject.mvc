using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task_Asp.netproject.mvc.Models;
using Task_Asp.netproject.mvc.Sql_Connection;

namespace Task_Asp.netproject.mvc.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            //ViewBag.Grade = GetGrade();
            ViewBag.Design = GetDesignation();
            return View();
        }
        public ActionResult Employee_View()
        {
            ViewBag.Grade = GetGrade();
            ViewBag.Design = GetDesignation();
            return View();
        }
        public ActionResult PostData(EmployeeModel model)
        {
            string flag;
            if (model.EmployeeId == 0)
            {
                flag = "I";

            }
            else
            {
                flag = "U";
            }
            Db_Connection conn = new Db_Connection();
            SqlConnection sqlconn = conn.Connect();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandText = "SpEmployeeSave";
            sqlcmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlcmd.Connection = sqlconn;
            sqlcmd.Parameters.AddWithValue("@EmployeeId", model.EmployeeId);
            sqlcmd.Parameters.AddWithValue("@Firstname", model.Firstname);
            sqlcmd.Parameters.AddWithValue("@LastName", model.Lastname);
            sqlcmd.Parameters.AddWithValue("@EmailAddress", model.EmailAddress);
            sqlcmd.Parameters.AddWithValue("@PhoneNumber", model.PhoneNumber);
            sqlcmd.Parameters.AddWithValue("@EmployeeDesignationId", model.EmployeeDesignationId);
            sqlcmd.Parameters.AddWithValue("@DesignationGradeId", model.DesignationGradeId);
            sqlcmd.Parameters.AddWithValue("@flag", flag);
            sqlcmd.ExecuteNonQuery();
            return RedirectToAction("Index");


        }
        public ActionResult GetByID(int ID)
        {
            ViewBag.Grade = GetGrade();
            ViewBag.Design = GetDesignation();
            DataTable dt = new DataTable();
            Db_Connection conn = new Db_Connection();
            dt = conn.FillQuery("Select * from Employee where EmployeeId = " + ID);
            EmployeeModel model = new EmployeeModel()
            {
                EmployeeId = Convert.ToInt32(dt.Rows[0]["EmployeeId"]),
                Firstname = Convert.ToString(dt.Rows[0]["Firstname"]),
                Lastname = Convert.ToString(dt.Rows[0]["Lastname"]),
                EmailAddress = Convert.ToString(dt.Rows[0]["EmailAddress"]),
                PhoneNumber = Convert.ToString(dt.Rows[0]["PhoneNumber"]),
                EmployeeDesignationId = Convert.ToInt32(dt.Rows[0]["EmployeeDesignationId"]),
                DesignationGradeId = Convert.ToInt32(dt.Rows[0]["DesignationGradeId"]),
            };
            return View("Index", model);
        }
        public ActionResult Delete(int ID)
        {
            DataTable dt = new DataTable();
            Db_Connection conn = new Db_Connection();
            dt = conn.FillQuery("Delete From Employee Where EmployeeId =" + ID);
            return RedirectToAction("Index");
        }
        public List<SelectListItem> GetDesignation()
        {
            List<SelectListItem> _List = new List<SelectListItem>();
            _List.Add(new SelectListItem { Text = "--SELECT--", Value = "0" });
            DataTable dt = new DataTable();
            Db_Connection con = new Db_Connection();
            dt = con.FillQuery(" Select EmployeeDesignationId, DesignationName From EmployeeDesignation");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _List.Add(new SelectListItem()
                {
                    Value = dt.Rows[i]["EmployeeDesignationId"].ToString(),
                    Text = dt.Rows[i]["DesignationName"].ToString(),
                });
            }
            return _List;
        }
        public List<SelectListItem> GetGrade()
        {
            List<SelectListItem> _list = new List<SelectListItem>();
            _list.Add(new SelectListItem { Text = "--SELECT__", Value = "0" });
            DataTable dt = new DataTable();
            Db_Connection con = new Db_Connection();
            dt = con.FillQuery("Select DesignationGradeId,GradeName From DesignationGrade");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _list.Add(new SelectListItem()
                {
                    Value = dt.Rows[i]["DesignationGradeId"].ToString(),
                    Text = dt.Rows[i]["GradeName"].ToString(),
                });

            }
            return _list;
        }
        public ActionResult GetDetails()
        {
            List<EmployeeReportModel> _list = new List<EmployeeReportModel>();
            DataTable dt = new DataTable();
            Db_Connection conn = new Db_Connection();
            dt = conn.FillQuery("select EmployeeId, Firstname,Lastname,EmailAddress,PhoneNumber,DesignationName,GradeName From Employee E,EmployeeDesignation D,DesignationGrade DG Where E.EmployeeDesignationId = D.EmployeeDesignationId and DG.DesignationGradeId = E.DesignationGradeId");
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                EmployeeReportModel model = new EmployeeReportModel()
                {
                    EmployeeId = Convert.ToInt32(dt.Rows[i]["EmployeeId"]),
                    Firstname = Convert.ToString(dt.Rows[i]["Firstname"]),
                    Lastname = Convert.ToString(dt.Rows[i]["Lastname"]),
                    EmailAddress = Convert.ToString(dt.Rows[i]["EmailAddress"]),
                    PhoneNumber = Convert.ToString(dt.Rows[i]["PhoneNumber"]),
                    DesignationName = Convert.ToString(dt.Rows[i]["DesignationName"]),
                    GradeName = Convert.ToString(dt.Rows[i]["GradeName"])
                };

                _list.Add(model);
            }
            return View(_list);
        }
        public ActionResult GetGradeByDesignationId(int designationId)
        {
            List<SelectListItem> grades = new List<SelectListItem>();
            grades.Add(new SelectListItem { Text = "__SELECT__", Value = "0" });
            DataTable dt = new DataTable();
            Db_Connection con = new Db_Connection();
            dt = con.FillQuery("SELECT DesignationGradeId , GradeName FROm DesignationGrade Where EmployeeDesignationId = " + designationId);
            foreach (DataRow row in dt.Rows)
            {
                grades.Add(new SelectListItem
                {
                    Value = row["DesignationGradeId"].ToString(),
                    Text = row["GradeName"].ToString()
                });
            }
            return Json(grades, JsonRequestBehavior.AllowGet);
        }
    }
}