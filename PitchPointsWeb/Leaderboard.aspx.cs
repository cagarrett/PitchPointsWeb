using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PitchPointsWeb
{
    public partial class _Default : Page
    {
        private void Get_Leaders(String Category)
        {

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataSource1.SelectParameters["compId"].DefaultValue = CompDropDownList.SelectedItem.Value;
            SqlDataSource2.SelectParameters["compId"].DefaultValue = CompDropDownList.SelectedItem.Value;
            SqlDataSource3.SelectParameters["compId"].DefaultValue = CompDropDownList.SelectedItem.Value;
            SqlDataSource4.SelectParameters["compId"].DefaultValue = CompDropDownList.SelectedItem.Value;
        }
        protected void BeginnerUpdateTimer_Tick(object sender, EventArgs e)
        {
            BeginnerDateStampLabel.Text = DateTime.Now.ToString();
        }
        protected void IntermediateUpdateTimer_Tick(object sender, EventArgs e)
        {
            IntermediateDateStampLabel.Text = DateTime.Now.ToString();
        }
        protected void AdvancedUpdateTimer_Tick(object sender, EventArgs e)
        {
            AdvancedDateStampLabel.Text = DateTime.Now.ToString();
        }
        protected void OpenUpdateTimer_Tick(object sender, EventArgs e)
        {
            OpenDateStampLabel.Text = DateTime.Now.ToString();
        }
    }
}