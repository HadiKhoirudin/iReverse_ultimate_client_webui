Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports Microsoft.VisualBasic

Namespace Mediatek.Mediatek_tool
    Public Class Flash_Tool
        Public Shared logs As String = My.Resources.logs
        Public Shared stringlist As String() = logs.Split(New Char(1) {ChrW(13), ChrW(10)}, StringSplitOptions.RemoveEmptyEntries)
        Public Shared logs2 As String = My.Resources.logs1
        Public Shared logs1 As String() = logs2.Split(New Char(1) {ChrW(13), ChrW(10)}, StringSplitOptions.RemoveEmptyEntries)

        Public Shared Sub Command(cmd As String, worker As BackgroundWorker, ee As DoWorkEventArgs)
            Progressbar1(0)
            Progressbar2(0)
            Dim startInfo As ProcessStartInfo = New ProcessStartInfo(Flashpath.Flashtoolexe, cmd) With {
                .CreateNoWindow = True,
                .WindowStyle = ProcessWindowStyle.Hidden,
                .UseShellExecute = False,
                .Verb = "runas",
                .RedirectStandardError = True,
                .RedirectStandardOutput = True
            }

            Using process As Process = Process.Start(startInfo)
                process.BeginOutputReadLine()

                If worker.CancellationPending Then
                    process.Dispose()
                    worker.CancelAsync()
                    ee.Cancel = True
                Else

                    If Flashcommand.Flasfirmware Then
                        AddHandler process.OutputDataReceived, Sub(sender As Object, e As DataReceivedEventArgs)
                                                                   Dim text As String = If(e.Data, String.Empty)

                                                                   If text <> String.Empty Then
                                                                       Console.WriteLine(text)

                                                                       If text.Contains("General settings create command") Then
                                                                           RichLogs(" General create command ... ", "black", False, False)
                                                                       End If

                                                                       If text.Contains("General command exec done") Then
                                                                           RichLogs("", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("Connecting to BROM") Then
                                                                           RichLogs(" Connecting to BROM ... ", "black", False, False)
                                                                       End If

                                                                       If text.Contains("BROM connected") Then
                                                                           RichLogs("OK", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("Connect BROM failed") Then
                                                                           RichLogs("failed", "chartreuse", False, True)
                                                                       End If

                                                                       If text.Contains("Download DA now") Then
                                                                           RichLogs(" Download DA ... ", "black", True, False)
                                                                       End If

                                                                       If text.Contains("DA Connected") Then
                                                                           RichLogs("Done ✓", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("executing DADownloadAll") Then
                                                                           RichLogs(" Executing DA Download All ... ", "black", False, False)
                                                                       End If

                                                                       If text.Contains("Stage:") Then
                                                                           RichLogs("OK ", "lime", False, False)
                                                                       End If

                                                                       If text.Contains("Download Succeeded.") Then
                                                                           RichLogs("Done ✓", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("of DA has been sent.") Then
                                                                           Dim parse As String = text.Replace("% of DA has been sent.", "")
                                                                           Progressbar2(Integer.Parse(parse))
                                                                       End If

                                                                       If text.Contains("of image data has been sent") Then
                                                                           Dim text2 As String = text.Substring(0, text.IndexOf("of image data has been sent"))
                                                                           text2 = text2.Replace("%", "")
                                                                           Progressbar1(Integer.Parse(text2))
                                                                       End If

                                                                       If text.Contains("of image data has been sent") Then
                                                                           Dim text3 As String = text.Replace("% of image data has been sent", "").Replace("of", "").Replace("(", "").Replace(")", "")
                                                                           Dim array2 As String() = text3.Split(CType(Nothing, String()), StringSplitOptions.RemoveEmptyEntries)
                                                                           Progressbar1(Integer.Parse(array2(0)))
                                                                       End If

                                                                       For Each value As String In logs1

                                                                           If text.Contains(value) Then
                                                                               Console.WriteLine(e.Data)
                                                                               RichLogs(" Write Partition ... ", "black", False, False)
                                                                               RichLogs(text.Substring(text.LastIndexOf("[") + 1).Replace(text.Substring(text.LastIndexOf("]")), ""), "gold", False, False)
                                                                               RichLogs(" ... ", "black", False, False)
                                                                               Exit For
                                                                           End If
                                                                       Next

                                                                       If text.Contains("err_msg") Then
                                                                           RichLogs(Environment.NewLine & text.Substring(text.IndexOf("g") + 2).Replace(";", "").Replace(".", ""), "orange", True, True)
                                                                           ''Main.ButtonSTOP_Click(sender, e)
                                                                       End If

                                                                       If text.Contains("Please select the authentication") Then
                                                                           RichLogs(" Please select the authentication file first !!!", "orange", False, True)
                                                                           ''Main.ButtonSTOP_Click(sender, e)
                                                                       End If

                                                                       If text.Contains("S_DL_GET_DRAM_SETTING_FAIL(5054)") Then
                                                                           RichLogs(vbLf & " NEED INCLUDE PRELOADER !!! ", "gold", True, True)
                                                                           ''Main.ButtonSTOP_Click(sender, e)
                                                                       End If

                                                                       If text.Contains("Error: lib DA NOT match!") Then
                                                                           RichLogs(vbLf & " " & text.Substring(text.IndexOf(":") + 2), "gold", True, True)
                                                                           ''Main.ButtonSTOP_Click(sender, e)
                                                                       End If

                                                                       If text.Contains("All command exec done") Then
                                                                           RichLogs("Done ✓", "lime", True, True)
                                                                           ''Main.ButtonSTOP_Click(sender, e)
                                                                       End If
                                                                   End If
                                                               End Sub
                    ElseIf Flashcommand.Formatdata Then
                        AddHandler process.OutputDataReceived, Sub(sender As Object, e As DataReceivedEventArgs)
                                                                   Dim text As String = If(e.Data, String.Empty)

                                                                   If text <> String.Empty Then
                                                                       Console.WriteLine(text)

                                                                       If text.Contains("Scanning USB port...") Then
                                                                           Dim listport As List(Of Mediatek_list.Listport.Info) = Mediatek_list.Listport.Devicelists()

                                                                           For Each info As Mediatek_list.Listport.Info In listport
                                                                               ''Main.DelegateFunction.ComboPort.Invoke(Sub()
                                                                               ''Main.DelegateFunction.ComboPort.Properties.Items.Clear()
                                                                               ''Main.DelegateFunction.ComboPort.EditValue = String.Empty
                                                                               ''Main.DelegateFunction.ComboPort.Properties.Items.AddRange(New Object() {info.Mediatekport})
                                                                               ''Main.DelegateFunction.ComboPort.EditValue = Main.DelegateFunction.ComboPort.Properties.Items(0)
                                                                               ''End Sub)
                                                                           Next
                                                                       End If

                                                                       If text.Contains("of DA has been sent.") Then
                                                                           Dim ss As String = text.Replace("% of DA has been sent.", "")
                                                                           Progressbar2(Integer.Parse(ss))
                                                                       End If

                                                                       If text.Contains("of image data has been sent") Then
                                                                           Dim text2 As String = text.Substring(0, text.IndexOf("of image data has been sent"))
                                                                           text2 = text2.Replace("%", "")
                                                                           Progressbar1(Integer.Parse(text2))
                                                                       End If

                                                                       If text.Contains("of image data has been sent") Then
                                                                           Dim text3 As String = text.Replace("% of image data has been sent", "").Replace("of", "").Replace("(", "").Replace(")", "")
                                                                           Dim array2 As String() = text3.Split(CType(Nothing, String()), StringSplitOptions.RemoveEmptyEntries)
                                                                           Progressbar1(Integer.Parse(array2(0)))
                                                                       End If

                                                                       If text.Contains("of flash has been formatted") Then
                                                                           Dim text3 As String = text.Replace("% of flash has been formatted.", "").Replace("of", "").Replace("(", "").Replace(")", "")
                                                                           Dim array2 As String() = text3.Split(CType(Nothing, String()), StringSplitOptions.RemoveEmptyEntries)
                                                                           Progressbar1(Integer.Parse(array2(0)))
                                                                       End If

                                                                       If text.Contains("Format Succeeded.") Then
                                                                           RichLogs("Done ✓", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("Connect BROM failed") OrElse text.Contains("One of the download blocks has invalid range") OrElse text.Contains("Scatter file which is loaded is not be supported") OrElse text.Contains("ERROR : S_AUTH_HANDLE_IS_NOT_READY") Then
                                                                           RichLogs("failed", "red", True, True)
                                                                           ''Main.ButtonSTOP_Click(sender, e)
                                                                       End If

                                                                       If text.Contains("All command exec done") Then
                                                                           RichLogs("Done ✓", "lime", True, True)
                                                                           ''Main.ButtonSTOP_Click(sender, e)
                                                                       End If
                                                                   End If
                                                               End Sub
                    ElseIf Flashcommand.Writememory Then
                        AddHandler process.OutputDataReceived, Sub(sender As Object, e As DataReceivedEventArgs)
                                                                   Dim text As String = If(e.Data, String.Empty)

                                                                   If text <> String.Empty Then
                                                                       Console.WriteLine(text)

                                                                       If text.Contains("General settings create command") Then
                                                                           RichLogs(Environment.NewLine & " Starting progres ... ", "black", False, False)
                                                                       End If

                                                                       If text.Contains("General command exec done") Then
                                                                           RichLogs("", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("Connecting to BROM") Then
                                                                           RichLogs(" Connecting to", "black", False, False)
                                                                           RichLogs(" BROM mode", "orange", True, False)
                                                                           RichLogs(" ... ", "black", False, False)
                                                                       End If

                                                                       If text.Contains("BROM connected") Then
                                                                           RichLogs("Done ✓", "lime", True, True)
                                                                       End If

                                                                       If text.Contains("Connect BROM failed") Then
                                                                           RichLogs("failed", "chartreuse", True, True)
                                                                       End If

                                                                       If text.Contains("Write Memory Initial") Then
                                                                           RichLogs(vbLf & " Save format data ... ", "black", True)
                                                                       End If

                                                                       If text.Contains("of DA has been sent.") Then
                                                                           Dim ss As String = text.Replace("% of DA has been sent.", "")
                                                                           Progressbar2(Integer.Parse(ss))
                                                                       End If

                                                                       If text.Contains("of image data has been sent") Then
                                                                           Dim text2 As String = text.Substring(0, text.IndexOf("of image data has been sent"))
                                                                           text2 = text2.Replace("%", "")
                                                                           Progressbar1(Integer.Parse(text2))
                                                                       End If

                                                                       If text.Contains("of data write to memory") Then
                                                                           Dim text3 As String = text.Replace("% of data write to memory", "").Replace(text.Substring(text.IndexOf(",")), "")
                                                                           Dim array2 As String() = text3.Split(CType(Nothing, String()), StringSplitOptions.RemoveEmptyEntries)
                                                                           Progressbar1(Integer.Parse(array2(0)))
                                                                       End If

                                                                       If text.Contains("All command exec done") Then
                                                                           RichLogs("Done ✓", "lime", True, True)
                                                                           ''Main.ButtonSTOP_Click(sender, e)
                                                                       End If

                                                                       If text.Contains("ERROR") Then
                                                                           RichLogs(vbLf & " Save format data ... ", "black", True)
                                                                           RichLogs("failed", "red", True, True)
                                                                           ''Main.ButtonSTOP_Click(sender, e)
                                                                       End If

                                                                       If text.Contains("Invalid xml file") Then
                                                                           RichLogs(vbLf & " Save format data ... ", "black", True)
                                                                           RichLogs("Not Supported", "red", True, True)
                                                                           ''Main.ButtonSTOP_Click(sender, e)
                                                                       End If
                                                                   End If
                                                               End Sub
                    End If
                    process.WaitForExit()
                End If
            End Using
        End Sub
    End Class
End Namespace
