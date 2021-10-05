Option Explicit On
Imports System.ComponentModel
Imports System.IO
Imports Continuous_Slope.GSRS.Net.Common.Scripts
Imports System.Guid
Imports Microsoft.Office.Interop
Public Class frmHorizontal
    'Variables to store Unique File Name - 06.04.21
    Public CUniqueId As String = ""
    Public LoggedOnUser As String = ""
    Public UserFirstName As String = ""
    Public UserLastName As String = ""
    Public UserOperation As String = "Save2Table"
    Private Sub butFilter_Click(sender As Object, e As EventArgs) Handles butFilter.Click
        frmMain.butCurve.Enabled = False
        Dim header As String = lstFinalOutputView.Items(0)
        Dim T_max = frmMain.cboMaxTemp.Text
        Dim data As New List(Of DataValue)
        ' Skip the header row by starting at 1:
        For i As Integer = 1 To lstFinalOutputView.Items.Count - 1
            data.Add(New DataValue(lstFinalOutputView.Items(i)))
        Next

        Dim results = From dv In data
        lstFinalOutputView.Items.Clear()
        lstFinalOutputView.Items.Add(header)


        For Each row In results
            If row.T_Final < T_max Then
                lstFinalOutputView.Items.Add(row.ToString)

            End If
        Next


        Dim header1 As String = lstFinalOutputView.Items(0)

        Dim data1 As New List(Of DataValue)
        ' Skip the header row by starting at 1:
        For i As Integer = 1 To lstFinalOutputView.Items.Count - 1
            data1.Add(New DataValue(lstFinalOutputView.Items(i)))
        Next

        Dim finalresults = From dv In data1
                           Order By dv.MaxWeight Descending, dv.MaxSpeed Descending
                           Group dv By dv.MaxWeight Into g = Group
                           Select g.First

        Dim V_max = CInt(frmMain.txtMaxSpeed.Text)
        lstFinalOutputView.Items.Clear()
        lstFinalOutputView.Items.Add(header)


        For Each row In finalresults
            lstFinalOutputView.Items.Add(row.ToString)
            If row.MaxSpeed = V_max Then
                Exit For
            End If
        Next

    End Sub
    Public Class DataValue
        Public Sub New(ByVal strInput As String)
            Dim values() As String = strInput.Split({" ", vbTab}, StringSplitOptions.RemoveEmptyEntries)
            If values.Length >= 6 Then
                Try
                    MaxWeight = Integer.Parse(values(0))
                    MaxSpeed = Integer.Parse(values(1))
                    T_Desc = Integer.Parse(values(2))
                    T_Emerg = Integer.Parse(values(3))
                    T_Final = Integer.Parse(values(4))
                    Time = Integer.Parse(values(5))
                Catch ex As Exception
                    MessageBox.Show("Invalid Input: Value failed to convert to Integer.")
                End Try
            Else
                MessageBox.Show("Invalid Input: Not enough values.")
            End If
        End Sub
        Public Overrides Function ToString() As String
            Return MaxWeight & vbTab & vbTab & MaxSpeed & vbTab & vbTab & T_Desc & vbTab & vbTab & T_Emerg & vbTab & vbTab & T_Final & vbTab & vbTab & Time
        End Function

        Public MaxWeight As Integer
        Public MaxSpeed As Integer
        Public Time As Integer
        Public T_Emerg As Integer
        Public T_Final As Integer
        Public T_Desc As Integer

    End Class
    Private Sub butSave_Click(sender As Object, e As EventArgs) Handles butSave.Click
        'Dim SaveFileDialog1 As New SaveFileDialog
        'SaveFileDialog1.FileName = ""
        'SaveFileDialog1.Filter = "(*.xls)|*.xls"

        'If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
        '    Dim sb As New System.Text.StringBuilder()

        '    For Each o As Object In Me.lstFinalOutputView.Items
        '        sb.AppendLine(o)
        '    Next

        '    System.IO.File.WriteAllText(SaveFileDialog1.FileName, sb.ToString())
        'End If

        Try
            Dim myHelper As New HelperClass
            Dim MaxWeight As String = ""
            Dim MaxSpeed As String = ""
            Dim TDesc As String = ""
            Dim TEmerg As String = ""
            Dim TFinal As String = ""
            Dim Time As String = ""

            If UserOperation = "Save2Table" Then
                'Generate new Unique for every save
                CUniqueId = LoggedOnUser & "-" & System.Guid.NewGuid.ToString()

                Dim SlopeTable As New DataTable("Slope")

                Dim ListViewItems() As String
                Dim Delim As String = vbTab & vbTab

                Dim SlopeColumn As DataColumn = SlopeTable.Columns.Add("Unique_Id", GetType(String))
                SlopeColumn.AllowDBNull = False
                SlopeColumn.Unique = False

                SlopeTable.Columns.Add("Max_Weight", GetType(Integer))
                SlopeTable.Columns.Add("Max_Speed", GetType(Integer))
                SlopeTable.Columns.Add("T_Desc", GetType(Integer))
                SlopeTable.Columns.Add("T_Emerg", GetType(Integer))
                SlopeTable.Columns.Add("T_Final", GetType(Integer))
                SlopeTable.Columns.Add("Time", GetType(Integer))

                'Write the list output values to a table
                For Each o As Object In lstFinalOutputView.Items
                    'sb.AppendLine(o)

                    Dim ListViewRow As String = o
                    ListViewRow = ListViewRow.Replace(vbCrLf, "").Trim()
                    'Debug.WriteLine(ListViewRow)


                    If Not ListViewRow.Contains("Max Weight") Then
                        ListViewRow = ListViewRow.Replace(Delim, "|")
                        ListViewItems = ListViewRow.Split("|")
                        For LVItem As Integer = 0 To ListViewItems.Length - 1

                            Select Case LVItem
                                Case 0
                                    MaxWeight = ListViewItems(LVItem)
                                Case 1
                                    MaxSpeed = ListViewItems(LVItem)
                                Case 2
                                    TDesc = ListViewItems(LVItem)
                                Case 3
                                    TEmerg = ListViewItems(LVItem)
                                Case 4
                                    TFinal = ListViewItems(LVItem)
                                Case 5
                                    Time = ListViewItems(LVItem)

                            End Select

                        Next
                        SlopeTable.Rows.Add(CUniqueId, MaxWeight, MaxSpeed, TDesc, TEmerg, TFinal, Time)
                        MaxWeight = ""
                        MaxSpeed = ""
                        TDesc = ""
                        TEmerg = ""
                        TFinal = ""
                        Time = ""

                    End If
                Next
                If myHelper.InsertSlopeData(SlopeTable, "ContinuousSlope") Then
                    UserOperation = "Export2Excel"
                    butSave.Text = "Export"
                End If
            ElseIf UserOperation = "Export2Excel" Then
                'Prompt the User to Select the XL
                Dim SaveFileDialog1 As New SaveFileDialog
                SaveFileDialog1.FileName = ""
                SaveFileDialog1.Filter = "Excel Files(*.xls)|*.xlsx"
                If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
                    Dim XLFileName As String = SaveFileDialog1.FileName
                    Dim ExcelApp As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application()
                    Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook
                    Dim xlWorkSheet As Microsoft.Office.Interop.Excel.Worksheet


                    If Not File.Exists(XLFileName) Then
                        'Create a new XL file 
                        xlWorkBook = ExcelApp.Workbooks.Add()
                        xlWorkSheet = CType(xlWorkBook.Sheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet)
                        xlWorkSheet.Name = "SlopeData"
                        xlWorkBook.SaveAs(XLFileName)
                        xlWorkBook.Close(True)
                    End If

                    If myHelper.GetSlopeData("ContinuousSlope", CUniqueId, XLFileName) Then
                        'Delete the records from the table
                        MessageBox.Show("Data exported to excel file")
                        UserOperation = "Save2Table"
                        butSave.Text = "Save"
                    End If

                End If


            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub frmHorizontal_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        frmMain.butCurve.Enabled = True
    End Sub

    Private Sub frmHorizontal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub butReset_Click(sender As Object, e As EventArgs) Handles butClear.Click
        lstFinalOutputView.Items.Clear()
    End Sub
End Class