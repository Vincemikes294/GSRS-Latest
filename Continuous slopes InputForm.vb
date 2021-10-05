Imports System.IO
Public Class frmMain

    Dim T_max As Double
    Public Grade() As Double
    Public Length() As Double
    Dim i As Integer
    Dim j As Integer
    Dim j_max As Integer
    Dim i_max As Double
    Dim W_max As Double
    Dim Valid As Boolean
    Dim Datagrade As String
    Dim DataLength As String
    Public Res() As Array
    Dim T_lim(,) As Double
    Public TL As Double
    Public W As Double
    Public V_max As Integer
    Dim V As Integer
    Dim T_0 As Double
    Dim T_inf As Double
    Dim T_e(,) As Double
    Dim HP_eng As Double
    Dim K2 As Double
    Dim K1 As Double
    Dim F_drag As Double
    Dim Theta As Double
    Dim L As Double
    Dim HP_b As Double
    Dim T_f(,) As Double
    Public Vs As Double
    Public T_lim_s As Integer
    Public T_f_s As Double
    Public T_e_s As Double
    Dim T_lims As Double
    Dim W_Maxinput As String
    Dim V_Maxinput As String
    Dim T_0_input As String
    Dim T_inf_input As String
    Dim T_max_input As String
    Dim Ts_e As Integer
    Dim Ts_f As Integer
    Dim Group_Number As String
    Dim N_secteion As String
    Dim TLnew As Integer
    Private Sub cboMaxTemp_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboMaxTemp.SelectedIndexChanged
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
                lstGradeLength.Items.Add("Grade(In Radians)" & vbTab & "Length(In Miles)")
                For Me.i = 1 To txtNumSections.Text

                    ReDim Preserve Grade(i)
                    ReDim Preserve Length(i)

                    Datagrade = (InputBox("Enter Decimal Grade " & i & " in Radians"))


                    Do While String.IsNullOrEmpty(Datagrade) Or IsNumeric(Datagrade) = False Or Datagrade >= "1"
                        MessageBox.Show("Please enter a Numeric Value less than 1")
                        Datagrade = (InputBox("Enter Decimal Grade " & i & " in Radians"))
                    Loop
                    Grade(i) = Datagrade




                    DataLength = (InputBox("Enter Length " & i & " in Miles"))

                    Do While String.IsNullOrEmpty(DataLength) Or IsNumeric(DataLength) = False
                        MessageBox.Show("Please enter a Numeric Value")
                        DataLength = (InputBox("Enter Length " & i & " in Miles"))
                    Loop
                    Length(i) = DataLength

                    lstGradeLength.Items.Add(Grade(i) & vbTab & vbTab & vbTab & Length(i) & vbCrLf)

                Next
                butCompute.Enabled = True
            End If
            butImport.Enabled = True

        ElseIf CInt(txtNumSections.Text) <= 6 Then

            lstGradeLength.Items.Add("Grade(In Radians)" & vbTab & "Length(In Miles)")
            For Me.i = 1 To txtNumSections.Text

                ReDim Preserve Grade(i)
                ReDim Preserve Length(i)

                Datagrade = (InputBox("Enter Decimal Grade " & i & " in Radians"))


                Do While String.IsNullOrEmpty(Datagrade) Or IsNumeric(Datagrade) = False Or Datagrade >= "1"
                    MessageBox.Show("Please enter a Numeric Value less than 1")
                    Datagrade = (InputBox("Enter Decimal Grade " & i & " in Radians"))
                Loop
                Grade(i) = Datagrade




                DataLength = (InputBox("Enter Length " & i & " in Miles"))

                Do While String.IsNullOrEmpty(DataLength) Or IsNumeric(DataLength) = False
                    MessageBox.Show("Please enter a Numeric Value")
                    DataLength = (InputBox("Enter Length " & i & " in Miles"))
                Loop
                Length(i) = DataLength

                lstGradeLength.Items.Add(Grade(i) & vbTab & vbTab & vbTab & Length(i) & vbCrLf)

            Next
            butCompute.Enabled = True
        End If
        butImport.Enabled = True
    End Sub
    Private Sub butCompute_Click(sender As System.Object, e As System.EventArgs) Handles butCompute.Click
        butSave.Enabled = True
        butFilter.Enabled = True
        butCompute.Enabled = False
        butGradeLength.Enabled = False
        Me.lstOutputView.Items.Add("Max Weight" & "  Max Speed" & "   T_Desc" & "   T_Emerg" & "   T_Final" & "     Time" & vbCrLf & vbCrLf)
        If IsNumeric(txtMaxWeight.Text) And txtMaxWeight.Text <> "" Then
            W_max = txtMaxWeight.Text
        Else : MsgBox("Please Enter a numeric value for Maximum Weight")
            W_Maxinput = (InputBox("Enter a numeric value for Maximum Weight"))
            Do While IsNumeric(W_Maxinput) = False
                MessageBox.Show("Please enter a numeric Value for Maximum Weight")
                W_Maxinput = (InputBox("Enter a numeric value for Maximum Weight"))
            Loop
            W_max = W_Maxinput
            txtMaxWeight.Text = W_max
        End If
        If IsNumeric(txtMaxSpeed.Text) And txtMaxSpeed.Text <> "" Then
            V_max = txtMaxSpeed.Text
        Else : MsgBox("Please Enter a numeric value for Maximum Speed")
            V_Maxinput = (InputBox("Enter a numeric value for Maximum Speed"))
            Do While IsNumeric(V_Maxinput) = False
                MessageBox.Show("Please enter a numeric Value for Maximum Speed")
                V_Maxinput = (InputBox("Enter a numeric value for Maximum Speed"))
            Loop
            V_max = V_Maxinput
            txtMaxSpeed.Text = V_max
        End If
        If IsNumeric(txtinitemp.Text) And txtinitemp.Text <> "" Then
            T_0 = txtinitemp.Text
        Else : MsgBox("Please Enter a numeric value for Initial Temperature")
            T_0_input = (InputBox("Enter a numeric value for Initial Temperature"))
            Do While IsNumeric(T_0_input) = False
                MessageBox.Show("Enter a numeric value for Initial Temperature")
                T_0_input = (InputBox("Enter a numeric value for Initial Temperature"))
            Loop
            txtinitemp.Text = T_0_input
        End If
        If IsNumeric(txtambient.Text) And txtambient.Text <> "" Then
            T_inf = txtinitemp.Text
        Else : MsgBox("Please Enter a numeric value for Ambient Temperature")
            T_inf_input = (InputBox("Enter a numerical value for Ambient Temperature"))
            Do While IsNumeric(T_inf_input) = False
                MessageBox.Show("Enter a numerical value for Ambient Temperature")
                T_inf_input = (InputBox("Enter a numerical value for Ambient Temperature"))
            Loop
            txtambient.Text = T_inf_input
        End If
        If IsNumeric(cboMaxTemp.Text) And cboMaxTemp.Text <> "" And cboMaxTemp.Text = "500" Or cboMaxTemp.Text = "530" Then
            T_max = cboMaxTemp.Text
        Else : MsgBox("Please input " & "500 or 530 for Maximum Temperature")
            T_max_input = (InputBox("Input " & "500 or 530 for Maximum Temperature"))
            Do While IsNumeric(T_max_input) = False Or T_max_input <> "500" And T_max_input <> "530"
                MessageBox.Show("Input " & "500 or 530 for Maximum Temperature")
                T_max_input = (InputBox("Input " & "500 or 530 for Maximum Temperature"))
            Loop
            cboMaxTemp.Text = T_max_input
        End If

        'Computations
        j_max = W_max / 5000

        For Me.i = 1 To txtNumSections.Text
            TL += Length(i)
        Next

        For Me.j = 0 To j_max
            W = W_max - j * 5000

            For Me.V = 1 To V_max

                T_0 = txtinitemp.Text 'initial brake temperature
                T_inf = txtambient.Text 'ambient temperature

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


                Vs = V
                T_lim_s = CInt(T_lim(V, 1))
                T_f_s = CInt(T_f(V, 1))
                T_e_s = CInt(T_e(V, 1))

                If T_lim(V, 1) > T_max Then
                    Vs = V - 1
                    T_lim_s = CInt(T_lim(V - 1, 1))
                    T_f_s = CInt(T_f(V - 1, 1))
                    T_e_s = CInt(T_e(V - 1, 1))
                End If


                lstOutputView.Items.Add(W & vbTab & vbTab & Vs & vbTab & T_f_s & vbTab & T_e_s & vbTab & T_lim_s & vbTab & CInt(TL * 60 / Vs) & vbCrLf)
            Next
        Next
    End Sub
    Private Sub frmGSRS(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
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
        Dim SaveFileDialog1 As New SaveFileDialog
        SaveFileDialog1.FileName = ""
        SaveFileDialog1.Filter = "Text Files (*.txt)|*.xls|All Files (*.*)|*.*"

        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim sb As New System.Text.StringBuilder()

            For Each o As Object In lstOutputView.Items
                sb.AppendLine(o)
            Next

            System.IO.File.WriteAllText(SaveFileDialog1.FileName, sb.ToString())
        End If
    End Sub
    Private Sub butFilter_Click_1(sender As System.Object, e As System.EventArgs) Handles butFilter.Click
        Dim header As String = lstOutputView.Items(0)

        Dim data As New List(Of DataValue)
        ' Skip the header row by starting at 1:
        For i As Integer = 1 To lstOutputView.Items.Count - 1
            data.Add(New DataValue(lstOutputView.Items(i)))
        Next

        Dim results = From dv In data
                      Order By dv.MaxWeight Descending, dv.T_Final Descending
                      Group dv By dv.MaxWeight Into g = Group
                      Select g.First

        Dim V_max = CInt(Me.txtMaxSpeed.Text)
        lstOutputView.Items.Clear()
        lstOutputView.Items.Add(header)
        For Each row In results
            lstOutputView.Items.Add(row.ToString)
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
            Return MaxWeight & vbTab & vbTab & MaxSpeed & vbTab & T_Desc & vbTab & T_Emerg & vbTab & T_Final & vbTab & Time
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
        GroupSeparateSlope.Enabled = False
    End Sub

    Private Sub RadioButtonSeperateSlope_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButtonSeperateSlope.CheckedChanged
        GroupContinuousSlope.Enabled = False
        GroupSeparateSlope.Enabled = True
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

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles butsImport.Click
        Dim MyFileDialog As New System.Windows.Forms.OpenFileDialog

        ' Configure the dialog to show only text files
        ' Set its title and set the filename field blank for the moment.
        MyFileDialog.Filter = "Text File (*.txt)|*.txt"
        MyFileDialog.Title = "Open a text file"
        MyFileDialog.FileName = ""
        ' Show the dialog and see if the user pressed ok.

        If MyFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            ' Check to see if they selected a file and that it exists.

            If File.Exists(MyFileDialog.FileName) Then

                Dim strFile As String = MyFileDialog.FileName

                Dim testFile1 As System.IO.FileInfo

                Dim reader As StreamReader

                Try
                    ' Setup a file stream reader to read the text file.

                    reader = New StreamReader(New FileStream(strFile, FileMode.Open, FileAccess.Read))

                    ' While there is data to be read, read each line into a rich edit box control.
                    testFile1 = My.Computer.FileSystem.GetFileInfo(strFile)

                    lblPath1.Text = testFile1.FullName

                    While reader.Peek > -1

                        RichTextBox2.Text &= reader.ReadLine() & vbCrLf

                    End While

                    ' Close the file

                    reader.Close()

                Catch ex As FileNotFoundException

                    ' If the file was not found, tell the user.

                    MessageBox.Show("File was not found. Please try again.")
                End Try

            End If
        Else
            txtsNumSections.Text = ""
            butsImport.Enabled = True
            butsGradeLength.Enabled = True
            butsClear.Enabled = True
            Exit Sub
        End If
        lstsGradeLength.Items.Add("Grade (in Radians)" & "   " & "Length (in Miles)")
        Dim k As Integer
        For k = 1 To CInt(UBound(RichTextBox2.Lines))
            ReDim Preserve Grade(k)
            ReDim Preserve Length(k)
            Grade(k) = RichTextBox2.Lines(k - 1).Split(" ").First
            Length(k) = RichTextBox2.Lines(k - 1).Split(" ").Last
            lstsGradeLength.Items.Add(Grade(k) & "                          " & Length(k) & vbCrLf)
            butsGradeLength.Enabled = False
        Next
        txtsNumSections.Text = UBound(RichTextBox2.Lines)
        butsImport.Enabled = False
        butsCompute.Enabled = True
    End Sub
    Private Sub butsSave_Click(sender As System.Object, e As System.EventArgs) Handles butsSave.Click
        Dim SaveFileDialog1 As New SaveFileDialog
        SaveFileDialog1.FileName = ""
        SaveFileDialog1.Filter = "Text Files (*.txt)|*.xls|All Files (*.*)|*.*"

        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim sb As New System.Text.StringBuilder()

            For Each o As Object In lstOutputView.Items
                sb.AppendLine(o)
            Next

            System.IO.File.WriteAllText(SaveFileDialog1.FileName, sb.ToString())
        End If
    End Sub
    Private Sub butsGradeLength_Click(sender As System.Object, e As System.EventArgs) Handles butsGradeLength.Click
        If txtsNumSections.Text = "" Or IsNumeric(txtsNumSections.Text) = False Then
            MsgBox("Please Enter number of segments")
            txtsNumSections.Text = ""
            butsCompute.Enabled = False
            butsGradeLength.Enabled = True
        ElseIf CInt(txtsNumSections.Text) > "6" Then
            If MsgBox("Would you like to import segment data?", vbYesNo) = MsgBoxResult.Yes Then
                butsImport.PerformClick()
                Exit Sub
            Else
                lstsGradeLength.Items.Add("Grade(In Radians)" & vbTab & "Length(In Miles)")
                For Me.i = 1 To txtsNumSections.Text

                    ReDim Preserve Grade(i)
                    ReDim Preserve Length(i)

                    Datagrade = (InputBox("Enter Decimal Grade " & i & " in Radians"))


                    Do While String.IsNullOrEmpty(Datagrade) Or IsNumeric(Datagrade) = False Or Datagrade >= "1"
                        MessageBox.Show("Please enter a Numeric Value less than 1")
                        Datagrade = (InputBox("Enter Decimal Grade " & i & " in Radians"))
                    Loop
                    Grade(i) = Datagrade




                    DataLength = (InputBox("Enter Length " & i & " in Miles"))

                    Do While String.IsNullOrEmpty(DataLength) Or IsNumeric(DataLength) = False
                        MessageBox.Show("Please enter a Numeric Value")
                        DataLength = (InputBox("Enter Length " & i & " in Miles"))
                    Loop
                    Length(i) = DataLength

                    lstsGradeLength.Items.Add(Grade(i) & vbTab & vbTab & vbTab & Length(i) & vbCrLf)

                Next
                butCompute.Enabled = True
            End If
            butImport.Enabled = True

        ElseIf CInt(txtNumSections.Text) <= 6 Then

            lstGradeLength.Items.Add("Grade(In Radians)" & vbTab & "Length(In Miles)")
            For Me.i = 1 To txtNumSections.Text

                ReDim Preserve Grade(i)
                ReDim Preserve Length(i)

                Datagrade = (InputBox("Enter Decimal Grade " & i & " in Radians"))


                Do While String.IsNullOrEmpty(Datagrade) Or IsNumeric(Datagrade) = False Or Datagrade >= "1"
                    MessageBox.Show("Please enter a Numeric Value less than 1")
                    Datagrade = (InputBox("Enter Decimal Grade " & i & " in Radians"))
                Loop
                Grade(i) = Datagrade




                DataLength = (InputBox("Enter Length " & i & " in Miles"))

                Do While String.IsNullOrEmpty(DataLength) Or IsNumeric(DataLength) = False
                    MessageBox.Show("Please enter a Numeric Value")
                    DataLength = (InputBox("Enter Length " & i & " in Miles"))
                Loop
                Length(i) = DataLength

                lstsGradeLength.Items.Add(Grade(i) & vbTab & vbTab & vbTab & Length(i) & vbCrLf)

            Next
            butCompute.Enabled = True
        End If
        butImport.Enabled = True
    End Sub
    Private Sub butsClear_Click(sender As System.Object, e As System.EventArgs) Handles butsClear.Click
        RichTextBox2.Clear()
        lstsGradeLength.Items.Clear()
        butsGradeLength.Enabled = True
        butsImport.Enabled = True
        txtsNumSections.Text = ""
        lblPath1.Text = ""
    End Sub
    Private Sub butsCompute_Click(sender As System.Object, e As System.EventArgs) Handles butsCompute.Click
        butsSave.Enabled = True
        butsFilter.Enabled = True
        butsCompute.Enabled = False
        butsGradeLength.Enabled = False
        Group_Number = txtsGroupNumber.Text
        If IsNumeric(Group_Number) And Group_Number <> "" And Group_Number >= "1" And Group_Number <= "5" Then
            Group_Number = txtsGroupNumber.Text
            txtsGroupNumber.Text = Group_Number
        Else : MsgBox("Please enter a Group Number between 1 and 5")
            Group_Number = (InputBox("Please enter a Group Number between 1 and 5"))
            Do While IsNumeric(Group_Number) = False
                MessageBox.Show("Please enter a Group Number between 1 and 5")
                Group_Number = (InputBox("Please enter a Group Number between 1 and 5"))
                If IsNumeric(Group_Number) And Group_Number <= "5" And Group_Number >= "1" Then
                    txtsGroupNumber.Text = Group_Number
                Else
                    txtsGroupNumber.Text = ""
                End If
            Loop
            txtsGroupNumber.Text = Group_Number
        End If
        If IsNumeric(txtsMaxWeight.Text) And txtsMaxWeight.Text <> "" Then
            W_max = txtsMaxWeight.Text
        Else : MsgBox("Please Enter a numeric value for Maximum Weight")
            W_Maxinput = (InputBox("Enter a numeric value for Maximum Weight"))
            Do While IsNumeric(W_Maxinput) = False
                MessageBox.Show("Please enter a numeric Value for Maximum Weight")
                W_Maxinput = (InputBox("Enter a numeric value for Maximum Weight"))
            Loop
            W_max = W_Maxinput
            txtsMaxWeight.Text = W_max
        End If
        If IsNumeric(txtsMaxSpeed.Text) And txtsMaxSpeed.Text <> "" Then
            V_max = txtsMaxSpeed.Text
        Else : MsgBox("Please Enter a numeric value for Maximum Speed")
            V_Maxinput = (InputBox("Enter a numeric value for Maximum Speed"))
            Do While IsNumeric(V_Maxinput) = False
                MessageBox.Show("Please enter a numeric Value for Maximum Speed")
                V_Maxinput = (InputBox("Enter a numeric value for Maximum Speed"))
            Loop
            V_max = V_Maxinput
            txtsMaxSpeed.Text = V_max
        End If
        If IsNumeric(txtsinitemp.Text) And txtsinitemp.Text <> "" Then
            T_0 = txtsinitemp.Text
        Else : MsgBox("Please Enter a numeric value for Initial Temperature")
            T_0_input = (InputBox("Enter a numeric value for Initial Temperature"))
            Do While IsNumeric(T_0_input) = False
                MessageBox.Show("Enter a numeric value for Initial Temperature")
                T_0_input = (InputBox("Enter a numeric value for Initial Temperature"))
            Loop
            txtsinitemp.Text = T_0_input
        End If
        If IsNumeric(txtsiniambient.Text) And txtsiniambient.Text <> "" Then
            T_inf = txtsiniambient.Text
        Else : MsgBox("Please Enter a numeric value for Ambient Temperature")
            T_inf_input = (InputBox("Enter a numerical value for Ambient Temperature"))
            Do While IsNumeric(T_inf_input) = False
                MessageBox.Show("Enter a numerical value for Ambient Temperature")
                T_inf_input = (InputBox("Enter a numerical value for Ambient Temperature"))
            Loop
            txtsiniambient.Text = T_inf_input
        End If
        If IsNumeric(cbosMaxTemp.Text) And cbosMaxTemp.Text <> "" And cbosMaxTemp.Text = "500" Or cbosMaxTemp.Text = "530" Then
            T_max = cbosMaxTemp.Text
        Else : MsgBox("Please input " & "500 or 530 for Maximum Temperature")
            T_max_input = (InputBox("Input " & "500 or 530 for Maximum Temperature"))
            Do While IsNumeric(T_max_input) = False Or T_max_input <> "500" And T_max_input <> "530"
                MessageBox.Show("Input " & "500 or 530 for Maximum Temperature")
                T_max_input = (InputBox("Input " & "500 or 530 for Maximum Temperature"))
            Loop
            cbosMaxTemp.Text = T_max_input
        End If

        lstsOutputView.Items.Add("Group" & "  Max Weight" & "  Max Speed" & "      T_Desc" & "      T_Emerg" & "      T_Final" & "     Time")


        'Computations
        'm = 0
        i_max = W_max / 5000
        N_secteion = txtsNumSections.Text
        Dim input_info(,) As Double = New Double(CInt(N_secteion - 1), 1) {}
        For Me.i = 1 To N_secteion
            Dim intIndexGrade As Integer
            Dim intIndexLength As Integer

            For intIndexGrade = 1 To N_secteion
                input_info(intIndexGrade - 1, 0) = Grade(intIndexGrade)
            Next
            For intIndexLength = 1 To N_secteion
                input_info(intIndexLength - 1, 1) = Length(intIndexLength)

            Next

            If Group_Number = 1 Or Group_Number = 3 Or Group_Number = 5 Then
                T_lim_s = Group1(input_info, V_max, W_max, T_0, Group_Number)

            ElseIf Group_Number = 2 Or Group_Number = 4 Then
                T_lim_s = CalVel(input_info, W_max, i_max, V_max, T_max, T_0, Group_Number)
            Else
                MsgBox("Wrong Cluster")
                txtsGroupNumber.Text = ""
            End If
        Next
    End Sub
    Private Sub butClear_Click(sender As System.Object, e As System.EventArgs) Handles butClear.Click
        RichTextBox1.Clear()
        lstGradeLength.Items.Clear()
        butGradeLength.Enabled = True
        butImport.Enabled = True
        txtNumSections.Text = ""
        lblPath.Text = ""
    End Sub
    Private Sub Button1_Click_1(sender As System.Object, e As System.EventArgs) Handles butImport.Click
        Dim MyFileDialog As New System.Windows.Forms.OpenFileDialog

        ' Configure the dialog to show only text files
        ' Set its title and set the filename field blank for the moment.
        MyFileDialog.Filter = "Text File (*.txt)|*.txt"
        MyFileDialog.Title = "Open a text file"
        MyFileDialog.FileName = ""
        ' Show the dialog and see if the user pressed ok.

        If MyFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            ' Check to see if they selected a file and that it exists.

            If File.Exists(MyFileDialog.FileName) Then

                Dim strFile As String = MyFileDialog.FileName

                Dim reader As StreamReader
                Dim testFile As System.IO.FileInfo

                Try
                    ' Setup a file stream reader to read the text file.

                    reader = New StreamReader(New FileStream(strFile, FileMode.Open, FileAccess.Read))
                    testFile = My.Computer.FileSystem.GetFileInfo(strFile)

                    lblPath.Text = testFile.FullName

                    ' While there is data to be read, read each line into a rich edit box control.

                    While reader.Peek > -1

                        RichTextBox1.Text &= reader.ReadLine() & vbCrLf

                    End While

                    ' Close the file

                    reader.Close()

                Catch ex As FileNotFoundException

                    ' If the file was not found, tell the user.

                    MessageBox.Show("File was not found. Please try again.")

                End Try

            End If
        Else
            txtNumSections.Text = ""
            butImport.Enabled = True
            butGradeLength.Enabled = True
            butClear.Enabled = True
            Exit Sub
        End If
        lstGradeLength.Items.Add("Grade (in Radians)" & "   " & "Length (in Miles)")
        Dim m As Integer
        For m = 1 To CInt(UBound(RichTextBox1.Lines))
            ReDim Preserve Grade(m)
            ReDim Preserve Length(m)
            Grade(m) = RichTextBox1.Lines(m - 1).Split(" ").First
            Length(m) = RichTextBox1.Lines(m - 1).Split(" ").Last
            lstGradeLength.Items.Add(Grade(m) & "                          " & Length(m) & vbCrLf)
            butGradeLength.Enabled = False
        Next

        txtNumSections.Text = UBound(RichTextBox1.Lines)
        butImport.Enabled = False
        butCompute.Enabled = True
    End Sub
    ' Function to calculate V
    Function Group1(input_info, V_max, W_max, T_0, Group_Number)
        W = W_max
        V = V_max
        For Me.i = 1 To txtsNumSections.Text
            TLnew += Length(i)
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
                Theta = Grade(i)
                L = Length(i)
                HP_b = (W * Theta - F_drag) * (V / 375) - 63.3 'power into brakes
                Ts_f = T_0 + (T_inf - T_0 + K2 * HP_b) * (1 - Math.Exp(-K1 * (L / V)))
                T_0 = Ts_f

            Next
            T_lims = Ts_f + Ts_e 'limiting brake temperature

            lstsOutputView.Items.Add(Group_Number & vbTab & W & vbTab & vbTab & V & vbTab & Ts_f & vbTab & Ts_e & vbTab & T_lims & vbTab & CInt(TLnew * 60 / V) & vbCrLf).ToString()
        Next
        If Group_Number = 1 Or Group_Number = 3 Or Group_Number = 5 Then
            Me.butsFilter.Enabled = False
        ElseIf Group_Number = 2 Or Group_Number = 4 Then
            Me.butsFilter.Enabled = True
        End If
    End Function
    Function CalVel(input_info, W_max, i_max, V_max, T_max, T_0, Group_Number)
        Dim T_f(,) As Double
        Dim T_e(,) As Double
        Dim j_max As Integer
        j_max = W_max / 5000
        For Me.i = 1 To txtsNumSections.Text
            TLnew += Length(i)
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

                For Me.i = 1 To CInt(txtsNumSections.Text)

                    Theta = Grade(i)
                    L = Length(i)
                    HP_b = (W * Theta - F_drag) * (V / 375) - 63.3 'power into brakes
                    ReDim T_f(V, 1)
                    T_f(V, 1) = T_0 + (T_inf - T_0 + K2 * HP_b) * (1 - Math.Exp(-K1 * (L / V)))
                    T_0 = T_f(V, 1)

                Next

                ReDim T_lim(V, 1)

                T_lim(V, 1) = T_f(V, 1) + T_e(V, 1)    'limiting brake temperature
                Dim var As Integer = CInt(T_lim(1, 1))


                Vs = V
                T_lim_s = CInt(T_lim(V, 1))
                T_f_s = CInt(T_f(V, 1))
                T_e_s = CInt(T_e(V, 1))

                If T_lim(V, 1) > T_max Then
                    Vs = V - 1
                    T_lim_s = T_lim(V - 1, 1)
                    T_f_s = T_f(V - 1, 1)
                    T_e_s = T_e(V - 1, 1)
                End If

                lstsOutputView.Items.Add(Group_Number & vbTab & W & vbTab & vbTab & V & vbTab & T_f_s & vbTab & T_e_s & vbTab & T_lim_s & vbTab & CInt(TL * 60 / V) & vbCrLf).ToString()
            Next
        Next

        If Group_Number = 1 Or Group_Number = 3 Or Group_Number = 5 Then
            Me.butsFilter.Enabled = False
        ElseIf Group_Number = 2 Or Group_Number = 4 Then
            Me.butsFilter.Enabled = True
        End If

    End Function
    Private Sub cbosMaxTemp_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbosMaxTemp.SelectedIndexChanged
        If cbosMaxTemp.Text = "500" Then
            T_max = 500
        ElseIf cbosMaxTemp.Text = "530" Then
            T_max = 530

        End If
    End Sub

    Private Sub butsFilter_Click(sender As System.Object, e As System.EventArgs) Handles butsFilter.Click
        Dim header As String = lstsOutputView.Items(0)

        Dim data As New List(Of DataValue1)
        ' Skip the header row by starting at 1:
        For i As Integer = 1 To lstsOutputView.Items.Count - 1
            data.Add(New DataValue1(lstsOutputView.Items(i)))
        Next

        Dim results = From dv In data
                      Order By dv.MaxWeight Descending, dv.T_Final Descending
                      Group dv By dv.MaxWeight Into g = Group
                      Select g.First

        Dim V_max = CInt(Me.txtsMaxSpeed.Text)
        lstsOutputView.Items.Clear()
        lstsOutputView.Items.Add(header)
        For Each row In results
            lstsOutputView.Items.Add(row.ToString)
            If row.MaxSpeed = V_max Then
                Exit For
            End If
        Next
    End Sub

    Private Sub butsReset_Click(sender As System.Object, e As System.EventArgs) Handles butsReset.Click
        txtsGroupNumber.Text = ""
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
        lblPath1.Text = ""
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
                MessageBox.Show("Invalid Input: Not enough values.")
            End If
        End Sub

        Public Overrides Function ToString() As String
            Return GroupNumber & vbTab & MaxWeight & vbTab & vbTab & MaxSpeed & vbTab & T_Desc & vbTab & T_Emerg & vbTab & T_Final & vbTab & Time
        End Function
        Public GroupNumber As Integer
        Public MaxWeight As Integer
        Public MaxSpeed As Integer
        Public Time As Integer
        Public T_Emerg As Integer
        Public T_Final As Integer
        Public T_Desc As Integer
    End Class

    Private Sub frmGSRS_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class