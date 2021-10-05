<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUserForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtFirstName = New System.Windows.Forms.TextBox()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.butAdd = New System.Windows.Forms.Button()
        Me.butDelete = New System.Windows.Forms.Button()
        Me.txtLastName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.lstViewUsers = New System.Windows.Forms.ListView()
        Me.VisualStyler1 = New SkinSoft.VisualStyler.VisualStyler(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.butUpdate = New System.Windows.Forms.Button()
        Me.butReset = New System.Windows.Forms.Button()
        CType(Me.VisualStyler1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(26, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "First Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(26, 82)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Username"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(26, 116)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 17)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Password"
        '
        'txtFirstName
        '
        Me.txtFirstName.Location = New System.Drawing.Point(210, 14)
        Me.txtFirstName.MaxLength = 1000
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.Size = New System.Drawing.Size(217, 22)
        Me.txtFirstName.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.txtFirstName, "Enter First Name of user")
        '
        'txtUsername
        '
        Me.txtUsername.Location = New System.Drawing.Point(210, 79)
        Me.txtUsername.MaxLength = 1000
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(217, 22)
        Me.txtUsername.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtUsername, "Enter username of user")
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(210, 113)
        Me.txtPassword.MaxLength = 1000
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(217, 22)
        Me.txtPassword.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.txtPassword, "Enter password of user")
        '
        'butAdd
        '
        Me.butAdd.Location = New System.Drawing.Point(83, 209)
        Me.butAdd.Name = "butAdd"
        Me.butAdd.Size = New System.Drawing.Size(76, 27)
        Me.butAdd.TabIndex = 7
        Me.butAdd.Text = "Add"
        Me.ToolTip1.SetToolTip(Me.butAdd, "Add User data")
        Me.butAdd.UseVisualStyleBackColor = True
        '
        'butDelete
        '
        Me.butDelete.Location = New System.Drawing.Point(261, 209)
        Me.butDelete.Name = "butDelete"
        Me.butDelete.Size = New System.Drawing.Size(76, 27)
        Me.butDelete.TabIndex = 8
        Me.butDelete.Text = "Delete"
        Me.ToolTip1.SetToolTip(Me.butDelete, "Delete User Data")
        Me.butDelete.UseVisualStyleBackColor = True
        '
        'txtLastName
        '
        Me.txtLastName.Location = New System.Drawing.Point(210, 45)
        Me.txtLastName.MaxLength = 1000
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.Size = New System.Drawing.Size(217, 22)
        Me.txtLastName.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.txtLastName, "Enter Last Name of User")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(26, 50)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 17)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Last Name"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(26, 150)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 17)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Status"
        '
        'cboStatus
        '
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Items.AddRange(New Object() {"Administrator", "Regular User"})
        Me.cboStatus.Location = New System.Drawing.Point(210, 149)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(217, 24)
        Me.cboStatus.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.cboStatus, "Select status of user")
        '
        'lstViewUsers
        '
        Me.lstViewUsers.HideSelection = False
        Me.lstViewUsers.Location = New System.Drawing.Point(15, 257)
        Me.lstViewUsers.Name = "lstViewUsers"
        Me.lstViewUsers.Size = New System.Drawing.Size(412, 188)
        Me.lstViewUsers.TabIndex = 15
        Me.lstViewUsers.UseCompatibleStateImageBehavior = False
        '
        'VisualStyler1
        '
        Me.VisualStyler1.HostForm = Me
        '
        'butUpdate
        '
        Me.butUpdate.Location = New System.Drawing.Point(172, 209)
        Me.butUpdate.Name = "butUpdate"
        Me.butUpdate.Size = New System.Drawing.Size(76, 27)
        Me.butUpdate.TabIndex = 16
        Me.butUpdate.Text = "Update"
        Me.ToolTip1.SetToolTip(Me.butUpdate, "Update User data")
        Me.butUpdate.UseVisualStyleBackColor = True
        '
        'butReset
        '
        Me.butReset.Location = New System.Drawing.Point(350, 209)
        Me.butReset.Name = "butReset"
        Me.butReset.Size = New System.Drawing.Size(76, 27)
        Me.butReset.TabIndex = 17
        Me.butReset.Text = "Reset Form"
        Me.ToolTip1.SetToolTip(Me.butReset, "Add User data")
        Me.butReset.UseVisualStyleBackColor = True
        '
        'frmUserForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(439, 457)
        Me.Controls.Add(Me.butReset)
        Me.Controls.Add(Me.butUpdate)
        Me.Controls.Add(Me.lstViewUsers)
        Me.Controls.Add(Me.cboStatus)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtLastName)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.butDelete)
        Me.Controls.Add(Me.butAdd)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtUsername)
        Me.Controls.Add(Me.txtFirstName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmUserForm"
        Me.Text = "              User  Registration "
        CType(Me.VisualStyler1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtFirstName As TextBox
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents butAdd As Button
    Friend WithEvents butDelete As Button
    Friend WithEvents txtLastName As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents cboStatus As ComboBox
    Friend WithEvents lstViewUsers As ListView
    Friend WithEvents ToolTip1 As ToolTip
    Private WithEvents VisualStyler1 As SkinSoft.VisualStyler.VisualStyler
    Friend WithEvents butReset As Button
    Friend WithEvents butUpdate As Button
End Class
