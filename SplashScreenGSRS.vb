Public NotInheritable Class SplashScreenGSRS

    Private Sub SplashScreen1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Private Sub ApplicationTitle_Click(sender As System.Object, e As System.EventArgs) Handles ApplicationTitle.Click

    End Sub
    Private Sub MainLayoutPanel_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles MainLayoutPanel.Paint

    End Sub
    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        ProgressBar1.Increment(1)
        If ProgressBar1.Value = 100 Then
            Me.Hide()
            Timer1.Enabled = False
            MDIGSRS.Show()
            frmLogin.Show()

        End If

    End Sub
End Class

