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
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace PitchPointsWeb.Account
{
    public partial class Profile : Page
    {

        public class Climbs
        {
            
            public int WitnessID { get; set; }
            public string WitnessName { get; set; }
            public int Falls { get; set; }
            public int Points { get; set; }
            public int RouteID { get; set; }
            public int CompID { get; set; }

            public Climbs() { }

        }

        public List<Climbs> CompletedClimbs = new List<Climbs>();
        public List<int> CompetedComps = new List<int>();

        static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }
        int prevComps = 0;

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
                            command.Parameters.AddWithValue("@email", TokenModel.Content.Email);
                            SqlDataReader rdr = command.ExecuteReader();
                            while (rdr.Read())
                            {
                                FirstLabel.Text = (UppercaseFirst(rdr["FirstName"].ToString()));
                                LastLabel.Text = (UppercaseFirst(rdr["LastName"].ToString()));
                                //courseNums.Add(rdr["CourseNumber"].ToString());
                            }
                            rdr.Close();
                            command.ExecuteNonQuery();
                        }
                    }

                    UpCompDataSource.SelectParameters["email"].DefaultValue = TokenModel.Content.Email;
                    CompCompDataSource.SelectParameters["email"].DefaultValue = TokenModel.Content.Email;
                    EmailLabel.Text = TokenModel.Content.Email;
                    LifeTimePointsLabel.Text = result.Points.ToString();
                    FallsLabel.Text = result.Falls.ToString();
                    ParticipatedCompsLabel.Text = result.ParticipatedCompetitions.ToString();
                    prevComps = Convert.ToInt32(result.ParticipatedCompetitions.ToString());
                }
                else
                {

                }
            }
            /*
            while (prevComps > 0)
            {
                Table table = new Table();
                table.ID = "PreviousComp";
                table.CssClass = "bordered centered highlight responsive-table";
                Page.Form.Controls.Add(table);
                for (int i = 0; i < 4; i++)
                {
                    TableRow tRow = new TableRow();
                    for (int j = 0; j < 6; j++)
                    {
                        if (i == 0)
                        {
                            TableCell cell = new TableCell();
                            cell.ID = "tbxRow_" + i + "_Col_" + j;
                            cell.Text = "bitch";
                            tRow.Cells.Add(cell);
                        }
                        if (i == 1)
                        {
                            TableCell cell = new TableCell();
                            cell.ID = "tbxRow_" + i + "_Col_" + j;
                            cell.Text = "bitch";
                            tRow.Cells.Add(cell);
                        }
                        if (i == 2)
                        {
                            TableCell cell = new TableCell();
                            cell.ID = "tbxRow_" + i + "_Col_" + j;
                            cell.Text = "bitch";
                            tRow.Cells.Add(cell);
                        }
                        if (i == 3)
                        {
                            TableCell cell = new TableCell();
                            TextBox tb = new TextBox();
                            tb.ID = "tbxRow_" + i + "_Col_" + j;
                            tb.Text = "";
                            cell.Controls.Add(tb);
                            tRow.Cells.Add(cell);
                        }
                    }
                    table.Rows.Add(tRow);
                    prevComps = prevComps - 1;
                }
            }
            */
        }
    }
}