using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestersErrorLog.Models;
using TestersErrorLog.ViewModel;


namespace TestersErrorLog.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index(EntityViewModel model)
        {
            if (this.Request.QueryString["Name"] != null)
            {
                SqlConnection sqlConnection1 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Testers"].ConnectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                cmd.CommandText = String.Format(@"Use Testers 
                                SELECT * FROM tbl_ErrorLog_{0}", this.Request.QueryString["Name"]);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();

                reader = cmd.ExecuteReader();

                model.Items = new List<Items>();
                while (reader.Read())
                {
                    ViewModel.Items data = new ViewModel.Items()
                    {
                        Id = (Int32)reader["ID"],
                        CompleteDate = reader["CompleteDate"].ToString(),
                        URL = reader["URL"].ToString(),
                        StatusLevel = reader["StatusLevel"].ToString(),
                        ErrorMessage = reader["ErrorMessage"].ToString(),

                    };
                    model.Items.Add(data);
                }

                sqlConnection1.Close();
            }
            return View(model);

        }

    }





}


