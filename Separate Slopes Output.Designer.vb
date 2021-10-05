<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSeparateSlopesOutput
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
        Me.butFilter = New System.Windows.Forms.Button()
        Me.butSave = New System.Windows.Forms.Button()
        Me.lstOutputView = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'butFilter
        '
        Me.butFilter.Location = New System.Drawing.Point(144, 372)
        Me.butFilter.Name = "butFilter"
        Me.butFilter.Size = New System.Drawing.Size(61, 35)
        Me.butFilter.TabIndex = 8
        Me.butFilter.Text = "Filter"
        Me.butFilter.UseVisualStyleBackColor = True
        '
        'butSave
        '
        Me.butSave.Location = New System.Drawing.Point(9, 372)
        Me.butSave.Name = "butSave"
        Me.butSave.Size = New System.Drawing.Size(75, 36)
        Me.butSave.TabIndex = 7
        Me.butSave.Text = "Save"
        Me.butSave.UseVisualStyleBackColor = True
        '
        'lstOutputView
        '
        Me.lstOutputView.FormattingEnabled = True
        Me.lstOutputView.HorizontalScrollbar = True
        Me.lstOutputView.ItemHeight = 16
        Me.lstOutputView.Location = New System.Drawing.Point(9, 10)
        Me.lstOutputView.Name = "lstOutputView"
        Me.lstOutputView.Size = New System.Drawing.Size(448, 356)
        Me.lstOutputView.TabIndex = 6
        '
        'frmSeparateSlopesOutput
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(469, 417)
        Me.Controls.Add(Me.butFilter)
        Me.Controls.Add(Me.butSave)
        Me.Controls.Add(Me.lstOutputView)
        Me.Name = "frmSeparateSlopesOutput"
        Me.Text = "Separate Slopes OutputViewer"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents butFilter As System.Windows.Forms.Button
    Friend WithEvents butSave As System.Windows.Forms.Button
    Friend WithEvents lstOutputView As System.Windows.Forms.ListBox
End Class
