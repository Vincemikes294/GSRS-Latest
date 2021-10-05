<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOption
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.butContinuousSlope = New System.Windows.Forms.Button()
        Me.butSeparateSlope = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(213, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "                         Analysis Options"
        '
        'butContinuousSlope
        '
        Me.butContinuousSlope.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.butContinuousSlope.Location = New System.Drawing.Point(15, 65)
        Me.butContinuousSlope.Name = "butContinuousSlope"
        Me.butContinuousSlope.Size = New System.Drawing.Size(139, 34)
        Me.butContinuousSlope.TabIndex = 1
        Me.butContinuousSlope.Text = "Continuous Slope"
        Me.butContinuousSlope.UseVisualStyleBackColor = True
        '
        'butSeparateSlope
        '
        Me.butSeparateSlope.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.butSeparateSlope.Location = New System.Drawing.Point(187, 65)
        Me.butSeparateSlope.Name = "butSeparateSlope"
        Me.butSeparateSlope.Size = New System.Drawing.Size(149, 34)
        Me.butSeparateSlope.TabIndex = 2
        Me.butSeparateSlope.Text = "Separate Slope"
        Me.butSeparateSlope.UseVisualStyleBackColor = True
        '
        'frmOption
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(350, 120)
        Me.Controls.Add(Me.butSeparateSlope)
        Me.Controls.Add(Me.butContinuousSlope)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmOption"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "  Option Form"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents butContinuousSlope As System.Windows.Forms.Button
    Friend WithEvents butSeparateSlope As System.Windows.Forms.Button
End Class
