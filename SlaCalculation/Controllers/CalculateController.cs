using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SlaCalculation.Controllers
{
    public class CalculateController : Controller
    {
      
        //
        // GET: /Calculate/
        [Authorize(Roles = "NormalUser")]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                string fileExtension = Path.GetExtension(file.FileName);
                string connectionString = "";
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);

                try
                {
                    var myCommand = new OleDbCommand();
                    OleDbDataAdapter Da;
                    var ds = new DataSet();
                    string sql = null;
                    string sql1 = null;

                    int i = 0;
                    if (fileExtension == ".xls")
                    {

                        connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + path + "';Extended Properties=Excel 8.0;";
                    }

                    else if (fileExtension == ".xlsx")
                    {

                        connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + path + "';Extended Properties=Excel 12.0;";
                    }
                    var myConnection = new OleDbConnection(connectionString);
                    myConnection.Open();
                    myCommand.Connection = myConnection;

                    sql1 = "Select * from [general_report$]";
                    myCommand.CommandText = sql1;
                    Da = new OleDbDataAdapter(myCommand);
                    Da.Fill(ds);
                    while (i < ds.Tables[0].Rows.Count)
                    {
                        var rd = Convert.ToDateTime(ds.Tables[0].Rows[i][5].ToString());
                        var cd = Convert.ToDateTime(ds.Tables[0].Rows[i][4].ToString());
                        var Time = rd.Subtract(cd);
                        sql = "Update [general_report$] set Result=" + rd.Hour + "  where [Ticket No]='" + ds.Tables[0].Rows[i][0] + "'";
                        myCommand.CommandText = sql;
                        myCommand.ExecuteNonQuery();
                        i++;
                    }
                    ds.Clear();
                    myConnection.Close();


                    myConnection.Open();
                    sql1 = "Select * from [general_report$]";
                    myCommand.CommandText = sql1;
                    Da = new OleDbDataAdapter(myCommand);
                    Da.Fill(ds.Tables[0]);
                    myConnection.Close();

                    myConnection.Open();

                    i = 0;

                    while (i < ds.Tables[0].Rows.Count)
                    {
                        int d = Convert.ToInt32(ds.Tables[0].Rows[i][6].ToString());
                        string pri = ds.Tables[0].Rows[i][2].ToString();
                        switch (pri)
                        {
                            case "03-Normal Queue":
                                if (d <= 8)
                                    sql = "Update [general_report$] set Boolean='Yes' where [Ticket No]='" + ds.Tables[0].Rows[i][0] + "'";
                                else
                                    sql = "Update [general_report$] set Boolean='No' where [Ticket No]='" + ds.Tables[0].Rows[i][0]+ "'";

                                break;
                            case "04-Low Priority":
                                if (d > 8)
                                    sql = "Update [general_report$] set Boolean='Yes' where [Ticket No]='" + ds.Tables[0].Rows[i][0] + "'";
                                else
                                    sql = "Update [general_report$] set Boolean='No' where [Ticket No]='" + ds.Tables[0].Rows[i][0]+ "'";

                                break;

                            case "02-Give High Attention":
                                if (d <= 4)
                                    sql = "Update [general_report$] set Boolean='Yes' where [Ticket No]='" + ds.Tables[0].Rows[i][0] + "'";
                                else
                                    sql = "Update [general_report$] set Boolean='No' where [Ticket No]='" + ds.Tables[0].Rows[i][0]+ "'";

                                break;

                            //case default: sql = "Update [general_report$] set Boolean='Undefined Priority' where [Ticket No]='" + Ds.Tables[0].Rows[i][0].ToString() + "'";
                        }

                        i++;

                        myCommand.CommandText = sql;
                        myCommand.ExecuteNonQuery();

                    }
                    ds.Clear();
                    myConnection.Close();
                    myConnection.Open();
                    var sql2 = "Select * from [general_report$] where Boolean='Yes'";
                    myCommand.CommandText = sql2;
                    Da = new OleDbDataAdapter(myCommand);
                    Da.Fill(ds);
                    int a = ds.Tables[0].Rows.Count;
                    ViewBag.Message = Convert.ToString(a);
                    myConnection.Close();

                    myConnection.Open();
                    sql1 = "Select * from [general_report$]";
                    myCommand.CommandText = sql1;
                    Da = new OleDbDataAdapter(myCommand);
                    Da.Fill(ds);
                    var model = ds.Tables[0];
                    myConnection.Close();
                    return View("Index", model);

                }

                catch (Exception ex)
                {

                    ViewBag.Message = ex.ToString();
                    return Content(ViewBag.Message);
                }    
            }
            return View();
        }
       
    }
}
