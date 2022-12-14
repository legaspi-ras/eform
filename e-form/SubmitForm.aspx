<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SubmitForm.aspx.vb" Inherits="e_form.Completed" %>

<!DOCTYPE html>
<html lang="en">
    <head>
        <meta charset="utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
        <meta name="description" content="" />
        <meta name="author" content="" />
        <title>Online e-Forms</title>
        <link href="https://cdn.jsdelivr.net/npm/simple-datatables@latest/dist/style.css" rel="stylesheet" />
        <link href="css/styles.css" rel="stylesheet" />
        <script src="https://use.fontawesome.com/releases/v6.1.0/js/all.js" crossorigin="anonymous"></script>
        <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.9.1/font/bootstrap-icons.css">

        <script type="text/javascript">
                function SetTargetSelf() {
                         document.forms[0].target = "_self";
                                         }
        </script>
        <script type="text/javascript">
            function SetTargetNew() {
                document.forms[0].target = "_blank";
            }
        </script>  
        <style type="text/css">
            .auto-style2 {
                width: 422px;
                height: 46px;
            }
            .auto-style5 {
                width: 422px;
                height: 26px;
            }
            .auto-style7 {
                width: 130px;
            }
            .auto-style8 {
                width: 130px;
                height: 46px;
            }
            .auto-style9 {
                width: 130px;
                height: 26px;
            }
        </style>
    </head>
    <body class="sb-nav-fixed">
        <nav class="sb-topnav navbar navbar-expand navbar-dark bg-dark">
            <!-- Navbar Brand-->
            <a class="navbar-brand ps-3" href="default.aspx">e-Forms</a>
            <!-- Sidebar Toggle-->
            <button class="btn btn-link btn-sm order-1 order-lg-0 me-4 me-lg-0" id="sidebarToggle" href="#!"><i class="fas fa-bars"></i></button>
            <!-- Navbar Search-->

            <!-- Navbar-->
            <ul class="navbar-nav d-md-inline-block form-inline ms-auto me-0 me-md-3 my-2 my-md-0">
                <li class="nav-item dropdown">
                    <a class="nav-link dropdown-toggle" id="navbarDropdown" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false"><i class="fas fa-user fa-fw"></i></a>
                    <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="navbarDropdown">
                        <li><a class="dropdown-item bi bi-box-arrow-in-right" href="Login.aspx">      Login</a></li>
                    </ul>
                </li>
            </ul> 
        </nav>
        <div id="layoutSidenav">
            <div id="layoutSidenav_nav">
                <nav class="sb-sidenav accordion sb-sidenav-dark" id="sidenavAccordion">
                    <div class="sb-sidenav-menu">
                        <div class="nav">
                            <div class="sb-sidenav-menu-heading">Home</div>
                            <a class="nav-link" href="default.aspx">
                                <div class="sb-nav-link-icon"><i class="fa-solid fa-book"></i></div>
                                List of e-Forms
                            </a>
