<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs"
    Inherits="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<asp:PlaceHolder ID="PlaceHolder1" runat="server">
    
</asp:PlaceHolder>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblHeader" runat="server" Text="Phone Book" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblName" runat="server" Text="Name :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RfvtName" runat="server" ErrorMessage="* Required"
                            ForeColor="#FF3300" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMobile" runat="server" Text="Mobile"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMobile" runat="server" MaxLength="10"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="RxvMobile" runat="server" ErrorMessage="Invalid Mobile Number"
                            ForeColor="#FF3300" ValidationExpression="^([0]{1})([0-9]{9})$" ControlToValidate="txtMobile"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ErrorMessage="* Required"
                            ForeColor="#FF3300" ControlToValidate="txtMobile"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEmail" runat="server" Text="E-Mail :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <br />
                        <asp:RegularExpressionValidator ID="RxvEmail" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="Invalid E-mail" ForeColor="#FF3300" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPhone" runat="server" Text="Phone :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPhone" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnClear" runat="server" CausesValidation="False" OnClick="btnClear_Click"
                            Text="Clear" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
    
</body>

</html>