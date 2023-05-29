﻿Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO

Namespace Mediatek.Metamode
    Public Class Meta
        Public Shared Sub Command(cmd As String, exe As String, worker As BackgroundWorker, ee As DoWorkEventArgs)
            Dim process As Process = New Process With {
                .StartInfo = New ProcessStartInfo With {
                    .FileName = exe,
                    .Arguments = cmd,
                    .Verb = "runas",
                    .UseShellExecute = False,
                    .CreateNoWindow = True,
                    .RedirectStandardOutput = True,
                    .RedirectStandardError = True
                }
            }
            process.Start()
            Dim standardOutput As StreamReader = process.StandardOutput

            While Not process.StandardOutput.EndOfStream
                Dim str As String = standardOutput.ReadLine()
                RichLogs(str, "black", False, True)
            End While

            process.Dispose()
            RichLogs(" All progress Completed ", "darkorange", False, True)
        End Sub
    End Class
End Namespace
