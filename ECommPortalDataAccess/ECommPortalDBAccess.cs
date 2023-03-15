using Entities;
using Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Numerics;
using Utilities;

namespace ECommPortalDataAccess
{
    public class ECommPortalDBAccess : IEcommPortalDBAccess
    {
        private string _conn;

        public ECommPortalDBAccess(IConfiguration configuration)
        {
            _conn = configuration.GetConnectionString("EcommPortalConnectionString");
        }
        public Plan GetPlanDetails()
        {
            DataHelper dh = new DataHelper(_conn);
            DataSet ds = new DataSet();

            Plan plan = new Plan();

            int j = 0;
            try
            {
                SqlParameter[] sqlparams = new SqlParameter[1];
                sqlparams[0] = new SqlParameter("@PlanID", 1);

                ds = dh.ExecuteDataSet("ECOMMERCE.uspGetPlanDetailsForAdmin", CommandType.StoredProcedure, sqlparams);

                if (ds != null && ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        plan.PlanID = Convert.ToInt32(ds.Tables[0].Rows[i]["PlanID"]);
                        plan.PlanName = Convert.ToString(ds.Tables[0].Rows[i]["PlanName"]);

                    }
                }
            }
            finally
            {
                if (dh != null)
                {
                    if (dh.DataConn != null)
                    {
                        dh.DataConn.Close();
                    }
                }
            }

            return plan;

        }

    }
}