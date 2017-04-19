using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PitchPointsWeb.Models;
using PitchPointsWeb.API;
using System.Diagnostics;
using PitchPointsWeb.Models.API;
using System.Data.SqlClient;
using System.Data;


namespace PitchPointsWeb
{
    public partial class CompInfo : Page
    {

        int LocationId = 0;
        int climberID = 0;
        bool registered = false;

        protected async void Page_Load(object sender, EventArgs e)
        {
            String CompId = Request.QueryString["Id"];

            string empty = "";
            if (CompetitionResults.Text == empty)
            {
                var controller = new AccountController();
                var TokenModel = new TokenModel
                {
                    Token = Master.ReadToken()
                };

                var result = await controller.GetUserSnapshot(TokenModel);
                if (result.Success)
                {
                    using (var connection = MasterController.GetConnection())
                    {
                        connection.Open();
                        using (var command = new SqlCommand("GetCompetitionLocation", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@comp", CompId);
                            SqlDataReader rdr = command.ExecuteReader();
                            while (rdr.Read())
                            {
                                LocationId = (Convert.ToInt32(rdr["LocationID"]));

                            }
                            rdr.Close();
                            command.ExecuteNonQuery();
                        }
                    }
                    CompCompDataSource.SelectParameters["email"].DefaultValue = TokenModel.Content.Email;
                    CompCompDataSource.SelectParameters["compId"].DefaultValue = CompId;

                    LocationDataSource.SelectParameters["comp"].DefaultValue = LocationId.ToString();
                    UnregisteredCompDataSource.SelectParameters["email"].DefaultValue = TokenModel.Content.Email;
                    UnregisteredCompDataSource.SelectParameters["targetComp"].DefaultValue = CompId;
                    RulesDataSource.SelectParameters["CompetitionID"].DefaultValue = CompId;
                    //CompetitionGridView.SelectParameters["compId"].DefaultValue = CompId;
                    if (LocationId == 2)
                    {
                        GymImage.Attributes["src"] = ResolveUrl("Assets/NuLuLogo.PNG");
                        // GymImage.src = Page.ResolveUrl("relative/path/to/image");
                    }
                    else if (LocationId == 1)
                    {
                        GymImage.Attributes["src"] = ResolveUrl("Assets/HHILogoWall.PNG");
                    }
                }
                else
                {

                }
            }



            CompetitionResults.Text = "Competition Results";
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {

        }
        protected void btnUnregister_Click(object sender, EventArgs e)
        {

        }
    }
}