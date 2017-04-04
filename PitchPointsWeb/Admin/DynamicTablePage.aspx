<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DynamicTablePage.aspx.cs" Inherits="PitchPointsWeb.Admin.DynamicTablePage" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function() {
            //$("#addRuleButton").click(function () {
            //    console.log("click");
            //    var ruleDescription = $('#ruleTextBox')[0].value;
            //    console.log(ruleDescription);
            //    var rule = "<label name='rules' id='rule' type='text' value='" + ruleDescription + "' />";
            //    var newRow = "<tr><td>" + ruleDescription + "</td><td>" + rule + "</td></tr>";
            //    console.log($('#ruleTable'));
            //    $('#ruleTable').append(newRow);
            //    return false;
            //});
        });
    </script>
    <asp:UpdatePanel runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:GridView runat="server"  ID="ruleGridView" ShowFooter="True" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:TextBox runat="server" placeholder="Rule description" ID="ruleDescriptionTextBox" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox runat="server" placeholder="Rule description" ID="ruleTextBox" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actions">
                        <FooterTemplate>
                            <asp:LinkButton runat="server" Text="Add" OnClick="addRuleButton_OnClick"/>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" Text="Delete" OnClick="deleteRuleButton_OnClick" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    
    
    
    <%--<asp:Table runat="server" ID="ruleTable" ClientIDMode="Static">
        <asp:TableHeaderRow runat="server">
            <asp:TableCell runat="server" Text="Actions" />
            <asp:TableCell runat="server" Text="Description" />
        </asp:TableHeaderRow>
        <asp:TableFooterRow runat="server">
            <asp:TableCell runat="server">
                <asp:LinkButton ID="addRuleButton" runat="server" Text="Add" ClientIDMode="Static" />
            </asp:TableCell>
            <asp:TableCell runat="server">
                <asp:TextBox ID="ruleTextBox" runat="server" ClientIDMode="Static" placeholder="Rule description" />
            </asp:TableCell>
        </asp:TableFooterRow>
    </asp:Table>--%>
</asp:Content>
