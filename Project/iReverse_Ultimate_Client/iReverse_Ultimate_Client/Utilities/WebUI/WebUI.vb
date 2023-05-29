Imports Newtonsoft.Json.Linq
Imports PusherClient
Imports System.Drawing
Module WebUI

    Public Watch As Stopwatch
    Public Sub Delay(ByVal dblSecs As Double)
        Now.AddSeconds(0.0000115740740740741)
        Dim dateTime As DateTime = Now.AddSeconds(0.0000115740740740741)
        Dim dateTime1 As DateTime = dateTime.AddSeconds(dblSecs)
        While DateTime.Compare(Now, dateTime1) <= 0
            Application.DoEvents()
        End While
    End Sub

    Public Sub RichLogs(message As String, color As String, Optional bold As Boolean = False, Optional newline As Boolean = False)

        Dim jsonData = New With {
        .message = message,
        .color = color,
        .newline = newline,
        .bold = bold
        }

        Dim channel As Channel = _pusher.GetChannel(XtraMain.mychannel)
        channel.Trigger(XtraMain.toEvent, jsonData)
    End Sub

    Public Sub Progressbar1(value As Long)
        Delay(0.1)
        Dim jsonData = New With {
        .progressbar1 = value
        }

        Dim channel As Channel = _pusher.GetChannel(XtraMain.mychannel)
        channel.Trigger(XtraMain.toEvent, jsonData)
    End Sub
    Public Sub Progressbar2(value As Long)
        Delay(0.1)
        Dim jsonData = New With {
        .progressbar2 = value
        }

        Dim channel As Channel = _pusher.GetChannel(XtraMain.mychannel)
        channel.Trigger(XtraMain.toEvent, jsonData)
    End Sub
End Module

Public Class TimeSpanElapsed
    Public Shared Sub ElapsedTime(Watch As Stopwatch)
        Dim elapsed = Watch.Elapsed
        RichLogs(Environment.NewLine, "black", False, False)
        RichLogs(" __________________________________________________________________", "black", True, True)
        RichLogs(Environment.NewLine, "black", False, False)
        RichLogs(" iREVERSE DROID ULTIMATE ", "black", True, False)
        RichLogs(Date.Now.ToString("ddd, dd MMM yyyy HH:mm:ss"), "black", True, True)
        Dim str = String.Format("{0:00m}: {1:00s}", elapsed.Minutes, elapsed.Seconds)
        RichLogs(" Elapsed Time : " & str, "darkorange", True, True)
        RichLogs(" ", "red", False, True)
    End Sub

    Public Shared Sub ElapsedPending(Watch As Stopwatch)
        Dim elapsed = Watch.Elapsed
        RichLogs(Environment.NewLine, "black", False, False)
        RichLogs(" __________________________________________________________________", "black", True, True)
        RichLogs(Environment.NewLine, "black", False, False)
        RichLogs(" iREVERSE DROID ULTIMATE ", "black", True, False)
        RichLogs(Date.Now.ToString("ddd, dd MMM yyyy HH:mm:ss"), "black", True, True)
        Dim str = String.Format("{0:00m}: {1:00s}", elapsed.Minutes, elapsed.Seconds)
        RichLogs(" Elapsed Time : " & str, "darkorange", True, True)
        RichLogs(" ", "red", False, True)
    End Sub
End Class
Public Class WaitingStart
    Public Shared Sub WaitingDevices()
        RichLogs(">clear<", "black")
        RichLogs(" Turn Off Phone, Hold ", "black", True)
        RichLogs("VOL", "chartreuse", True)
        RichLogs(" + And ", "black", True)
        RichLogs("VOL", "chartreuse", True)
        RichLogs(" - Then Insert Usb Cable", "black", True, True)
        RichLogs(" Phone must have battery inside!", "darkorange", True, True)
        RichLogs(" Waiting For Device", "black", True)
        RichLogs(" 20s ... ", "black", True)
        RichLogs(" ", "red", False, True)
    End Sub

    Public Shared Sub WaitingUniversaDevices()
        RichLogs(">clear<", "black")
        RichLogs(" Turn Off Phone, Hold ", "black", True)
        RichLogs("VOL", "chartreuse", True)
        RichLogs(" + And ", "black", True)
        RichLogs("VOL", "chartreuse", True)
        RichLogs(" - Then Insert Usb Cable", "black", True, True)
        RichLogs(" Phone must have battery inside!", "darkorange", True, True)
        RichLogs(" Waiting For Device", "black", True)
        RichLogs(" ... ", "black", True, True)
        RichLogs(" ", "red", False, True)
    End Sub
End Class