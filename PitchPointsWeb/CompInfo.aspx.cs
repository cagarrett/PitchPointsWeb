﻿using System;
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

        String email = "";
        int climberID = 0;
        int registered = 0;

        int competitionId = 0;
        int category = 0;
        bool logIn = true; 

        protected async void Page_Load(object sender, EventArgs e)
        {
            
            String CompId = Request.QueryString["Id"];
            competitionId = Convert.ToInt32(CompId);

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
                    logIn = true;
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
                    email = TokenModel.Content.Email;
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

        protected async void btnRegister_Click(object sender, EventArgs e)
        {
            if (logIn)
            {
                String empty = "";
                var controller = new CompetitionsController();
                var RegClimberModel = new CompetitionRegistrationModel
                {
                    Token = Master.ReadToken(),
                    CompetitionId = competitionId,
                    Register = 1
                    
                };
                var result = await controller.ModifyCompetitionStatus(RegClimberModel);
                if (result.ResponseCode == 0)
                {
                    
                    //ClimberId.Value = empty; witness_id.Value = empty; route_id.Value = empty; falls.Value = empty;
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "RegisterSuccess();", true);
                }
                else if (result.ResponseCode == 1)
                {
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "RegisterError();", true);
                }
                else if (result.ResponseCode == 2)
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ReRegisterSuccess();", true);
                }
                else if (result.ResponseCode == 3)
                {

                    //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "completeForm();", true);
                }

            }
            
        }
        protected async void btnUnregister_Click(object sender, EventArgs e)
        {
            if (logIn && registered == 1)
            {
                String empty = "";
                var controller = new CompetitionsController();
                var RegClimberModel = new CompetitionRegistrationModel
                {

                    CompetitionId = competitionId,
                    Register = 0

                };
                var result = await controller.ModifyCompetitionStatus(RegClimberModel);
                if (result.ResponseCode == 0)
                {

                    //ClimberId.Value = empty; witness_id.Value = empty; route_id.Value = empty; falls.Value = empty;
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "UnregisterSuccess();", true);
                }
                else if (result.ResponseCode == 1)
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "UnregisterError();", true);
                }
                else if (result.ResponseCode == 2)
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ReUnregisterError();", true);
                }
                else if (result.ResponseCode == 3)
                {

                    //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "success();", true);
                }
                else if (result.ResponseCode == 4)
                {

                    //ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "serverError();", true);
                }
            }
        }
    }
}