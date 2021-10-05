<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmLogin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogin))
        Me.butlogin = New System.Windows.Forms.Button()
        Me.txtusername = New System.Windows.Forms.TextBox()
        Me.txtpassword = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.butReset = New System.Windows.Forms.Button()
        Me.butAddUser = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.VisualStyler1 = New SkinSoft.VisualStyler.VisualStyler(Me.components)
        CType(Me.VisualStyler1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'butlogin
        '
        Me.butlogin.Location = New System.Drawing.Point(15, 117)
        Me.butlogin.Name = "butlogin"
        Me.butlogin.Size = New System.Drawing.Size(78, 30)
        Me.butlogin.TabIndex = 0
        Me.butlogin.Text = "login"
        Me.ToolTip1.SetToolTip(Me.butlogin, "Click to login")
        Me.butlogin.UseVisualStyleBackColor = True
        '
        'txtusername
        '
        Me.txtusername.Location = New System.Drawing.Point(153, 25)
        Me.txtusername.Name = "txtusername"
        Me.txtusername.Size = New System.Drawing.Size(118, 22)
        Me.txtusername.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtusername, "Enter username")
        '
        'txtpassword
        '
        Me.txtpassword.Location = New System.Drawing.Point(153, 62)
        Me.txtpassword.Name = "txtpassword"
        Me.txtpassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtpassword.Size = New System.Drawing.Size(118, 22)
        Me.txtpassword.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.txtpassword, "Enter password")
        Me.txtpassword.UseSystemPasswordChar = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 17)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Username"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 17)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Password"
        '
        'butReset
        '
        Me.butReset.Location = New System.Drawing.Point(144, 117)
        Me.butReset.Name = "butReset"
        Me.butReset.Size = New System.Drawing.Size(77, 30)
        Me.butReset.TabIndex = 5
        Me.butReset.Text = "Reset"
        Me.ToolTip1.SetToolTip(Me.butReset, "Click to change username and password")
        Me.butReset.UseVisualStyleBackColor = True
        '
        'butAddUser
        '
        Me.butAddUser.Location = New System.Drawing.Point(264, 117)
        Me.butAddUser.Name = "butAddUser"
        Me.butAddUser.Size = New System.Drawing.Size(84, 30)
        Me.butAddUser.TabIndex = 6
        Me.butAddUser.Text = "Add User"
        Me.butAddUser.UseVisualStyleBackColor = True
        '
        'ToolTip1
        '
        '
        'VisualStyler1
        '
        Me.VisualStyler1.HostForm = Me
        Me.VisualStyler1.License = CType(resources.GetObject("VisualStyler1.License"), SkinSoft.VisualStyler.Licensing.VisualStylerLicense)
        Me.VisualStyler1.LoadVisualStyle(Nothing, "OSX (Tiger).vssf")
        '
        'frmLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(378, 172)
        Me.Controls.Add(Me.butAddUser)
        Me.Controls.Add(Me.butReset)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtpassword)
        Me.Controls.Add(Me.txtusername)
        Me.Controls.Add(Me.butlogin)
        Me.Name = "frmLogin"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login"
        CType(Me.VisualStyler1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents butlogin As System.Windows.Forms.Button
    Friend WithEvents txtusername As System.Windows.Forms.TextBox
    Friend WithEvents txtpassword As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents butReset As Button
    Friend WithEvents butAddUser As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents VisualStyler1 As SkinSoft.VisualStyler.VisualStyler
End Class