<%--                            <a class="nav-link" href="RevisedFormClient.aspx">
                                <div class="sb-nav-link-icon"><i class="fa-solid fa-book"></i></div>
                                List of Revised Forms
                            </a>--%>
                            <div class="sb-sidenav-menu-heading">On Process Forms</div>
                            <a class="nav-link collapsed" href="#" data-bs-toggle="collapse" data-bs-target="#collapseLayouts" aria-expanded="false" aria-controls="collapseLayouts">
                                <div class="sb-nav-link-icon"><i class="fas fa-columns"></i></div>
                                Filled Forms
                                <div class="sb-sidenav-collapse-arrow"><i class="fas fa-angle-down"></i></div>
                            </a>
                            <div class="collapse" id="collapseLayouts" aria-labelledby="headingOne" data-bs-parent="#sidenavAccordion">
                                <nav class="sb-sidenav-menu-nested nav">
                                    <a class="nav-link" href="Approval.aspx">For Approval</a>
                                </nav>
                            </div>
                            <a class="nav-link collapsed" href="#" data-bs-toggle="collapse" data-bs-target="#collapsePages" aria-expanded="false" aria-controls="collapsePages">
                                <div class="sb-nav-link-icon"><i class="fas fa-book-open"></i></div>
                                Approved Forms
                                <div class="sb-sidenav-collapse-arrow"><i class="fas fa-angle-down"></i></div>
                            </a>
                            <div class="collapse" id="collapsePages" aria-labelledby="headingTwo" data-bs-parent="#sidenavAccordion">
                                <nav class="sb-sidenav-menu-nested nav accordion" id="sidenavAccordionPages">
                                    <a class="nav-link" href="Approved.aspx">
                                        Approved per Department                                   
                                    </a>
                                </nav>
                            </div>
                            <div class="sb-sidenav-menu-heading">Services</div>
                            <a class="nav-link" href="SubmitForm.aspx">
                                <div class="sb-nav-link-icon"><i class="fa-solid fa-share-from-square"></i></div>
                                Submit Approved Form
                            </a>
                        </div>
                    </div>
                </nav>
            </div>
            <div id="layoutSidenav_content">
                <main>
                   
                    <form runat="server">
                    <div class="container-fluid px-4">
                        <h1 class="mt-4">Submit Approved Form </h1>
                        <ol class="breadcrumb mb-4">
                            <li class="breadcrumb-item active">Submission of approved form per department</li>
                        </ol>
                         <div class="row">
                             <div class="col-lg-6">
                        <div class="card mb-4">
                            <div class="card-header">
                                <i class="fas fa-table me-1"></i>
                                Select accomplished form
                            </div>
                            <div class="card-body">
                               
                            <asp:GridView ID="datatablesSimple" runat="server" class="table table-bordered" AutoGenerateColumns="False" EmptyDataText = "No files uploaded">
                            <Columns>
                                <asp:BoundField DataField="Text" HeaderText="File Name" />
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" Text = "Select" runat="server" CausesValidation="false" CommandName="Select"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField>
                                        <ItemTemplate>    
                                            <asp:LinkButton ID="lnkView" OnClientClick="SetTargetNew();" Text = "View" CommandArgument = '<%# Eval("Value") %>' runat="server" OnClick = "ViewFile"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                                    
                                                                               
                            </div>
                            </div>
                        </div>
                             <div class="col-lg-6">
                        <div class="card mb-4">
                            <div class="card-header">
                                <i class="fas fa-table me-1"></i>
                                Form for submission
                            </div>
                            <div class="card-body">
                         <asp:Label ID="lblsuccess" runat="server" style="color:green" class="bi bi-check-circle-fill" Text=" Submit Successful." Visible="false"></asp:Label>
                          <asp:Label ID="lblAlert1" runat="server" style="color:red" class="bi bi-exclamation-triangle-fill" Text=" File already exist.." Visible="false"></asp:Label>
                      <table style="width: 100%; height: 100%; margin-top: 0px; ">
                        <tr>
                <td class="auto-style8">
                     <asp:Label ID="Label3" runat="server" Text="Filename : "></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="txtFilename" class="form-control" runat="server" Width="450px" Enabled="False"></asp:TextBox>                
                </td>
            </tr>
            <tr>
                <td class="auto-style9">
                     <asp:Label ID="Label1" runat="server" Text="Department Area : "></asp:Label>
                </td>
                <td class="auto-style5">
        
                     <asp:DropDownList ID="ddlDepartment" runat="server" class="form-select" DataTextField="DEPARTMENT" DataValueField="DEPARTMENT" ToolTip="Please select depeartment" Width="200px">
                         <asp:ListItem Value="mis">MIS</asp:ListItem>
                         <asp:ListItem Value="admin">Admin</asp:ListItem>
                         <asp:ListItem Value="security">Security</asp:ListItem>
                         <asp:ListItem Value="purchasing">Purchasing</asp:ListItem>
                         <asp:ListItem Value="production">Production</asp:ListItem>
                         <asp:ListItem Value="ppc">PPC</asp:ListItem>
                         <asp:ListItem Value="qa">Quality Assurance</asp:ListItem>
                         <asp:ListItem Value="ee">Equipment </asp:ListItem>
                         <asp:ListItem Value="store">Store</asp:ListItem>
                         <asp:ListItem Value="hrd">Human Resource</asp:ListItem>
                         <asp:ListItem Value="finance">Finance</asp:ListItem>
                         <asp:ListItem Value="logistics">Logistic</asp:ListItem>
                         <asp:ListItem Value="training">Training</asp:ListItem>
                         <asp:ListItem Value="general">General</asp:ListItem>
                     </asp:DropDownList>
                </td>
            </tr>

            <tr>   <td class="auto-style7">

                   </td>
                <td style="height: 66px; text-align: left; width: 400px;">
                     <asp:Button ID="btnSubmit" class="btn btn-success" runat="server" Text="Submit" Width="127px" Enabled="False" />
                </td>
            </tr>


        </table>

                            </div>
                            </div>
                        </div>
                             </div>
                    </div>
                    </form>
             
                </main>
                <footer class="py-4 bg-light mt-auto">
                    <div class="container-fluid px-4">
                        <div class="d-flex align-items-center justify-content-between small">
                            <div class="text-muted">Copyright &copy; Your Website 2022</div>
                            <div>
                                <a href="#">Privacy Policy</a>
                                &middot;
                                <a href="#">Terms &amp; Conditions</a>
                            </div>
                        </div>
                    </div>
                </footer>
            </div>
        </div>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" crossorigin="anonymous"></script>
        <script src="js/scripts.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.8.0/Chart.min.js" crossorigin="anonymous"></script>
        <script src="assets/demo/chart-area-demo.js"></script>
        <script src="assets/demo/chart-bar-demo.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/simple-datatables@latest" crossorigin="anonymous"></script>
        <script src="js/datatables-simple-demo.js"></script>
    </body>
</html>
