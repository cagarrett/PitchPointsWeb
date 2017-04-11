using System;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PitchPointsWeb.Models;
using PitchPointsWeb.API;
using System.Diagnostics;
using PitchPointsWeb.Models.API;
using System.Data.SqlClient;
using System.Data;

namespace PitchPointsWeb.Account
{
    public partial class Profile : Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            Master.ReadToken();

            
            string empty = "";
            if (EmailLabel.Text == empty)
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
                        using (var command = new SqlCommand("GetAllUserInfo", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@userId", TokenModel.Content.Email);
                            SqlDataReader rdr = command.ExecuteReader();
                            while (rdr.Read())
                            {
                                FirstLabel.Text = (rdr["FirstName"].ToString());
                                LastLabel.Text = (rdr["LastName"].ToString());
                                //courseNums.Add(rdr["CourseNumber"].ToString());
                            }
                            rdr.Close();
                            command.ExecuteNonQuery();
                        }
                    }
                    EmailLabel.Text = TokenModel.Content.Email;
                    LifeTimePointsLabel.Text = result.Points.ToString();
                    FallsLabel.Text = result.Falls.ToString();
                    ParticipatedCompsLabel.Text = result.ParticipatedCompetitions.ToString();
                }
                else
                {

                }
            }

        }
    }
}