Public Class frmSeparateSlopesInput
    Dim Group_Number As Integer
    Dim W_max As Double
    Dim V_max As Integer
    Public Grade() As Double
    Public Length() As Double
    Public input_info(,) As Double
    Dim i As Integer
    Dim Datagrade As String
    Dim DataLength As String
    Dim T_max As Double
    Dim T_0 As Double
    Dim T_G() As Double
    Dim m As Integer
    Dim i_max As Double
    Dim N_secteion As Integer
    Dim T_lim_s As Integer
    Dim W As Integer
    Dim V As Integer
    Dim TL As Double
    Dim T_Inf As Double
    Dim T_e As Integer
    Dim HP_eng As Double
    Dim K2 As Double
    Dim K1 As Double
    Dim F_drag As Double
    Dim j As Integer
    Dim theta As Double
    Dim L As Double
    Dim HP_b As Double
    Public T_F As Integer
    Public T_e_s As Integer
    Public T_f_s As Integer
    Public T_lim(,) As Double
    Public Vs As Integer
    Dim W_Maxinput As String
    Dim V_Maxinput As String
    Dim T_0_input As String
    Dim T_inf_input As String
    Dim T_max_input As String
   Private Sub butCompute_Click(sender As System.Object, e As System.EventArgs) Handles butCompute.Click
        Do While String.IsNullOrEmpty(txtGroupNumber.Text) Or txtGroupNumber.Text > "5"
            MessageBox.Show("Please enter a Number between 1 and 5")
            Group_Number = (InputBox("Please enter a Number between 1 and 5"))
            If IsNumeric(Group_Number) And Group_Number < "5" Then
                txtGroupNumber.Text = Group_Number
            Else
                txtGroupNumber.Text = ""
            End If
        Loop
        Group_Number = txtGroupNumber.Text
        butGradeLength.Enabled = False
        frmOutputViewer.lstOutputView.Items.Add("Max Weight" & "  Max Speed" & "   T_Desc" & "   T_Emerg" & "   T_Final" & "     Time" & vbCrLf & vbCrLf)
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
        If IsNumeric(txtinitemp.Text) And txtMaxSpeed.Text <> "" Then
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
            T_Inf = txtinitemp.Text
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
        butGradeLength.Enabled = False

        frmMain.lstOutputView.Items.Add("Group" & "  Max Weight" & "  Max Speed" & "      T_Desc" & "      T_Emerg" & "      T_Final" & "     Time")


        'Computations
        i_max = W_max / 5000
        N_secteion = txtNumSections.Text
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
            End If
        Next
    End Sub
    ' Function to calculate V
    Function Group1(input_info, V_max, W_max, T_0, Group_Number)
        W = W_max
        V = V_max
        For Me.i = 1 To txtNumSections.Text
            TL += Length(i)
        Next
        For Me.j = 0 To (V_max - 15) / 5
            V = V_max - 5 * j
            T_0 = txtinitemp.Text
            T_Inf = txtambient.Text  'ambient temperature

            T_e = (0.000000311) * W * (V ^ 2) ' temperature from emergency stopping
            HP_eng = 63.3 'engine brake force
            K2 = 1 / (0.1602 + 0.0078 * V) 'heat transfer parameter
            K1 = 1.5 * (1.1852 + 0.0331 * V) 'diffusivity constant
            F_drag = 459.35 + 0.132 * (V ^ 2) 'drag forces

            For Me.i = 0 To txtNumSections.Text
                theta = Grade(i)
                L = Length(i)
                HP_b = (W * theta - F_drag) * (V / 375) - 63.3 'power into brakes
                T_F = T_0 + (T_Inf - T_0 + K2 * HP_b) * (1 - Math.Exp(-K1 * (L / V)))
                T_0 = T_F

            Next
            T_lim_s = T_F + T_e 'limiting brake temperature
            frmMain.lstOutputView.Items.Add(Group_Number & vbTab & W & vbTab & vbTab & V & vbTab & T_F & vbTab & T_e & vbTab & T_lim_s & vbTab & CInt(TL * 60 / V) & vbCrLf).ToString()
        Next
        If Group_Number = 1 Or Group_Number = 3 Or Group_Number = 5 Then
            frmMain.butFilter.Enabled = False
        ElseIf Group_Number = 2 Or Group_Number = 4 Then
            frmMain.butFilter.Enabled = True
        End If
    End Function
    Function CalVel(input_info, W_max, i_max, V_max, T_max, T_0, Group_Number)
        Dim T_f(,) As Double
        Dim T_e(,) As Double
        Dim j_max As Integer
        j_max = W_max / 5000
        For Me.i = 1 To txtNumSections.Text
            TL += Length(i)
        Next


        For Me.j = 0 To txtNumSections.Text

            W = W_max - j * 5000

            For Me.V = 1 To V_max

                T_0 = txtinitemp.Text
                T_Inf = 90  'Ambient temperature
                ReDim T_e(V, 1)
                T_e(V, 1) = (0.000000311) * W * (V ^ 2) 'temperature from emergency stopping
                HP_eng = 63.3 'engine brake force
                K2 = 1 / (0.1602 + 0.0078 * V) 'heat transfer parameter
                K1 = 1.5 * (1.1852 + 0.0331 * V) 'diffusivity constant
                F_drag = 459.35 + 0.132 * (V ^ 2) 'drag forces

                For Me.i = 1 To txtNumSections.Text
                    theta = Grade(i)
                    L = Length(i)
                    HP_b = (W * theta - F_drag) * (V / 375) - 63.3 'power into brakes
                    ReDim T_f(V, 1)
                    T_f(V, 1) = T_0 + (T_Inf - T_0 + K2 * HP_b) * (1 - Math.Exp(-K1 * (L / V)))
                    T_0 = T_f(V, 1)

                Next

                ReDim T_lim(V, 1)
                T_lim(V, 1) = T_f(V, 1) + T_e(V, 1) 'limiting brake temperature


                Vs = V
                T_lim_s = T_lim(V, 1)
                T_f_s = T_f(V, 1)
                T_e_s = T_e(V, 1)

                If T_lim(V, 1) > T_max Then
                    Vs = V - 1
                    T_lim_s = T_lim(V - 1, 1)
                    T_f_s = T_f(V - 1, 1)
                    T_e_s = T_e(V - 1, 1)
                End If
                frmMain.lstOutputView.Items.Add(Group_Number & vbTab & W & vbTab & vbTab & Vs & vbTab & T_f_s & vbTab & T_e_s & vbTab & T_lim_s & vbTab & CInt(TL * 60 / Vs) & vbCrLf).ToString()
            Next


        Next


        If Group_Number = 1 Or Group_Number = 3 Or Group_Number = 5 Then
            frmMain.butFilter.Enabled = False
        ElseIf Group_Number = 2 Or Group_Number = 4 Then
            frmMain.butFilter.Enabled = True
        End If

    End Function
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

        'butGradeLength.Enabled = False
    End Sub

    Private Sub frmSeparateSlopesInput_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        butCompute.Enabled = False
    End Sub

    Private Sub butClear_Click(sender As System.Object, e As System.EventArgs) Handles butClear.Click
        lstGradeLength.Items.Clear()
        butGradeLength.Enabled = True
    End Sub
End Class