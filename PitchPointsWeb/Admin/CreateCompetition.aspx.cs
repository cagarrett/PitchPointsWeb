using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PitchPointsWeb.Admin
{
    public partial class CreateCompetition : System.Web.UI.Page
    {

        private const string RuleTable = "RuleTable";
        private const string RouteTable = "RouteTable";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetInitialRuleTableSource();
            }
        }

        protected void addRuleButton_OnClick(object sender, EventArgs e)
        {
            if (ViewState[RuleTable] == null) return;
            var dataTable = (DataTable)ViewState[RuleTable];
            var newRuleBox = (TextBox) ruleGridView.FooterRow.FindControl("ruleTextBox");
            var newRule = newRuleBox.Text;
            var newRow = dataTable.NewRow();
            newRow["Description"] = newRule;
            if (dataTable.AsEnumerable().Count(row => row["Description"].Equals(newRule)) != 0 || newRule == null || newRule.Length == 0)
            {
                // Row already exists
                newRuleBox.Text = "";
                return;
            }
            if (ruleGridView.Rows.Count == 1 && !ruleGridView.Rows[0].Visible)
            {
                dataTable.Rows.RemoveAt(0);
            }
            else
            {
                ExtractExistingRules(ref dataTable);
            }
            dataTable.Rows.Add(newRow);

            ViewState[RuleTable] = dataTable;

            ruleGridView.DataSource = dataTable;
            ruleGridView.DataBind();

            RestoreRuleValuesToGrid(ref dataTable);
        }

        protected void deleteRuleButton_OnClick(object sender, EventArgs e)
        {
            var button = sender as LinkButton;
            var dataTable = (DataTable)ViewState[RuleTable];
            if (button != null && dataTable != null)
            {
                var row = ((GridViewRow)button.Parent.Parent).RowIndex;
                if (dataTable.Rows.Count == 1) // Last row that is being deleted
                {
                    SetInitialRuleTableSource();
                }
                else
                {
                    ExtractExistingRules(ref dataTable);
                    ViewState[RuleTable] = dataTable;
                    dataTable.Rows.RemoveAt(row);
                    ruleGridView.DataSource = dataTable;
                    ruleGridView.DataBind();
                    RestoreRuleValuesToGrid(ref dataTable);
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

        private void RestoreRuleValuesToGrid(ref DataTable table)
        {
            for (var i = 0; i < ruleGridView.Rows.Count; i++)
            {
                var textBox = (TextBox)ruleGridView.Rows[i].FindControl("ruleDescriptionTextBox");
                textBox.Text = table.Rows[i]["Description"].ToString();
            }
        }

        private void SetInitialRuleTableSource()
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("Description");
            dataTable.Columns.Add("Actions");
            dataTable.Rows.Add(dataTable.NewRow());
            ViewState[RuleTable] = dataTable;
            ruleGridView.DataSource = dataTable;
            ruleGridView.DataBind();
            ruleGridView.Rows[0].Visible = false;
        }

        private void SetInitialRouteTableSource()
        {
            var dataTable = new DataTable();
            // add dataTable columns by name here
            dataTable.Rows.Add(dataTable.NewRow());
            ViewState[RouteTable] = dataTable;
            routeGridView.DataSource = dataTable;
            routeGridView.DataBind();
            routeGridView.Rows[0].Visible = false;
        }

    }
}