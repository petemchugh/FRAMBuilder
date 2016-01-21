Public Class form_StatusAndSummary

    Private Sub StatusAndSummary_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        lbl_CWTdb4.Text = CWTdatabasename

    End Sub

    Private Sub btnReturn1_Click(sender As System.Object, e As System.EventArgs) Handles btn_Return1.Click

        Me.Visible = False
        form_WelcomeAndConnect.Visible = True


    End Sub

End Class