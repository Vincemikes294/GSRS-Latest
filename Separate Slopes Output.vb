Public Class frmSeparateSlopesOutput
    Private Sub butFilter_Click(sender As System.Object, e As System.EventArgs) Handles butFilter.Click
        Dim header As String = lstOutputView.Items(0)

        Dim data As New List(Of DataValue1)
        ' Skip the header row by starting at 1:
        For i As Integer = 1 To lstOutputView.Items.Count - 1
            data.Add(New DataValue1(lstOutputView.Items(i)))
        Next

        Dim results = From dv In data
                      Order By dv.MaxWeight Descending, dv.T_Final Descending
                      Group dv By dv.MaxWeight Into g = Group
                      Select g.First

        Dim V_max = CInt(frmSeparateSlopesInput.txtMaxSpeed.Text)
        lstOutputView.Items.Clear()
        lstOutputView.Items.Add(header)
        For Each row In results
            lstOutputView.Items.Add(row.ToString)
            If row.MaxSpeed = V_max Then
                Exit For
            End If
        Next
    End Sub
    Private Sub butSave_Click(sender As System.Object, e As System.EventArgs) Handles butSave.Click
        Dim SaveFileDialog1 As New SaveFileDialog
        SaveFileDialog1.FileName = ""
        SaveFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"

        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim sb As New System.Text.StringBuilder()

            For Each o As Object In lstOutputView.Items
                sb.AppendLine(o)
            Next

            System.IO.File.WriteAllText(SaveFileDialog1.FileName, sb.ToString())
        End If
    End Sub

    Private Sub frmSeparateSlopesOutput_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        frmSeparateSlopesInput.Close()
        frmSeparateSlopesInput.Show()
        frmSeparateSlopesInput.MdiParent = MDI
        Me.MdiParent = MDI
    End Sub
    Private Sub lstOutputView_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lstOutputView.SelectedIndexChanged

    End Sub
    Private Sub frmSeparateSlopesOutput_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        frmSeparateSlopesInput.butCompute.Enabled = False
    End Sub
End Class
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

