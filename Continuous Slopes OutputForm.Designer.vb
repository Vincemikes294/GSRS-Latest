<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmContinuousSlopeOutput
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
        Me.lstOutput = New System.Windows.Forms.ListBox()
        Me.butSave = New System.Windows.Forms.Button()
        Me.butFilter = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lstOutput
        '
        Me.lstOutput.FormattingEnabled = True
        Me.lstOutput.HorizontalScrollbar = True
        Me.lstOutput.ItemHeight = 16
        Me.lstOutput.Location = New System.Drawing.Point(12, 30)
        Me.lstOutput.Name = "lstOutput"
        Me.lstOutput.ScrollAlwaysVisible = True
        Me.lstOutput.Size = New System.Drawing.Size(419, 324)
        Me.lstOutput.TabIndex = 0
        '
        'butSave
        '
        Me.butSave.Location = New System.Drawing.Point(12, 360)
        Me.butSave.Name = "butSave"
        Me.butSave.Size = New System.Drawing.Size(75, 36)
        Me.butSave.TabIndex = 1
        Me.butSave.Text = "Save"
        Me.butSave.UseVisualStyleBackColor = True
        '
        'butFilter
        '
        Me.butFilter.Location = New System.Drawing.Point(147, 360)
        Me.butFilter.Name = "butFilter"
        Me.butFilter.Size = New System.Drawing.Size(61, 35)
        Me.butFilter.TabIndex = 2
        Me.butFilter.Text = "Filter"
        Me.butFilter.UseVisualStyleBackColor = True
        '
        'frmContinuousSlopeOutput
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(442, 408)
        Me.Controls.Add(Me.butFilter)
        Me.Controls.Add(Me.butSave)
        Me.Controls.Add(Me.lstOutput)
        Me.Name = "frmContinuousSlopeOutput"
        Me.Text = "Continuous Slope OutputForm"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstOutput As System.Windows.Forms.ListBox
    Friend WithEvents butSave As System.Windows.Forms.Button
    Friend WithEvents butFilter As System.Windows.Forms.Button
End Class
