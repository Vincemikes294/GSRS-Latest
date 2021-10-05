Imports System.Windows.Forms.DataVisualization.Charting
Public Class frmTemperaturePlot
    Public s As New Series
    Private Sub TemperaturePlot_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        frmTempProfile.buttempCompute.PerformClick()
        ChartTemp.Series.Clear()
        ChartTemp.Titles.Add("Continuous Slope")

        'Create a new series and add data points to it.

        s.Name = "Temperature Profile"

        'Change to a line graph.

        's.ChartType = SeriesChartType.Line

        'Add the series to the Chart1 control.
        ChartTemp.Series.Add(s)
        ChartTemp.ChartAreas("ChartArea1").AxisX.MajorGrid.Enabled = True
        ChartTemp.ChartAreas("ChartArea1").AxisY.MajorGrid.Enabled = True
        ChartTemp.ChartAreas("ChartArea1").AxisX.MinorGrid.Enabled = True
        ChartTemp.ChartAreas("ChartArea1").AxisY.MinorGrid.Enabled = True
        ChartTemp.ChartAreas("ChartArea1").CursorY.Position = frmMain.cboMaxTemp.Text
        ChartTemp.ChartAreas("ChartArea1").CursorY.LineWidth = 3
        ChartTemp.ChartAreas("ChartArea1").CursorY.LineColor = Color.Red
        ChartTemp.ChartAreas("ChartArea1").AxisX.Title = "Distance from start of downgrade (Miles)"
        ChartTemp.ChartAreas("ChartArea1").AxisY.Title = "Brake Temperature (F)"
    End Sub

    Private Sub ChartTemp_Click(sender As Object, e As EventArgs) Handles ChartTemp.Click

    End Sub
End Class