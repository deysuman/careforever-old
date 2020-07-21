<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNew.master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="careforever.Login" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.30512.18430, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:Panel ID="panel" runat="server" DefaultButton="btnsignin">
        <div class="container">
            <div class="row">
                <div class="col-sm-6">
                    <div class="well">
                        <h2>New Registration</h2>
                        <p>
                            <strong>Register Account</strong>
                        </p>
                        <p>
                            By creating an account you will be able to shop faster, be up to date on an order's
                            status, and keep track of the orders you have previously made.
                        </p>
                        <asp:Button ID="btnRegister" runat="server" Text="Continue" CssClass="btn btn-primary"
                            OnClick="btnRegister_Click" />
                        <%--<a href="Register.aspx" class="btn btn-primary">Continue</a>--%>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="well">
                        <h2>Member Login</h2>

                        <div class="form-group">
                            <label class="control-label" for="input-email">
                                Account No</label>
                            <asp:TextBox runat="server" ID="txtemail" TabIndex="1" CssClass="form-control" placeholder="Account No"
                                MaxLength="8"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RFVtxtemail" ErrorMessage="Please Enter Account No"
                                CssClass="error_message" SetFocusOnError="true" ControlToValidate="txtemail"
                                ValidationGroup="signup" Display="Dynamic"></asp:RequiredFieldValidator>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                TargetControlID="txtemail" FilterType="Numbers">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </div>
                        <div class="form-group">
                            <label class="control-label" for="input-password">
                                Password</label>
                            <asp:TextBox runat="server" ID="txtpassword" TabIndex="2" TextMode="Password" CssClass="form-control"
                                placeholder="Password" MaxLength="16"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RFVtxtpassword" ErrorMessage="Please Enter Password"
                                CssClass="error_message" SetFocusOnError="true" ControlToValidate="txtpassword"
                                ValidationGroup="signup" Display="Dynamic"></asp:RequiredFieldValidator>
                            <br />
                            <asp:Label ID="lblError" runat="server" Text="Invalid Account Credentials" CssClass="error_message"
                                ForeColor="Red" Visible="False"></asp:Label>
                            <asp:Button runat="server" TabIndex="3" ID="btnsignin" CssClass="btn btn-info" Text="Sign in"
                                ValidationGroup="signup" OnClick="btnsignin_Click" />
                        </div>
                        <div style="margin-top: 10px;">
                            <asp:LinkButton runat="server" PostBackUrl="~/ForgotPassword.aspx" ID="lnkforgotpwd"
                                Text="Forgot Password" OnClick="lnkforgotpwd_Click"></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>


        </div>
    </asp:Panel>

    <div>
        <fieldset>
            <asp:Button ID="btnPopUp" runat="server" Visible="true" Style="display: none;" />
            <ajaxToolkit:ModalPopupExtender ID="Orders_ModelPopUp" runat="server" PopupControlID="OrdersPanel"
                TargetControlID="btnPopUp" BackgroundCssClass="Background">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="OrdersPanel" runat="server" CssClass="Popup" align="center">
                <div class="panelt panel-default">
                    <div class="panel-heading">
                        <strong>Declaration</strong>
                        <%--<asp:Button ID="btnClose" runat="server" Text="Close" Style="margin-left: 30%" />--%>
                        <%--  <button id="btnClose" aria-hidden="true" data-dismiss="modal"  type="button">
                                    &times;</button>--%>
                    </div>
                    <div class="panel-body">
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12 product_content">
                                    <p>
                                        I, Mr/Miss/Mrs <b>
                                            <%--<input type="text" id="textname" runat="server" />--%>
                                            <asp:Label ID="textname" runat="server"> </asp:Label>
                                        </b>Aged <b>
                                            <asp:Label ID="txtages" runat="server"> </asp:Label>
                                        </b>years residing at <b>
                                            <asp:Label ID="textaddress" runat="server"> </asp:Label>
                                        </b>do hereby 
                            declare and affirm as follows:
                                    </p>
                                    <br />
                                    <p>
                                        1) I am voluntarily registering myself as a distributor of Blulife Marketing Pvt Ltd hereinafter
                             referred to as the principal and there was no inducement to join or participate as Distributor.
                                    </p>
                                    <br />
                                    <p>
                                        2) I have read and understood the code of ethics to be complied by Blulife Distributor.
                                    </p>
                                    <br />
                                    <p>
                                        3) I understand that I will be acting as Independent operators and not employee/agent
                             of the principal and shall keep the principal fully indemnified in respect of all my acts and omission. 
                                    </p>
                                    <br />
                                    <p>
                                        4) I understand the marketing plan and undertake to adhere to it.
                                    </p>
                                    <br />
                                    <p>
                                        5) I understand the Prohibited conduct and undertake not to breach the same.
                                    </p>
                                    <br />
                                    <p>
                                        6) I have not paid any deposit to the principal with promise to return
                             in cash/kind/service with or without benefit in form of interest/bonus/profit.
                                    </p>
                                    <br />
                                    <p>
                                        I declare that whatever has been stated above is true to the best of my knowledge,
                             correct and executed after having read/explained in language known to me.      
	                        I have executed this Declaration on the <b>
                                <asp:Label ID="lbldate" runat="server"> </asp:Label>
                            </b>
                                    </p>
                                    <div class="btn-ground" style="float: right">
                                        <%-- <input type="button" id="BtnOK" class="btn btn-primary" value="OK" />--%>
                                        <asp:Button ID="BtnOK" class="btn btn-primary" Text="OK" runat="server" OnClick="Btnok_Click" />
                                        <%--   <input type="button" id="BtnCancel" class="btn btn-primary" value="CANCEL" data-dismiss="modal" />--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </fieldset>
    </div>


    <script>


        $('#BtnOK').on('click', function () {
            alert('clicked')
        })
    </script>
</asp:Content>