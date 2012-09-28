using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult FinalDisplay()
        {
            try
            {
                var myCommand = new OleDbCommand();
                OleDbDataAdapter Da;
                var ds = new DataSet();
                string sql = null;
                string sql1 = null;

                int i = 0;
                var myConnection = new OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='E:\\Sample.xls';Extended Properties=Excel 8.0;");
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
                    sql = "Update [general_report$] set Result=" + rd.Hour + "  where [Ticket No]='" + ds.Tables[0].Rows[i][0].ToString() + "'";
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
                                sql = "Update [general_report$] set Boolean='Yes' where [Ticket No]='" + ds.Tables[0].Rows[i][0].ToString() + "'";
                            else
                                sql = "Update [general_report$] set Boolean='No' where [Ticket No]='" + ds.Tables[0].Rows[i][0].ToString() + "'";

                            break;
                        case "04-Low Priority":
                            if (d > 8)
                                sql = "Update [general_report$] set Boolean='Yes' where [Ticket No]='" + ds.Tables[0].Rows[i][0].ToString() + "'";
                            else
                                sql = "Update [general_report$] set Boolean='No' where [Ticket No]='" + ds.Tables[0].Rows[i][0].ToString() + "'";

                            break;

                        case "02-Give High Attention":
                            if (d <= 4)
                                sql = "Update [general_report$] set Boolean='Yes' where [Ticket No]='" + ds.Tables[0].Rows[i][0].ToString() + "'";
                            else
                                sql = "Update [general_report$] set Boolean='No' where [Ticket No]='" + ds.Tables[0].Rows[i][0].ToString() + "'";

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
                sql1 = "Select * from [general_report$]";
                myCommand.CommandText = sql1;
                Da = new OleDbDataAdapter(myCommand);
                Da.Fill(ds);
                var model = ds.Tables[0];
                myConnection.Close();
                return View(model);

            }

            catch (Exception ex)
            {

                ViewBag.Message = ex.ToString();
                return Content(ViewBag.Message);
            }
         
        }
    }
}
