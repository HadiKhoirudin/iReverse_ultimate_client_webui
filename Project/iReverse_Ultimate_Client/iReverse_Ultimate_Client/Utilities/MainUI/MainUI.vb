Module MainUI
    Public Sub lboperation(value As String)
        If XtraMain.DelegateFunction.lboperation.InvokeRequired Then
            XtraMain.DelegateFunction.lboperation.Invoke(Sub() XtraMain.DelegateFunction.lboperation.Text = value)
        Else
            XtraMain.DelegateFunction.lboperation.Text = value
        End If
    End Sub
    Public Sub lbstatserver(value As String)
        If XtraMain.DelegateFunction.lbstatserver.InvokeRequired Then
            XtraMain.DelegateFunction.lbstatserver.Invoke(Sub() XtraMain.DelegateFunction.lbstatserver.Text = value)
        Else
            XtraMain.DelegateFunction.lbstatserver.Text = value
        End If
    End Sub
    Public Sub lbusername(value As String)
        If XtraMain.DelegateFunction.lbusername.InvokeRequired Then
            XtraMain.DelegateFunction.lbusername.Invoke(Sub() XtraMain.DelegateFunction.lbusername.Text = value)
        Else
            XtraMain.DelegateFunction.lbusername.Text = value
        End If
    End Sub
    Public Sub lbmyEvent(value As String)
        If XtraMain.DelegateFunction.lbmyEvent.InvokeRequired Then
            XtraMain.DelegateFunction.lbmyEvent.Invoke(Sub() XtraMain.DelegateFunction.lbmyEvent.Text = value)
        Else
            XtraMain.DelegateFunction.lbmyEvent.Text = value
        End If
    End Sub
End Module
