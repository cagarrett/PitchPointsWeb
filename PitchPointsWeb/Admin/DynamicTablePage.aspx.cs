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
                var newRule = ((TextBox)ruleGridView.FooterRow.FindControl("ruleTextBox")).Text;
                var newRow = dataTable.NewRow();
                newRow["Description"] = newRule;
                if (ruleGridView.Rows.Count == 1 && !ruleGridView.Rows[0].Visible)
                {
                    dataTable.Rows.RemoveAt(0);
                }
                else
                {
                    ExtractExistingRules(ref dataTable);
                }
                dataTable.Rows.Add(newRow);

                ViewState["RuleTable"] = dataTable;

                ruleGridView.DataSource = dataTable;
                ruleGridView.DataBind();

                RestoreValuesToGrid(ref dataTable);
            }
        }

        protected void deleteRuleButton_OnClick(object sender, EventArgs e)
        {
            var button = sender as LinkButton;
            var dataTable = (DataTable) ViewState["RuleTable"];
            if (button != null && dataTable != null)
            {
                var row = ((GridViewRow) button.Parent.Parent).RowIndex;
                if (dataTable.Rows.Count == 1) // Last row that is being deleted
                {
                    AddInitialRuleRow();
                }
                else
                {
                    ExtractExistingRules(ref dataTable);
                    ViewState["RuleTable"] = dataTable;
                    dataTable.Rows.RemoveAt(row);
                    ruleGridView.DataSource = dataTable;
                    ruleGridView.DataBind();
                    RestoreValuesToGrid(ref dataTable);
                }
            }
        }

        private void ExtractExistingRules(ref DataTable table)
        {
            for (var i = 0; i < table.Rows.Count; i++) // Load form into datatable
            {
                var box = (TextBox)ruleGridView.Rows[i].FindControl("ruleDescriptionTextBox");
                table.Rows[i]["Description"] = box.Text;
            }
        }

        private void RestoreValuesToGrid(ref DataTable table)
        {
            for (var i = 0; i < ruleGridView.Rows.Count; i++)
            {
                var textBox = (TextBox)ruleGridView.Rows[i].Cells[0].FindControl("ruleDescriptionTextBox");
                textBox.Text = table.Rows[i]["Description"].ToString();
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
            ruleGridView.Rows[0].Visible = false;
        }

    }
}