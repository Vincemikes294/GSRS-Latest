<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHorizontal
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
        Me.butClear = New System.Windows.Forms.Button()
        Me.butFilter = New System.Windows.Forms.Button()
        Me.butSave = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'lstFinalOutputView
        '
        Me.lstFinalOutputView.FormattingEnabled = True
        Me.lstFinalOutputView.HorizontalScrollbar = True
        Me.lstFinalOutputView.ItemHeight = 16
        Me.lstFinalOutputView.Location = New System.Drawing.Point(12, 12)
        Me.lstFinalOutputView.Name = "lstFinalOutputView"
        Me.lstFinalOutputView.Size = New System.Drawing.Size(415, 340)
        Me.lstFinalOutputView.TabIndex = 26
        '
        'butClear
        '
        Me.butClear.Location = New System.Drawing.Point(328, 375)
        Me.butClear.Name = "butClear"
        Me.butClear.Size = New System.Drawing.Size(72, 35)
        Me.butClear.TabIndex = 31
        Me.butClear.Text = "Clear"
        Me.ToolTip1.SetToolTip(Me.butClear, "Clear listbox")
        Me.butClear.UseVisualStyleBackColor = True
        '
        'butFilter
        '
        Me.butFilter.Location = New System.Drawing.Point(176, 376)
        Me.butFilter.Name = "butFilter"
        Me.butFilter.Size = New System.Drawing.Size(77, 35)
        Me.butFilter.TabIndex = 30
        Me.butFilter.Text = "Filter"
        Me.ToolTip1.SetToolTip(Me.butFilter, "Filter Data")
        Me.butFilter.UseVisualStyleBackColor = True
        '
        'butSave
        '
        Me.butSave.Location = New System.Drawing.Point(28, 375)
        Me.butSave.Name = "butSave"
        Me.butSave.Size = New System.Drawing.Size(82, 36)
        Me.butSave.TabIndex = 32
        Me.butSave.Text = "Save"
        Me.ToolTip1.SetToolTip(Me.butSave, "Save data as excel or text file")
        Me.butSave.UseVisualStyleBackColor = True
        '
        'frmHorizontal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(439, 431)
        Me.Controls.Add(Me.butSave)
        Me.Controls.Add(Me.butClear)
        Me.Controls.Add(Me.butFilter)
        Me.Controls.Add(Me.lstFinalOutputView)
        Me.Name = "frmHorizontal"
        Me.Text = "                                            Final Output"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lstFinalOutputView As ListBox
    Friend WithEvents butClear As Button
    Friend WithEvents butFilter As Button
    Friend WithEvents butSave As Button
    Friend WithEvents ToolTip1 As ToolTip
End Class
