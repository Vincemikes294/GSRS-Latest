Option Explicit On
Imports System.ComponentModel
Imports System.IO
Imports Continuous_Slope.GSRS.Net.Common.Scripts
Imports System.Guid
Imports Microsoft.Office.Interop
Public Class frmHorizontalseparate
    Public a As Integer
    Public p As Integer

    'Variables to store Unique File Name - 06.04.21
    Public SUniqueId As String = ""
    Public LoggedOnUser As String = ""
    Public UserFirstName As String = ""
    Public UserLastName As String = ""
    Public UserOperation As String = "Save2Table"
    Public NumberGrades As Integer = 0

    Private Sub butSave_Click(sender As Object, e As EventArgs) Handles butSave.Click
        'Dim SaveFileDialog1 As New SaveFileDialog
        'SaveFileDialog1.FileName = ""
        'SaveFileDialog1.Filter = "Text Files(*.txt)|*.txt|(*.xls)|*.xls"

        'If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
        '    Dim sb As New System.Text.StringBuilder()

        '    For Each o As Object In Me.lstFinalOutputView.Items
        '        sb.AppendLine(o)
        '    Next

        '    System.IO.File.WriteAllText(SaveFileDialog1.FileName, sb.ToString())
        'End If

        Try
            Dim myHelper As New HelperClass
            Dim GroupNumber As String = ""
            Dim MaxWeight As String = ""
            Dim MaxSpeed As String = ""
            Dim TDesc As String = ""
            Dim TEmerg As String = ""
            Dim TFinal As String = ""
            Dim Time As String = ""


            If UserOperation = "Save2Table" Then
                'Generate new Unique for every save
                SUniqueId = LoggedOnUser & "-" & System.Guid.NewGuid.ToString()
                Dim SlopeTable As New DataTable("Slope")

                Dim ListViewItems() As String
                Dim Delim As String = vbTab

                Dim SlopeColumn As DataColumn = SlopeTable.Columns.Add("Unique_Id", GetType(String))
                SlopeColumn.AllowDBNull = False
                SlopeColumn.Unique = False

                SlopeTable.Columns.Add("Number_Grades", GetType(Integer))
                SlopeTable.Columns.Add("Group_Number", GetType(Integer))
                SlopeTable.Columns.Add("Max_Weight", GetType(Integer))
                SlopeTable.Columns.Add("Max_Speed", GetType(Integer))
                SlopeTable.Columns.Add("T_Desc", GetType(Integer))
                SlopeTable.Columns.Add("T_Emerg", GetType(Integer))
                SlopeTable.Columns.Add("T_Final", GetType(Integer))
                SlopeTable.Columns.Add("Time", GetType(Integer))

                'Write the list output values to a table
                For Each o As Object In lstFinalOutputView.Items
                    'sb.AppendLine(o)

                    Dim ListViewRow As String = o.ToString
                    ListViewRow = ListViewRow.Replace(vbCrLf, "").Trim()

                    If Not ListViewRow.Contains("Max Weight") Then
                        ListViewRow = ListViewRow.Replace(Delim, "|")
                        ListViewItems = ListViewRow.Split("|")
                        Dim TempListView As String = ""

                        For LVItem As Integer = 0 To ListViewItems.Length - 1
                            If ListViewItems(LVItem).Trim <> "" Then
                                TempListView = TempListView & ListViewItems(LVItem).Trim & "|"
                            End If
                        Next

                        ListViewItems = TempListView.Split("|")

                        For LVItem As Integer = 0 To ListViewItems.Length - 1
                            Select Case LVItem
                                Case 0
                                    GroupNumber = ListViewItems(LVItem)
                                Case 1
                                    MaxWeight = ListViewItems(LVItem)
                                Case 2
                                    MaxSpeed = ListViewItems(LVItem)
                                Case 3
                                    TDesc = ListViewItems(LVItem)
                                Case 4
                                    TEmerg = ListViewItems(LVItem)
                                Case 5
                                    TFinal = ListViewItems(LVItem)
                                Case 6
                                    Time = ListViewItems(LVItem)
                            End Select

                        Next
                        SlopeTable.Rows.Add(SUniqueId, NumberGrades, GroupNumber, MaxWeight, MaxSpeed, TDesc, TEmerg, TFinal, Time)
                        GroupNumber = ""
                        MaxWeight = ""
                        MaxSpeed = ""
                        TDesc = ""
                        TEmerg = ""
                        TFinal = ""
                        Time = ""

                    End If
                Next
                myHelper.InsertSlopeData(SlopeTable, "SeparateSlope")
                UserOperation = "Export2Excel"
                butSave.Text = "Export"
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

                        xlWorkBook = ExcelApp.Workbooks.Add()
                        xlWorkSheet = CType(xlWorkBook.Sheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet)
                        xlWorkSheet.Name = "SlopeData"
                        xlWorkBook.SaveAs(XLFileName)
                        xlWorkBook.Close(True)
                    End If


                    If myHelper.GetSlopeData("SeparateSlope", SUniqueId, XLFileName) Then
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
    Private Sub butReset_Click(sender As Object, e As EventArgs) Handles butReset.Click
        lstFinalOutputView.Items.Clear()
    End Sub
    Private Sub butsReset_Click(sender As Object, e As EventArgs)
        lstFinalOutputView.Items.Clear()
    End Sub
    Private Sub butFilter_Click(sender As Object, e As EventArgs) Handles butFilter.Click
        a = frmMain.txtsGroupNumber.Text
        frmMain.butCurve.Enabled = False
        Dim header As String
        Dim data As New List(Of DataValue1)
        Dim results = From dv In data
        Dim header1 As String
        Dim header2 As String
        Dim data1 As New List(Of DataValue1)
        Dim data2 As New List(Of DataValue1)
        Dim finalresults = From dv In data1
        Dim finalresults2 = From dv In data1
        Dim V_max = CInt(frmMain.txtsMaxSpeed.Text)
        Dim T_max = CInt(frmMain.cbosMaxTemp.Text)
        Dim Max_weight = CInt(frmMain.txtsMaxWeight.Text)
        If a Mod 2 = 0 Then
            header = lstFinalOutputView.Items(0)

            ' Skip the header row by starting at 1:
            For i As Integer = 1 To lstFinalOutputView.Items.Count - 1
                data.Add(New DataValue1(lstFinalOutputView.Items(i)))
            Next

            lstFinalOutputView.Items.Clear()
            lstFinalOutputView.Items.Add(header)


            For Each row In results
                If row.T_Final < T_max Then
                    lstFinalOutputView.Items.Add(row.ToString)
                End If
            Next


            header1 = lstFinalOutputView.Items(0)

            ' Skip the header row by starting at 1:
            For i As Integer = 1 To lstFinalOutputView.Items.Count - 1
                data1.Add(New DataValue1(lstFinalOutputView.Items(i)))
            Next

            finalresults = From dv In data1
                           Order By dv.MaxWeight Descending, dv.MaxSpeed Descending
                           Group dv By dv.MaxWeight Into g = Group
                           Select g.First


            lstFinalOutputView.Items.Clear()
            lstFinalOutputView.Items.Add(header)


            For Each row In finalresults
                lstFinalOutputView.Items.Add(row.ToString)
                If row.MaxSpeed = V_max Then
                    Exit For
                End If
            Next

            MsgBox("Select row for maximum weight",, "Seperate Slope")
            header2 = lstFinalOutputView.Items(0)

            ' Skip the header row by starting at 1:
            For i As Integer = 1 To lstFinalOutputView.Items.Count - 1
                data2.Add(New DataValue1(lstFinalOutputView.Items(i)))
            Next

            finalresults2 = From dv In data2
                            Order By dv.MaxWeight Descending
                            Group dv By dv.MaxWeight Into g = Group
                            Select g.First

            lstFinalOutputView.Items.Clear()
            lstFinalOutputView.Items.Add(header)

            For Each row In finalresults2
                lstFinalOutputView.Items.Add(row.ToString)
                If row.MaxWeight = Max_weight Then
                    txtNewTemp.Text = row.T_Final
                End If
                Exit For
            Next
        End If

        If a Mod 2 = 1 Then
            ' Skip the header row by starting at 1:

            header = lstFinalOutputView.Items(0)
            For i As Integer = 1 To lstFinalOutputView.Items.Count - 1
                data.Add(New DataValue1(lstFinalOutputView.Items(i)))
            Next

            lstFinalOutputView.Items.Clear()
            lstFinalOutputView.Items.Add(header)


            For Each row In results
                If row.T_Final < T_max Then
                    lstFinalOutputView.Items.Add(row.ToString)

                End If
            Next

            ' Skip the header row by starting at 1:
            For i As Integer = 1 To lstFinalOutputView.Items.Count - 1
                data1.Add(New DataValue1(lstFinalOutputView.Items(i)))
            Next

            finalresults = From dv In data1
                           Order By dv.MaxWeight Descending, dv.Time Ascending
                           Group dv By dv.MaxWeight Into g = Group
                           Select g.First

            lstFinalOutputView.Items.Clear()
            lstFinalOutputView.Items.Add(header)


            For Each row In finalresults

                lstFinalOutputView.Items.Add(row.ToString)
                txtNewTemp.Text = row.T_Final

            Next
        End If

        Dim Answer As Integer
        If a Mod 2 = 0 Then
            Answer = MsgBox("Enter segments for downgrade of next braking segment?", vbYesNoCancel, "Alert")
            If Answer = vbYes Then
                btnNext.Enabled = True
                btnNext.Select()
            Else
            End If
        End If
        If a Mod 2 = 1 Then
            Answer = MsgBox("Enter segments for downgrade of next non-braking segment?", vbYesNoCancel, "Alert")
            If Answer = vbYes Then
                btnNext.Enabled = True
                btnNext.Select()
            Else

            End If
        End If
    End Sub
    Public Class DataValue1

        Public Sub New(ByVal strInput As String)
            Dim values() As String = strInput.Split({" ", vbTab}, StringSplitOptions.RemoveEmptyEntries)
            If values.Length >= 7 Then
                Try
                    GroupNumber = Integer.Parse(values(0))
                    MaxWeight = Integer.Parse(values(1))
                    MaxSpeed = Integer.Parse(values(2))
                    T_Desc = Integer.Parse(values(3))
                    T_Emerg = Integer.Parse(values(4))
                    T_Final = Integer.Parse(values(5))
                    Time = Integer.Parse(values(6))
                Catch ex As Exception
                    MessageBox.Show("Invalid Input: Value failed to convert to Integer.")
                End Try
            Else
                MessageBox.Show("Invalid Input:  Not enough values.")
            End If
        End Sub
        Public Overrides Function ToString() As String
            Dim Space = ("        ")

            Return Space & GroupNumber & vbTab & vbTab & MaxWeight & vbTab & vbTab & MaxSpeed & vbTab & vbTab & Space & Space & T_Desc & Space & vbTab & vbTab & Space & T_Emerg & vbTab & Space & T_Final & vbTab & Space & vbTab & Time

        End Function
        Public GroupNumber As Integer
        Public MaxWeight As Integer
        Public MaxSpeed As Integer
        Public Time As Integer
        Public T_Emerg As Integer
        Public T_Final As Integer
        Public T_Desc As Integer
    End Class
    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        p = frmMain.txtsNumberGrades.Text
        Me.Close()
        frmMain.Group_Number = frmMain.Group_Number + 1
        a = frmMain.Group_Number
        frmMain.txtsGroupNumber.Text = a
        frmMain.txtsNumSections.Text = ""
        frmMain.txtsinitemp.Text = ""
        frmMain.cboMaxTemp.Text = ""
        frmMain.butsImport.Enabled = True
        frmMain.butsGradeLength.Enabled = True
        frmMain.butsClear.Enabled = True
        frmMain.butsCompute.Enabled = False
        frmMain.lstsGradeLength.Items.Clear()
        frmMain.lstsOutputView.Items.Clear()
        frmMain.RichTextBox2.Clear()
        frmMain.lblnPath.Text = ""
        frmMain.TLnew = 0
        frmMain.txtsinitemp.Text = txtNewTemp.Text
        Dim Query As Integer
        If frmMain.Group_Number = p + 1 Then
            Query = MsgBox("Downgrade limit reached; Reset?", vbYesNoCancel, "Alert")
            If Query = vbYes Then
                frmMain.butsReset.PerformClick()
                frmMain.RadioButtonSeperateSlope.Checked = True
            Else
            End If
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles butsReset.Click
        frmMain.butsReset.PerformClick()
    End Sub
    Private Sub frmHorizontalseparate_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        frmMain.butsCurve.Enabled = True
    End Sub
End Class
