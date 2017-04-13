﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PitchPointsWeb.Admin
{
    public partial class CreateCompetition : Page
    {

        private const string RuleTable = "RuleTable";
        private const string RouteTable = "RouteTable";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetInitialRuleTableSource();
                SetInitialRouteTableSource();
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {

        }

        protected void deleteRouteButton_OnClick(object sender, EventArgs e)
        {
            var button = sender as LinkButton;
            var dataTable = (DataTable)ViewState[RouteTable];
            if (button != null && dataTable != null)
            {
                var row = ((GridViewRow)button.Parent.Parent).RowIndex;
                if (dataTable.Rows.Count == 1) // Last row that is being deleted
                {
                    SetInitialRouteTableSource();
                }
                else
                {
                    ExtractExistingRoutes(ref dataTable);
                    ViewState[RouteTable] = dataTable;
                    dataTable.Rows.RemoveAt(row);
                    routeGridView.DataSource = dataTable;
                    routeGridView.DataBind();
                    RestoreRouteValuesToGrid(ref dataTable);
                }
            }
        }

        protected void addRouteButton_OnClick(object sender, EventArgs e)
        {
            if (ViewState[RouteTable] == null) return;
            var dataTable = (DataTable)ViewState[RouteTable];
            var newIDBox = (TextBox)routeGridView.FooterRow.FindControl("routeID");
            var newRouteID = Convert.ToInt32(newIDBox.Text);

            var newGradeBox = (DropDownList)routeGridView.FooterRow.FindControl("gradeInput");
            var newGrade = newGradeBox.SelectedItem.Value.ToString();
            var newRow = dataTable.NewRow();

            newRow["ID"] = newRouteID;
            newRow["Grade"] = newGrade;
            
            if (routeGridView.Rows.Count >= 1)
            {
                newRow["ID"] = routeGridView.Rows.Count;
                newRow["Grade"] = newGradeBox.SelectedItem.Value.ToString();
            }
            if (routeGridView.Rows.Count == 1 && !routeGridView.Rows[0].Visible)
            {
                dataTable.Rows.RemoveAt(0);
            }
            else
            {
                ExtractExistingRoutes(ref dataTable);
            }
            dataTable.Rows.Add(newRow);

            ViewState[RouteTable] = dataTable;

            routeGridView.DataSource = dataTable;
            routeGridView.DataBind();

            RestoreRouteValuesToGrid(ref dataTable);
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

        private void ExtractExistingRoutes(ref DataTable table)
        {
            for (var i = 0; i < table.Rows.Count; i++) // Load form into datatable
            {
                var IDBox = (TextBox)routeGridView.Rows[i].FindControl("routeIDInput");
                table.Rows[i]["ID"] = IDBox.Text;
                var categoryBox = (DropDownList)routeGridView.Rows[i].FindControl("categoryInput");
                table.Rows[i]["Category"] = categoryBox.SelectedValue.ToString();
                var gradeBox = (DropDownList)routeGridView.Rows[i].FindControl("gradeInput");
                table.Rows[i]["Grade"] = gradeBox.SelectedItem.Value.ToString();
            }
        }

        private void RestoreRouteValuesToGrid(ref DataTable table)
        {
            for (var i = 0; i < routeGridView.Rows.Count; i++)
            {
                var textBox = (TextBox)routeGridView.Rows[i].FindControl("routeIDInput");
                textBox.Text = Convert.ToString(i+1);
                var categoryBox = (DropDownList)routeGridView.Rows[i].FindControl("categoryInput");
                categoryBox.SelectedValue = table.Rows[i]["Category"].ToString();
                var gradeBox = (DropDownList)routeGridView.Rows[i].FindControl("gradeInput");
                gradeBox.SelectedItem.Value = table.Rows[i]["Grade"].ToString();
            }
            var footerID = (TextBox)routeGridView.FooterRow.FindControl("routeID");
            footerID.Text = Convert.ToString(routeGridView.Rows.Count + 1);
            var footerPoints = (TextBox)routeGridView.FooterRow.FindControl("routePointsValue");
            var footerGrade = (DropDownList)routeGridView.FooterRow.FindControl("gradeInput");
            footerPoints.Text = Convert.ToString(footerGrade.SelectedItem.Value);
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
            dataTable.Columns.Add("Actions");
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Category");
            dataTable.Columns.Add("Grade");
            dataTable.Columns.Add("Points");
            dataTable.Rows.Add(dataTable.NewRow());
            ViewState[RouteTable] = dataTable;
            routeGridView.DataSource = dataTable;
            routeGridView.DataBind();
            routeGridView.Rows[0].Visible = false;
        }
    }
}