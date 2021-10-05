
Public Class frmContinuousSlopeOutput
    Dim i As Integer

    Private Sub lstOutput_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lstOutput.SelectedIndexChanged

    End Sub

    Private Sub butSave_Click(sender As System.Object, e As System.EventArgs) Handles butSave.Click
        Dim SaveFileDialog1 As New SaveFileDialog
        SaveFileDialog1.FileName = ""
        SaveFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"

        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim sb As New System.Text.StringBuilder()

            For Each o As Object In lstOutput.Items
                sb.AppendLine(o)
            Next

            System.IO.File.WriteAllText(SaveFileDialog1.FileName, sb.ToString())
        End If


    End Sub

    Private Sub frmContinuousSlopeOutput_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

    End Sub

    Private Sub frmContinuousSlopeOutput_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class

