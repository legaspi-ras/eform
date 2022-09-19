<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RevisedForm.aspx.vb" Inherits="e_form.RevisedForm" %>

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
    </head>
    <body class="sb-nav-fixed">
        <nav class="sb-topnav navbar navbar-expand navbar-dark bg-dark">
            <!-- Navbar Brand-->
            <a class="navbar-brand ps-3" href="AdminDashboard.aspx">e-Forms</a>
            <!-- Sidebar Toggle-->
            <button class="btn btn-link btn-sm order-1 order-lg-0 me-4 me-lg-0" id="sidebarToggle" href="#!"><i class="fas fa-bars"></i></button>
            <!-- Navbar-->
            <ul class="navbar-nav d-md-inline-block form-inline ms-auto me-0 me-md-3 my-2 my-md-0">
                <li class="nav-item dropdown">
                    <a class="nav-link dropdown-toggle" id="navbarDropdown" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false"><i class="fas fa-user fa-fw"></i></a>
                    <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="navbarDropdown">
                        <li><a class="dropdown-item bi bi-box-arrow-in-left" href="Login.aspx">      Logout</a></li>
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
                            <a class="nav-link" href="AdminDashboard.aspx">
                                <div class="sb-nav-link-icon"><i class="fa-solid fa-book"></i></div>
                                List of Templates
                            </a>
                            <a class="nav-link" href="RevisedForm.aspx">
                                <div class="sb-nav-link-icon"><i class="fa-solid fa-book"></i></div>
                                List of Revised Forms
                            </a>
                            <div class="sb-sidenav-menu-heading">Form Templates</div>
                            <a class="nav-link collapsed" href="#" data-bs-toggle="collapse" data-bs-target="#collapseLayouts" aria-expanded="false" aria-controls="collapseLayouts">
                                <div class="sb-nav-link-icon"><i class="fa-solid fa-wrench"></i></div>
                                Services
                                <div class="sb-sidenav-collapse-arrow"><i class="fas fa-angle-down"></i></div>
                            </a>
                            <div class="collapse" id="collapseLayouts" aria-labelledby="headingOne" data-bs-parent="#sidenavAccordion">
                                <nav class="sb-sidenav-menu-nested nav">
                                  <a class="nav-link bi bi-file-earmark-plus" href="AddNewForm.aspx"> Add</a>
                                  <a class="nav-link bi bi-pencil-square" href="UpdateForm.aspx"> Update</a>
                                  <a class="nav-link bi bi-file-earmark-x" href="DeleteForm.aspx"> Delete</a>
                                </nav>
                            </div>
                            <div class="sb-sidenav-menu-heading">Approved Forms</div>
                            <a class="nav-link collapsed" href="#" data-bs-toggle="collapse" data-bs-target="#collapsePages" aria-expanded="false" aria-controls="collapsePages">
                                <div class="sb-nav-link-icon"><i class="fa-solid fa-wrench"></i></div>
                                Services
                                <div class="sb-sidenav-collapse-arrow"><i class="fas fa-angle-down"></i></div>
                            </a>
                            <div class="collapse" id="collapsePages" aria-labelledby="headingTwo" data-bs-parent="#sidenavAccordion">
                                <nav class="sb-sidenav-menu-nested nav accordion" id="sidenavAccordionPages">
                                    <a class="nav-link collapsed bi bi-file-earmark-plus " href="AddRevisedForm.aspx" >
                                        Add                                  
                                    </a>
                                    <a class="nav-link collapsed bi bi-pencil-square " href="UpdateFilledForm.aspx" >
                                        Update                                   
                                    </a>
                                    <a class="nav-link collapsed bi bi-file-earmark-x " href="#" >
                                        Delete                                   
                                    </a>
                                </nav>
                            </div>
                        </div>
                    </div>
                    <div class="sb-sidenav-footer">
                        <div class="small">Logged in as :</div>
                        <i class="bi bi-person-badge"></i> &nbsp Administrator
                    </div>
                </nav>
            </div>
            <div id="layoutSidenav_content">
                <main>
                    <div class="container-fluid px-4">
                        <h1 class="mt-4">Revised Forms</h1>
                        <ol class="breadcrumb mb-4">
                            <li class="breadcrumb-item active">List of Revised forms</li>
                        </ol>
                        
                        <div class="card mb-4">
                            <div class="card-header">
                                <i class="fas fa-table me-1"></i>
                                Choose e-forms available below
                            </div>
                            <div class="card-body">
                                <form runat="server">
                                     <asp:GridView ID="datatablesSimple" runat="server" class="table table-bordered" AutoGenerateColumns="False" EmptyDataText = "No files uploaded">
                                         <Columns>
                                             <asp:TemplateField HeaderText="Specifications" SortExpression="formTitle">
                                                 <ItemTemplate>
                                                     <asp:Label ID="Label1" runat="server" Text='<%# Bind("formControlnum") %>' Font-Bold="True"></asp:Label> - 
                                                     <asp:Label ID="Label2" runat="server" Text='<%# Bind("formTitle") %>' Font-Bold="True"></asp:Label></br>    
                                                     <asp:Label ID="Label5" runat="server" Text="Department - " Font-Size="XX-Small"></asp:Label>
                                                     <asp:Label ID="Label4" runat="server" Text='<%# Bind("formDepartment") %>' Font-Bold="True" Font-Size="XX-Small"></asp:Label> | 
                                                     <asp:Label ID="Label6" runat="server" Text="Originator - " Font-Size="XX-Small"></asp:Label>
                                                     <asp:Label ID="Label3" runat="server" Text='<%# Bind("formOriginator") %>' Font-Bold="True" Font-Size="XX-Small"></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             <asp:BoundField DataField="formRevisionnum" HeaderText="Revision No." SortExpression="formRevisionum" />
                                             <asp:BoundField DataField="formRevisiondate" HeaderText="Revision Date" SortExpression="formRevisiondate" />                      
                                             <asp:TemplateField>
                                                 <ItemTemplate>
                                                     <asp:LinkButton ID="lnkDownload" CommandName="Select" OnClientClick="SetTargetSelf();" Text = "View" runat="server" CommandArgument = '<%# Eval("formControlnum") %>' OnClick="lnkDownload_Click">Download</asp:LinkButton>
                                                 </ItemTemplate>
                                             </asp:TemplateField>

                                             <asp:TemplateField>
                                                 <ItemTemplate>
                                                     <asp:LinkButton ID="lnkView" CommandName="Select" OnClientClick="SetTargetNew();" Text = "View" runat="server" CommandArgument = '<%# Eval("formControlnum") %>' OnClick="lnkView_Click">View</asp:LinkButton>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                         </Columns>
                                     </asp:GridView>

                                </form>                                                       
                            </div>
                        </div>
                    </div>
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