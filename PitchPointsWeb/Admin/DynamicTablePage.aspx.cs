using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PitchPointsWeb.Admin
{
    public partial class DynamicTablePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                AddInitialRuleRow();
            }
        }

        protected void addRuleButton_OnClick(object sender, EventArgs e)
        {
            if (ViewState["RuleTable"] != null)
            {
                var dataTable = (DataTable) ViewState["RuleTable"];
                DataRow tableRow = null;
                for (var i = 0; i < dataTable.Rows.Count; i++)
                {
                    var box = (TextBox) ruleGridView.Rows[i].Cells[0].FindControl("ruleDescriptionTextBox");
                    tableRow = dataTable.NewRow();
                    dataTable.Rows[i]["Description"] = box.Text;
                }
                //if (tableRow == null)
                //{
                //    tableRow = dataTable.NewRow();
                //    tableRow
                //}
                //if (tableRow != null)
                //{
                //    dataTable.Rows.Add(tableRow);
                //}
                ViewState["RuleTable"] = dataTable;

                ruleGridView.DataSource = dataTable;
                ruleGridView.DataBind();

                for (var i = 0; i < dataTable.Rows.Count; i++)
                {
                    var textBox = (TextBox)ruleGridView.Rows[i].Cells[0].FindControl("ruleDescriptionTextBox");
                    textBox.Text = dataTable.Rows[i]["Description"].ToString();
                }
            }
        }

        protected void deleteRuleButton_OnClick(object sender, EventArgs e)
        {
        }

        private void LoadPreviousData()
        {
            var dataTable = (DataTable) ViewState["RuleTable"];
            if (dataTable?.Rows.Count > 0)
            {
                
            }
        }

        private void AddInitialRuleRow()
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("Description");
            dataTable.Columns.Add("Actions");
            dataTable.Rows.Add(dataTable.NewRow());
            ViewState["RuleTable"] = dataTable;
            ruleGridView.DataSource = dataTable;
            ruleGridView.DataBind();
        }

    }
}