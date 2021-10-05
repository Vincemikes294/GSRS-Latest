<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHorizontalseparate
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
        Me.lstFinalOutputView = New System.Windows.Forms.ListBox()
        Me.butSave = New System.Windows.Forms.Button()
        Me.butReset = New System.Windows.Forms.Button()
        Me.butFilter = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.txtNewTemp = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.butsReset = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lstFinalOutputView
        '
        Me.lstFinalOutputView.FormattingEnabled = True
        Me.lstFinalOutputView.HorizontalScrollbar = True
        Me.lstFinalOutputView.ItemHeight = 16
        Me.lstFinalOutputView.Location = New System.Drawing.Point(12, 12)
        Me.lstFinalOutputView.Name = "lstFinalOutputView"
        Me.lstFinalOutputView.Size = New System.Drawing.Size(429, 324)
        Me.lstFinalOutputView.TabIndex = 27
        '
        'butSave
        '
        Me.butSave.Location = New System.Drawing.Point(12, 371)
        Me.butSave.Name = "butSave"
        Me.butSave.Size = New System.Drawing.Size(82, 36)
        Me.butSave.TabIndex = 35
        Me.butSave.Text = "Save"
        Me.ToolTip1.SetToolTip(Me.butSave, "Save data in excel or text format")
        Me.butSave.UseVisualStyleBackColor = True
        '
        'butReset
        '
        Me.butReset.Location = New System.Drawing.Point(195, 372)
        Me.butReset.Name = "butReset"
        Me.butReset.Size = New System.Drawing.Size(72, 35)
        Me.butReset.TabIndex = 34
        Me.butReset.Text = "Clear"
        Me.ToolTip1.SetToolTip(Me.butReset, "Clear listbox")
        Me.butReset.UseVisualStyleBackColor = True
        '
        'butFilter
        '
        Me.butFilter.Location = New System.Drawing.Point(100, 372)
        Me.butFilter.Name = "butFilter"
        Me.butFilter.Size = New System.Drawing.Size(77, 35)
        Me.butFilter.TabIndex = 33
        Me.butFilter.Text = "Filter"
        Me.ToolTip1.SetToolTip(Me.butFilter, "Filter data")
        Me.butFilter.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Enabled = False
        Me.btnNext.Location = New System.Drawing.Point(361, 372)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(63, 35)
        Me.btnNext.TabIndex = 39
        Me.btnNext.Text = "Next"
        Me.ToolTip1.SetToolTip(Me.btnNext, "Click to proceed to next segment")
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'txtNewTemp
        '
        Me.txtNewTemp.Location = New System.Drawing.Point(378, 342)
        Me.txtNewTemp.Name = "txtNewTemp"
        Me.txtNewTemp.Size = New System.Drawing.Size(61, 22)
        Me.txtNewTemp.TabIndex = 40
        Me.txtNewTemp.Visible = False
        '
        'butsReset
        '
        Me.butsReset.Location = New System.Drawing.Point(273, 372)
        Me.butsReset.Name = "butsReset"
        Me.butsReset.Size = New System.Drawing.Size(72, 35)
        Me.butsReset.TabIndex = 41
        Me.butsReset.Text = "Reset"
        Me.ToolTip1.SetToolTip(Me.butsReset, "Reset")
        Me.butsReset.UseVisualStyleBackColor = True
        '
        'frmHorizontalseparate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(453, 418)
        Me.Controls.Add(Me.butsReset)
        Me.Controls.Add(Me.txtNewTemp)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.butSave)
        Me.Controls.Add(Me.butReset)
        Me.Controls.Add(Me.butFilter)
        Me.Controls.Add(Me.lstFinalOutputView)
        Me.Name = "frmHorizontalseparate"
        Me.Text = "                            Final Outputform"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lstFinalOutputView As ListBox
    Friend WithEvents butSave As Button
    Friend WithEvents butReset As Button
    Friend WithEvents butFilter As Button
    Friend WithEvents btnNext As Button
    Friend WithEvents txtNewTemp As TextBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents butsReset As Button
End Class
