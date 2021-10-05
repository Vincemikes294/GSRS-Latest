Option Explicit On
Imports System.ComponentModel
Imports System.IO
Imports Continuous_Slope.GSRS.Net.Common.Scripts
Imports System.Guid
Imports Microsoft.Office.Interop
Public Class frmMain
    Public T_max As Double
    Public Shared Grade() As Double
    Public Shared Length() As Double
    Public Shared Radius() As Double
    Public Shared Superelevation() As Double
    Public Shared Angle() As Double
    Public Shared Anglec() As Double
    Public Shared Gradec() As Double
    Public Shared Lengthc() As Double
    Public Shared Radiusc() As Double
    Public Shared Superelevationc() As Double
    Public i As Integer
    Public j As Integer
    Public k As Integer
    Public j_max As Integer
    Public i_max As Double
    Public W_max As Double
    Public Valid As Boolean
    Public Datagrade As String
    Public DataLength As String
    Public DataRadius As String
    Public DataSuperelevation As String
    Public DataAngle As String
    Public Datagradec As String
    Public DataLengthc As String
    Public DataRadiusc As String
    Public DataSuperelevationc As String
    Public DataAnglec As String
    Public Res() As Array
    Public T_lim(,) As Double
    Public TL As Double
    Public W As Double
    Public V_max As Double
    Public V As Integer
    Public T_0 As Double
    Public T_inf As Double
    Public T_e(,) As Long
    Public HP_eng As Double
    Public K2 As Double
    Public K1 As Double
    Public F_drag As Double
    Public Theta As Double
    Public L As Double
    Public HP_b As Double
    Public T_f(,) As Double
    Public Vs As Double
    Public T_lim_s As Integer
    Public T_f_s As Double
    Public T_e_s As Double
    Public T_lims As Double
    Public W_Maxinput As String
    Public V_Maxinput As String
    Public T_0_input As String
    Public T_inf_input As String
    Public T_max_input As String
    Public Ts_e As Integer
    Public Ts_f As Integer
    Public p As Integer
    Public Group_Number As String
    Public N_secteion As String
    Public TLnew As Double
    Public a As Integer
    Public Grades_max As String
    Public Grades_maxinput As String
    Public Sections_max As String
    Public Sections_maxinput As String
    Public Vsf() As Integer
    Public Vro() As Integer
    Public Vmin() As Integer
    Public Vsfs() As Integer
    Public Vros() As Integer
    Public Vmins() As Integer
    Public Vi As Integer
    Public skidding As Double
    Public rollover As Double
    Public Sidefrictionfactor As Integer = 0
    Public rolloverthreshold As Integer = 0
    Public Property ExcelReaderFactory As Object
    Public Property ExcelDataReader As Object

    'Variables to store Unique File Name - 05.27.21
    Public LoggedOnUser As String = ""
    Public UserFirstName As String = ""
    Public UserLastName As String = ""
    Public UserOperation As String = "Save2Table"

    Public CUniqueId As String = ""
    Public SUniqueId As String = ""

    Private Sub cboMaxTemp_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        If cboMaxTemp.Text = "500" Then
            T_max = 500
        ElseIf cboMaxTemp.Text = "530" Then
            T_max = 530
        End If
    End Sub
    Private Sub butGradeLength_Click(sender As System.Object, e As System.EventArgs) Handles butGradeLength.Click
        If txtNumSections.Text = "" Or IsNumeric(txtNumSections.Text) = False Then
            MsgBox("Please Enter number of segments")
            txtNumSections.Text = ""
            butCompute.Enabled = False
            butGradeLength.Enabled = True
        ElseIf CInt(txtNumSections.Text) > 6 Then
            If MsgBox("Would you like to import segment data?", vbYesNo) = MsgBoxResult.Yes Then
                butImport.PerformClick()
                Exit Sub
            Else
                lstGradeLength.Items.Add("Grade(In Radians)" & vbTab & "Length(In Miles)" & vbTab & vbTab & "Radius(In Feet)" & vbTab & vbTab & "Super-elevation(In Decimal)" & vbTab & vbTab & "Angle (In Degrees)")
                For Me.i = 1 To txtNumSections.Text

                    ReDim Preserve Grade(i)
                    ReDim Preserve Length(i)
                    ReDim Preserve Radius(i)
                    ReDim Preserve Superelevation(i)
                    ReDim Preserve Angle(i)

                    Datagrade = (InputBox("Enter Decimal Grade " & i & " in Radians"))


                    Do While String.IsNullOrEmpty(Datagrade) Or IsNumeric(Datagrade) = False Or Datagrade >= "1" Or Datagrade < "0"
                        MessageBox.Show("Please enter a positive Numeric Value less than 1")
                        Datagrade = (InputBox("Enter Decimal Grade " & i & " in Radians"))
                    Loop
                    Grade(i) = Datagrade


                    DataLength = (InputBox("Enter Length " & i & " in Miles"))

                    Do While String.IsNullOrEmpty(DataLength) Or IsNumeric(DataLength) = False Or DataLength <= "0"
                        MessageBox.Show("Please enter a Numeric Value greater than 0")
                        DataLength = (InputBox("Enter Length " & i & " in Miles"))
                    Loop
                    Length(i) = DataLength


                    DataRadius = (InputBox("Enter Radius " & i & " in Feet"))

                    Do While String.IsNullOrEmpty(DataRadius) Or IsNumeric(DataRadius) = False Or DataRadius <= "0"
                        MessageBox.Show("Please enter a Numeric Value greater than 0")
                        DataRadius = (InputBox("Enter Radius " & i & " in Feet"))
                    Loop
                    Radius(i) = DataRadius


                    DataSuperelevation = (InputBox("Enter Decimal Super-elevation " & i))

                    Do While String.IsNullOrEmpty(DataSuperelevation) Or IsNumeric(DataSuperelevation) = False Or DataSuperelevation >= "1" Or DataSuperelevation < "0"
                        MessageBox.Show("Please enter a non-negative Numeric Value less than 1")
                        DataSuperelevation = (InputBox("Enter Decimal Super-elevation " & i))
                    Loop
                    Superelevation(i) = DataSuperelevation

                    DataAngle = (InputBox("Enter Radius Angle " & i))

                    Do While String.IsNullOrEmpty(DataAngle) Or IsNumeric(DataAngle) = False Or DataAngle <= "0"
                        MessageBox.Show("Please enter a Numeric Value greater than 0")
                        DataAngle = (InputBox("Enter Radius Angle in Degrees " & i))
                    Loop
                    Angle(i) = DataAngle

                    lstGradeLength.Items.Add(Grade(i) & vbTab & vbTab & vbTab & Length(i) & vbTab & vbTab & vbTab & Radius(i) & vbTab & vbTab & vbTab & Superelevation(i) & vbTab & vbTab & vbTab & Angle(i) & vbCrLf)

                Next
                butCompute.Enabled = True
            End If
            butImport.Enabled = True

        ElseIf CInt(txtNumSections.Text) <= 6 Then

            lstGradeLength.Items.Add("Grade(In Radians)" & vbTab & "Length(In Miles)" & vbTab & vbTab & "Radius(In Feet)" & vbTab & vbTab & "Super-elevation(In Decimal)" & vbTab & vbTab & "Angle of Radius (In Degrees)")
            For Me.i = 1 To txtNumSections.Text

                ReDim Preserve Grade(i)
                ReDim Preserve Length(i)
                ReDim Preserve Radius(i)
                ReDim Preserve Superelevation(i)
                ReDim Preserve Angle(i)

                Datagrade = (InputBox("Enter Decimal Grade " & i & " in Radians"))


                Do While String.IsNullOrEmpty(Datagrade) Or IsNumeric(Datagrade) = False Or Datagrade >= "1" Or Datagrade < "0"
                    MessageBox.Show("Please enter a positive Numeric Value less than 1")
                    Datagrade = (InputBox("Enter Decimal Grade " & i & " in Radians"))
                Loop
                Grade(i) = Datagrade




                DataLength = (InputBox("Enter Length " & i & " in Miles"))

                Do While String.IsNullOrEmpty(DataLength) Or IsNumeric(DataLength) = False Or DataLength <= "0"
                    MessageBox.Show("Please enter a positive Numeric Value")
                    DataLength = (InputBox("Enter Length " & i & " in Miles"))
                Loop
                Length(i) = DataLength


                DataRadius = (InputBox("Enter Radius " & i & " in Feet"))

                Do While String.IsNullOrEmpty(DataRadius) Or IsNumeric(DataRadius) = False Or DataRadius <= "0"
                    MessageBox.Show("Please enter a positive Numeric Value")
                    DataRadius = (InputBox("Enter Radius " & i & " in Feet"))
                Loop
                Radius(i) = DataRadius


                DataSuperelevation = (InputBox("Enter Decimal Super-elevation " & i))

                Do While String.IsNullOrEmpty(DataSuperelevation) Or IsNumeric(DataSuperelevation) = False Or DataSuperelevation >= "1" Or DataSuperelevation < "0"
                    MessageBox.Show("Please enter a positive Numeric Value less than 1")
                    DataSuperelevation = (InputBox("Enter Decimal Super-elevation " & i))
                Loop
                Superelevation(i) = DataSuperelevation

                DataAngle = (InputBox("Enter Radius Angle in Degrees " & i))

                Do While String.IsNullOrEmpty(DataAngle) Or IsNumeric(DataAngle) = False Or DataAngle <= "0"
                    MessageBox.Show("Please enter a Numeric Value greater than 0")
                    DataAngle = (InputBox("Enter Radius Angle in Degrees" & i))
                Loop
                Angle(i) = DataAngle

                lstGradeLength.Items.Add(Grade(i) & vbTab & vbTab & vbTab & Length(i) & vbTab & vbTab & vbTab & Radius(i) & vbTab & vbTab & vbTab & Superelevation(i) & vbTab & vbTab & vbTab & Angle(i) & vbCrLf)

            Next
            butCompute.Enabled = True
        End If
        butImport.Enabled = True
    End Sub
    Private Sub butCompute_Click(sender As System.Object, e As System.EventArgs) Handles butCompute.Click
        butSave.Enabled = True
        butFilter.Enabled = True
        butCurve.Enabled = True
        butCompute.Enabled = False
        butGradeLength.Enabled = False

        If IsNumeric(txtMaxWeight.Text) And txtMaxWeight.Text <> "" And txtMaxWeight.Text > "0" Then
            W_max = txtMaxWeight.Text
        Else : MsgBox("Please Enter a positive numeric value for Maximum Weight")
            W_Maxinput = (InputBox("Enter a positive numeric value for Maximum Weight"))
            Do While IsNumeric(W_Maxinput) = False Or W_Maxinput < "0"
                MessageBox.Show("Please enter a positive numeric Value for Maximum Weight")
                W_Maxinput = (InputBox("Enter a positive numeric value for Maximum Weight"))
            Loop
            W_max = CDbl(W_Maxinput)
            txtMaxWeight.Text = W_max
        End If
        If IsNumeric(txtMaxSpeed.Text) And txtMaxSpeed.Text <> "" And txtMaxSpeed.Text > "0" Then
            V_max = txtMaxSpeed.Text
        Else : MsgBox("Please Enter a positive numeric value for Maximum Speed")
            V_Maxinput = (InputBox("Enter a positive numeric value for Maximum Speed"))
            Do While IsNumeric(V_Maxinput) = False Or V_Maxinput < "0"
                MessageBox.Show("Please enter a positive numeric Value for Maximum Speed")
                V_Maxinput = (InputBox("Enter a positive numeric value for Maximum Speed"))
            Loop
            V_max = CDbl(V_Maxinput)
            txtMaxSpeed.Text = V_max
        End If

        If IsNumeric(txtinitemp.Text) Then
            If txtinitemp.Text >= 90 Then
                T_0 = txtinitemp.Text
                txtinitemp.Text = T_0
            ElseIf txtinitemp.Text < 90 Then
                T_0 = 150
                txtinitemp.Text = T_0
            End If
        Else : MsgBox("Enter a numeric value greater or equal to 90 for Initial Temperature")
            T_0_input = (InputBox("Enter a numeric value greater or equal to 90 for Initial Temperature", "Alert", "150"))
            Do While IsNumeric(T_0_input) = False Or T_0_input = ""
                MessageBox.Show("Enter a numeric value greater or equal to 90 for Initial Temperature")
                T_0_input = (InputBox("Enter a numeric value greater or equal to 90 for Initial Temperature", "Alert", "150"))
            Loop
            If T_0_input >= 90 Then
                T_0 = T_0_input
                txtinitemp.Text = T_0
            ElseIf T_0_input < 90 Then
                T_0 = 150
                txtinitemp.Text = T_0
            End If
        End If

        If IsNumeric(txtambient.Text) Then
            If txtambient.Text >= 90 Then
                T_inf = 90
                txtambient.Text = T_inf
            ElseIf txtambient.Text < 90 Then
                T_inf = 90
                txtambient.Text = T_inf
            End If
        Else : MsgBox("Enter a value of 90 for the Ambient Temperature")
            T_inf_input = (InputBox("Enter a value of 90 for the Ambient Temperature", "Alert", "90"))
            Do While IsNumeric(T_inf_input) = False Or T_inf_input = ""
                MessageBox.Show("Enter a value of 90 for the Ambient Temperature")
                T_inf_input = (InputBox("Enter a value of 90 for the Ambient Temperature", "Alert", "90"))
            Loop
            If T_inf_input >= 90 Then
                T_inf = 90
                txtambient.Text = T_inf
            ElseIf T_inf_input < 90 Then
                T_inf = 90
                txtambient.Text = T_inf
            End If
        End If
        If IsNumeric(cboMaxTemp.Text) And cboMaxTemp.Text <> "" And cboMaxTemp.Text = "500" Or cboMaxTemp.Text = "530" Then
            T_max = cboMaxTemp.Text
        Else : MsgBox("Please input " & "500 or 530 for Maximum Brake Temperature")
            T_max_input = (InputBox("Input " & "500 or 530 for Maximum Brake Temperature"))
            Do While IsNumeric(T_max_input) = False Or T_max_input <> "500" And T_max_input <> "530"
                MessageBox.Show("Input " & "500 or 530 for Maximum Brake Temperature")
                T_max_input = (InputBox("Input " & "500 or 530 for Maximum Brake Temperature"))
            Loop
            T_max = CDbl(T_max_input)
            cboMaxTemp.Text = T_max
        End If
        'Me.lstOutputView.Items.Add("Max Weight (lb) " & "    Max Speed (mph) " & "     T_Desc (F) " & "           T_Emerg (F) " & "        T_Final (F)" & "                Time (min) " & vbCrLf & vbCrLf)
        'Me.lstOutputView.Items.Add("Max Weight (lb)" & vbTab & vbTab & "Max Speed (mph)" & vbTab & vbTab & "T_Desc (F)" & vbTab & vbTab & "T_Emerg (F)" & vbTab & vbTab & "T_Final (F)" & vbTab & vbTab & "Time (min)" & vbCrLf & vbCrLf)
        Me.lstOutputView.Items.Add("Max Weight (lb)" & vbTab & "Max Speed (mph)" & vbTab & "T_Desc (F)" & vbTab & "T_Emerg (F)" & vbTab & "T_Final (F)" & vbTab & "Time (min)" & vbCrLf & vbCrLf)

        'Computations

        j_max = W_max / 5000

        'This check is to allow only less than 7 segments
        'If CInt(txtNumSections.Text) <= 7 Then

        'End If

        For Me.i = 1 To CInt(txtNumSections.Text)
            TL += Length(i)
        Next

        For Me.j = 0 To j_max
            W = W_max - j * 5000

            For Me.V = 1 To V_max

                T_0 = CDbl(txtinitemp.Text) 'initial brake temperature
                T_inf = CDbl(txtambient.Text) 'ambient temperature

                ReDim T_e(V, 1)
                T_e(V, 1) = (0.000000311) * W * (V ^ 2) 'temperature from emergency stopping
                HP_eng = 63.3 'Engine brake force
                K2 = 1 / (-0.9078 + 0.0621 * V) 'Heat transfer parameter
                K1 = 1.5 * (1.1852 + 0.0331 * V) 'Diffusivity constant
                F_drag = 459.35 + 0.132 * (V ^ 2) 'Drag forces

                For Me.i = 1 To txtNumSections.Text

                    Theta = Grade(i)
                    L = Length(i)
                    HP_b = (W * Theta - F_drag) * (V / 375) - 63.3 'power into brakes
                    ReDim T_f(V, 1)
                    T_f(V, 1) = T_0 + (T_inf - T_0 + K2 * HP_b) * (1 - Math.Exp(-K1 * (L / V)))
                    T_0 = T_f(V, 1)

                Next

                ReDim T_lim(V, 1)
                T_lim(V, 1) = T_f(V, 1) + T_e(V, 1)    'limiting brake temperature


                Vs = V
                T_lim_s = CInt(T_lim(Vs, 1))
                T_f_s = CInt(T_f(Vs, 1))
                T_e_s = CInt(T_e(Vs, 1))


                lstOutputView.Items.Add(W & vbTab & vbTab & Vs & vbTab & vbTab & T_f_s & vbTab & vbTab & T_e_s & vbTab & vbTab & T_lim_s & vbTab & vbTab & CInt(TL * 60 / Vs) & vbCrLf)
                'lstOutputView.Items.Add(W & vbTab & Vs & vbTab & vbTab & T_f_s & vbTab & vbTab & T_e_s & vbTab & vbTab & T_lim_s & vbTab & vbTab & CInt(TL * 60 / Vs) & vbCrLf)

            Next
        Next

        If txtNumSections.Text <> "" And lstGradeLength.Items.Count <> 0 Then
            butTempProfile.Enabled = True
        Else
            butTempProfile.Enabled = False
        End If

    End Sub
    Private Sub frmGSRS(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        RadioButtonContinuousSlope.Checked = True
        butSave.Enabled = False
        butFilter.Enabled = False
        butsSave.Enabled = False
        butsFilter.Enabled = False

    End Sub
    Private Sub butSave_Click(sender As System.Object, e As System.EventArgs)

    End Sub
    Private Sub butFilter_Click(sender As System.Object, e As System.EventArgs)

    End Sub
    Private Sub butSave_Click_1(sender As System.Object, e As System.EventArgs) Handles butSave.Click
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
                For Each o As Object In lstOutputView.Items
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

        End Try
    End Sub


    Private Sub butFilter_Click_1(sender As System.Object, e As System.EventArgs) Handles butFilter.Click
        Dim header As String = lstOutputView.Items(0)

        Dim data As New List(Of DataValue)
        ' Skip the header row by starting at 1:
        For i As Integer = 1 To lstOutputView.Items.Count - 1
            data.Add(New DataValue(lstOutputView.Items(i)))
        Next

        Dim results = From dv In data
        lstOutputView.Items.Clear()
        lstOutputView.Items.Add(header)


        For Each row In results
            If row.T_Final < T_max Then
                lstOutputView.Items.Add(row.ToString)

            End If
        Next


        Dim header1 As String = lstOutputView.Items(0)

        Dim data1 As New List(Of DataValue)
        ' Skip the header row by starting at 1:
        For i As Integer = 1 To lstOutputView.Items.Count - 1
            data1.Add(New DataValue(lstOutputView.Items(i)))
        Next

        Dim finalresults = From dv In data1
                           Order By dv.MaxWeight Descending, dv.MaxSpeed Descending
                           Group dv By dv.MaxWeight Into g = Group
                           Select g.First

        Dim V_max = CInt(Me.txtMaxSpeed.Text)
        lstOutputView.Items.Clear()
        lstOutputView.Items.Add(header)


        For Each row In finalresults
            lstOutputView.Items.Add(row.ToString)
            If row.MaxSpeed = V_max Then
                Exit For
            End If
        Next

        'Try
        '    Dim myHelper As New HelperClass
        '    Dim MaxWeight As String = ""
        '    Dim MaxSpeed As String = ""
        '    Dim TDesc As String = ""
        '    Dim TEmerg As String = ""
        '    Dim TFinal As String = ""
        '    Dim Time As String = ""
        '    Dim SlopeTable As New DataTable("Slope")


        '    Dim SlopeColumn As DataColumn = SlopeTable.Columns.Add("Unique_Id", GetType(String))
        '    SlopeColumn.AllowDBNull = False
        '    SlopeColumn.Unique = False

        '    SlopeTable.Columns.Add("Max_Weight", GetType(Integer))
        '    SlopeTable.Columns.Add("Max_Speed", GetType(Integer))
        '    SlopeTable.Columns.Add("T_Desc", GetType(Integer))
        '    SlopeTable.Columns.Add("T_Emerg", GetType(Integer))
        '    SlopeTable.Columns.Add("T_Final", GetType(Integer))
        '    SlopeTable.Columns.Add("Time", GetType(Integer))

        '    'Write the filter list to a table

        '    For Each row In finalresults

        '        lstOutputView.Items.Add(row.ToString)

        '        If row.MaxSpeed = V_max Then
        '            'Insert the row in to the table

        '            MaxWeight = row.MaxWeight.ToString
        '            MaxSpeed = row.MaxSpeed.ToString
        '            TDesc = row.T_Desc.ToString
        '            TEmerg = row.T_Emerg.ToString
        '            TFinal = row.T_Final.ToString
        '            Time = row.Time.ToString

        '            SlopeTable.Rows.Add(UniqueId, MaxWeight, MaxSpeed, TDesc, TEmerg, TFinal, Time)
        '            MaxWeight = ""
        '            MaxSpeed = ""
        '            TDesc = ""
        '            TEmerg = ""
        '            TFinal = ""
        '            Time = ""

        '            myHelper.InsertSlopeData(SlopeTable, "ContinuousSlopeFilter")

        '            Exit For
        '        End If
        '    Next

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try

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
                MessageBox.Show("Invalid Input:  Not enough values.")
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

    Private Sub RadioButtonContinuousSlope_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButtonContinuousSlope.CheckedChanged
        GroupContinuousSlope.Enabled = True
        GroupSeparateSlope.Enabled = True
        butsReset.PerformClick()
        GroupSeparateSlope.Enabled = False

    End Sub

    Private Sub RadioButtonSeperateSlope_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButtonSeperateSlope.CheckedChanged
        GroupSeparateSlope.Enabled = True
        GroupContinuousSlope.Enabled = True
        butReset.PerformClick()
        GroupContinuousSlope.Enabled = False
        Group_Number = "1"
        txtsGroupNumber.Text = Group_Number
        a = txtsGroupNumber.Text
        'Init the User Operation
        UserOperation = "Save2Table"
    End Sub
    Private Sub butReset_Click(sender As System.Object, e As System.EventArgs) Handles butReset.Click
        txtNumSections.Text = ""
        txtambient.Text = ""
        txtMaxSpeed.Text = ""
        txtMaxWeight.Text = ""
        txtinitemp.Text = ""
        cboMaxTemp.Text = ""
        butImport.Enabled = True
        butGradeLength.Enabled = True
        butClear.Enabled = True
        butCompute.Enabled = False
        butTempProfile.Enabled = False
        lstGradeLength.Items.Clear()
        lstOutputView.Items.Clear()
        RichTextBox1.Clear()
        lblPath.Text = ""
        TL = 0

    End Sub

    Private Sub GroupBox10_Enter(sender As System.Object, e As System.EventArgs) Handles GroupBox10.Enter

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub butsSave_Click(sender As System.Object, e As System.EventArgs) Handles butsSave.Click
        Try
            Dim myHelper As New HelperClass
            Dim NumberGrades As String = ""
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
                For Each o As Object In lstsOutputView.Items
                    'sb.AppendLine(o)

                    Dim ListViewRow As String = o.ToString
                    ListViewRow = ListViewRow.Replace(vbCrLf, "").Trim()
                    Debug.WriteLine(ListViewRow)

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
                        NumberGrades = txtsNumSections.Text
                        SlopeTable.Rows.Add(SUniqueId, NumberGrades, GroupNumber, MaxWeight, MaxSpeed, TDesc, TEmerg, TFinal, Time)
                        NumberGrades = ""
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
                butsSave.Text = "Export"
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
                        butsSave.Text = "Save"
                    End If

                End If

            End If


        Catch ex As Exception

        End Try
    End Sub
    Private Sub butsClear_Click(sender As System.Object, e As System.EventArgs) Handles butsClear.Click
        RichTextBox2.Clear()
        lstsGradeLength.Items.Clear()
        lstsOutputView.Items.Clear()
        butsGradeLength.Enabled = True
        butsImport.Enabled = True
        txtsNumSections.Text = ""
        lblnPath.Text = ""
        butFilter.Enabled = False
        TLnew = 0
    End Sub
    Private Sub butsCompute_Click(sender As System.Object, e As System.EventArgs) Handles butsCompute.Click
        butsSave.Enabled = True
        butsFilter.Enabled = True
        butsCurve.Enabled = True
        butsCompute.Enabled = False
        butsGradeLength.Enabled = False
        Dim numgradesoutput As Integer
        If IsNumeric(txtsNumberGrades.Text) And txtsNumberGrades.Text <> "" And txtsNumberGrades.Text > "0" And (Integer.TryParse(txtsNumberGrades.Text, numgradesoutput)) = True Then
            Grades_max = txtsNumberGrades.Text
        Else : MsgBox("Please Enter a positive integer value for Number of Grades", , "Seperate Slope")
            Grades_maxinput = (InputBox("Enter a positive integer value for Number of Grades", "Seperate Slope"))
            Do While IsNumeric(Grades_maxinput) = False Or Grades_maxinput < "0" Or (Integer.TryParse(Grades_maxinput, numgradesoutput)) = False
                MessageBox.Show("Please enter a positive integer value for Number of Grades", "Seperate Slope")
                Grades_maxinput = (InputBox("Enter a positive integer value for Number of Grades", "Seperate Slope"))
            Loop
            Grades_max = Grades_maxinput
        End If
        txtsNumberGrades.Text = Grades_max

        If IsNumeric(txtsMaxWeight.Text) And txtsMaxWeight.Text <> "" And txtsMaxWeight.Text > "0" Then
            W_max = txtsMaxWeight.Text
        Else : MsgBox("Please Enter a positive numeric value for Maximum Weight", , "Seperate Slope")
            W_Maxinput = (InputBox("Enter a positive numeric value for Maximum Weight", "Seperate Slope"))
            Do While IsNumeric(W_Maxinput) = False Or W_Maxinput < "0"
                MessageBox.Show("Please enter a positive numeric Value for Maximum Weight", "Seperate Slope")
                W_Maxinput = (InputBox("Enter a positive numeric value for Maximum Weight", "Seperate Slope"))
            Loop
            W_max = CDbl(W_Maxinput)
        End If
        txtsMaxWeight.Text = W_max
        If IsNumeric(txtsMaxSpeed.Text) And txtsMaxSpeed.Text <> "" And txtsMaxSpeed.Text > "0" Then
            V_max = txtsMaxSpeed.Text
        Else : MsgBox("Please Enter a positive numeric value for Maximum Speed", , "Seperate Slope")
            V_Maxinput = (InputBox("Enter a positive numeric value for Maximum Speed", "Seperate Slope"))
            Do While IsNumeric(V_Maxinput) = False Or V_Maxinput < "0"
                MessageBox.Show("Please enter a positive numeric Value for Maximum Speed", "Seperate Slope")
                V_Maxinput = (InputBox("Enter a positive numeric value for Maximum Speed", "Seperate Slope"))
            Loop
            V_max = CDbl(V_Maxinput)
        End If
        txtsMaxSpeed.Text = V_max
        If IsNumeric(txtsinitemp.Text) Then
            If txtsinitemp.Text >= 90 Then
                T_0 = txtsinitemp.Text
                txtsinitemp.Text = T_0
            ElseIf txtsinitemp.Text < 90 Then
                T_0 = "150"
                txtsinitemp.Text = T_0
            End If
        Else : MsgBox("Enter a numeric value greater Or equal to 90 for Initial Temperature", , "Seperate Slope")
            T_0_input = (InputBox("Enter a numeric value greater Or equal to 90 for Initial Temperature", "Alert", "150"))
            Do While IsNumeric(T_0_input) = False Or T_0_input = ""
                MessageBox.Show("Enter a numeric value greater Or equal to 90 for Initial Temperature", "Seperate Slope")
                T_0_input = (InputBox("Enter a numeric value greater Or equal to 90 for Initial Temperature", "Alert", "150"))
            Loop
            If T_0_input >= 90 Then
                T_0 = T_0_input
                txtsinitemp.Text = T_0
            ElseIf T_0_input < 90 Then
                T_0 = "150"
                txtsinitemp.Text = T_0
            End If
        End If

        If IsNumeric(txtsiniambient.Text) Then
            If txtsiniambient.Text >= 90 Then
                T_inf = "90"
                txtsiniambient.Text = T_inf
            ElseIf txtsiniambient.Text < 90 Then
                T_inf = "90"
                txtsiniambient.Text = T_inf
            End If
        Else : MsgBox("Enter a value of 90 for the Ambient Temperature", , "Seperate Slope")
            T_inf_input = (InputBox("Enter a value of 90 for the Ambient Temperature", "Alert", "90"))
            Do While IsNumeric(T_inf_input) = False Or T_inf_input = ""
                MessageBox.Show("Enter a value of 90 for the Ambient Temperature", "Seperate Slope")
                T_inf_input = (InputBox("Enter a value of 90 for the Ambient Temperature", "Alert", "90"))
            Loop
            If T_inf_input >= 90 Then
                T_inf = "90"
                txtsiniambient.Text = T_inf
            ElseIf T_inf_input < 90 Then
                T_inf = "90"
                txtsiniambient.Text = T_inf
            End If
        End If
        If IsNumeric(cbosMaxTemp.Text) And cbosMaxTemp.Text <> "" And cbosMaxTemp.Text = "500" Or cbosMaxTemp.Text = "530" Then
            T_max = cbosMaxTemp.Text
        Else : MsgBox("Please input " & "500 Or 530 for Maximum Temperature", , "Seperate Slope")
            T_max_input = (InputBox("Input " & "500 Or 530 for Maximum Temperature", "Seperate Slope"))
            Do While IsNumeric(T_max_input) = False Or T_max_input <> "500" And T_max_input <> "530"
                MessageBox.Show("Input " & "500 Or 530 for Maximum Temperature", "Seperate Slope")
                T_max_input = (InputBox("Input " & "500 Or 530 for Maximum Temperature", "Seperate Slope"))
            Loop
            T_max = CDbl(T_max_input)
        End If
        cbosMaxTemp.Text = T_max
        lstsOutputView.Items.Add("Group" & "        Max Weight (lb)" & "           Max Speed (mph)" & "                         T_Desc (F)" & "               T_Emerg (F)" & "      T_Final (F)" & "          Time (min) " & vbCrLf & vbCrLf)

        a = txtsGroupNumber.Text
        Group_Number = a
        p = txtsNumberGrades.Text
        'Computations

        i_max = W_max / 5000
        N_secteion = txtsNumSections.Text
        Dim input_info(,) As Double = New Double(CInt(N_secteion - 1), 1) {}
        For Me.i = 1 To N_secteion
            Dim intIndexGrade As New Integer
            Dim intIndexLength As New Integer

            For intIndexGrade = 1 To N_secteion
                input_info(intIndexGrade - 1, 0) = Gradec(intIndexGrade)
            Next
            For intIndexLength = 1 To N_secteion
                input_info(intIndexLength - 1, 1) = Lengthc(intIndexLength)

            Next
        Next

        If a Mod 2 = 1 Then
            T_lim_s = Group1(input_info, V_max, W_max, T_0, Group_Number)
        End If

        If a Mod 2 = 0 Then
            T_lim_s = CalVel(input_info, W_max, i_max, V_max, T_max, T_0, Group_Number)
        End If

    End Sub
    Private Sub butClear_Click(sender As System.Object, e As System.EventArgs) Handles butClear.Click
        RichTextBox1.Clear()
        lstGradeLength.Items.Clear()
        lstOutputView.Items.Clear()
        butGradeLength.Enabled = True
        butImport.Enabled = True
        txtNumSections.Text = ""
        lblPath.Text = ""
        butTempProfile.Enabled = False
        butFilter.Enabled = False
        TL = 0
    End Sub
    Private Sub Button1_Click_1(sender As System.Object, e As System.EventArgs) Handles butImport.Click
        Dim MyFileDialog As New System.Windows.Forms.OpenFileDialog

        ' Configure the dialog to show both text and excel files
        ' Set its title and set the filename field blank for the moment.
        MyFileDialog.Filter = "(*.xlsx)|*.xlsx"
        MyFileDialog.Title = " Open an excel file"
        MyFileDialog.FileName = ""
        ' Show the dialog and see if the user pressed ok.

        If MyFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            ' Check to see if they selected a file and that it exists.

            If File.Exists(MyFileDialog.FileName) Then

                Dim strFile As String = MyFileDialog.FileName
                Dim textextension As String
                Dim testFile As System.IO.FileInfo
                Try

                    ' Setup a file stream reader to read the excel file.
                    textextension = Path.GetExtension(strFile)
                    If textextension = ".xlsx" Then
                        Dim oExcel As Object = CreateObject("Excel.Application")
                        Dim oBook As Object = oExcel.Workbooks.Open(strFile)
                        Dim oSheet As Object = oBook.Worksheets(1)
                        Dim i As Integer
                        Dim cellA As String
                        Dim cellB As String
                        Dim cellC As String
                        Dim cellD As String
                        Dim cellE As String
                        lstGradeLength.Items.Add("Grade (in Radians)" & vbTab & "   Length (in Miles)" & vbTab & vbTab & "Radius (in Feet)" & vbTab & vbTab & "Super-elevation (in Decimal)" & vbTab & vbTab & "Angle (in Degrees)")
                        For i = 0 To AscW(lstGradeLength.Items.Count.ToString()(i = i + 1)) - 1

                            cellA = "A" & Convert.ToString(i + 1)
                            cellB = "B" & Convert.ToString(i + 1)
                            cellC = "C" & Convert.ToString(i + 1)
                            cellD = "D" & Convert.ToString(i + 1)
                            cellE = "E" & Convert.ToString(i + 1)
                            cellA = oSheet.Range(cellA).Value
                            cellB = oSheet.Range(cellB).Value
                            cellC = oSheet.Range(cellC).Value
                            cellD = oSheet.Range(cellD).Value
                            cellE = oSheet.Range(cellE).Value
                            If cellA = "" And cellB = "" And cellC = "" And cellD = "" And cellE = "" Then
                                Exit For
                            Else
                                RichTextBox1.AppendText(cellA & " " & cellB & " " & cellC & " " & cellD & " " & cellE & vbCrLf)
                            End If
                        Next
                        oExcel.Quit()
                    End If

                Catch ex As FileNotFoundException
                    ' If the file was not found, tell the user.
                    MessageBox.Show("File was Not found. Please try again.")
                End Try
                Try
                    ' Setup a file stream reader to read the excel file.
                    textextension = Path.GetExtension(strFile)
                    If textextension = ".xlsx" Then

                        Dim m As Integer
                        For m = 1 To CInt(UBound(RichTextBox1.Lines))
                            ReDim Preserve Grade(m)
                            ReDim Preserve Length(m)
                            ReDim Preserve Radius(m)
                            ReDim Preserve Superelevation(m)
                            ReDim Preserve Angle(m)
                            Grade(m) = RichTextBox1.Lines(m - 1).Split(" ").First
                            Length(m) = RichTextBox1.Lines(m - 1).Split(" "c)(1)
                            Radius(m) = RichTextBox1.Lines(m - 1).Split(" "c)(2)
                            Superelevation(m) = RichTextBox1.Lines(m - 1).Split(" ")(3)
                            Angle(m) = RichTextBox1.Lines(m - 1).Split(" ")(4)
                            lstGradeLength.Items.Add(Grade(m) & vbTab & vbTab & vbTab & Length(m) & vbTab & vbTab & vbTab & Radius(m) & vbTab & vbTab & vbTab & Superelevation(m) & vbTab & vbTab & vbTab & Angle(m) & vbCrLf)
                            butGradeLength.Enabled = False
                        Next
                        testFile = My.Computer.FileSystem.GetFileInfo(strFile)

                        lblPath.Text = testFile.FullName

                        txtNumSections.Text = lstGradeLength.Items.Count - 1
                    End If
                Catch ex As Exception
                    ' If the data is in an incorrect format, tell the user.
                    MessageBox.Show("Data in incorrect format")
                    lstGradeLength.Items.Clear()
                End Try
            End If
        Else
            txtNumSections.Text = ""
            butImport.Enabled = True
            butGradeLength.Enabled = True
            butClear.Enabled = True
            Exit Sub
        End If

        butImport.Enabled = False
        butCompute.Enabled = True
    End Sub
    ' Function to calculate V
    Function Group1(input_info, V_max, W_max, T_0, Group_Number)
        Dim Space = ("        ")
        W = W_max
        V = V_max
        For Me.i = 1 To txtsNumSections.Text
            TLnew += Lengthc(i)
        Next
        For Me.j = 0 To (V_max - 15) / 5
            V = V_max - 5 * j
            T_0 = txtsinitemp.Text
            T_inf = txtsiniambient.Text  'ambient temperature

            Ts_e = (0.000000311) * W * (V ^ 2) ' temperature from emergency stopping
            HP_eng = 63.3 'engine brake force
            K2 = 1 / (0.1602 + 0.0078 * V) 'heat transfer parameter
            K1 = 1.5 * (1.1852 + 0.0331 * V) 'diffusivity constant
            F_drag = 459.35 + 0.132 * (V ^ 2) 'drag forces

            For Me.i = 1 To txtsNumSections.Text
                Theta = Gradec(i)
                L = Lengthc(i)
                HP_b = (W * Theta - F_drag) * (V / 375) - 63.3 'power into brakes
                Ts_f = T_0 + (T_inf - T_0 + K2 * HP_b) * (1 - Math.Exp(-K1 * (L / V)))
                T_0 = Ts_f
            Next
            T_lims = Ts_f + Ts_e 'limiting brake temperature

            lstsOutputView.Items.Add(Space & Group_Number & vbTab & vbTab & W & vbTab & vbTab & Space & V & vbTab & vbTab & Space & vbTab & Ts_f & vbTab & Space & vbTab & Ts_e & vbTab & Space & T_lims & vbTab & vbTab & Space & CInt(TLnew * 60 / V) & vbCrLf)
        Next


        Me.butsFilter.Enabled = True
    End Function
    Function CalVel(input_info, W_max, i_max, V_max, T_max, T_0, Group_Number)
        Dim T_f(,) As Double
        Dim T_e(,) As Double
        Dim j_max As Integer
        j_max = W_max / 5000
        For Me.i = 1 To txtsNumSections.Text
            TLnew += Lengthc(i)
        Next

        For Me.j = 0 To j_max
            W = W_max - j * 5000

            For Me.V = 1 To V_max

                T_0 = txtsinitemp.Text 'initial brake temperature
                T_inf = txtsiniambient.Text 'ambient temperature

                ReDim T_e(V, 1)
                T_e(V, 1) = (0.000000311) * W * (V ^ 2) 'temperature from emergency stopping
                HP_eng = 63.3 'Engine brake force
                K2 = 1 / (0.1602 + 0.0078 * V) 'Heat transfer parameter
                K1 = 1.5 * (1.1852 + 0.0331 * V) 'Diffusivity constant
                F_drag = 459.35 + 0.132 * (V ^ 2) 'Drag forces

                For Me.i = 1 To txtsNumSections.Text

                    Theta = Gradec(i)
                    L = Lengthc(i)
                    HP_b = (W * Theta - F_drag) * (V / 375) - 63.3 'power into brakes
                    ReDim T_f(V, 1)
                    T_f(V, 1) = T_0 + (T_inf - T_0 + K2 * HP_b) * (1 - Math.Exp(-K1 * (L / V)))
                    T_0 = T_f(V, 1)

                Next

                ReDim T_lim(V, 1)

                T_lim(V, 1) = T_f(V, 1) + T_e(V, 1)    'limiting brake temperature

                T_lim_s = CInt(T_lim(V, 1))
                T_f_s = CInt(T_f(V, 1))
                T_e_s = CInt(T_e(V, 1))
                Dim Space = ("        ")
                lstsOutputView.Items.Add(Space & Group_Number & vbTab & vbTab & W & vbTab & vbTab & Space & V & vbTab & vbTab & Space & vbTab & T_f_s & vbTab & Space & vbTab & T_e_s & vbTab & Space & T_lim_s & vbTab & vbTab & Space & CInt(TLnew * 60 / V) & vbCrLf)
            Next
        Next

        Me.butsFilter.Enabled = True
    End Function
    Private Sub cbosMaxTemp_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbosMaxTemp.SelectedIndexChanged
        If cbosMaxTemp.Text = "500" Then
            T_max = 500
        ElseIf cbosMaxTemp.Text = "530" Then
            T_max = 530

        End If
    End Sub
    Private Sub butsFilter_Click(sender As System.Object, e As System.EventArgs) Handles butsFilter.Click
        Dim header As String
        Dim data As New List(Of DataValue1)
        Dim results = From dv In data
        Dim header1 As String
        Dim header2 As String
        Dim data1 As New List(Of DataValue1)
        Dim data2 As New List(Of DataValue1)
        Dim finalresults = From dv In data1
        Dim finalresults2 = From dv In data1
        Dim V_max = CInt(Me.txtsMaxSpeed.Text)
        Dim Max_weight = CInt(Me.txtsMaxWeight.Text)
        If a Mod 2 = 0 Then
            header = lstsOutputView.Items(0)

            ' Skip the header row by starting at 1:
            For i As Integer = 1 To lstsOutputView.Items.Count - 1
                data.Add(New DataValue1(lstsOutputView.Items(i)))
            Next

            lstsOutputView.Items.Clear()
            lstsOutputView.Items.Add(header)


            For Each row In results
                If row.T_Final < T_max Then
                    lstsOutputView.Items.Add(row.ToString)

                End If
            Next


            header1 = lstsOutputView.Items(0)

            ' Skip the header row by starting at 1:
            For i As Integer = 1 To lstsOutputView.Items.Count - 1
                data1.Add(New DataValue1(lstsOutputView.Items(i)))
            Next

            finalresults = From dv In data1
                           Order By dv.MaxWeight Descending, dv.MaxSpeed Descending
                           Group dv By dv.MaxWeight Into g = Group
                           Select g.First


            lstsOutputView.Items.Clear()
            lstsOutputView.Items.Add(header)


            For Each row In finalresults
                lstsOutputView.Items.Add(row.ToString)
                If row.MaxSpeed = V_max Then
                    Exit For
                End If
            Next

            MsgBox("Select row for maximum weight",, "Seperate Slope")
            header2 = lstsOutputView.Items(0)

            ' Skip the header row by starting at 1:
            For i As Integer = 1 To lstsOutputView.Items.Count - 1
                data2.Add(New DataValue1(lstsOutputView.Items(i)))
            Next

            finalresults2 = From dv In data2
                            Order By dv.MaxWeight Descending
                            Group dv By dv.MaxWeight Into g = Group
                            Select g.First

            lstsOutputView.Items.Clear()
            lstsOutputView.Items.Add(header)

            For Each row In finalresults2
                lstsOutputView.Items.Add(row.ToString)
                If row.MaxWeight = Max_weight Then
                    txtNewTemp.Text = row.T_Final
                End If
                Exit For
            Next
        End If

        If a Mod 2 = 1 Then
            ' Skip the header row by starting at 1:

            header = lstsOutputView.Items(0)
            For i As Integer = 1 To lstsOutputView.Items.Count - 1
                data.Add(New DataValue1(lstsOutputView.Items(i)))
            Next

            lstsOutputView.Items.Clear()
            lstsOutputView.Items.Add(header)


            For Each row In results
                If row.T_Final < T_max Then
                    lstsOutputView.Items.Add(row.ToString)

                End If
            Next

            ' Skip the header row by starting at 1:
            For i As Integer = 1 To lstsOutputView.Items.Count - 1
                data1.Add(New DataValue1(lstsOutputView.Items(i)))
            Next

            finalresults = From dv In data1
                           Order By dv.MaxWeight Descending, dv.Time Ascending
                           Group dv By dv.MaxWeight Into g = Group
                           Select g.First

            lstsOutputView.Items.Clear()
            lstsOutputView.Items.Add(header)


            For Each row In finalresults

                lstsOutputView.Items.Add(row.ToString)
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
    'Private Sub butsFilter_Click(sender As System.Object, e As System.EventArgs) Handles butsFilter.Click
    '    Dim header As String
    '    Dim data As New List(Of DataValue1)
    '    Dim results = From dv In data
    '    Dim header1 As String
    '    Dim header2 As String
    '    Dim data1 As New List(Of DataValue1)
    '    Dim data2 As New List(Of DataValue1)
    '    Dim finalresults = From dv In data1
    '    Dim finalresults2 = From dv In data1
    '    Dim V_max = CInt(Me.txtsMaxSpeed.Text)
    '    Dim Max_weight = CInt(Me.txtsMaxWeight.Text)
    '    If a Mod 2 = 0 Then
    '        header = lstsOutputView.Items(0)

    '        ' Skip the header row by starting at 1:
    '        For i As Integer = 1 To lstsOutputView.Items.Count - 1
    '            data.Add(New DataValue1(lstsOutputView.Items(i)))
    '        Next

    '        lstsOutputView.Items.Clear()
    '        lstsOutputView.Items.Add(header)


    '        For Each row In results
    '            If row.T_Final < T_max Then
    '                lstsOutputView.Items.Add(row.ToString)

    '            End If
    '        Next


    '        header1 = lstsOutputView.Items(0)

    '        ' Skip the header row by starting at 1:
    '        For i As Integer = 1 To lstsOutputView.Items.Count - 1
    '            data1.Add(New DataValue1(lstsOutputView.Items(i)))
    '        Next

    '        finalresults = From dv In data1
    '                       Order By dv.MaxWeight Descending, dv.MaxSpeed Descending
    '                       Group dv By dv.MaxWeight Into g = Group
    '                       Select g.First


    '        lstsOutputView.Items.Clear()
    '        lstsOutputView.Items.Add(header)


    '        MsgBox("Select row for maximum weight",, "Seperate Slope")
    '        header2 = lstsOutputView.Items(0)

    '        ' Skip the header row by starting at 1:
    '        For i As Integer = 1 To lstsOutputView.Items.Count - 1
    '            data2.Add(New DataValue1(lstsOutputView.Items(i)))
    '        Next

    '        finalresults2 = From dv In data2
    '                        Order By dv.MaxWeight Descending
    '                        Group dv By dv.MaxWeight Into g = Group
    '                        Select g.First

    '        lstsOutputView.Items.Clear()
    '        lstsOutputView.Items.Add(header)

    '        For Each row In finalresults2
    '            lstsOutputView.Items.Add(row.ToString)
    '            If row.MaxWeight = Max_weight Then
    '                txtNewTemp.Text = row.T_Final
    '            End If
    '            Exit For
    '        Next
    '    End If

    '    If a Mod 2 = 1 Then
    '        ' Skip the header row by starting at 1:

    '        header = lstsOutputView.Items(0)
    '        For i As Integer = 1 To lstsOutputView.Items.Count - 1
    '            data.Add(New DataValue1(lstsOutputView.Items(i)))
    '        Next

    '        lstsOutputView.Items.Clear()
    '        lstsOutputView.Items.Add(header)


    '        For Each row In results
    '            If row.T_Final < T_max Then
    '                lstsOutputView.Items.Add(row.ToString)

    '            End If
    '        Next

    '        ' Skip the header row by starting at 1:
    '        For i As Integer = 1 To lstsOutputView.Items.Count - 1
    '            data1.Add(New DataValue1(lstsOutputView.Items(i)))
    '        Next

    '        finalresults = From dv In data1
    '                       Order By dv.MaxWeight Descending, dv.Time Ascending
    '                       Group dv By dv.MaxWeight Into g = Group
    '                       Select g.First

    '        lstsOutputView.Items.Clear()
    '        lstsOutputView.Items.Add(header)


    '        For Each row In finalresults

    '            lstsOutputView.Items.Add(row.ToString)
    '            txtNewTemp.Text = row.T_Final

    '        Next
    '    End If


    '    Dim Answer As Integer

    '    If a Mod 2 = 0 Then
    '        Answer = MsgBox("Enter segments for downgrade of next braking segment?", vbYesNoCancel, "Alert")
    '        If Answer = vbYes Then
    '            btnNext.Enabled = True
    '            btnNext.Select()
    '        Else
    '        End If
    '    End If
    '    If a Mod 2 = 1 Then
    '        Answer = MsgBox("Enter segments for downgrade of next non-braking segment?", vbYesNoCancel, "Alert")
    '        If Answer = vbYes Then
    '            btnNext.Enabled = True
    '            btnNext.Select()
    '        Else

    '        End If
    '    End If

    '    For Each row In finalresults
    '        lstsOutputView.Items.Add(row.ToString)
    '        If row.MaxSpeed = V_max Then
    '            Exit For
    '        End If
    '    Next

    '    'Try
    '    '    Dim myHelper As New HelperClass
    '    '    Dim Group As String = ""
    '    '    Dim MaxWeight As String = ""
    '    '    Dim MaxSpeed As String = ""
    '    '    Dim TDesc As String = ""
    '    '    Dim TEmerg As String = ""
    '    '    Dim TFinal As String = ""
    '    '    Dim Time As String = ""
    '    '    Dim SlopeTable As New DataTable("Slope")


    '    '    Dim SlopeColumn As DataColumn = SlopeTable.Columns.Add("Unique_Id", GetType(String))
    '    '    SlopeColumn.AllowDBNull = False
    '    '    SlopeColumn.Unique = False

    '    '    SlopeTable.Columns.Add("Group", GetType(Integer))
    '    '    SlopeTable.Columns.Add("Max_Weight", GetType(Integer))
    '    '    SlopeTable.Columns.Add("Max_Speed", GetType(Integer))
    '    '    SlopeTable.Columns.Add("T_Desc", GetType(Integer))
    '    '    SlopeTable.Columns.Add("T_Emerg", GetType(Integer))
    '    '    SlopeTable.Columns.Add("T_Final", GetType(Integer))
    '    '    SlopeTable.Columns.Add("Time", GetType(Integer))

    '    '    'Write the filter list to a table

    '    '    For Each row In finalresults

    '    '        lstsOutputView.Items.Add(row.ToString)

    '    '        If row.MaxSpeed = V_max Then
    '    '            'Insert the row in to the table
    '    '            Group = row.GroupNumber.ToString
    '    '            MaxWeight = row.MaxWeight.ToString
    '    '            MaxSpeed = row.MaxSpeed.ToString
    '    '            TDesc = row.T_Desc.ToString
    '    '            TEmerg = row.T_Emerg.ToString
    '    '            TFinal = row.T_Final.ToString
    '    '            Time = row.Time.ToString

    '    '            SlopeTable.Rows.Add(UniqueId, Group, MaxWeight, MaxSpeed, TDesc, TEmerg, TFinal, Time)
    '    '            Group = ""
    '    '            MaxWeight = ""
    '    '            MaxSpeed = ""
    '    '            TDesc = ""
    '    '            TEmerg = ""
    '    '            TFinal = ""
    '    '            Time = ""

    '    '            myHelper.InsertSlopeData(SlopeTable, "SeparateSlopeFilter")

    '    '            Exit For
    '    '        End If
    '    '    Next

    '    'Catch ex As Exception
    '    '    MessageBox.Show(ex.Message)
    '    'End Try

    'End Sub
    Private Sub butsReset_Click(sender As System.Object, e As System.EventArgs) Handles butsReset.Click
        txtsNumberGrades.Text = ""
        txtsGroupNumber.Text = 1
        txtsNumSections.Text = ""
        txtsiniambient.Text = ""
        txtsMaxSpeed.Text = ""
        txtsMaxWeight.Text = ""
        txtsinitemp.Text = ""
        cbosMaxTemp.Text = ""
        butsImport.Enabled = True
        butsGradeLength.Enabled = True
        butsClear.Enabled = True
        butsCompute.Enabled = False
        lstsGradeLength.Items.Clear()
        lstsOutputView.Items.Clear()
        RichTextBox2.Clear()
        lblnPath.Text = ""
        TLnew = 0
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
                MessageBox.Show("Invalid Input:   Not enough values.")
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
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles butTempProfile.Click
        frmTempProfile.LoggedOnUser = LoggedOnUser
        frmTempProfile.Show()
    End Sub
    Private Sub txtNumSections_TextChanged(sender As Object, e As EventArgs) Handles txtNumSections.TextChanged
        If txtNumSections.Text <> "" And lstGradeLength.Items.Count <> 0 Then
            butCompute.Enabled = True
        Else
            butCompute.Enabled = False
        End If
    End Sub
    Private Sub lstGradeLength_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstGradeLength.SelectedIndexChanged
        If txtNumSections.Text <> "" And lstGradeLength.Items.Count <> 0 Then
            butCompute.Enabled = True
        Else
            butCompute.Enabled = False

        End If
    End Sub
    Private Sub butTempProfile_MouseHover(sender As Object, e As EventArgs) Handles butTempProfile.MouseHover
        If txtNumSections.Text <> "" And txtambient.Text <> "" And txtMaxSpeed.Text <> "" And txtMaxWeight.Text <> "" And txtinitemp.Text <> "" And cboMaxTemp.Text <> "" And lstGradeLength.Items.Count <> 0 Then
            butTempProfile.Enabled = True
        Else
            butTempProfile.Enabled = False
        End If
    End Sub
    Private Sub GroupSeparateSlope_Enter(sender As Object, e As EventArgs) Handles GroupSeparateSlope.Enter

    End Sub
    Private Sub txtsNumSections_TextChanged(sender As Object, e As EventArgs) Handles txtsNumSections.TextChanged
        If txtsNumberGrades.Text <> "" And txtsNumSections.Text <> "" And lstsGradeLength.Items.Count <> 0 Then
            butsCompute.Enabled = True
        Else
            butsCompute.Enabled = False
        End If
    End Sub
    Private Sub cboMaxTemp_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles cboMaxTemp.SelectedIndexChanged
        If cboMaxTemp.Text = "500" Then
            T_max = 500
        ElseIf cboMaxTemp.Text = "530" Then
            T_max = 530

        End If
    End Sub
    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Group_Number = Group_Number + 1
        a = Group_Number
        txtsGroupNumber.Text = a
        txtsNumSections.Text = ""
        txtsinitemp.Text = ""
        cboMaxTemp.Text = ""
        butsImport.Enabled = True
        butsGradeLength.Enabled = True
        butsClear.Enabled = True
        butsCompute.Enabled = False
        lstsGradeLength.Items.Clear()
        lstsOutputView.Items.Clear()
        RichTextBox2.Clear()
        lblnPath.Text = ""
        TLnew = 0
        txtsinitemp.Text = txtNewTemp.Text

        Dim Query As Integer

        If Group_Number = p + 1 Then
            Query = MsgBox("Downgrade limit reached; Reset?", vbYesNoCancel, "Alert")
            If Query = vbYes Then
                butsReset.PerformClick()
                RadioButtonSeperateSlope.Checked = True
            Else

            End If
        End If
    End Sub
    Private Sub butsGradeLength_Click(sender As Object, e As EventArgs) Handles butsGradeLength.Click
        Dim numbergrades As Integer
        Dim numsegments As Integer
        If IsNumeric(txtsNumberGrades.Text) And txtsNumberGrades.Text <> "" And txtsNumberGrades.Text > "0" And (Integer.TryParse(txtsNumberGrades.Text, numbergrades)) Then
            Grades_max = txtsNumberGrades.Text
        Else : MsgBox("Please Enter a positive integer value for Number of Grades",, "Seperate Slope")
            Grades_maxinput = (InputBox("Enter a positive integer value for Number of Grades"))
            Do While IsNumeric(Grades_maxinput) = False Or Grades_maxinput < "0" Or Grades_maxinput = "" Or (Not Integer.TryParse(Grades_maxinput, numbergrades))
                MessageBox.Show("Please enter a positive integer value for Number of Grades")
                Grades_maxinput = (InputBox("Enter a positive integer value for Number of Grades", "Seperate Slope"))
                butsCompute.Enabled = False
                butsGradeLength.Enabled = True
            Loop
            Grades_max = Grades_maxinput
        End If
        txtsNumberGrades.Text = Grades_max
        If IsNumeric(txtsNumSections.Text) And txtsNumSections.Text <> "" And txtNumSections.Text > "0" And (Integer.TryParse(txtsNumSections.Text, numsegments)) Then
            Sections_max = txtsNumSections.Text
        Else : MsgBox("Please Enter a positive integer value for Number of Segments for Group(" & a & ")")
            Sections_maxinput = (InputBox("Please Enter a positive integer value for Number of Segments for Group(" & a & ")"))
            Do While IsNumeric(Sections_maxinput) = False Or Sections_maxinput < "0" Or Sections_maxinput = "" Or (Not Integer.TryParse(Sections_maxinput, numsegments))
                MessageBox.Show("Please Enter a positive integer value for Number of Segments for Group(" & a & ")")
                Sections_maxinput = (InputBox("Please Enter a positive integer value for Number of Segments for Group(" & a & ")"))
                butsCompute.Enabled = False
                butsGradeLength.Enabled = True
            Loop
            Sections_max = Sections_maxinput
        End If
        txtsNumSections.Text = Sections_max
        If txtsNumSections.Text > 6 Then
            If MsgBox("Would you Like to import segment data?", vbYesNo) = MsgBoxResult.Yes Then
                butsImport.PerformClick()
                Exit Sub
            Else
                lstsGradeLength.Items.Add("Grade(In Radians)" & vbTab & "Length(In Miles)" & vbTab & vbTab & "Radius(In Feet)" & vbTab & vbTab & "Super-elevation (In Decimal)" & vbTab & vbTab & "Angle (In Degrees)")
                For Me.i = 1 To txtsNumSections.Text

                    ReDim Preserve Gradec(i)
                    ReDim Preserve Lengthc(i)
                    ReDim Preserve Radiusc(i)
                    ReDim Preserve Superelevationc(i)
                    ReDim Preserve Anglec(i)

                    Datagradec = (InputBox("Enter Decimal Grade " & i & " in Radians"))


                    Do While String.IsNullOrEmpty(Datagradec) Or IsNumeric(Datagradec) = False Or Datagradec >= "1" Or Datagradec < "0"
                        MessageBox.Show("Please enter a positive Numeric Value less than 1")
                        Datagradec = (InputBox("Enter Decimal Grade " & i & " in Radians"))
                    Loop
                    Gradec(i) = Datagradec




                    DataLengthc = (InputBox("Enter Length " & i & " in Miles"))


                    Do While String.IsNullOrEmpty(DataLengthc) Or IsNumeric(DataLengthc) = False Or DataLengthc <= "0"
                        MessageBox.Show("Please enter a positive Numeric Value")
                        DataLengthc = (InputBox("Enter Length " & i & " in Miles"))
                    Loop
                    Lengthc(i) = DataLengthc


                    DataRadiusc = (InputBox("Enter Radius " & i & " in Feet"))

                    Do While String.IsNullOrEmpty(DataRadiusc) Or IsNumeric(DataRadiusc) = False Or DataRadiusc <= "0"
                        MessageBox.Show("Please enter a positive Numeric Value")
                        DataRadiusc = (InputBox("Enter Radius " & i & " in Feet"))
                    Loop
                    Radiusc(i) = DataRadiusc


                    DataSuperelevationc = (InputBox("Enter Decimal Super-elevation " & i))

                    Do While String.IsNullOrEmpty(DataSuperelevationc) Or IsNumeric(DataSuperelevationc) = False Or DataSuperelevationc >= "1" Or DataSuperelevationc < "0"
                        MessageBox.Show("Please enter a positive Numeric Value less than 1")
                        DataSuperelevationc = (InputBox("Enter Decimal Super-elevation " & i))
                    Loop
                    Superelevationc(i) = DataSuperelevationc

                    DataAnglec = (InputBox("Enter Radius Angle " & i))

                    Do While String.IsNullOrEmpty(DataAnglec) Or IsNumeric(DataAnglec) = False Or DataAnglec <= "0"
                        MessageBox.Show("Please enter a positive Numeric Value in Degrees")
                        DataAnglec = (InputBox("Enter Decimal Super-elevation " & i))
                    Loop
                    Anglec(i) = DataAnglec

                    lstsGradeLength.Items.Add(Gradec(i) & vbTab & vbTab & vbTab & Lengthc(i) & vbTab & vbTab & vbTab & Radiusc(i) & vbTab & vbTab & vbTab & Superelevationc(i) & vbTab & vbTab & vbTab & Anglec(i) & vbCrLf)

                Next
                butsCompute.Enabled = True
            End If
            butsImport.Enabled = True

        ElseIf CInt(txtsNumSections.Text) <= 6 Then

            lstsGradeLength.Items.Add("Grade(In Radians)" & vbTab & "Length(In Miles)" & vbTab & vbTab & "Radius(In Feet)" & vbTab & vbTab & "Super-elevation(In Decimal)" & vbTab & vbTab & "Angle (In Degrees)")
            For Me.i = 1 To txtsNumSections.Text

                ReDim Preserve Gradec(i)
                ReDim Preserve Lengthc(i)
                ReDim Preserve Radiusc(i)
                ReDim Preserve Superelevationc(i)
                ReDim Preserve Anglec(i)

                Datagradec = (InputBox("Enter Decimal Grade " & i & " in Radians"))


                Do While String.IsNullOrEmpty(Datagradec) Or IsNumeric(Datagradec) = False Or Datagradec >= "1" Or Datagradec < "0"
                    MessageBox.Show("Please enter a positive Numeric Value less than 1")
                    Datagradec = (InputBox("Enter Decimal Grade " & i & " in Radians"))
                Loop
                Gradec(i) = Datagradec




                DataLengthc = (InputBox("Enter Length " & i & " in Miles"))

                Do While String.IsNullOrEmpty(DataLengthc) Or IsNumeric(DataLengthc) = False Or DataLengthc <= "0"
                    MessageBox.Show("Please enter a positive Numeric Value")
                    DataLengthc = (InputBox("Enter Length " & i & " in Miles"))
                Loop
                Lengthc(i) = DataLengthc


                DataRadiusc = (InputBox("Enter Radius " & i & " in Feet"))

                Do While String.IsNullOrEmpty(DataRadiusc) Or IsNumeric(DataRadiusc) = False Or DataRadiusc <= "0"
                    MessageBox.Show("Please enter a positive Numeric Value")
                    DataRadiusc = (InputBox("Enter Radius " & i & " in Feet"))
                Loop
                Radiusc(i) = DataRadiusc


                DataSuperelevationc = (InputBox("Enter Decimal Super-elevation " & i))

                Do While String.IsNullOrEmpty(DataSuperelevationc) Or IsNumeric(DataSuperelevationc) = False Or DataSuperelevationc >= "1" Or DataSuperelevation < "0"
                    MessageBox.Show("Please enter a positive Numeric Value less than 1")
                    DataSuperelevationc = (InputBox("Enter Decimal Super-elevation " & i))
                Loop
                Superelevationc(i) = DataSuperelevationc

                Do While String.IsNullOrEmpty(DataAnglec) Or IsNumeric(DataAnglec) = False Or DataAnglec <= "0"
                    MessageBox.Show("Please enter a positive Numeric Value greater than 0")
                    DataAnglec = (InputBox("Enter Angle in Degrees " & i))
                Loop
                Anglec(i) = DataAnglec

                lstsGradeLength.Items.Add(Gradec(i) & vbTab & vbTab & vbTab & Lengthc(i) & vbTab & vbTab & vbTab & Radiusc(i) & vbTab & vbTab & vbTab & Superelevationc(i) & vbTab & vbTab & vbTab & Anglec(i) & vbCrLf)


            Next
            butsCompute.Enabled = True
        End If
        butsImport.Enabled = True
    End Sub
    Private Interface IExcelDataReader
        Sub Close()
        Function ReadLine() As String
        Function Peek() As Integer
    End Interface

    Private Sub butsImport_Click(sender As Object, e As EventArgs) Handles butsImport.Click
        Dim MyFileDialog As New System.Windows.Forms.OpenFileDialog

        ' Configure the dialog to show only text and excel files
        ' Set its title and set the filename field blank for the moment.
        MyFileDialog.Filter = "(*.xlsx)|*.xlsx"
        MyFileDialog.Title = "Open an excel file"
        MyFileDialog.FileName = ""
        ' Show the dialog and see if the user pressed ok.

        If MyFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            ' Check to see if they selected a file and that it exists.

            If File.Exists(MyFileDialog.FileName) Then


                Dim strFile As String = MyFileDialog.FileName
                Dim textextension As String
                Dim testFile As System.IO.FileInfo
                Try
                    ' Setup a file stream reader to read the text and excel files.

                    textextension = Path.GetExtension(strFile)
                    If textextension = ".xlsx" Then
                        Dim oExcel As Object = CreateObject("Excel.Application")
                        Dim oBook As Object = oExcel.Workbooks.Open(strFile)
                        Dim oSheet As Object = oBook.Worksheets(1)
                        Dim i As Integer
                        Dim cellA As String
                        Dim cellB As String
                        Dim cellC As String
                        Dim cellD As String
                        Dim cellE As String
                        lstsGradeLength.Items.Add("Grade (in Radians)" & vbTab & "   Length (in Miles)" & vbTab & vbTab & "Radius (in Feet)" & vbTab & vbTab & "Super-elevation (in Decimal)" & vbTab & vbTab & "Angle (in Degrees)")
                        For i = 0 To AscW(lstsGradeLength.Items.Count.ToString()(i = i + 1)) - 1

                            cellA = "A" & Convert.ToString(i + 1)
                            cellB = "B" & Convert.ToString(i + 1)
                            cellC = "C" & Convert.ToString(i + 1)
                            cellD = "D" & Convert.ToString(i + 1)
                            cellE = "E" & Convert.ToString(i + 1)
                            cellA = oSheet.Range(cellA).Value
                            cellB = oSheet.Range(cellB).Value
                            cellC = oSheet.Range(cellC).Value
                            cellD = oSheet.Range(cellD).Value
                            cellE = oSheet.Range(cellE).Value
                            If cellA = "" And cellB = "" And cellC = "" And cellD = "" And cellE = "" Then
                                Exit For
                            Else
                                RichTextBox2.AppendText(cellA & " " & cellB & " " & cellC & " " & cellD & " " & cellE & vbCrLf)

                            End If
                        Next
                        oExcel.Quit()
                        Dim m As Integer
                        For m = 1 To CInt(UBound(RichTextBox2.Lines))
                            ReDim Preserve Gradec(m)
                            ReDim Preserve Lengthc(m)
                            ReDim Preserve Radiusc(m)
                            ReDim Preserve Superelevationc(m)
                            ReDim Preserve Anglec(m)
                            Gradec(m) = RichTextBox2.Lines(m - 1).Split(" ").First
                            Lengthc(m) = RichTextBox2.Lines(m - 1).Split(" "c)(1)
                            Radiusc(m) = RichTextBox2.Lines(m - 1).Split(" "c)(2)
                            Superelevationc(m) = RichTextBox2.Lines(m - 1).Split(" "c)(3)
                            Anglec(m) = RichTextBox2.Lines(m - 1).Split(" "c)(4)
                            lstsGradeLength.Items.Add(Gradec(m) & vbTab & vbTab & vbTab & Lengthc(m) & vbTab & vbTab & vbTab & Radiusc(m) & vbTab & vbTab & vbTab & Superelevationc(m) & vbTab & vbTab & vbTab & Anglec(m) & vbCrLf)
                            butsGradeLength.Enabled = False
                        Next
                        testFile = My.Computer.FileSystem.GetFileInfo(strFile)

                        lblnPath.Text = testFile.FullName

                        txtsNumSections.Text = lstsGradeLength.Items.Count - 1
                    End If

                Catch ex As FileNotFoundException

                    ' If the file was not found, tell the user.

                    MessageBox.Show("File was Not found. Please try again.")

                End Try

            End If
        Else
            txtsNumSections.Text = ""
            butsImport.Enabled = True
            butsGradeLength.Enabled = True
            butsClear.Enabled = True
            Exit Sub
        End If

        butsImport.Enabled = False
        butsCompute.Enabled = True
    End Sub
    Private Sub butlogout_Click(sender As Object, e As EventArgs) Handles butlogout.Click
        Me.Close()
        frmLogin.Show()
        frmLogin.txtusername.Text = ""
        frmLogin.txtpassword.Text = ""
    End Sub
    Private Sub butsCurve_Click(sender As Object, e As EventArgs) Handles butsCurve.Click
        frmHorizontalseparate.LoggedOnUser = LoggedOnUser
        If txtsNumberGrades.Text <> "" Then
            frmHorizontalseparate.NumberGrades = CInt(txtsNumberGrades.Text)
        End If

        frmHorizontalseparate.Show()
        butsCompute.Enabled = False
        butsCurve.Enabled = False
        butsGradeLength.Enabled = False
        Dim numgradesoutput As Integer
        If IsNumeric(txtsNumberGrades.Text) And txtsNumberGrades.Text <> "" And txtsNumberGrades.Text > "0" And (Integer.TryParse(txtsNumberGrades.Text, numgradesoutput)) = True Then
            Grades_max = txtsNumberGrades.Text
        Else : MsgBox("Please Enter a positive integer value for Number of Grades", , "Seperate Slope")
            Grades_maxinput = (InputBox("Enter a positive integer value for Number of Grades", "Seperate Slope"))
            Do While IsNumeric(Grades_maxinput) = False Or Grades_maxinput < "0" Or (Integer.TryParse(Grades_maxinput, numgradesoutput)) = False
                MessageBox.Show("Please enter a positive integer value for Number of Grades", "Seperate Slope")
                Grades_maxinput = (InputBox("Enter a positive integer value for Number of Grades", "Seperate Slope"))
            Loop
            Grades_max = Grades_maxinput
        End If
        txtsNumberGrades.Text = Grades_max

        If IsNumeric(txtsMaxWeight.Text) And txtsMaxWeight.Text <> "" And txtsMaxWeight.Text > "0" Then
            W_max = txtsMaxWeight.Text
        Else : MsgBox("Please Enter a positive numeric value for Maximum Weight", , "Seperate Slope")
            W_Maxinput = (InputBox("Enter a positive numeric value for Maximum Weight", "Seperate Slope"))
            Do While IsNumeric(W_Maxinput) = False Or W_Maxinput < "0"
                MessageBox.Show("Please enter a positive numeric Value for Maximum Weight", "Seperate Slope")
                W_Maxinput = (InputBox("Enter a positive numeric value for Maximum Weight", "Seperate Slope"))
            Loop
            W_max = CDbl(W_Maxinput)
        End If
        txtsMaxWeight.Text = W_max
        If IsNumeric(txtsMaxSpeed.Text) And txtsMaxSpeed.Text <> "" And txtsMaxSpeed.Text > "0" Then
            V_max = txtsMaxSpeed.Text
        Else : MsgBox("Please Enter a positive numeric value for Maximum Speed", , "Seperate Slope")
            V_Maxinput = (InputBox("Enter a positive numeric value for Maximum Speed", "Seperate Slope"))
            Do While IsNumeric(V_Maxinput) = False Or V_Maxinput < "0"
                MessageBox.Show("Please enter a positive numeric Value for Maximum Speed", "Seperate Slope")
                V_Maxinput = (InputBox("Enter a positive numeric value for Maximum Speed", "Seperate Slope"))
            Loop
            V_max = CDbl(V_Maxinput)
        End If
        txtsMaxSpeed.Text = V_max
        If IsNumeric(txtsinitemp.Text) Then
            If txtsinitemp.Text >= 90 Then
                T_0 = txtsinitemp.Text
                txtsinitemp.Text = T_0
            ElseIf txtsinitemp.Text < 90 Then
                T_0 = "150"
                txtsinitemp.Text = T_0
            End If
        Else : MsgBox("Enter a numeric value greater Or equal to 90 for Initial Temperature", , "Seperate Slope")
            T_0_input = (InputBox("Enter a numeric value greater Or equal to 90 for Initial Temperature", "Alert", "150"))
            Do While IsNumeric(T_0_input) = False Or T_0_input = ""
                MessageBox.Show("Enter a numeric value greater Or equal to 90 for Initial Temperature", "Seperate Slope")
                T_0_input = (InputBox("Enter a numeric value greater Or equal to 90 for Initial Temperature", "Alert", "150"))
            Loop
            If T_0_input >= 90 Then
                T_0 = T_0_input
                txtsinitemp.Text = T_0
            ElseIf T_0_input < 90 Then
                T_0 = "150"
                txtsinitemp.Text = T_0
            End If
        End If
        If IsNumeric(txtsiniambient.Text) Then
            If txtsiniambient.Text >= 90 Then
                T_inf = "90"
                txtsiniambient.Text = T_inf
            ElseIf txtsiniambient.Text < 90 Then
                T_inf = "90"
                txtsiniambient.Text = T_inf
            End If
        Else : MsgBox("Enter a value of 90 for the Ambient Temperature", , "Seperate Slope")
            T_inf_input = (InputBox("Enter a value of 90 for the Ambient Temperature", "Alert", "90"))
            Do While IsNumeric(T_inf_input) = False Or T_inf_input = ""
                MessageBox.Show("Enter a value of 90 for the Ambient Temperature", "Seperate Slope")
                T_inf_input = (InputBox("Enter a value of 90 for the Ambient Temperature", "Alert", "90"))
            Loop
            If T_inf_input >= 90 Then
                T_inf = "90"
                txtsiniambient.Text = T_inf
            ElseIf T_inf_input < 90 Then
                T_inf = "90"
                txtsiniambient.Text = T_inf
            End If
        End If
        If IsNumeric(cbosMaxTemp.Text) And cbosMaxTemp.Text <> "" And cbosMaxTemp.Text = "500" Or cbosMaxTemp.Text = "530" Then
            T_max = cbosMaxTemp.Text
        Else : MsgBox("Please input " & "500 Or 530 for Maximum Temperature", , "Seperate Slope")
            T_max_input = (InputBox("Input " & "500 Or 530 for Maximum Temperature", "Seperate Slope"))
            Do While IsNumeric(T_max_input) = False Or T_max_input <> "500" And T_max_input <> "530"
                MessageBox.Show("Input " & "500 Or 530 for Maximum Temperature", "Seperate Slope")
                T_max_input = (InputBox("Input " & "500 Or 530 for Maximum Temperature", "Seperate Slope"))
            Loop
            T_max = CDbl(T_max_input)
        End If
        cbosMaxTemp.Text = T_max
        frmHorizontalseparate.lstFinalOutputView.Items.Add("Group" & "        Max Weight (lb)" & "           Max Speed (mph)" & "                         T_Desc (F)" & "               T_Emerg (F)" & "      T_Final (F)" & "          Time (min) " & vbCrLf & vbCrLf)

        a = txtsGroupNumber.Text
        Group_Number = a
        p = txtsNumberGrades.Text

        'Computations

        i_max = W_max / 5000
        N_secteion = txtsNumSections.Text
        Dim input_info(,) As Double = New Double(CInt(N_secteion - 1), 1) {}
        For Me.i = 1 To N_secteion
            Dim intIndexGrade As New Integer
            Dim intIndexLength As New Integer

            For intIndexGrade = 1 To N_secteion
                input_info(intIndexGrade - 1, 0) = Gradec(intIndexGrade)
            Next
            For intIndexLength = 1 To N_secteion
                input_info(intIndexLength - 1, 1) = Lengthc(intIndexLength)
            Next
        Next

        If a Mod 2 = 1 Then
            T_lim_s = Group1curve(input_info, V_max, W_max, T_0, Group_Number)
        End If

        If a Mod 2 = 0 Then
            T_lim_s = CalVelcurve(input_info, W_max, i_max, V_max, T_max, T_0, Group_Number)
        End If

    End Sub
    Function CalVelcurve(input_info, W_max, i_max, V_max, T_max, T_0, Group_Number)
        Dim T_f(,) As Double
        Dim T_e(,) As Double
        Dim Space = ("        ")
        Dim j_max As Integer
        j_max = W_max / 5000
        For Me.i = 1 To txtsNumSections.Text
            TLnew += Lengthc(i)
        Next

        For Me.j = 0 To j_max
            W = W_max - j * 5000

            For Me.V = 1 To V_max

                T_0 = txtsinitemp.Text 'initial brake temperature
                T_inf = txtsiniambient.Text 'ambient temperature

                ReDim T_e(V, 1)
                T_e(V, 1) = (0.000000311) * W * (V ^ 2) 'temperature from emergency stopping
                HP_eng = 63.3 'Engine brake force
                K2 = 1 / (0.1602 + 0.0078 * V) 'Heat transfer parameter
                K1 = 1.5 * (1.1852 + 0.0331 * V) 'Diffusivity constant
                F_drag = 459.35 + 0.132 * (V ^ 2) 'Drag forces

                For Me.i = 1 To txtsNumSections.Text
                    Theta = Gradec(i)
                    L = Lengthc(i)
                    HP_b = (W * Theta - F_drag) * (V / 375) - 63.3 'power into brakes
                    ReDim T_f(V, 1)
                    T_f(V, 1) = T_0 + (T_inf - T_0 + K2 * HP_b) * (1 - Math.Exp(-K1 * (L / V)))
                    T_0 = T_f(V, 1)
                Next

                ReDim T_lim(V, 1)

                T_lim(V, 1) = T_f(V, 1) + T_e(V, 1)    'limiting brake temperature

                For i = 0 To CInt(txtsNumSections.Text)
                    ReDim Preserve Vsfs(i)
                    ReDim Preserve Vros(i)
                    ReDim Preserve Vmins(i)

                    skidding = (((0.91544 - 0.00166 * Anglec(i) - 0.000002 * W - 0.054248 * Superelevationc(i) - Sidefrictionfactor) / 0.013939) * Radiusc(i))
                    rollover = (((1.04136 - 0.004528 * Anglec(i) - 0.000004 * W - 0.338711 * Superelevationc(i) - rolloverthreshold) / 0.014578) * Radiusc(i))

                    If skidding < 0 Or rollover < 0 Then
                        MsgBox("Error! Check your input variables- Shorten segments and recompute angles",, "Alert!")
                        Exit Function
                    Else
                        Vsfs(i) = CInt(skidding ^ 0.5)
                        Vros(i) = CInt(rollover ^ 0.5)
                    End If

                    Vmins(i) = Math.Min(Vsfs(i), Vros(i))

                    If Vmins(i) = 0 Then
                        Vmins(i) = V_max
                    End If
                Next

                Vi = CInt(Vmins.Min)

                If V < Vi Then
                    T_lim_s = CInt(T_lim(V, 1))
                    T_f_s = CInt(T_f(V, 1))
                    T_e_s = CInt(T_e(V, 1))
                    frmHorizontalseparate.lstFinalOutputView.Items.Add(Space & Group_Number & vbTab & vbTab & W & vbTab & vbTab & Space & V & vbTab & vbTab & Space & vbTab & T_f_s & vbTab & Space & vbTab & T_e_s & vbTab & Space & T_lim_s & vbTab & vbTab & Space & CInt(TLnew * 60 / V) & vbCrLf)
                Else
                    T_0 = CDbl(txtsinitemp.Text) 'initial brake temperature
                    T_inf = CDbl(txtsiniambient.Text) 'ambient temperature

                    ReDim T_e(Vi, 1)
                    T_e(Vi, 1) = (0.000000311) * W * (Vi ^ 2) 'temperature from emergency stopping
                    HP_eng = 63.3 'Engine brake force
                    K2 = 1 / (0.1602 + 0.0078 * Vi) 'Heat transfer parameter
                    K1 = 1.5 * (1.1852 + 0.0331 * Vi) 'Diffusivity constant
                    F_drag = 459.35 + 0.132 * (Vi ^ 2) 'Drag forces

                    For Me.i = 1 To txtsNumSections.Text
                        Theta = Gradec(i)
                        L = Lengthc(i)
                        HP_b = (W * Theta - F_drag) * (Vi / 375) - 63.3 'power into brakes
                        ReDim T_f(Vi, 1)
                        T_f(Vi, 1) = T_0 + (T_inf - T_0 + K2 * HP_b) * (1 - Math.Exp(-K1 * (L / Vi)))
                        T_0 = T_f(Vi, 1)
                    Next

                    ReDim T_lim(Vi, 1)
                    T_lim(Vi, 1) = T_f(Vi, 1) + T_e(Vi, 1)    'limiting brake temperature

                    T_lim_s = CInt(T_lim(Vi, 1))
                    T_f_s = CInt(T_f(Vi, 1))
                    T_e_s = CInt(T_e(Vi, 1))
                    frmHorizontalseparate.lstFinalOutputView.Items.Add(Space & Group_Number & vbTab & vbTab & W & vbTab & vbTab & Space & Vi & vbTab & vbTab & Space & vbTab & T_f_s & vbTab & Space & vbTab & T_e_s & vbTab & Space & T_lim_s & vbTab & vbTab & Space & CInt(TLnew * 60 / Vi) & vbCrLf)
                End If
            Next
        Next
        Me.butsFilter.Enabled = True
    End Function
    ' Function to calculate V
    Function Group1curve(input_info, V_max, W_max, T_0, Group_Number)
        Dim Space = ("        ")
        W = W_max
        V = V_max
        For Me.i = 1 To txtsNumSections.Text
            TLnew += Lengthc(i)
        Next
        For Me.j = 0 To (V_max - 15) / 5
            V = V_max - 5 * j
            T_0 = txtsinitemp.Text
            T_inf = txtsiniambient.Text  'ambient temperature

            Ts_e = (0.000000311) * W * (V ^ 2) ' temperature from emergency stopping
            HP_eng = 63.3 'engine brake force
            K2 = 1 / (0.1602 + 0.0078 * V) 'heat transfer parameter
            K1 = 1.5 * (1.1852 + 0.0331 * V) 'diffusivity constant
            F_drag = 459.35 + 0.132 * (V ^ 2) 'drag forces

            For Me.i = 1 To txtsNumSections.Text
                Theta = Gradec(i)
                L = Lengthc(i)
                HP_b = (W * Theta - F_drag) * (V / 375) - 63.3 'power into brakes
                Ts_f = T_0 + (T_inf - T_0 + K2 * HP_b) * (1 - Math.Exp(-K1 * (L / V)))
                T_0 = Ts_f
            Next

            T_lims = Ts_f + Ts_e 'limiting brake temperature

            For i = 0 To CInt(txtsNumSections.Text)
                ReDim Preserve Vsfs(i)
                ReDim Preserve Vros(i)
                ReDim Preserve Vmins(i)

                skidding = CInt(((0.91544 - 0.00166 * Anglec(i) - 0.000002 * W - 0.054248 * Superelevationc(i) - Sidefrictionfactor) / 0.013939) * Radiusc(i))
                rollover = CInt(((1.04136 - 0.004528 * Anglec(i) - 0.000004 * W - 0.338711 * Superelevationc(i) - rolloverthreshold) / 0.014578) * Radiusc(i))

                If skidding < 0 Or rollover < 0 Then
                    MsgBox("Error! Check your input variables- Shorten segments and recompute angles",, "Alert!")
                    Exit Function
                Else
                    Vsfs(i) = CInt(skidding ^ 0.5)
                    Vros(i) = CInt(rollover ^ 0.5)
                End If

                Vmins(i) = Math.Min(Vsfs(i), Vros(i))

                If Vmins(i) = 0 Then
                    Vmins(i) = V_max
                End If
            Next

            Vi = CInt(Vmins.Min)

            If V < Vi Then
                frmHorizontalseparate.lstFinalOutputView.Items.Add(Space & Group_Number & vbTab & vbTab & W & vbTab & vbTab & Space & V & vbTab & vbTab & Space & vbTab & Ts_f & vbTab & Space & vbTab & Ts_e & vbTab & Space & T_lims & vbTab & vbTab & Space & CInt(TLnew * 60 / V) & vbCrLf)
            Else
                T_0 = CDbl(txtsinitemp.Text) 'initial brake temperature
                T_inf = CDbl(txtsiniambient.Text) 'ambient temperature
                Ts_e = (0.000000311) * W * (Vi ^ 2) 'temperature from emergency stopping
                HP_eng = 63.3 'Engine brake force
                K2 = 1 / (0.1602 + 0.0078 * Vi) 'Heat transfer parameter
                K1 = 1.5 * (1.1852 + 0.0331 * Vi) 'Diffusivity constant
                F_drag = 459.35 + 0.132 * (Vi ^ 2) 'Drag forces
                For Me.i = 1 To txtsNumSections.Text
                    Theta = Gradec(i)
                    L = Lengthc(i)
                    HP_b = (W * Theta - F_drag) * (Vi / 375) - 63.3 'power into brakes
                    Ts_f = T_0 + (T_inf - T_0 + K2 * HP_b) * (1 - Math.Exp(-K1 * (L / Vi)))
                    T_0 = Ts_f
                Next
                T_lims = Ts_f + Ts_e  'limiting brake temperature
                frmHorizontalseparate.lstFinalOutputView.Items.Add(Space & Group_Number & vbTab & vbTab & W & vbTab & vbTab & Space & Vi & vbTab & vbTab & Space & vbTab & Ts_f & vbTab & Space & vbTab & Ts_e & vbTab & Space & T_lims & vbTab & vbTab & Space & CInt(TLnew * 60 / Vi) & vbCrLf)
            End If
        Next
        Me.butsFilter.Enabled = True
    End Function
    Private Sub butCurve_Click(sender As Object, e As EventArgs) Handles butCurve.Click
        frmHorizontal.LoggedOnUser = LoggedOnUser
        frmHorizontal.Show()
        butCompute.Enabled = False
        butGradeLength.Enabled = False
        If IsNumeric(txtMaxWeight.Text) And txtMaxWeight.Text <> "" And txtMaxWeight.Text > "0" Then
            W_max = txtMaxWeight.Text
        Else : MsgBox("Please Enter a positive numeric value for Maximum Weight")
            W_Maxinput = (InputBox("Enter a positive numeric value for Maximum Weight"))
            Do While IsNumeric(W_Maxinput) = False Or W_Maxinput < "0"
                MessageBox.Show("Please enter a positive numeric Value for Maximum Weight")
                W_Maxinput = (InputBox("Enter a positive numeric value for Maximum Weight"))
            Loop
            W_max = CDbl(W_Maxinput)
            txtMaxWeight.Text = W_max
        End If
        If IsNumeric(txtMaxSpeed.Text) And txtMaxSpeed.Text <> "" And txtMaxSpeed.Text > "0" Then
            V_max = txtMaxSpeed.Text
        Else : MsgBox("Please Enter a positive numeric value for Maximum Speed")
            V_Maxinput = (InputBox("Enter a positive numeric value for Maximum Speed"))
            Do While IsNumeric(V_Maxinput) = False Or V_Maxinput < "0"
                MessageBox.Show("Please enter a positive numeric Value for Maximum Speed")
                V_Maxinput = (InputBox("Enter a positive numeric value for Maximum Speed"))
            Loop
            V_max = CDbl(V_Maxinput)
            txtMaxSpeed.Text = V_max
        End If
        If IsNumeric(txtinitemp.Text) Then
            If txtinitemp.Text >= 90 Then
                T_0 = txtinitemp.Text
                txtinitemp.Text = T_0
            ElseIf txtinitemp.Text < 90 Then
                T_0 = "150"
                txtinitemp.Text = T_0
            End If
        Else : MsgBox("Enter a numeric value greater or equal to 90 for Initial Temperature")
            T_0_input = (InputBox("Enter a numeric value greater or equal to 90 for Initial Temperature", "Alert", "150"))
            Do While IsNumeric(T_0_input) = False Or T_0_input = ""
                MessageBox.Show("Enter a numeric value greater or equal to 90 for Initial Temperature")
                T_0_input = (InputBox("Enter a numeric value greater or equal to 90 for Initial Temperature", "Alert", "150"))
            Loop
            If T_0_input >= 90 Then
                T_0 = T_0_input
                txtinitemp.Text = T_0
            ElseIf T_0_input < 90 Then
                T_0 = "150"
                txtinitemp.Text = T_0
            End If
        End If
        If IsNumeric(txtambient.Text) Then
            If txtambient.Text >= 90 Then
                T_inf = "90"
                txtambient.Text = T_inf
            ElseIf txtambient.Text < 90 Then
                T_inf = "90"
                txtambient.Text = T_inf
            End If
        Else : MsgBox("Enter a value of 90 for the Ambient Temperature")
            T_inf_input = (InputBox("Enter a value of 90 for the Ambient Temperature", "Alert", "90"))
            Do While IsNumeric(T_inf_input) = False Or T_inf_input = ""
                MessageBox.Show("Enter a value of 90 for the Ambient Temperature")
                T_inf_input = (InputBox("Enter a value of 90 for the Ambient Temperature", "Alert", "90"))
            Loop
            If T_inf_input >= 90 Then
                T_inf = "90"
                txtambient.Text = T_inf
            ElseIf T_inf_input < 90 Then
                T_inf = "90"
                txtambient.Text = T_inf
            End If
        End If
        If IsNumeric(cboMaxTemp.Text) And cboMaxTemp.Text <> "" And cboMaxTemp.Text = "500" Or cboMaxTemp.Text = "530" Then
            T_max = cboMaxTemp.Text
        Else : MsgBox("Please input " & "500 or 530 for Maximum Brake Temperature")
            T_max_input = (InputBox("Input " & "500 or 530 for Maximum Brake Temperature"))
            Do While IsNumeric(T_max_input) = False Or T_max_input <> "500" And T_max_input <> "530"
                MessageBox.Show("Input " & "500 or 530 for Maximum Brake Temperature")
                T_max_input = (InputBox("Input " & "500 or 530 for Maximum Brake Temperature"))
            Loop
            T_max = CDbl(T_max_input)
            cboMaxTemp.Text = T_max
        End If

        frmHorizontal.lstFinalOutputView.Items.Add("Max Weight (lb) " & "    Max Speed (mph) " & "     T_Desc (F) " & "           T_Emerg (F) " & "        T_Final (F)" & "                Time (min) " & vbCrLf & vbCrLf)

        'Computations
        j_max = W_max / 5000

        For i = 1 To CInt(txtNumSections.Text)
            TL += Length(i)
        Next

        For Me.j = 0 To j_max
            W = W_max - j * 5000

            For Me.V = 1 To V_max

                T_0 = CDbl(txtinitemp.Text) 'initial brake temperature
                T_inf = CDbl(txtambient.Text) 'ambient temperature

                ReDim T_e(V, 1)
                T_e(V, 1) = (0.000000311) * W * (V ^ 2) 'temperature from emergency stopping
                HP_eng = 63.3 'Engine brake force
                K2 = 1 / (0.1602 + 0.0078 * V) 'Heat transfer parameter
                K1 = 1.5 * (1.1852 + 0.0331 * V) 'Diffusivity constant
                F_drag = 459.35 + 0.132 * (V ^ 2) 'Drag forces

                For Me.i = 1 To txtNumSections.Text

                    Theta = Grade(i)
                    L = Length(i)
                    HP_b = (W * Theta - F_drag) * (V / 375) - 63.3 'power into brakes
                    ReDim T_f(V, 1)
                    T_f(V, 1) = T_0 + (T_inf - T_0 + K2 * HP_b) * (1 - Math.Exp(-K1 * (L / V)))
                    T_0 = T_f(V, 1)

                Next

                ReDim T_lim(V, 1)
                T_lim(V, 1) = T_f(V, 1) + T_e(V, 1)    'limiting brake temperature

                For i = 0 To CInt(txtNumSections.Text)
                    ReDim Preserve Vsf(i)
                    ReDim Preserve Vro(i)
                    ReDim Preserve Vmin(i)

                    skidding = CInt(((0.91544 - 0.00166 * Angle(i) - 0.000002 * W - 0.054248 * Superelevation(i) - Sidefrictionfactor) / 0.013939) * Radius(i))
                    rollover = CInt(((1.04136 - 0.004528 * Angle(i) - 0.000004 * W - 0.338711 * Superelevation(i) - rolloverthreshold) / 0.014578) * Radius(i))

                    If skidding < 0 Or rollover < 0 Then
                        MsgBox("Error! Check your input variables- Shorten segments and recompute angles!",, "Alert!")
                        Exit Sub
                    Else
                        Vsf(i) = (skidding ^ 0.5)
                        Vro(i) = (rollover ^ 0.5)
                    End If

                    Vmin(i) = Math.Min(Vsf(i), Vro(i))

                    If Vmin(i) = 0 Then
                        Vmin(i) = V_max
                    End If
                Next

                Vi = CInt(Vmin.Min)
                If V < Vi Then
                    T_lim_s = CInt(T_lim(V, 1))
                    T_f_s = CInt(T_f(V, 1))
                    T_e_s = CInt(T_e(V, 1))
                    frmHorizontal.lstFinalOutputView.Items.Add(W & vbTab & vbTab & V & vbTab & vbTab & T_f_s & vbTab & vbTab & T_e_s & vbTab & vbTab & T_lim_s & vbTab & vbTab & CInt(TL * 60 / V) & vbCrLf)
                Else
                    T_0 = CDbl(txtinitemp.Text) 'initial brake temperature
                    T_inf = CDbl(txtambient.Text) 'ambient temperature

                    ReDim T_e(Vi, 1)
                    T_e(Vi, 1) = (0.000000311) * W * (Vi ^ 2) 'temperature from emergency stopping
                    HP_eng = 63.3 'Engine brake force
                    K2 = 1 / (0.1602 + 0.0078 * Vi) 'Heat transfer parameter
                    K1 = 1.5 * (1.1852 + 0.0331 * Vi) 'Diffusivity constant
                    F_drag = 459.35 + 0.132 * (Vi ^ 2) 'Drag forces

                    For Me.i = 1 To txtNumSections.Text
                        Theta = Grade(i)
                        L = Length(i)
                        HP_b = (W * Theta - F_drag) * (Vi / 375) - 63.3 'power into brakes
                        ReDim T_f(Vi, 1)
                        T_f(Vi, 1) = T_0 + (T_inf - T_0 + K2 * HP_b) * (1 - Math.Exp(-K1 * (L / Vi)))
                        T_0 = T_f(Vi, 1)
                    Next

                    ReDim T_lim(Vi, 1)
                    T_lim(Vi, 1) = T_f(Vi, 1) + T_e(Vi, 1)    'limiting brake temperature
                    T_lim_s = CInt(T_lim(Vi, 1))
                    T_f_s = CInt(T_f(Vi, 1))
                    T_e_s = CInt(T_e(Vi, 1))
                    frmHorizontal.lstFinalOutputView.Items.Add(W & vbTab & vbTab & Vi & vbTab & vbTab & T_f_s & vbTab & vbTab & T_e_s & vbTab & vbTab & T_lim_s & vbTab & vbTab & CInt(TL * 60 / Vi) & vbCrLf)
                End If
            Next
        Next
    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click

    End Sub
End Class
