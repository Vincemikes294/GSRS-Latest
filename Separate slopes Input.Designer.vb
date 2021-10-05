<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSeparateSlopesInput
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
        Me.lstGradeLength = New System.Windows.Forms.ListBox()
        Me.butGradeLength = New System.Windows.Forms.Button()
        Me.txtMaxSpeed = New System.Windows.Forms.TextBox()
        Me.txtMaxWeight = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtGroupNumber = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboMaxTemp = New System.Windows.Forms.ComboBox()
        Me.butCompute = New System.Windows.Forms.Button()
        Me.txtNumSections = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtinitemp = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtambient = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.butClear = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstGradeLength
        '
        Me.lstGradeLength.FormattingEnabled = True
        Me.lstGradeLength.HorizontalScrollbar = True
        Me.lstGradeLength.ItemHeight = 16
        Me.lstGradeLength.Location = New System.Drawing.Point(6, 21)
        Me.lstGradeLength.Name = "lstGradeLength"
        Me.lstGradeLength.Size = New System.Drawing.Size(280, 164)
        Me.lstGradeLength.TabIndex = 19
        '
        'butGradeLength
        '
        Me.butGradeLength.Location = New System.Drawing.Point(317, 31)
        Me.butGradeLength.Name = "butGradeLength"
        Me.butGradeLength.Size = New System.Drawing.Size(104, 29)
        Me.butGradeLength.TabIndex = 18
        Me.butGradeLength.Text = "Enter "
        Me.butGradeLength.UseVisualStyleBackColor = True
        '
        'txtMaxSpeed
        '
        Me.txtMaxSpeed.Location = New System.Drawing.Point(338, 105)
        Me.txtMaxSpeed.Name = "txtMaxSpeed"
        Me.txtMaxSpeed.Size = New System.Drawing.Size(61, 22)
        Me.txtMaxSpeed.TabIndex = 17
        '
        'txtMaxWeight
        '
        Me.txtMaxWeight.Location = New System.Drawing.Point(338, 66)
        Me.txtMaxWeight.Name = "txtMaxWeight"
        Me.txtMaxWeight.Size = New System.Drawing.Size(61, 22)
        Me.txtMaxWeight.TabIndex = 16
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 108)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(292, 17)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Please enter maximum descent speed (mi/hr)"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 69)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(314, 17)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Please enter maximum weight for downgrade (lb)"
        '
        'txtGroupNumber
        '
        Me.txtGroupNumber.Location = New System.Drawing.Point(331, 18)
        Me.txtGroupNumber.Name = "txtGroupNumber"
        Me.txtGroupNumber.Size = New System.Drawing.Size(62, 22)
        Me.txtGroupNumber.TabIndex = 13
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(187, 17)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Please Enter Group Number"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(281, 17)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Please select the maximum temperature (F)"
        '
        'cboMaxTemp
        '
        Me.cboMaxTemp.FormattingEnabled = True
        Me.cboMaxTemp.Items.AddRange(New Object() {"500", "530"})
        Me.cboMaxTemp.Location = New System.Drawing.Point(337, 24)
        Me.cboMaxTemp.Name = "cboMaxTemp"
        Me.cboMaxTemp.Size = New System.Drawing.Size(62, 24)
        Me.cboMaxTemp.TabIndex = 10
        '
        'butCompute
        '
        Me.butCompute.Location = New System.Drawing.Point(315, 124)
        Me.butCompute.Name = "butCompute"
        Me.butCompute.Size = New System.Drawing.Size(109, 25)
        Me.butCompute.TabIndex = 20
        Me.butCompute.Text = "Compute"
        Me.butCompute.UseVisualStyleBackColor = True
        '
        'txtNumSections
        '
        Me.txtNumSections.Location = New System.Drawing.Point(331, 51)
        Me.txtNumSections.Name = "txtNumSections"
        Me.txtNumSections.Size = New System.Drawing.Size(62, 22)
        Me.txtNumSections.TabIndex = 22
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 56)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(222, 17)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "Please Enter number of segments"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtNumSections)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtGroupNumber)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(422, 89)
        Me.GroupBox1.TabIndex = 23
        Me.GroupBox1.TabStop = False
        '
        'txtinitemp
        '
        Me.txtinitemp.Location = New System.Drawing.Point(338, 143)
        Me.txtinitemp.Name = "txtinitemp"
        Me.txtinitemp.Size = New System.Drawing.Size(61, 22)
        Me.txtinitemp.TabIndex = 25
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 146)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(275, 17)
        Me.Label6.TabIndex = 24
        Me.Label6.Text = "Please enter initial brake temperature (F)  "
        '
        'txtambient
        '
        Me.txtambient.Location = New System.Drawing.Point(337, 178)
        Me.txtambient.Name = "txtambient"
        Me.txtambient.Size = New System.Drawing.Size(61, 22)
        Me.txtambient.TabIndex = 27
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 181)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(245, 17)
        Me.Label7.TabIndex = 26
        Me.Label7.Text = "Please enter ambient temperature (F)"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtambient)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txtinitemp)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.txtMaxSpeed)
        Me.GroupBox2.Controls.Add(Me.txtMaxWeight)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.cboMaxTemp)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 107)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(422, 216)
        Me.GroupBox2.TabIndex = 28
        Me.GroupBox2.TabStop = False
        '
        'butClear
        '
        Me.butClear.Location = New System.Drawing.Point(315, 80)
        Me.butClear.Name = "butClear"
        Me.butClear.Size = New System.Drawing.Size(106, 26)
        Me.butClear.TabIndex = 33
        Me.butClear.Text = "Clear"
        Me.butClear.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.butClear)
        Me.GroupBox3.Controls.Add(Me.butCompute)
        Me.GroupBox3.Controls.Add(Me.lstGradeLength)
        Me.GroupBox3.Controls.Add(Me.butGradeLength)
        Me.GroupBox3.Location = New System.Drawing.Point(13, 329)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(433, 199)
        Me.GroupBox3.TabIndex = 34
        Me.GroupBox3.TabStop = False
        '
        'frmSeparateSlopesInput
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(451, 540)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmSeparateSlopesInput"
        Me.Text = "Separate slopes Input Form"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstGradeLength As System.Windows.Forms.ListBox
    Friend WithEvents butGradeLength As System.Windows.Forms.Button
    Friend WithEvents txtMaxSpeed As System.Windows.Forms.TextBox
    Friend WithEvents txtMaxWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtGroupNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboMaxTemp As System.Windows.Forms.ComboBox
    Friend WithEvents butCompute As System.Windows.Forms.Button
    Friend WithEvents txtNumSections As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtinitemp As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtambient As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents butClear As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
End Class
