Imports System.IO
Module Module1
    Sub Main()
        Dim i As Integer = 0

        ' Loop through each line in RichTextBox.
        Dim line As String
        For Each line In frmMain.RichTextBox1.Text

            ' Split line on space.
            Dim parts As String() = line.Split(New Char() {" "c})

            ' Loop over each string received.
            Dim part As String
            For Each part In parts
                'Display in listbox
                frmMain.lstGradeLength.Items.Add(part)
            Next
            i += 1
        Next
    End Sub
End Module



