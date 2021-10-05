<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTemperaturePlot
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
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.ChartTemp = New System.Windows.Forms.DataVisualization.Charting.Chart()
        CType(Me.ChartTemp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ChartTemp
        '
        ChartArea1.Name = "ChartArea1"
        Me.ChartTemp.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Me.ChartTemp.Legends.Add(Legend1)
        Me.ChartTemp.Location = New System.Drawing.Point(0, 0)
        Me.ChartTemp.Name = "ChartTemp"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Me.ChartTemp.Series.Add(Series1)
        Me.ChartTemp.Size = New System.Drawing.Size(622, 300)
        Me.ChartTemp.TabIndex = 0
        Me.ChartTemp.Text = "Chart1"
        '
        'frmTemperaturePlot
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(612, 300)
        Me.Controls.Add(Me.ChartTemp)
        Me.Name = "frmTemperaturePlot"
        Me.Text = "                                                      Temperature    Plot"
        CType(Me.ChartTemp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ChartTemp As DataVisualization.Charting.Chart
End Class
