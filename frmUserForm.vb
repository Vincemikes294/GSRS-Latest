Imports System.Data.OleDb
Imports System.IO
Imports Continuous_Slope.GSRS.Net.Common.Scripts

Public Class frmUserForm
    Dim FirstName As String
    Dim LastName As String
    Dim Username As String
    Dim Password As String
    Dim Status As String

    Dim lview As ListViewItem
    Dim lview2 As ListViewItem.ListViewSubItem
    Dim row, col As Integer

    'Variables to Logged on User - 05.27.21
    Public LoggedOnUser As String = ""

    Private FNameChanged As Boolean = False
    Private LNameChanged As Boolean = False
    Private PwdChanged As Boolean = False
    Private StatusChanged As Boolean = False
    Private UserNameChanged As Boolean = False

    Private Sub butAdd_Click(sender As Object, e As EventArgs) Handles butAdd.Click
        Try
            Dim myHelper As New HelperClass
            Dim AddUser As Boolean = False

            AddUser = ValidateFields("ADD")

            If AddUser Then
                'Refresh the ListView with latest data from the User Details table
                If myHelper.InsertUserDetails(FirstName, LastName, Username, Password, Status) Then
                    lstViewUsers.View = View.Details

                    lstViewUsers.Clear()

                    lstViewUsers.Columns.Add("First Name", 100, HorizontalAlignment.Center)
                    lstViewUsers.Columns.Add("Last Name", 100, HorizontalAlignment.Center)
                    lstViewUsers.Columns.Add("Username", 100, HorizontalAlignment.Center)
                    lstViewUsers.Columns.Add("Password", 100, HorizontalAlignment.Center)
                    lstViewUsers.Columns.Add("Status", 100, HorizontalAlignment.Center)
                    Dim UserDetails As DataTable

                    UserDetails = myHelper.GetUserDetails()
                    Dim UserPwd As String

                    For Each myRow In UserDetails.Rows
                        lstViewUsers.Items.Add(myRow.Item(0))
                        lstViewUsers.Items(lstViewUsers.Items.Count - 1).SubItems.Add(myRow.Item(1))
                        lstViewUsers.Items(lstViewUsers.Items.Count - 1).SubItems.Add(myRow.Item(2))
                        UserPwd = myHelper.Decrypt(myRow.Item(3).ToString)
                        lstViewUsers.Items(lstViewUsers.Items.Count - 1).SubItems.Add(UserPwd)
                        lstViewUsers.Items(lstViewUsers.Items.Count - 1).SubItems.Add(myRow.Item(4))
                    Next

                    'lstViewUsers.View = View.Details
                    'lstViewUsers.Items.Add(New ListViewItem(New String() {FirstName, LastName, Username, Password, Status}))
                    txtFirstName.Text = ""
                    txtLastName.Text = ""
                    txtUsername.Text = ""
                    txtPassword.Text = ""
                    cboStatus.Text = ""
                Else
                    MessageBox.Show("Failed to Add User")
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub butDelete_Click(sender As Object, e As EventArgs) Handles butDelete.Click
        Try
            'Not needed anymore

            For Each item As ListViewItem In lstViewUsers.SelectedItems
                'item.Remove()
                If MessageBox.Show("Please confirm to Delete the User", "Confirmation", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    Dim myHelper As New HelperClass
                    Dim UserName As String = ""
                    UserName = item.SubItems(2).Text
                    If myHelper.DeleteUser(UserName) Then
                        Dim UserDetails As DataTable

                        lstViewUsers.View = View.Details

                        lstViewUsers.Clear()

                        lstViewUsers.Columns.Add("First Name", 100, HorizontalAlignment.Center)
                        lstViewUsers.Columns.Add("Last Name", 100, HorizontalAlignment.Center)
                        lstViewUsers.Columns.Add("Username", 100, HorizontalAlignment.Center)
                        lstViewUsers.Columns.Add("Password", 100, HorizontalAlignment.Center)
                        lstViewUsers.Columns.Add("Status", 100, HorizontalAlignment.Center)

                        UserDetails = myHelper.GetUserDetails()
                        Dim UserPwd As String

                        For Each myRow In UserDetails.Rows
                            lstViewUsers.Items.Add(myRow.Item(0))
                            lstViewUsers.Items(lstViewUsers.Items.Count - 1).SubItems.Add(myRow.Item(1))
                            lstViewUsers.Items(lstViewUsers.Items.Count - 1).SubItems.Add(myRow.Item(2))
                            UserPwd = myHelper.Decrypt(myRow.Item(3).ToString)
                            lstViewUsers.Items(lstViewUsers.Items.Count - 1).SubItems.Add(UserPwd)
                            lstViewUsers.Items(lstViewUsers.Items.Count - 1).SubItems.Add(myRow.Item(4))
                        Next

                        txtFirstName.Text = ""
                        txtLastName.Text = ""
                        txtUsername.Text = ""
                        txtPassword.Text = ""
                        cboStatus.Text = ""

                        FNameChanged = False
                        LNameChanged = False
                        PwdChanged = False
                        StatusChanged = False
                        UserNameChanged = False
                        txtUsername.ReadOnly = False


                    End If


                End If
            Next





            'butLoad.Enabled = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub
    Private Sub frmUserForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            'Not needed anymore
            'butLoad.Enabled = True
            'butLoad.PerformClick()
            lstViewUsers.View = View.Details
            lstViewUsers.Columns.Add("First Name", 100, HorizontalAlignment.Center)
            lstViewUsers.Columns.Add("Last Name", 100, HorizontalAlignment.Center)
            lstViewUsers.Columns.Add("Username", 100, HorizontalAlignment.Center)
            lstViewUsers.Columns.Add("Password", 100, HorizontalAlignment.Center)
            lstViewUsers.Columns.Add("Status", 100, HorizontalAlignment.Center)
            'lstViewUsers.Columns.Add(vbCrLf)
            Dim myHelper As New HelperClass
            Dim UserDetails As DataTable
            Dim UserPwd As String
            UserDetails = myHelper.GetUserDetails()

            For Each myRow In UserDetails.Rows
                lstViewUsers.Items.Add(myRow.Item(0))
                lstViewUsers.Items(lstViewUsers.Items.Count - 1).SubItems.Add(myRow.Item(1))
                lstViewUsers.Items(lstViewUsers.Items.Count - 1).SubItems.Add(myRow.Item(2))
                UserPwd = myHelper.Decrypt(myRow.Item(3).ToString)
                lstViewUsers.Items(lstViewUsers.Items.Count - 1).SubItems.Add(UserPwd)
                lstViewUsers.Items(lstViewUsers.Items.Count - 1).SubItems.Add(myRow.Item(4))
            Next

            'butLoad.Enabled = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub butReset_Click(sender As Object, e As EventArgs) Handles butReset.Click
        txtFirstName.Text = ""
        txtLastName.Text = ""
        txtUsername.Text = ""
        txtPassword.Text = ""
        cboStatus.Text = ""

        txtUsername.ReadOnly = False
    End Sub

    Private Sub butUpdate_Click(sender As Object, e As EventArgs) Handles butUpdate.Click
        Try
            '1. Make sure only 1 row is selected
            '2. Populate the data from the selected row into respective fields
            '3. Update the record in the table if there is change in data
            Dim UpdateRecord As Boolean = False

            UpdateRecord = ValidateFields("UPDATE")

            If UpdateRecord Then
                'Reset the flag 
                UpdateRecord = False
                If FirstName <> txtFirstName.Text And FNameChanged = True Then
                    UpdateRecord = True
                ElseIf LastName <> txtLastName.Text And LNameChanged = True Then
                    UpdateRecord = True
                ElseIf PwdChanged = True Then
                    UpdateRecord = True
                ElseIf Status <> cboStatus.Text And StatusChanged = True Then
                    UpdateRecord = True
                End If

                If UpdateRecord Then
                    'Call UpdateUser Details
                    'item.Remove()
                    If MessageBox.Show("Please confirm to Update the User", "Confirmation", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        Dim myHelper As New HelperClass


                        If myHelper.UpdateUserDetails(FirstName, LastName, Username, Password, Status) Then
                            Dim UserDetails As DataTable

                            lstViewUsers.View = View.Details

                            lstViewUsers.Clear()

                            lstViewUsers.Columns.Add("First Name", 100, HorizontalAlignment.Center)
                            lstViewUsers.Columns.Add("Last Name", 100, HorizontalAlignment.Center)
                            lstViewUsers.Columns.Add("Username", 100, HorizontalAlignment.Center)
                            lstViewUsers.Columns.Add("Password", 100, HorizontalAlignment.Center)
                            lstViewUsers.Columns.Add("Status", 100, HorizontalAlignment.Center)


                            UserDetails = myHelper.GetUserDetails()
                            Dim UserPwd As String

                            For Each myRow In UserDetails.Rows
                                lstViewUsers.Items.Add(myRow.Item(0))
                                lstViewUsers.Items(lstViewUsers.Items.Count - 1).SubItems.Add(myRow.Item(1))
                                lstViewUsers.Items(lstViewUsers.Items.Count - 1).SubItems.Add(myRow.Item(2))
                                UserPwd = myHelper.Decrypt(myRow.Item(3).ToString)
                                lstViewUsers.Items(lstViewUsers.Items.Count - 1).SubItems.Add(UserPwd)
                                lstViewUsers.Items(lstViewUsers.Items.Count - 1).SubItems.Add(myRow.Item(4))
                            Next

                            txtFirstName.Text = ""
                            txtLastName.Text = ""
                            txtUsername.Text = ""
                            txtPassword.Text = ""
                            cboStatus.Text = ""

                            FNameChanged = False
                            LNameChanged = False
                            PwdChanged = False
                            StatusChanged = False
                            txtUsername.ReadOnly = False

                        End If


                    End If
                Else
                    MessageBox.Show("User Record not changed")
                End If
            End If



        Catch ex As Exception

        End Try
    End Sub

    Private Sub lstViewUsers_MouseClick(sender As Object, e As MouseEventArgs) Handles lstViewUsers.MouseClick


        Try
            '1. Allow only 1 row to be selected
            '2. Populate the data from the selected row into respective fields
            If lstViewUsers.SelectedItems.Count > 1 Then
                MessageBox.Show("Please select only one record to update")
            Else
                For Each UserRecord As ListViewItem In lstViewUsers.SelectedItems
                    If UserRecord.SubItems(2).Text <> "admin" Then
                        txtFirstName.Text = UserRecord.SubItems(0).Text
                        txtLastName.Text = UserRecord.SubItems(1).Text
                        txtUsername.Text = UserRecord.SubItems(2).Text
                        txtPassword.Text = UserRecord.SubItems(3).Text
                        cboStatus.Text = UserRecord.SubItems(4).Text
                        butDelete.Enabled = True
                        butUpdate.Enabled = True

                        txtUsername.ReadOnly = True
                    Else
                        txtFirstName.Text = ""
                        txtLastName.Text = ""
                        txtUsername.Text = ""
                        txtPassword.Text = ""
                        cboStatus.Text = ""

                        butDelete.Enabled = False
                        butUpdate.Enabled = False
                        txtUsername.ReadOnly = False
                    End If


                    FirstName = UserRecord.SubItems(0).Text
                    LastName = UserRecord.SubItems(1).Text
                    Username = UserRecord.SubItems(2).Text
                    Status = UserRecord.SubItems(3).Text

                Next
            End If


        Catch ex As Exception

        End Try

    End Sub

    Private Sub frmUserForm_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        frmLogin.butAddUser.Enabled = True
    End Sub

    Private Sub txtFirstName_TextChanged(sender As Object, e As EventArgs) Handles txtFirstName.TextChanged
        FNameChanged = True
    End Sub

    Private Sub txtLastName_TextChanged(sender As Object, e As EventArgs) Handles txtLastName.TextChanged
        LNameChanged = True
    End Sub

    Private Sub txtPassword_TextChanged(sender As Object, e As EventArgs) Handles txtPassword.TextChanged
        PwdChanged = True
    End Sub


    Private Sub cboStatus_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboStatus.SelectedValueChanged
        StatusChanged = True
    End Sub

    Private Sub txtUsername_TextChanged(sender As Object, e As EventArgs) Handles txtUsername.TextChanged
        UserNameChanged = True
    End Sub

    Private Function ValidateFields(UserAction As String) As Boolean

        Dim ValidFields As Boolean = True
        Dim myHelper As New HelperClass

        FirstName = txtFirstName.Text
        LastName = txtLastName.Text
        Username = txtUsername.Text
        Password = txtPassword.Text
        Status = cboStatus.Text
        If FirstName = "" Or Not myHelper.IsValidText(FirstName) Then
            MessageBox.Show("Please enter valid First Name")
            ValidFields = False
            txtFirstName.Select()
        ElseIf LastName = "" Or Not myHelper.IsValidText(LastName) Then
            MessageBox.Show("Please enter valid Last Name")
            ValidFields = False
            txtLastName.Select()
        ElseIf Username = "" Or Not myHelper.IsValidText(Username) Then
            MessageBox.Show("Please enter valid User Name")
            ValidFields = False
            txtUsername.Select()
        ElseIf Password = "" Or Not myHelper.IsValidText(Password) And ((UserAction = "ADD") Or (UserAction = "UPDATE" And Not PwdChanged)) Then
            MessageBox.Show("Please enter valid User Password")
            ValidFields = False
            txtPassword.Select()
        ElseIf Status = "" Then
            MessageBox.Show("Please select User Status")
            ValidFields = False
            cboStatus.Select()
        End If

        Return ValidFields
    End Function


End Class

